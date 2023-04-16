using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPooling : MonoBehaviour
{
    private ItemPool _itemPool;

    private void Awake()
    {
        _itemPool = GetComponent<ItemPool>();
    }

    // TODO: 박스 사라지는 거 구독해서 .. 그 위치에 Get 한 아이템 배치할 수 있도록
}
