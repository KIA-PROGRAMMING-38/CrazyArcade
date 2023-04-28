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

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 이동 방향 정하기
        while(true)
        {
            _curDir = (DIRECTION)Random.Range(1, 5);

            if (_curDir != _prehitDir)
                break;
        }
        _direction = GetDirVec(_curDir);
        animator.SetFloat(BossMonster.BossAnimID.HORIZONTAL, _direction.x);
        animator.SetFloat(BossMonster.BossAnimID.VERTICAL, _direction.y);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Translate(_direction * (Time.deltaTime * _speed));

        // 맵의 끝에 도달하는 경우 IDLE로 전이
        RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, _direction, 2f, Layers.STAGEOBJ_LAYERMASK);
        if(hit.collider != null)
        {
            _prehitDir = _curDir;
            animator.SetTrigger(BossMonster.BossAnimID.SET_IDLE);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
