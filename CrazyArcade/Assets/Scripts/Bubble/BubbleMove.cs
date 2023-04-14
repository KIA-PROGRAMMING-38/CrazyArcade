using UnityEngine;

public class BubbleMove : StateMachineBehaviour
{
    public static class BubbleAnimID
    {
        public static readonly int GET_FORCE = Animator.StringToHash("getForce");
        public static readonly int ARRIVED = Animator.StringToHash("arrived");
    }

    private Vector2 _direction;
    private float _speed = 15f;
    private Bubble _bubble;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bubble = animator.transform.root.GetComponent<Bubble>();
        _direction = _bubble._moveDirection;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, _direction, 0.1f, LayerMask.GetMask("Block"));
        
        if (hit.collider != null)
        {
            float radius = _bubble.GetComponent<CircleCollider2D>().radius;
            _bubble.transform.position = hit.point + hit.normal * radius;
            animator.SetTrigger(BubbleAnimID.ARRIVED);
        }
        else
        {
            _bubble.transform.Translate(_direction * (_speed * Time.deltaTime));
        }
    }
}
