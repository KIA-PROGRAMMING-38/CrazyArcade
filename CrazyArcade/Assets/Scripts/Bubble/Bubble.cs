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

    private ExplosionRootPool _explosionRootPool;
    private ExplosionLeftPool _explosionLeftPool;
    private ExplosionRightPool _explosionRightPool;
    private ExplosionUpPool _explosionUpPool;
    private ExplosionDownPool _explosionDownPool;
    private ExplosionEndDownPool _explosionEndDownPool;
    private ExplosionEndUpPool _explosionEndUpPool;
    private ExplosionEndLeftPool _explosionEndLeftPool;
    private ExplosionEndRightPool _explosionEndRightPool;
    private IObjectPool<Explosion>[] _effectPools = new IObjectPool<Explosion>[9];

    struct BubbleEffectData
    {
        public Vector2Int pos;
        public int effectIndex;
        public Explosion parentEffect;
    }

    public GameObject _player;
    public int _playerPower;

    private void Start()
    {
        _explosionInterval = new WaitForSeconds(GameManager.Instance.ExplosionInterval);

        _explosionDownPool = GetComponent<ExplosionDownPool>();
        _explosionUpPool = GetComponent<ExplosionUpPool>();
        _explosionLeftPool = GetComponent<ExplosionLeftPool>();
        _explosionRightPool = GetComponent<ExplosionRightPool>();
        // --------------------------------------------------------------------------
        _explosionEndDownPool = GetComponent<ExplosionEndDownPool>();
        _explosionEndUpPool = GetComponent<ExplosionEndUpPool>();
        _explosionEndLeftPool = GetComponent<ExplosionEndLeftPool>();
        _explosionEndRightPool = GetComponent<ExplosionEndRightPool>();
        // --------------------------------------------------------------------------
        _explosionRootPool = GetComponent<ExplosionRootPool>();

        _effectPools[0] = _explosionDownPool.effectPool;
        _effectPools[1] = _explosionUpPool.effectPool;
        _effectPools[2] = _explosionLeftPool.effectPool;
        _effectPools[3] = _explosionRightPool.effectPool;
        // --------------------------------------------------------------------------
        _effectPools[4] = _explosionEndDownPool.effectPool;
        _effectPools[5] = _explosionEndUpPool.effectPool;
        _effectPools[6] = _explosionEndLeftPool.effectPool;
        _effectPools[7] = _explosionEndRightPool.effectPool;
        // --------------------------------------------------------------------------
        _effectPools[8] = _explosionRootPool.rootPool;
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

        int count = -1;
        Vector2Int startPos = Vector2Int.RoundToInt(transform.position);
        // 시작 위치
        _bubbleEffectQueue.Enqueue(new BubbleEffectData { pos = startPos, effectIndex = 8, parentEffect = null });
        visitedNode[startPos.y, startPos.x] = true;

        while (_bubbleEffectQueue.Count > 0)
        {
            int len = _bubbleEffectQueue.Count;
            ++count;

            for (int i = 0; i < len; ++i)
            {
                BubbleEffectData effectData = _bubbleEffectQueue.Dequeue();
                Vector2Int effectPos = effectData.pos;
                int poolIndex = effectData.effectIndex;

                Explosion newEffect = _effectPools[poolIndex].Get();
                newEffect.transform.position = new Vector3(effectPos.x, effectPos.y, 0f);
                newEffect.ParentNode = effectData.parentEffect;
                newEffect.EventSubscribe();

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
                        
                        _bubbleEffectQueue.Enqueue(new BubbleEffectData { pos = new Vector2Int(nx, ny), effectIndex = j + 4, parentEffect = newEffect });
                        continue;
                    }

                    _bubbleEffectQueue.Enqueue(new BubbleEffectData { pos = new Vector2Int(nx, ny), effectIndex = j, parentEffect = newEffect });
                }
            }

            if (count >= playerPower)
                break;

            yield return _explosionInterval;
        }

        bubblePool.Release(this);
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
