using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGate : MonoBehaviour
{
    bool backWayFlag = false; //脱出フラグ

    [SerializeField] InfrareSensor infrareSensor;
    [SerializeField] GameObject GateObj;

    private bool onceCreate = true;

    // Start is called before the first frame update
    void Start()
    {
        //GateObj = transform.Find("Gate").gameObject;
        //GateObj.SetActive(false);
        onceCreate = true;
    }

    // Update is called once per frame
    void Update()
    {
        backWayFlag = RoundTripManager.GetIsBackWay();

        if (!backWayFlag) return; //脱出フラグ
        if (infrareSensor.GetPower() && onceCreate) //センサーの電源がONの時
        {
            //GateObj.SetActive(true);
            Instantiate(GateObj, this.transform.position, transform.rotation, transform);
            onceCreate = false;
        }
    }
}