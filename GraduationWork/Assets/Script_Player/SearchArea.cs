using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour
{
    private int enemyCount = 0;
    private List<GameObject> objList = new List<GameObject>();

    public float liveTime = 0.5f;
    private float countTime = 0.0f;

    private void Update()
    {
        if (countTime >= liveTime)
        {
            countTime = 0.0f;
            Destroy(gameObject);
        }
        countTime += Time.deltaTime;
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Enemy" && !objList.Contains(other.gameObject))
        {
            Debug.Log(other);
            objList.Add(other.gameObject);
            enemyCount++;
            Debug.Log(enemyCount);
        }
    }
}
