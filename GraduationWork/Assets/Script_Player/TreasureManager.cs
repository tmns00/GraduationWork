using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    public GameObject treasureObj;
    public Transform[] treasurePositions;

    private int posNum;

    // Start is called before the first frame update
    void Start()
    {
        posNum = treasurePositions.Length;
        Create();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Create()
    {
        int rnd;
        rnd = Random.Range(0, posNum);
        Instantiate(treasureObj, treasurePositions[rnd]);
    }
}
