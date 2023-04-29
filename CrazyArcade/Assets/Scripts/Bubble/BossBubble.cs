using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class BossBubble : MonoBehaviour
{
    private Vector2[] _shootPositions = new Vector2[9] {
            new Vector2(2, 2), new Vector2(7, 2), new Vector2(12, 2), new Vector2(2, 6), new Vector2(7, 6),
            new Vector2(12, 6), new Vector2(2, 10), new Vector2(7, 10), new Vector2(12, 10)};
    private Vector2 _start;
    private Vector2 _dest;
    private float _speed;

    private bool _isArrived = false;

    private IObjectPool<BubbleEffect> _bubbleEffectPool;

    private void Awake()
    {
        _speed = 25f;
        _bubbleEffectPool = GetComponent<BubbleEffectPool>().EffectPool;
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
        int x = (int)_dest.x;
        int y = (int)_dest.y;

        for(int i = -1; i <= 1; ++i)
        {
            for(int j = -1; j <= 1; ++j)
            {
                int newX = x + i;
                int newY = y + j;
                Vector2 newPos = new Vector2(newX, newY);

                BubbleEffect newEffect = _bubbleEffectPool.Get();
                newEffect.transform.position = newPos;
                newEffect.SetEffectInfo(0, 4);
            }
        }

        Destroy(gameObject);
    }

    public void SetDestPosition(int positionIndex)
    {
        _dest = _shootPositions[positionIndex];

        Collider2D hit = Physics2D.OverlapBox(_dest, new Vector2(2.5f, 2.5f), 0, Layers.BOSS_LAYERMASK);
        if(hit != null)
        {
            Destroy(gameObject);
            return;
        }

        _start = new Vector2(_dest.x, 15);
        transform.position = _start;
    }
}
