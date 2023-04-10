using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bubble : MonoBehaviour
{
    private float _boomReadyTime = 3f;
    private float _elapsedTime;
    private WaitForSeconds _explosionInterval;

    private Queue<BubbleEffectData> _bubbleEffectQueue = new Queue<BubbleEffectData>();
    static int[] s_dy = { -1, 1, 0, 0 };
    static int[] s_dx = { 0, 0, -1, 1 };

    private IObjectPool<BubbleEffect> _bubbleEffectPool;

    struct BubbleEffectData
    {
        public Vector2Int pos;
        public int type;
        public int direction;
    }

    public int _playerPower;

    private void Start()
    {
        _explosionInterval = new WaitForSeconds(GameManager.Instance.ExplosionInterval);
        _bubbleEffectPool = GetComponent<BubbleEffectPool>().EffectPool;
    }

    private void OnEnable()
    {
        _elapsedTime = 0f;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        isBoom = true;
    }
    bool isBoom;
    private void Update()
    {
        if (isBoom == true)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _boomReadyTime)
            {
                isBoom = false;
                _elapsedTime = 0f;
                Boom();
            }
        }
    }

    private void Boom()
    {
        StartCoroutine(GenerateBubbleEffect(_playerPower));
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
    
    bool[,] visitedNode = new bool[14, 16];
    private IEnumerator GenerateBubbleEffect(int playerPower)
    {
        _bubbleEffectQueue.Clear();

        for (int y = 0; y < 14; ++y)
        {
            for (int x = 0; x < 16; ++x)
            {
                visitedNode[y, x] = false;
            }
        }

        // 시작 위치
        int count = -1;
        Vector2Int startPos = Vector2Int.RoundToInt(transform.position);
        _bubbleEffectQueue.Enqueue(new BubbleEffectData { pos = startPos, type = 0, direction = 4 });
        visitedNode[startPos.y, startPos.x] = true;

        while (_bubbleEffectQueue.Count > 0)
        {
            int len = _bubbleEffectQueue.Count;
            ++count;

            for (int i = 0; i < len; ++i)
            {
                BubbleEffectData effectData = _bubbleEffectQueue.Dequeue();
                Vector2Int effectPos = effectData.pos;
                int effectType = effectData.type;
                int effectDirection = effectData.direction;

                BubbleEffect newEffect = _bubbleEffectPool.Get();
                newEffect.transform.position = new Vector3(effectPos.x, effectPos.y, 0f);
                newEffect.SetEffectInfo(effectType, effectDirection);

                for (int j = 0; j < 4; ++j)
                {
                    int ny = effectPos.y + s_dy[j];
                    int nx = effectPos.x + s_dx[j];

                    if (ny != startPos.y && nx != startPos.x)
                    {
                        continue;
                    }

                    if (ny < 0 || nx < 0 || ny > 13 || nx > 15)
                    {
                        continue;
                    }

                    if (visitedNode[ny, nx] == true)
                    {
                        continue;
                    }

                    visitedNode[ny, nx] = true;

                    // 범위의 마지막인 경우
                    if (count + 1 == playerPower)
                    {
                        
                        _bubbleEffectQueue.Enqueue(new BubbleEffectData { pos = new Vector2Int(nx, ny), type = 2, direction = j });
                        continue;
                    }

                    _bubbleEffectQueue.Enqueue(new BubbleEffectData { pos = new Vector2Int(nx, ny), type = 1, direction = j });
                }
            }

            if (count >= playerPower)
                break;

            yield return _explosionInterval;
        }

        bubblePool.Release(this);
    }

    public void SetBubble(Vector3Int position, int power)
    {
        transform.position = position;
        _playerPower = power;
    }

    private IObjectPool<Bubble> bubblePool;
    public void SetPool(IObjectPool<Bubble> pool)
    {
        bubblePool = pool;
    }
}
