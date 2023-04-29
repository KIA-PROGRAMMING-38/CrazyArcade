using UnityEngine;
using UnityEngine.UIElements;

public class BossBubble : MonoBehaviour
{
    private Vector2[] _shootPositions = new Vector2[8] {
            new Vector2(2, 2), new Vector2(7, 2), new Vector2(12, 2), new Vector2(2, 6), new Vector2(7, 6),
            new Vector2(12, 6), new Vector2(2, 10), new Vector2(12, 10)};
    private Vector2 _start;
    private Vector2 _dest;
    private float _speed;

    private bool _isArrived = false;

    private void Start()
    {
        _speed = 25f;
    }

    private void Update()
    {
        if (_isArrived == false)
        {
            transform.Translate(Vector2.down * (_speed * Time.deltaTime));

            if (Vector2.Distance(transform.position, _dest) < 0.1f)
            {
                _isArrived = true;
                Boom();
            }
        }
    }

    private void Boom()
    {
        Debug.Log("Boss Bubble Boom");
        // TODO: 9칸 계산해서 물줄기 이펙트 생성
    }

    public void SetDestPosition(int positionIndex)
    {
        _dest = _shootPositions[positionIndex];
        _start = new Vector2(_dest.x, 15);
        transform.position = _start;
    }
}
