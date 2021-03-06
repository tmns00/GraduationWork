using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int createNumsFirst;
    public int addBackWayNum;
    public GameObject[] objects;
    private List<int> numList = new List<int>();
    private int objectCount;
    private bool onceCreate = true;

    // Start is called before the first frame update
    void Start()
    {
        objectCount = objects.Length;

        for (int i = 0; i < createNumsFirst; i++)
            CreateEnemy();
    }

    private void FixedUpdate()
    {
        if (!onceCreate)
            return;

        if (!RoundTripManager.GetIsBackWay())
            return;

        for (int i = 0; i < addBackWayNum; i++)
            CreateEnemy();

        onceCreate = false;
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
