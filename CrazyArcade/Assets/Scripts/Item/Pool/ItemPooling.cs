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

    private void OnEnable()
    {
        Block.OnBreak += PutItemOnMap;
        FixedBlock.OnFixedBlockBreak += PutItemOnMap;
    }

    private void OnDisable()
    {
        Block.OnBreak -= PutItemOnMap;
        FixedBlock.OnFixedBlockBreak -= PutItemOnMap;
    }

    private void PutItemOnMap(Transform boxTransform)
    {
        Item item = _itemPool.ItemsPool.Get();
        if (item == null)
            return;

        item.transform.position = boxTransform.position;
    }
}
