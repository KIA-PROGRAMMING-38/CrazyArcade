using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapManager
{
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

    // TODO: ���� �� ������ ���� ���� static ������ ������ �� �ֵ��� ���� �ʿ�
    public static MapInfo[,] mapInfo = new MapInfo[14, 16];

    /// <summary>
    /// �� ��ü ��ǥ�� ���� ���, ��, ��ǳ�� ��ġ ���θ� mapInfo�� ����
    /// </summary>
    public static void GetTotalMapInfo()
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

                    if(col.gameObject.layer == LayerMask.NameToLayer("WallBlock"))
                    {
                        isWall = true;
                    }
                }

                mapInfo[y, x] = new MapInfo(isBubble, isWall, isBlock);
            }
        }
    }

    /// <summary>
    /// ���� Ư�� ��ǥ�� ���� ������ MapInfo Ÿ������ ��ȯ
    /// </summary>
    /// <param name="x">x��ǥ</param>
    /// <param name="y">y��ǥ</param>
    /// <returns></returns>
    public static MapInfo GetCoordinateInfo(int x, int y)
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

            if (col.gameObject.layer == LayerMask.NameToLayer("Block"))
            {
                isBlock = true;
            }

            if (col.gameObject.layer == LayerMask.NameToLayer("WallBlock"))
            {
                isWall = true;
            }
        }

        MapInfo mapInfo = new MapInfo(isBubble, isWall, isBlock);

        return mapInfo;
    }
}
