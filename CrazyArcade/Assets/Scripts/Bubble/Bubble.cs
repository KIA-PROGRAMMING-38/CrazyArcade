using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bubble : MonoBehaviour
{
    private float _boomReadyTime = 3f;
    private float _elapsedTime;

    // private Queue<Vector2Int> _bubbleEffectQueue = new Queue<Vector2Int>();
    private Queue<BubbleEffectData> _bubbleEffectQueueTest = new Queue<BubbleEffectData>();
    static int[] s_dy = { -1, 1, 0, 0 };
    static int[] s_dx = { 0, 0, -1, 1 };

    public GameObject[] BubbleEffect;
    public GameObject[] EndEffect;
    public GameObject RootEffect;

    public GameObject _player;
    public int _playerPower;
    struct BubbleEffectData
    {
        public Vector2Int pos;
        public GameObject effect;
    }

    private void OnEnable()
    {
        _elapsedTime = 0f;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= _boomReadyTime)
        {
            Boom();
        }
    }

    private void Boom()
    {
        Debug.Log("Boom 호출");
        GenerateBubbleEffect(_playerPower);
        bubblePool.Release(this);
    }

    bool[,] visitedNode = new bool[14, 16];
    private void GenerateBubbleEffect(int playerPower)
    {
        for(int y = 0; y < 14; ++y)
        {
            for(int x = 0; x < 16; ++x)
            {
                visitedNode[y, x] = false;
            }
        }

        int count = -1;
        Vector2Int startPos = Vector2Int.RoundToInt(transform.position);
        // 시작 위치
        _bubbleEffectQueueTest.Enqueue(new BubbleEffectData { pos = startPos, effect = RootEffect });
        visitedNode[startPos.y, startPos.x] = true;

        while (_bubbleEffectQueueTest.Count > 0)
        {
            int len = _bubbleEffectQueueTest.Count;
            ++count;

            for (int i = 0; i < len; ++i)
            {                                                  
                BubbleEffectData effectData = _bubbleEffectQueueTest.Dequeue();
                Vector2Int effectPos = effectData.pos;

                GameObject newEffect = Instantiate(effectData.effect);
                newEffect.transform.position = new Vector3(effectPos.x, effectPos.y, 0f);

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
                    if(count + 1 == playerPower)
                    {
                        _bubbleEffectQueueTest.Enqueue(new BubbleEffectData { pos = new Vector2Int(nx, ny), effect = EndEffect[j] });
                        continue;
                    }

                    _bubbleEffectQueueTest.Enqueue(new BubbleEffectData { pos = new Vector2Int(nx, ny), effect = BubbleEffect[j] });
                }
            }

            if (count >= playerPower)
                break;
        }
    }

    private IObjectPool<Bubble> bubblePool;

    public void SetBubble(Vector3Int position, int power)
    {
        transform.position = position;
        _playerPower = power;
    }
    public void SetPool(IObjectPool<Bubble> pool)
    {
        bubblePool = pool;
    }
}
