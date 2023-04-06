using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Pool;

public class PlayableCharacter : Character
{
    private int _selectedCharacterId = 0;  // 아직 캐릭터 선택 기능이 없어 정보를 참조할 곳이 명확하지 않으므로 우선 0(배찌)으로 지정
    private PlayerInput _input;
    private Transform _playerTransform;
    private Animator _animator;
    
    private float deltaTime;

    private StatusReader _statusReader;
    [SerializeField]private float _speed;
    private float _maxSpeed;
    private Vector2 _moveDirection;

    private BubblePool _bubblePool;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _input = transform.root.GetComponent<PlayerInput>();
        _playerTransform = transform.root.GetComponent<Transform>();
        _statusReader = GameObject.Find("CharacterStatus").GetComponent<StatusReader>();
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
        Bubble newBubble = _bubblePool.bubblePool.Get();
        Vector3Int bubblePosition = Vector3Int.RoundToInt(transform.position);
        newBubble.transform.position = bubblePosition;
    }

    public override void Die()
    {
        base.Die();
    }

    private void GetStatus(int id)
    {
        _speed = _statusReader.PlayableCharacters[id].speed / 2;
        _maxSpeed = _statusReader.PlayableCharacters[id].maxSpeed / 2;
    }
}
