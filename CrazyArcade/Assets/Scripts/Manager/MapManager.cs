using UnityEngine;

public static class MapManager
{
    public class MapInfo
    {
        public bool IsBubble;
        public bool IsBlock;
        public bool IsEdge;

        public MapInfo(bool _isBubble, bool _isBlock, bool _isEdge)
        {
            IsBubble = _isBubble;
            IsBlock = _isBlock;
            IsEdge = _isEdge;
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
                bool isBlock = false;
                bool isEdge = false;

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

                    if(col.gameObject.layer == LayerMask.NameToLayer("NeedleBlock"))
                    {
                        isBlock = true;
                    }

                    if(x == 0 || y == 0)
                    {
                        isEdge = true;
                    }
                }

                mapInfo[y, x] = new MapInfo(isBubble, isBlock, isEdge);
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
        bool isBlock = false;
        bool isEdge = false;

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

            if (x == 0 || y == 0)
            {
                isEdge = true;
            }
        }

        MapInfo mapInfo = new MapInfo(isBubble, isBlock, isEdge);

        return mapInfo;
    }
}
