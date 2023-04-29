using UnityEngine;

public class BossWalkBehaviour : StateMachineBehaviour
{
    private enum DIRECTION
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    private Vector2 GetDirVec(DIRECTION randomDir)
    {
        switch (randomDir)
        {
            case DIRECTION.Up:
                return Vector2.up;

            case DIRECTION.Down:
                return Vector2.down;

            case DIRECTION.Left:
                return Vector2.left;

            case DIRECTION.Right:
                return Vector2.right;

            default:
                return Vector2.zero;
        }
    }

    private float _speed = 0.8f;
    private Vector2 _direction;
    private DIRECTION _prehitDir;
    private DIRECTION _curDir;
    private float _elapsedTime;
    private float _moveTime;
    private float _deltaTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �̵� ���� ���ϱ�
        while(true)
        {
            _curDir = (DIRECTION)Random.Range(1, 5);

            if (_curDir != _prehitDir)
                break;
        }
        _direction = GetDirVec(_curDir);
        animator.SetFloat(BossMonster.BossAnimID.HORIZONTAL, _direction.x);
        animator.SetFloat(BossMonster.BossAnimID.VERTICAL, _direction.y);

        // �̵� �ð� ���ϱ�
        _moveTime = Random.Range(3, 6);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _deltaTime = Time.deltaTime;
        _elapsedTime += _deltaTime;

        if(_elapsedTime >= _moveTime)
        {
            animator.GetComponent<BossMonster>().DecideNextBehaviour();
        }

        animator.transform.Translate(_direction * (_deltaTime * _speed));

        // ���� ���� �����ϴ� ��� IDLE�� ����
        RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, _direction, 2f, Layers.STAGEOBJ_LAYERMASK);
        if(hit.collider != null)
        {
            _prehitDir = _curDir;
            animator.SetTrigger(BossMonster.BossAnimID.SET_IDLE);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _elapsedTime = 0f;
    }
}
