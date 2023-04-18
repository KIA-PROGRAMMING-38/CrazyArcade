using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static BubbleMove;

public class Bubble : MonoBehaviour
{
    private Animator _animator;
    private float _boomReadyTime = 3f;
    private float _elapsedTime;
    private int _playerPower;
    private WaitForSeconds _explosionInterval;
    private IEnumerator _generateBubbleEffect;

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


    private void Start()
    {
        _explosionInterval = new WaitForSeconds(0.05f);
        _bubbleEffectPool = GetComponent<BubbleEffectPool>().EffectPool;
        _animator = GetComponentInChildren<Animator>();
        _generateBubbleEffect = GenerateBubbleEffect();
    }

    private void OnEnable()
    {
        _elapsedTime = 0f;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        isBoom = true;
    }

    private bool isBoom;
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

    public void Boom()
    {
        StartCoroutine(_generateBubbleEffect);
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    bool[,] visitedNode = new bool[14, 16];
    private IEnumerator GenerateBubbleEffect()
    {
        while (true)
        {
            _bubbleEffectQueue.Clear();

            for (int y = 0; y < 14; ++y)
            {
                for (int x = 0; x < 16; ++x)
                {
                    visitedNode[y, x] = false;
                }
            }

            // 맵 정보 갱신
            MapManager.GetTotalMapInfo();

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

                        if (ny < 0 || nx < 0 || ny > 12 || nx > 14)
                        {
                            continue;
                        }

                        if (MapManager.mapInfo[ny, nx].IsBlock)
                        {
                            if (ny + s_dy[j] < 0 || nx + s_dx[j] < 0)
                                continue;

                            visitedNode[ny + s_dy[j], nx + s_dx[j]] = true;
                        }

                        if (visitedNode[ny, nx] == true)
                        {
                            continue;
                        }

                        visitedNode[ny, nx] = true;

                        // 범위의 마지막인 경우
                        if (count + 1 == _playerPower)
                        {

                            _bubbleEffectQueue.Enqueue(new BubbleEffectData { pos = new Vector2Int(nx, ny), type = 2, direction = j });
                            continue;
                        }

                        _bubbleEffectQueue.Enqueue(new BubbleEffectData { pos = new Vector2Int(nx, ny), type = 1, direction = j });
                    }
                }

                if (count >= _playerPower)
                    break;

                yield return _explosionInterval;
            }

            StopCoroutine(_generateBubbleEffect);
            bubblePool.Release(this);
            yield return null;
        }
    }

    public Vector2 _moveDirection { get; private set; }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector2Int _playerPosition = Vector2Int.RoundToInt(collision.gameObject.transform.position);
            _moveDirection = new Vector2Int((int)transform.position.x - _playerPosition.x, (int)transform.position.y - _playerPosition.y);
            if (_moveDirection.x == 0 || _moveDirection.y == 0)
            {
                _animator.SetTrigger(BubbleAnimID.GET_FORCE);
            }
        }
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