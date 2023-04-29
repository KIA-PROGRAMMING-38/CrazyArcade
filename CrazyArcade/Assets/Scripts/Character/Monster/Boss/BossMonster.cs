using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMonster : Monster, IDamageable
{
    public static event Action<string[]> OnSpeak;
    private ScriptPrinter _printer;
    private string[] _introScript = { "���� ���� ��������̾�", "��� ���� ����", "�ɿ� ���� �شܴ�"};
    private string[] _rollScripts = { "������������~", "���� ���� �ɹ� ������!", "�ʻ� ����!!! Ǯ�� �߱�����!!" };
    private string[] _shootScripts = { "���� ���� �ɹ� ���ֱ�!!!", "����ź�� �������~!!", "�� �� ����!!" };
    private string[] _normalScripts = { "�� ���߰� ��~", "����~", "�����ϴ� ���� ����!", "�� ���� ����~" };
    private string[] _hitScripts = { "��������~~~", "�� ����~", "������", "���ϴ� ���´�~" };
    private string _dieScript = "�پ�~";

    private bool _introScriptEnd = false;

    [SerializeField] BossBubble _bossBubble;
    private BossMonsterHPBar _hpBar;
    public static class BossAnimID
    {
        public static readonly int HORIZONTAL = Animator.StringToHash("horizontal");
        public static readonly int VERTICAL = Animator.StringToHash("vertical");
        public static readonly int SET_IDLE = Animator.StringToHash("setIdle");
        public static readonly int SET_WALK = Animator.StringToHash("setWalk");
        public static readonly int DAMAGED = Animator.StringToHash("damaged");
        public static readonly int LAST_HIT = Animator.StringToHash("lastHit");
        public static readonly int DIE = Animator.StringToHash("die");
        public static readonly int ROLL = Animator.StringToHash("roll");
        public static readonly int SHOOT = Animator.StringToHash("shoot");
    }

    public enum BEHAVIOUR_TYPE
    {
        IDLE,
        WALK,
        ROLL,
        SHOOT
    }

    public int Hp = 13;
    public int MaxHp = 13;

    private RoundManager _roundManager;

    private Animator _animator;

    private void Awake()
    {
        _numOfBehaviours = System.Enum.GetValues(typeof(BEHAVIOUR_TYPE)).Length;
        _hpBar = GetComponentInChildren<BossMonsterHPBar>();
        Hp = MaxHp;
        _hpBar.UpdateHPBar(Hp / (float)MaxHp);
        _animator = GetComponent<Animator>();
        _preBehaviourType = 0;
        _printer = GetComponentInChildren<ScriptPrinter>();
        _shoot = ShootCoroutine();
    }

    private void Start()
    {
        _roundManager = GameManager.Instance.GetComponentInChildren<RoundManager>();
        _roundManager.SurvivePlayersTeam2.Add(this);

        Invoke("StartPrintIntroScripts", 1.5f);
    }

    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }

    private int _preBehaviourType;
    private int _currentTrigger;
    private int _behaviourType;
    private int _numOfBehaviours;
    public void DecideNextBehaviour()
    {
        if (_introScriptEnd == false)
            return;

        while (_behaviourType == _preBehaviourType)
        {
            _behaviourType = Random.Range(0, _numOfBehaviours);
        }

        _preBehaviourType = _behaviourType;

        switch (_behaviourType)
        {
            case (int)BEHAVIOUR_TYPE.IDLE:
                _animator.ResetTrigger(_currentTrigger);
                _currentTrigger = BossAnimID.SET_IDLE;
                _animator.SetTrigger(_currentTrigger);
                OnSpeak?.Invoke(_normalScripts);
                break;

            case (int)BEHAVIOUR_TYPE.WALK:
                _animator.ResetTrigger(_currentTrigger);
                _currentTrigger = BossAnimID.SET_WALK;
                _animator.SetTrigger(_currentTrigger);
                OnSpeak?.Invoke(_normalScripts);
                break;

            case (int)BEHAVIOUR_TYPE.ROLL:
                _animator.ResetTrigger(_currentTrigger);
                _currentTrigger = BossAnimID.ROLL;
                _animator.SetTrigger(_currentTrigger);
                OnSpeak?.Invoke(_rollScripts);
                break;

            case (int)BEHAVIOUR_TYPE.SHOOT:
                _animator.ResetTrigger(_currentTrigger);
                _currentTrigger = BossAnimID.SHOOT;
                _animator.SetTrigger(_currentTrigger);
                OnSpeak?.Invoke(_shootScripts);
                break;
        }
    }

    private bool _isDying;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.PLAYER)
        {
            if (_isDying == false)
            {
                collision.GetComponent<PlayableCharacter>().ImmediatelyDie();
            }
            else
            {
                _animator.SetTrigger(BossAnimID.DIE);
            }
        }

        if (collision.gameObject.layer == Layers.BUBBLE)
        {
            collision.GetComponentInChildren<Bubble>().Boom();
        }
    }

    private bool _isHit;
    public void Hit()
    {
        if (_isHit == true)
            return; 

        _animator.SetTrigger(BossAnimID.DAMAGED);
        _isHit = true;
        Hp -= 1;

        OnSpeak?.Invoke(_hitScripts);

        if (Hp <= 0)
        {
            Hp = 0;
            _animator.SetTrigger(BossAnimID.LAST_HIT);
            _isDying = true;
            _printer.PrintScript(_dieScript);
        }

        float ratio = Hp / (float)MaxHp;
        _hpBar.UpdateHPBar(ratio);
    }

    public void ChangeIsHitState()
    {
        _isHit = false;
    }

    private IEnumerator _shoot;
    private WaitForSeconds _shootInterval = new WaitForSeconds(0.3f);
    private IEnumerator ShootCoroutine()
    {
        while(true)
        {
            for(int index = 0; index < 8; ++index)
            {
                BossBubble newBossBubble = Instantiate(_bossBubble);
                newBossBubble.SetDestPosition(index);
            
                yield return _shootInterval;
            }

            StopCoroutine(_shoot);
            yield return null;
        }
    }

    public void Shoot()
    {
        StartCoroutine(_shoot);
    }

    private WaitForSeconds _introScriptInterval = new WaitForSeconds(2f);
    public void StartPrintIntroScripts()
    {
        StartCoroutine(PrintScriptsSequence());
    }

    private IEnumerator PrintScriptsSequence()
    {
        for (int i = 0; i < _introScript.Length; ++i)
        {
            _printer.PrintScript(_introScript[i]);
            yield return _introScriptInterval;
        }

        _animator.SetTrigger(BossAnimID.SHOOT);
        _introScriptEnd = true;
    }
}

