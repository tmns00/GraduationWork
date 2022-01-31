using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorPowerBox : MonoBehaviour
{
    bool gimkPower = true;//ギミックの電力
    [SerializeField] InfrareSensor infrareSensor;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Pick"))
            {
               infrareSensor.ShutDown();
            }
        }

    }
    
}
