using Unity.VisualScripting;
using UnityEngine;

public class PlayableCharacter : Character
{
    private Status _status;
    private PlayerInput _input;
    private Animator _animator;
    public static class PlayerAnimID
    {
        public static readonly int HORIZONTAL = Animator.StringToHash("horizontal");
        public static readonly int VERTICAL = Animator.StringToHash("vertical");
        public static readonly int IS_MOVING = Animator.StringToHash("isMoving");
        public static readonly int IS_DYING = Animator.StringToHash("isDying");
        public static readonly int IS_DYING_LAST = Animator.StringToHash("isDyingLast");
    }

    private bool _isMoving;

    private float deltaTime;

    private Vector2 _moveDirection;

    [SerializeField] private int _currentCount;

    private BubblePool _bubblePool;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _input = transform.root.GetComponent<PlayerInput>();
        _status = GetComponent<Status>();
        _bubblePool = GetComponent<BubblePool>();
    }

    private void Update()
    {
        deltaTime = Time.deltaTime;
        if (_input._isPutBubbleBtn)
        {
            Attack();
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

        ++_currentCount;
        Vector3Int bubblePosition = Vector3Int.RoundToInt(transform.position);

        // ��ǳ���� �ʿ� ������ �� �ִ� ������ �Ѿ�� �ʵ��� ����
        if (_currentCount > _status.Count)
        {
            _currentCount -= 1;
            return;
        }

        // �� ������ �޾ƿ� �������� ��ġ�� ��ǳ���� �ִ� ��� ���� �� ������ ����
        MapManager.MapInfo mapInfo = MapManager.GetCoordinateInfo(bubblePosition.x, bubblePosition.y);
        if(mapInfo.isBubble)
        {
            _currentCount -= 1;
            return;
        }

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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bubble"))
        {
            collision.isTrigger = false;
        }
    }

    public void DecreaseCurrentCount()
    {
        --_currentCount;
    }

    public void ReadyDie()
    {
        _animator.SetTrigger(PlayerAnimID.IS_DYING);
    }    

    public override void Die()
    {
        base.Die();
    }
}
