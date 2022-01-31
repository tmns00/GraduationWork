using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveCamPowerBox : MonoBehaviour
{
    bool gimkPower = true;//ギミックの電力
    [SerializeField] SurveillanceCamera survCam;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (Input.GetButtonDown("Pick"))
            {
                survCam.ShutDown();               
            }

        }

    }
    
}
