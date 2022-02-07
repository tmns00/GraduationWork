using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorPowerBox : MonoBehaviour
{
    bool gimkPower = true;//�M�~�b�N�̓d��
    [SerializeField] GameObject sensorObj;
    bool onSwich;//�X�C�b�`�������ꂽ

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
