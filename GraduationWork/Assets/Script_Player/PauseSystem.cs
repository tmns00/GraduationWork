using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    private static bool isPause;

    // Update is called once per frame
    void Update()
    {
        if (isPause)
            Time.timeScale = 0;
        else if (!isPause)
            Time.timeScale = 1;
    }

    public static void SetPauseFlag(bool flag)
    {
        isPause = flag;
    }

    public static bool GetPauseFlag()
    {
        return isPause;
    }
}
