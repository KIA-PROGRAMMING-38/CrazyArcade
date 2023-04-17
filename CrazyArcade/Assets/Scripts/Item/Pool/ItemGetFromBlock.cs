using UnityEngine;

public class ItemGetFromBlock : StateMachineBehaviour
{
    private BoxCollider2D _collider;
    private float _elapsedTime;
    private float _duration;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _collider = animator.GetComponent<BoxCollider2D>();
        _collider.enabled = false;
        animator.transform.localScale = Vector3.zero;
        _duration = 0.3f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _elapsedTime += Time.deltaTime;
        animator.transform.localScale = scaleLerp(0f, 1f, _elapsedTime / _duration);
    }

    private Vector3 scaleLerp(float start, float end, float value)
    {
        Vector3 result = new Vector3();

        result.x = EaseOutQuad(start, end, value);
        result.y = EaseOutQuad(start, end, value);
        result.z = 1f;

        return result;
    }

    public float EaseOutQuad(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        end -= start;
        return -end * value * (value - 2) + start;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _collider.enabled = true;
    }
}
