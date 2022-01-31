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
    AudioSource alertAudio; //アラート音
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
        if (SurveillanceCamera.GetSearchHit()) //監視カメラに見つかったら
        {
            hit = true;
            audioPlayTime = audioPlayTime + 1;

            if (audioPlayTime == 590)
            {
                audioPlayTime = 0;
                alertAudioFlag = true;
            }

        }
        else if( InfrareSensor.GetLaserHit())//センサーに触れたら
        {
            hit = true;
            alertAudioFlag = true;
        }
     
        return hit;
    }

}
