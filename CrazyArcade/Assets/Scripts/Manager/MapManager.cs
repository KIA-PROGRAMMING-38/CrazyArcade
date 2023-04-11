using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    [System.Serializable]
    public class MapInfo
    {
        public bool isBubble;
        public bool isWall;
        public bool isBlock;

        public MapInfo(bool _isBubble, bool _isWall, bool _isBlock)
        {
            isBubble = _isBubble;
            isWall = _isWall;
            isBlock = _isBlock;
        }
    }

    // TODO: ���� �� ������ ���� ���� static ������ ������ �� �ֵ��� ���� �ʿ� (14, 16 �� �� ������ + 1)
    public MapInfo[,] mapInfo = new MapInfo[14, 16];

    public void GetMapInfo()
    {
        for (int y = 0; y < 14; ++y)
        {
            for (int x = 0; x < 16; ++x)
            {
                bool isBubble = false;
                bool isWall = false;
                bool isBlock = false;

                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(x, y), 0.4f))
                {
                    if (col.gameObject.layer == LayerMask.NameToLayer("Bubble"))
                    {
                        isBubble = true;
                    }

                    if(col.gameObject.layer == LayerMask.NameToLayer("Block"))
                    {
                        isBlock = true;
                    }

                    if(col.gameObject.layer == LayerMask.NameToLayer("Wall"))
                    {
                        isWall = true;
                    }
                }

                mapInfo[y, x] = new MapInfo(isBubble, isWall, isBlock);
            }
        }
    }
}
