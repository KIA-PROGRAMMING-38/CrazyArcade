using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayableCharacter : Character
{
    private RoundManager _roundManager;


    private SpriteRenderer _renderer;
    private const float FADE_INTERVAL = 0.15f;
    private IEnumerator _blinkCoroutine;


    private Status _status;
    private PlayerInput _input;
    private Animator _animator;
    private Inventory _inventory;
    public static class PlayerAnimID
    {
        // Parameters
        public static readonly int HORIZONTAL = Animator.StringToHash("horizontal");
        public static readonly int VERTICAL = Animator.StringToHash("vertical");
        public static readonly int IS_MOVING = Animator.StringToHash("isMoving");
        public static readonly int IS_DYING = Animator.StringToHash("isDying");
        public static readonly int IS_DYING_LAST = Animator.StringToHash("isDyingLast");
        public static readonly int REVIVAL = Animator.StringToHash("isRevival");
        public static readonly int IS_DIE = Animator.StringToHash("isDie");
        public static readonly int IS_WIN = Animator.StringToHash("isWin");
        public static readonly int GAME_START = Animator.StringToHash("gameStart");

        // StateInfo
        public static readonly int ON_IS_DYING_START = Animator.StringToHash("Base Layer.Dying.Dying_Start");
    }

    private bool _isMoving;
    public bool IsAlive { get; set; } = true; 

    private float deltaTime;

    private Vector2 _moveDirection;

    [SerializeField] private int _currentCount;

    private BubblePool _bubblePool;

    private void OnEnable()
    {
        StartUI.OnStart += GameStart;
        RoundManager.OnGameEnd += GameEnd;
    }

    private void OnDisable()
    {
        StartUI.OnStart -= GameStart;
        RoundManager.OnGameEnd -= GameEnd;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _input = transform.root.GetComponent<PlayerInput>();
        _status = GetComponent<Status>();
        _bubblePool = new BubblePool(this);
        _inventory = transform.root.GetComponentInChildren<Inventory>();
        _renderer = GetComponent<SpriteRenderer>();
        _blinkCoroutine = BlinkPlayer();
    }

    private void Start()
    {
        _roundManager = GameManager.Instance.GetComponentInChildren<RoundManager>();

        switch(GameManager.Instance.SelectedStage.GameMode)
        {
            case GAME_MODE.One_on_one:
                if(transform.parent.name == "Player1")
                {
                    _roundManager.SurvivePlayersTeam1.Add(this);
                }
                else
                {
                    _roundManager.SurvivePlayersTeam2.Add(this);
                }

                break;

            case GAME_MODE.Monster:
                _roundManager.SurvivePlayersTeam1.Add(this);
                break;
        }
    }

    private void Update()
    {
        deltaTime = Time.deltaTime;
        
        if(_input._inputCut == true)
        {   
            return;
        }

        if (_input._isPutBubbleBtn)
        {
            Attack();
        }

        if(_input._isUseItemBtn)
        {
            _inventory.UseItem(0, this);
        }

        Move();
    }

    public override void Move()
    {
        base.Move();
        _isMoving = _input._horizontal != 0 || _input._vertical != 0;
        _animator.SetBool(PlayerAnimID.IS_MOVING, _isMoving);

        if (_isMoving == false)
        {
            return;
        }

        if(_input._horizontal != 0)
        {
            _input._vertical = 0;
        }

        _moveDirection = new Vector2(_input._horizontal, _input._vertical);
        transform.Translate(_moveDirection * (_status.Speed * deltaTime));
        _animator.SetFloat(PlayerAnimID.HORIZONTAL, _input._horizontal);
        _animator.SetFloat(PlayerAnimID.VERTICAL, _input._vertical);
    }

    public override void Attack()
    {
        base.Attack();

        if (IsAlive == false)
        {
            return;
        }

        ++_currentCount;
        Vector3Int bubblePosition = Vector3Int.RoundToInt(transform.position);

        // 물풍선이 맵에 존재할 수 있는 개수를 넘어서지 않도록 제한
        if (_currentCount > _status.Count)
        {
            _currentCount -= 1;
            return;
        }

        // 맵 정보를 받아와 놓으려는 위치에 물풍선이 있는 경우 놓을 수 없도록 제한
        MapManager.MapInfo mapInfo = MapManager.GetCoordinateInfo(bubblePosition.x, bubblePosition.y);
        if(mapInfo.IsBubble)
        {
            _currentCount -= 1;
            return;
        }

        AudioManager.Instance.PlaySFX("set_bubble");
        Bubble newBubble = _bubblePool.bubblePool.Get();
        newBubble.SetBubble(bubblePosition, _status.Power);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickupable item = collision.GetComponent<IPickupable>();
        item?.Pickup(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.BUBBLE)
        {
            collision.isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("NeedleBlock"))
        {
            Vector2 needleDir = collision.gameObject.GetComponent<NeedleWallDirection>().NeedleDirection;
            if (needleDir + _moveDirection == Vector2.zero || needleDir == Vector2.zero)
            {
                _animator.SetTrigger(PlayerAnimID.IS_DIE);
            }
        }

        if(collision.gameObject.layer == Layers.PLAYER)
        {
            _animator.SetTrigger(PlayerAnimID.IS_DIE);
        }
    }

    public void DecreaseCurrentCount()
    {
        --_currentCount;
    }

    // Material mat;
    public void ReadyDie()
    {
        // mat.SetColor("_OutlineColor", Color.red);
        _animator.SetTrigger(PlayerAnimID.IS_DYING);
    }    

    public void ImmediatelyDie()
    {
        _status.MoveRestrict = true;
        _animator.SetTrigger(PlayerAnimID.IS_DIE);
    }

    public override void Die()
    {
        base.Die();
        transform.root.GetChild(1).gameObject.SetActive(false);
    }

    public void PlayPopSound()
    {
        AudioManager.Instance.PlaySFX("bubble_pop");
    }

    public void StartBlinkPlayer()
    {
        StartCoroutine(_blinkCoroutine);
    }

    private IEnumerator BlinkPlayer()
    {
        float t = 0;
        float destAlpha = 0;
        float startAlpha = 1;
        Color col = Color.white;

        while (true)
        {
            t += Time.deltaTime;

            if (t > FADE_INTERVAL)
            {
                t = 0f;
                destAlpha = destAlpha == 0 ? 1 : 0;
                startAlpha = startAlpha == 1 ? 0 : 1;
            }

            col.a = Mathf.Lerp(startAlpha, destAlpha, t / FADE_INTERVAL);
            _renderer.color = col;
            yield return null;
        }
    }

    private void GameStart()
    {
        _animator.SetTrigger(PlayerAnimID.GAME_START);
    }

    private void GameEnd()
    {
        _animator.SetTrigger(PlayerAnimID.IS_WIN);
    }
}
