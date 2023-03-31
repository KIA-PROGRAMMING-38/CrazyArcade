using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    private bool isWall;
    private Node ParentNode;
    private int x, y, G, H;
    public int F { get { return G + H; } }

    public Node(bool _isWall, int _x, int _y)
    {
        isWall = _isWall;
        x = _x;
        y = _y;
    }
}

public class BubbleController : MonoBehaviour
{
    private WaitForSeconds _boomReadyTime = new WaitForSeconds(3f);
    private Coroutine _boomReadyCoroutine;

    private void OnEnable()
    {
        _boomReadyCoroutine = StartCoroutine(BoomReady());
    }

    private IEnumerator BoomReady()
    {
        Debug.Log("BoomReady");

        yield return _boomReadyTime;

        Boom();
    }

    private void Boom()
    {
        Debug.Log("Boom »£√‚");
        gameObject.SetActive(false);
    }

    private void GetReachPath()
    {

    }
}
