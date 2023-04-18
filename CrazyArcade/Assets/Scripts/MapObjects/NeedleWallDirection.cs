using UnityEngine;

public class NeedleWallDirection : MonoBehaviour
{
    public enum WallDirection
    {
        Left,
        Right,
        Up,
        Down,
        Around
    }

    public WallDirection Direction;

    public Vector2 NeedleDirection
    {
        get
        {
            switch (Direction)
            {
                case WallDirection.Left:
                    return Vector2.left;

                case WallDirection.Right:
                    return Vector2.right;

                case WallDirection.Up:
                    return Vector2.up;

                case WallDirection.Down:
                    return Vector2.down;

                default:
                    return Vector2.zero;
            }
        }
    }
}
