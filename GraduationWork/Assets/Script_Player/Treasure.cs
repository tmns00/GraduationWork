using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private bool isGet = false;
    [SerializeField]
    GameSystem4 toClear;

    private void Update()
    {
        if (isGet)
            toClear.StartGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isGet = true;
    }

    public bool IsGetFlag()
    {
        return isGet;
    }
}
