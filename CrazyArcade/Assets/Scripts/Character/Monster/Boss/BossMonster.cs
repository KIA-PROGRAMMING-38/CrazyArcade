using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMonster : Monster, IDamageable
{
    public static class BossAnimID
    {
        public static readonly int HORIZONTAL = Animator.StringToHash("horizontal");
        public static readonly int VERTICAL = Animator.StringToHash("vertical");
        public static readonly int SET_IDLE = Animator.StringToHash("setIdle");
        public static readonly int SET_WALK = Animator.StringToHash("setWalk");
        public static readonly int DAMAGED = Animator.StringToHash("damaged");
        public static readonly int LAST_HIT = Animator.StringToHash("lastHit");
        public static readonly int DIE = Animator.StringToHash("die");
    }

    public enum BEHAVIOUR_TYPE
    {
        IDLE,
        WALK
    }

    public int Hp = 3;

    private IEnumerator _decideNextBehaviour;
    private WaitForSeconds _decideInterval = new WaitForSeconds(3f);
    private RoundManager _roundManager;

    private Animator _animator;

    private void Awake()
    {
        _numOfBehaviours = System.Enum.GetValues(typeof(BEHAVIOUR_TYPE)).Length;
        _animator = GetComponent<Animator>();
        _decideNextBehaviour = DecideNextBehaviour();
        StartCoroutine(_decideNextBehaviour);
    }

    private void Start()
    {
        _roundManager = GameManager.Instance.GetComponentInChildren<RoundManager>();
        _roundManager.SurvivePlayersTeam2.Add(this);
    }

    public override void Move()
    {
        base.Move();

    }

    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }

    private int _currentTrigger;
    private int _behaviourType;
    private int _numOfBehaviours;
    private IEnumerator DecideNextBehaviour()
    {
        while (true)
        {
            // _currentBehaviourCount = 0;
            _behaviourType = Random.Range(0, _numOfBehaviours);

            switch (_behaviourType)
            {
                case (int)BEHAVIOUR_TYPE.IDLE:
                    _animator.ResetTrigger(_currentTrigger);
                    _currentTrigger = BossAnimID.SET_IDLE;
                    _animator.SetTrigger(_currentTrigger);
                    break;

                case (int)BEHAVIOUR_TYPE.WALK:
                    _animator.ResetTrigger(_currentTrigger);
                    _currentTrigger = BossAnimID.SET_WALK;
                    _animator.SetTrigger(_currentTrigger);
                    break;
            }

            yield return _decideInterval;
        }

    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.layer == Layers.BUBBLE)
        {
            collision.GetComponentInChildren<Bubble>().Boom();
        }
    }

    public void Hit()
    {
        _animator.SetTrigger(BossAnimID.DAMAGED);
        Hp -= 1;

        if(Hp <= 0)
        {
            Hp = 0;
            _animator.SetTrigger(BossAnimID.LAST_HIT);
        }
    }
}

