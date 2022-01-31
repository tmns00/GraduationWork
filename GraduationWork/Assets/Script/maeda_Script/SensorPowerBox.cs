using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorPowerBox : MonoBehaviour
{
    bool gimkPower = true;//ギミックの電力
    [SerializeField] GameObject sensorObj;
    bool onSwich;//スイッチが押された

    InfrareSensor infrareSensor = null;

    private void Start()
    {
        infrareSensor = sensorObj.GetComponent<InfrareSensor>();

    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Pick"))
            {
                //センサーをシャットダウンする
                infrareSensor.ShutDown();
            }
        }

    }

    public bool getOnSwich()
    {
        return onSwich;
    }
    
}
