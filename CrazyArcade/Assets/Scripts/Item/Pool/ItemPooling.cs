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

    // TODO: �ڽ� ������� �� �����ؼ� .. �� ��ġ�� Get �� ������ ��ġ�� �� �ֵ���
}
