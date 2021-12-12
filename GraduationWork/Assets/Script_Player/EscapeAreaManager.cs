using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeAreaManager : MonoBehaviour
{
    public GameObject escapeArea;
    public Transform[] areaPoss;

    private Treasure treasureScript; 
    private int posNum;

    // Start is called before the first frame update
    void Start()
    {
        posNum = areaPoss.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Create()
    {
        int rnd;
        rnd = Random.Range(0, posNum);
        Instantiate(escapeArea, areaPoss[posNum]);
    }
}
