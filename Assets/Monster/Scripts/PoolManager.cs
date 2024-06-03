using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public UnityEngine.GameObject[] prefabs;
    public List<UnityEngine.GameObject>[] pools;
    public int maxPoolSize = 20;
    int Active = 0;
    public int maxActive = 50;

    private void Awake()
    {
        pools = new List<UnityEngine.GameObject>[prefabs.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<UnityEngine.GameObject>();
        }
    }

    public UnityEngine.GameObject Get(int index)
    {
        UnityEngine.GameObject select = null;

        foreach (UnityEngine.GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                Active++;
                select = item;
                select.SetActive(true);
                return select;
            }

            if (Active >= maxActive)
            {
                break;
            }
        }

        if (pools[index].Count < maxPoolSize)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}