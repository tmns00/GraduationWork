using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickManager : MonoBehaviour
{
    [SerializeField] BarCtrl barCtrl;
    [SerializeField] AudioClip audioClip;
    GameObject[] SCameras;
    GameObject[] ISensors;
    GameObject[] PB_SCam;
    GameObject[] PB_ISensor;

    int cameraNum,sensorNum;

    InfrareSensor infrSensor;
    SurveillanceCamera survCamera;
    AudioSource alertAudio; //�A���[�g��
    int audioPlayTime = 0;
    bool alertAudioFlag;

    // Start is called before the first frame update
    void Start()
    {
        ISensors = GameObject.FindGameObjectsWithTag("InfraredSensor");
        SCameras = GameObject.FindGameObjectsWithTag("SurveillanceCamera");
        PB_ISensor = GameObject.FindGameObjectsWithTag("PB_Sensor");
        PB_SCam = GameObject.FindGameObjectsWithTag("PB_SurveCamera");
        cameraNum = SCameras.Length;
        sensorNum = ISensors.Length;

        alertAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HitPlayer())
        {           
            Debug.Log(audioPlayTime);
            barCtrl.SetHP(10.0f);

            if (alertAudioFlag)
            {
                alertAudio.PlayOneShot(audioClip);
                alertAudioFlag = false;

            }

        }
    }


    bool HitPlayer()
    {
        var hit = false;
        if (SurveillanceCamera.GetSearchHit()) //�Ď��J�����Ɍ���������
        {
            hit = true;
            audioPlayTime = audioPlayTime + 1;

            if (audioPlayTime == 590)
            {
                audioPlayTime = 0;
                alertAudioFlag = true;
            }

        }
        else if( InfrareSensor.GetLaserHit())//�Z���T�[�ɐG�ꂽ��
        {
            hit = true;
            alertAudioFlag = true;
        }
     
        return hit;
    }

}
