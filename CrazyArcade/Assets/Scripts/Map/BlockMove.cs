using UnityEngine;
using static Block;

public class BlockMove : StateMachineBehaviour
{
    private Vector2 _direction;
    private Vector2 _startPosition;
    private Vector2 _targetPosition;

    private Block _block;

    private bool _canMove;

    private float _elapsedTime;
    [SerializeField]private float _duration = 0.5f;
    private float _speed = 10f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _block = animator.GetComponent<Block>();

        _block._canMove = false;
        _direction = _block._moveDirection;
        _startPosition = animator.transform.position;
        _targetPosition = new Vector2(_startPosition.x + _direction.x, _startPosition.y + _direction.y);

        MapManager.MapInfo targetPos = MapManager.GetCoordinateInfo((int)_targetPosition.x, (int)_targetPosition.y);

        // isBlock, isBubble 확인하여 이동할 수 없는 경우 처리
        if(targetPos.isBlock || targetPos.isBubble)
        {
            animator.SetTrigger(MapAnimID.ARRIVED);
            _canMove = false;
        }
        else
        {
            _canMove = true;
        }

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // isBlock, isBubble 아닌 경우 한 칸 보간으로 이동
        if(_canMove)
        {
            _elapsedTime += Time.deltaTime;
            animator.transform.position = Vector3.Lerp(_startPosition, _targetPosition, _elapsedTime / _duration);

            if(_elapsedTime >= _duration)
            {
                animator.GetComponent<Block>()._canMove = true;

                _elapsedTime = 0f;
                _canMove = false;
                animator.SetTrigger(MapAnimID.ARRIVED);
            }

        }
    }
}
