using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickManager : MonoBehaviour
{
    //[SerializeField] AudioClip audioClip;
    //[SerializeField] BarCtrl barCtrl;

    GameObject[] SCameras;
    GameObject[] ISensors;
    GameObject[] PB_SCam;
    GameObject[] PB_ISensor;

    int cameraNum,sensorNum;

    InfrareSensor infrSensor;
    SurveillanceCamera survCamera;
    AudioSource alertAudio; //ÉAÉâÅ[Égâπ

    // Start is called before the first frame update
    void Start()
    {
        ISensors = GameObject.FindGameObjectsWithTag("InfraredSensor");
        SCameras = GameObject.FindGameObjectsWithTag("SurveillanceCamera");
        PB_ISensor = GameObject.FindGameObjectsWithTag("PB_Sensor");
        PB_SCam = GameObject.FindGameObjectsWithTag("PB_SurveCamera");
        cameraNum = SCameras.Length;
        sensorNum = ISensors.Length;

       // alertAudio = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (HitPlayer())
        {
            Debug.Log("î≠å©");
            //barCtrl.SetHP(10.0f);
            //alertAudio.PlayOneShot(audioClip);
        }
    }

    bool HitPlayer()
    {
        var hit = false;
        if (SurveillanceCamera.GetSearchHit()||
            InfrareSensor.GetLaserHit())
        {
            hit = true;
        }
     
        return hit;
    }

}
