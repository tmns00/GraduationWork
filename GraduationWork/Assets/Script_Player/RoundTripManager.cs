using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTripManager : MonoBehaviour
{
    public static bool isBackWay = false;

    private void Start()
    {
        isBackWay = false;
    }

    public static bool GetIsBackWay()
    {
        return isBackWay;
    }

    public static void SetIsBackWay(bool flag)
    {
        isBackWay = flag;
    }
}
