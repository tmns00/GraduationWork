using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Treasure : MonoBehaviour
{
    private bool isGet = false;
    //[SerializeField]
    //GameSystem4 toClear;
    //GameObject gauge;
    //[SerializeField] GameObject gauge2;
    //[SerializeField] GameObject text;

    private void Start()
    {
        //gauge = GameObject.Find("Canvas");
    }

    private void Update()
    {
        //if (isGet)
        //{
        //    gauge.SetActive(false);
        //    this.gameObject.SetActive(false);
        //}
        //else
        //{
        //    gauge2.SetActive(false);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isGet = true;
        //gauge2.SetActive(true);
        //text.SetActive(true);
    }

    public bool IsGetFlag()
    {
        return isGet;
    }
}
