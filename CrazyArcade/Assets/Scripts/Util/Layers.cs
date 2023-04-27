using UnityEngine;

public static class Layers
{
    // Layer
    public static readonly int BUBBLE = LayerMask.NameToLayer("Bubble");
    public static readonly int PLAYER = LayerMask.NameToLayer("Player");

    // LayerMask
    public static readonly int STAGEOBJ_LAYERMASK = LayerMask.GetMask("StageObject");
}
