using UnityEngine;
using static BubbleMove;

public class Block : MonoBehaviour
{
    public static class MapAnimID
    {
        public static readonly int POP = Animator.StringToHash("Pop");
        public static readonly int GET_FORCE = Animator.StringToHash("GetForce");
        public static readonly int ARRIVED = Animator.StringToHash("Arrived");
    }

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Pop()
    {
        _animator.SetTrigger(MapAnimID.POP);
    }

    public Vector2 _moveDirection {get; private set;}
    void OnTriggerEnter2D(Collider2D collision)
    {
        IRemovable removableObj = collision.GetComponent<IRemovable>();
        removableObj?.Remove();

        if (collision.gameObject.CompareTag("BubbleEffect"))
        {
            _animator.SetTrigger(MapAnimID.POP);
        }
    }

    public bool _canMove { get; set; } = true;
    private float _collisionTime;
    private float _requiredTimeForMove = 0.5f;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && _canMove)
        {
            _collisionTime += Time.deltaTime;

            if(_collisionTime > _requiredTimeForMove )
            {
                _collisionTime = 0f;
                Vector2Int _playerPosition = Vector2Int.RoundToInt(collision.gameObject.transform.position);
                _moveDirection = new Vector2Int((int)transform.position.x - _playerPosition.x, (int)transform.position.y - _playerPosition.y);

                if (_moveDirection.x == 0 || _moveDirection.y == 0)
                {
                    _animator.SetTrigger(MapAnimID.GET_FORCE);
                }
            }
        }
    }
    private void Deactive()
    {
        gameObject.SetActive(false);
    }

    private void Disable()
    {
        _spriteRenderer.enabled = false;
    }

    private void Enable()
    {
        _spriteRenderer.enabled = true;
    }
}
