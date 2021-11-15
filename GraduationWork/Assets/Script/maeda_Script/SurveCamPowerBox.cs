using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveCamPowerBox : MonoBehaviour
{
    bool gimkPower = true;//ギミックの電力

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (Input.GetButtonDown("Pick"))
            {
                if (gimkPower)
                {
                    gimkPower = false;
                }
                else
                {
                    gimkPower = true;
                }

            }

        }

    }

    public bool PowerSwitch()
    {
        return gimkPower;
    }


}
