using UnityEngine;

public static class MapManager
{
    public class MapInfo
    {
        public bool IsBubble;
        public bool IsBlock;
        public bool IsEdge;
        public bool IsOut;

        public MapInfo(bool _isBubble, bool _isBlock, bool _isEdge, bool _isOut)
        {
            IsBubble = _isBubble;
            IsBlock = _isBlock;
            IsEdge = _isEdge;
            IsOut = _isOut;
        }
    }

    // TODO: 추후 맵 정보에 대한 내용 static 등으로 관리할 수 있도록 수정 필요
    public static MapInfo[,] mapInfo = new MapInfo[14, 16];

    /// <summary>
    /// 맵 전체 좌표에 대해 블록, 벽, 물풍선 배치 여부를 mapInfo에 저장
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
                bool isOut = false;

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

                    //if(x == 0 || y == 0)
                    //{
                    //    isEdge = true;
                    //}

                    if (x < 0 || y < 0)
                    {
                        isOut = true;
                    }
                }

                mapInfo[y, x] = new MapInfo(isBubble, isBlock, isEdge, isOut);
            }
        }
    }

    /// <summary>
    /// 맵의 특정 좌표에 대한 정보를 MapInfo 타입으로 반환
    /// </summary>
    /// <param name="x">x좌표</param>
    /// <param name="y">y좌표</param>
    /// <returns></returns>
    public static MapInfo GetCoordinateInfo(int x, int y)
    {
        bool isBubble = false;
        bool isBlock = false;
        bool isEdge = false;
        bool isOut = false;

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

            //if (x == 0 || y == 0)
            //{
            //    isEdge = true;
            //}

            if (x < 0 || y < 0)
            {
                isOut = true;
            }
        }

        MapInfo mapInfo = new MapInfo(isBubble, isBlock, isEdge, isOut);

        return mapInfo;
    }
}
