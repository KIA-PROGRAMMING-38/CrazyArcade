using UnityEngine;

public class PlayableCharacter : Character
{
    private int _selectedCharacterId = 0;  // ���� ĳ���� ���� ����� ���� ������ ������ ���� ��Ȯ���� �����Ƿ� �켱 0(����)���� ����
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

        // ��ǳ���� �ʿ� ������ �� �ִ� ������ �Ѿ�� �ʵ��� ����
        if (_currentCount > _count)
        {
            _currentCount -= 1;
            return;
        }

        // �� ������ �޾ƿ� �������� ��ġ�� ��ǳ���� �ִ� ��� ���� �� ������ ����
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
