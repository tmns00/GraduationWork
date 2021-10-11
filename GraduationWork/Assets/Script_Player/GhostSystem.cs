using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSystem : MonoBehaviour
{
    private bool isGhost = false;


    // Update is called once per frame
    void Update()
    {
        TurnFlag();
    }

    private void TurnFlag()
    {
        if (Input.GetKeyDown(KeyCode.E))
            isGhost = true;
    }

    public bool GetIsGhost()
    {
        return isGhost;
    }

    public void SetIsGhost(bool flag)
    {
        isGhost = flag;
    }
}
