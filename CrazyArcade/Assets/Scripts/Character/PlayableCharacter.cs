using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : Character
{
    private int _selectedCharacterId = 0;  // ���� ĳ���� ���� ����� ���� ������ ������ ���� ��Ȯ���� �����Ƿ� �켱 0(����)���� ����
    private Transform _playerTransform;
    
    private float deltaTime;

    private StatusReader _statusReader;
    [SerializeField]private float _speed;
    private float _maxSpeed;

    private void Awake()
    {
        _playerTransform = transform.root.GetComponent<Transform>();
        _statusReader = GameObject.Find("CharacterStatus").GetComponent<StatusReader>();
    }

    private void Start()
    {
        GetStatus(_selectedCharacterId);
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
        Debug.Log($"{name}: ��ǳ�� ����");
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
