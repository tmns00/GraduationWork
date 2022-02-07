using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorPowerBox : MonoBehaviour
{
    bool gimkPower = true;//ギミックの電力
    [SerializeField] GameObject sensorObj;
    bool onSwich;//スイッチが押された

    InfrareSensor infrareSensor = null;

    private AudioSource source;

    private void Start()
    {
        infrareSensor = sensorObj.GetComponent<InfrareSensor>();
        source = GetComponent<AudioSource>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Pick"))
            {
                source.PlayOneShot(source.clip);
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
