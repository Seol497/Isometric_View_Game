using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //..�����յ��� ������ ���� (������������ŭ ����Ʈ �ʿ�)
    public GameObject[] prefabs;

    //..Ǯ�� ����� ����Ʈ��
    public List<GameObject>[] pools;

    public void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        //Ǯ�� ���� ����Ʈ �ʱ�ȭ
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
            //Ǯ�ȿ� ������ �ʱ�ȭ
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        // ... ������ Ǯ�� ��� �ִ� ���ӿ�����Ʈ ����
        //... �߰��ϸ� select������ �Ҵ�
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //... ��� ���������� ���� ����
        if (!select)
        {
            //...���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }

}
