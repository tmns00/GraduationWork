using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorPowerBox : MonoBehaviour
{
    bool gimkPower = true;//�M�~�b�N�̓d��
    [SerializeField] GameObject sensorObj;
    bool onSwich;//�X�C�b�`�������ꂽ

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
                //�Z���T�[���V���b�g�_�E������
                infrareSensor.ShutDown();
            }
        }

    }

    public bool getOnSwich()
    {
        return onSwich;
    }
    
}
