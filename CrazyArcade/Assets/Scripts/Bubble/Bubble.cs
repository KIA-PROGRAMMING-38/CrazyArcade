using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bubble : MonoBehaviour
{
    private float _boomReadyTime = 3f;
    private float _elapsedTime;

    private Queue<Vector2Int> _bubbleEffectQueue = new Queue<Vector2Int>();
    static int[] s_dy = { -1, 1, 0, 0 };
    static int[] s_dx = { 0, 0, -1, 1 };

    public GameObject BubbleEffect;

    struct BubbleEffectData
    {
        Vector2 pos;
        Sprite effect;
        bool isLast;
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
        GenerateBubbleEffect();
        bubblePool.Release(this);
    }

    int power = 4;
    bool[,] visitedNode = new bool[14, 16];
    private void GenerateBubbleEffect()
    {
        for(int y = 0; y < 13; ++y)
        {
            for(int x = 0; x < 15; ++x)
            {
                visitedNode[y, x] = false;
            }
        }

        int count = -1;
        Vector2Int startPos = Vector2Int.RoundToInt(transform.position);
        // 시작 위치
        _bubbleEffectQueue.Enqueue(startPos);

        while(_bubbleEffectQueue.Count > 0)
        {
            int len = _bubbleEffectQueue.Count;
            
            for(int i = 0; i < len; ++i)
            {
                Vector2Int effectPos = _bubbleEffectQueue.Dequeue();
                if(effectPos != startPos)
                {
                    Instantiate(BubbleEffect);
                    BubbleEffect.transform.position = new Vector3(effectPos.x, effectPos.y, 0f);
                }

                for(int j = 0; j < 4; ++j)
                {
                    int ny = effectPos.y + s_dy[j];
                    int nx = effectPos.x + s_dx[j];

                    if (ny != startPos.y && nx != startPos.x)
                    {
                        continue;
                    }

                    if(ny < 0 || nx < 0 || ny > 13 || nx > 15)
                    {
                        continue;
                    }
                    
                    if (visitedNode[ny, nx] == true)
                    {
                        continue;
                    }

                    visitedNode[ny, nx] = true;
                    _bubbleEffectQueue.Enqueue(new Vector2Int(nx, ny));
                }
            }

            ++count;

            if (count >= power)
                break;
        }
    }

    private IObjectPool<Bubble> bubblePool;
    public void SetPool(IObjectPool<Bubble> pool)
    {
        bubblePool = pool;
    }
}
