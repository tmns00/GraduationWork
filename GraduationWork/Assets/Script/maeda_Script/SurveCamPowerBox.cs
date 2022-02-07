using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveCamPowerBox : MonoBehaviour
{
    bool gimkPower = true;//ギミックの電力
    [SerializeField] SurveillanceCamera survCam;
    [SerializeField] GameObject buttonObj;

    private void Start()
    {
        buttonObj.GetComponent<Renderer>().material.color = Color.red;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (Input.GetButtonDown("Pick"))
            {
                buttonObj.GetComponent<Renderer>().material.color =
                   Color.green;
                survCam.ShutDown();               
            }

        }

    }
    
}
