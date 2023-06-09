using Unity.VisualScripting;
using UnityEngine;

public class BubbleMove : StateMachineBehaviour
{
    public static class BubbleAnimID
    {
        public static readonly int GET_FORCE = Animator.StringToHash("getForce");
        public static readonly int ARRIVED = Animator.StringToHash("arrived");
    }

    private float _radius;
    private Vector2 _direction;
    private float _speed = 15f;
    private Bubble _bubble;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bubble = animator.transform.root.GetComponent<Bubble>();
        _radius = _bubble.GetComponent<CircleCollider2D>().radius;
        _direction = _bubble._moveDirection;
        _bubble.GetComponent<CircleCollider2D>().enabled = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, _direction, _radius, LayerMask.GetMask("Block"));
        RaycastHit2D hitBubble = Physics2D.Raycast(animator.transform.position, _direction, _radius, LayerMask.GetMask("Bubble"));
        RaycastHit2D hitStage = Physics2D.Raycast(animator.transform.position, _direction, 0.3f, LayerMask.GetMask("StageObject"));
        RaycastHit2D hitNeedleWall = Physics2D.Raycast(animator.transform.position, _direction, _radius, LayerMask.GetMask("NeedleBlock"));
        RaycastHit2D hitPlayer = Physics2D.Raycast(animator.transform.position, _direction, _radius, LayerMask.GetMask("BubbleHitBox"));

        if (hitBubble.collider != null)
        {
            animator.SetTrigger(BubbleAnimID.ARRIVED);
        }

        if (hit.collider != null || hitStage.collider != null || hitPlayer.collider != null)
        {
            animator.SetTrigger(BubbleAnimID.ARRIVED);
        }
        else if (hitNeedleWall.collider != null)
        {
            Vector2 needleDir = hitNeedleWall.transform.GetComponent<NeedleWallDirection>().NeedleDirection;
            if (needleDir + _direction == Vector2.zero || needleDir == Vector2.zero)
            {
                animator.transform.root.GetComponent<Bubble>().Boom();
            }
        }
        else
        {
            _bubble.transform.Translate(_direction * (_speed * Time.deltaTime));
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bubble.GetComponent<CircleCollider2D>().enabled = true;
    }
}
