using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRollBehaviour : StateMachineBehaviour
{
    private enum DIRECTION
    {
        Left,
        Right
    }

    private Vector2 GetDirVec(DIRECTION randomDir)
    {
        switch (randomDir)
        {
            case DIRECTION.Left:
                return Vector2.left;

            case DIRECTION.Right:
                return Vector2.right;

            default:
                return Vector2.zero;
        }
    }

    private float _speed = 10f;
    private Vector2 _direction;
    private DIRECTION _curDir;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 이동 방향 정하기
        while(true)
        {
            _curDir = (DIRECTION)Random.Range(0, 2);
            _direction = GetDirVec(_curDir);

            // 벽과 가까운 경우는 다시 방향을 찾도록 함
            RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, _direction, 4f, Layers.STAGEOBJ_LAYERMASK);
            if (hit.collider == null)
                break;
        }

        animator.SetFloat(BossMonster.BossAnimID.HORIZONTAL, _direction.x);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Translate(_direction * (Time.deltaTime * _speed));

        RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, _direction, 2f, Layers.STAGEOBJ_LAYERMASK);
        if(hit.collider != null)
        {
            animator.GetComponent<BossMonster>().DecideNextBehaviour();
        }
    }
}
