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
    bool hitWaitFlag;
    float hitWaitCount;
    const float sethitWaitCount = 30;

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
        hitWaitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (HitPlayer())
        {

            HitCoroutin();
        }
        else
        {
            hitWaitFlag = false;
        }
    }


    bool HitPlayer()
    {
        var hit = false;
        if (SurveillanceCamera.GetSearchHit()) //監視カメラに見つかったら
        {
            hit = true;

        }
        else if( InfrareSensor.GetLaserHit())//センサーに触れたら
        {
            hit = true;
        }
     
        return hit;
    }


    void HitCoroutin()
    {
        if (hitWaitFlag)
        {
            hitWaitCount += 0.1f;

            if(hitWaitCount >= sethitWaitCount)
            {
                hitWaitFlag = false;
                hitWaitCount = 0;
            }

            return;
        }

        barCtrl.SetHP(10.0f);

        alertAudio.Stop();
        alertAudio.PlayOneShot(audioClip);

        hitWaitFlag = true;


    }

}
