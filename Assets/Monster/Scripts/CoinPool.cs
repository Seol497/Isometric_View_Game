using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public List<GameObject> coinPool;
    public GameObject coinPrefab;


    public GameObject GetCoin()
    {
        // ��Ȱ��ȭ�� ���� �� �ϳ��� ã�Ƽ� ��ȯ
        for (int i = 0; i < coinPool.Count; i++)
        {
            if (!coinPool[i].activeInHierarchy)
            {
                return coinPool[i];
            }
        }

        // ���� Ǯ�� ��� ������ ������ ������ ���� �����ؼ� ��ȯ
        GameObject newCoin = Instantiate(coinPrefab, Vector3.zero, Quaternion.identity, transform);
        newCoin.SetActive(false);
        coinPool.Add(newCoin);
        return newCoin;
    }

    public void ActivateCoin(Vector3 position)
    {
        GameObject coin = GetCoin();
        coin.transform.position = position;
        coin.SetActive(true);
    }

}
