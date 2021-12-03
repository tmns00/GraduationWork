using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int createNums;
    public GameObject[] objects;
    private List<int> numList = new List<int>();
    private int objectCount;

    // Start is called before the first frame update
    void Start()
    {
        objectCount = objects.Length;

        for (int i = 0; i < createNums; i++)
            CreateEnemy();
    }


    private void CreateEnemy()
    {
        int num;
        num = Random.Range(0, objectCount);
        if (numList.Contains(num))
        {
            CreateEnemy();
        }
        else
        {
            Instantiate(objects[num]);
            numList.Add(num);
        }   
    }
}
