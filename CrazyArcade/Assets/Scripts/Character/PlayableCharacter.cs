using UnityEngine;

public class PlayableCharacter : Character
{
    private int _selectedCharacterId = 0;  // 아직 캐릭터 선택 기능이 없어 정보를 참조할 곳이 명확하지 않으므로 우선 0(배찌)으로 지정
    private PlayerInput _input;
    private Transform _playerTransform;
    private Animator _animator;

    private float deltaTime;

    [SerializeField] private float _speed;
    private float _maxSpeed;
    private Vector2 _moveDirection;

    [SerializeField] private int _power;
    private int _maxPower;

    [SerializeField] private int _count;
    [SerializeField] private int _currentCount;
    private int _maxCount;

    private BubblePool _bubblePool;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _input = transform.root.GetComponent<PlayerInput>();
        _playerTransform = transform.root.GetComponent<Transform>();
        _bubblePool = GetComponent<BubblePool>();
    }

    private void Start()
    {
        GetStatus(_selectedCharacterId);
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
        _moveDirection = new Vector2(_input._horizontal, _input._vertical);
        _playerTransform.Translate(_moveDirection * (_speed * deltaTime));

        _animator.SetFloat("horizontal", _input._horizontal);
        _animator.SetFloat("vertical", _input._vertical);
    }

    public override void Attack()
    {
        base.Attack();

        ++_currentCount;
        Vector3Int bubblePosition = Vector3Int.RoundToInt(transform.position);

        // 물풍선이 맵에 존재할 수 있는 개수를 넘어서지 않도록 제한
        if (_currentCount > _count)
        {
            _currentCount -= 1;
            return;
        }

        // 맵 정보를 받아와 놓으려는 위치에 물풍선이 있는 경우 놓을 수 없도록 제한
        GameManager.Instance.MapManager.GetMapInfo();
        if (GameManager.Instance.MapManager.mapInfo[bubblePosition.y, bubblePosition.x].isBubble == true)
        {
            _currentCount -= 1;
            return;
        }

        Bubble newBubble = _bubblePool.bubblePool.Get();
        newBubble.SetBubble(bubblePosition, _power);
    }

    public void DecreaseCount()
    {
        --_currentCount;
    }

    public override void Die()
    {
        base.Die();
    }

    private void GetStatus(int id)
    {
        _speed = DataReader.PlayableCharacters[id].speed / 2;
        _maxSpeed = DataReader.PlayableCharacters[id].maxSpeed / 2;
        _power = DataReader.PlayableCharacters[id].power;
        _maxPower = DataReader.PlayableCharacters[id].maxPower;
        _count = DataReader.PlayableCharacters[id].count;
        _maxCount = DataReader.PlayableCharacters[id].maxCount;
    }
}
