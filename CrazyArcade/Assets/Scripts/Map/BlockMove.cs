using UnityEngine;
using static Block;

public class BlockMove : StateMachineBehaviour
{
    private Vector2 _direction;
    private Vector2 _startPosition;
    private Vector2 _targetPosition;

    private Block _block;

    private float _elapsedTime;
    private float _duration = 0.5f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _block = animator.GetComponent<Block>();

        _block._canMove = false;
        _direction = _block._moveDirection;
        _startPosition = animator.transform.position;
        _targetPosition = new Vector2(_startPosition.x + _direction.x, _startPosition.y + _direction.y);

        MapManager.MapInfo targetPos = MapManager.GetCoordinateInfo((int)_targetPosition.x, (int)_targetPosition.y);

        // 맵 범위 내로 제한
        if(_targetPosition.x < 0 || _targetPosition.y < 0 || _targetPosition.x > 14 || _targetPosition.y > 12 
        {
            animator.SetTrigger(MapAnimID.ARRIVED);
        }

        // isBlock, isBubble 확인하여 이동할 수 없는 경우 처리
        if (targetPos.isBlock || targetPos.isBubble)
        {
            animator.SetTrigger(MapAnimID.ARRIVED);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // isBlock, isBubble 아닌 경우 한 칸 보간으로 이동
        _elapsedTime += Time.deltaTime;
        animator.transform.position = Vector3.Lerp(_startPosition, _targetPosition, _elapsedTime / _duration);

        if (_elapsedTime >= _duration)
        {
            _elapsedTime = 0f;
            animator.GetComponent<Block>()._canMove = true;
            animator.SetTrigger(MapAnimID.ARRIVED);
        }
    }
}
