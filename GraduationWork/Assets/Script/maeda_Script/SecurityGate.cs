using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGate : MonoBehaviour
{
    bool backWayFlag = false; //�E�o�t���O

    InfrareSensor infrareSensor;
    GameObject GateObj;


    // Start is called before the first frame update
    void Start()
    {
        GateObj = transform.Find("Gate").gameObject;
        GateObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        backWayFlag = RoundTripManager.GetIsBackWay();

        if (!backWayFlag) return; //�E�o�t���O
        if (infrareSensor.GetPower()) return; //�Z���T�[�̓d����OFF�̎�

        GateObj.SetActive(true);

    }
}