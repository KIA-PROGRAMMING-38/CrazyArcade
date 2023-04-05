using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayableCharacter : Character
{
    private int _selectedCharacterId = 0;  // 아직 캐릭터 선택 기능이 없어 정보를 참조할 곳이 명확하지 않으므로 우선 0(배찌)으로 지정
    private PlayerInput _input;
    private Transform _playerTransform;
    
    private float deltaTime;

    private StatusReader _statusReader;
    [SerializeField]private float _speed;
    private float _maxSpeed;

    private BubblePool _bubblePool;

    private void Awake()
    {
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
        if (_input._isPutBubbleBtn)
        {
            Attack();
        }
    }

    public override void Move(Vector2 direction)
    {
        base.Move(direction);
        deltaTime = Time.deltaTime;
        _playerTransform.Translate(direction * (_speed * deltaTime));
    }

    public override void Attack()
    {
        base.Attack();
        Bubble newBubble = _bubblePool.bubblePool.Get();
        newBubble.transform.position = transform.position;
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
