using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacked : MonoBehaviour
{
    [SerializeField]
    BarCtrl barCtrl;
    [SerializeField]
    RespawnPlayer respawnPlayer;
    [SerializeField]
    GhostSystem ghostSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !ghostSystem.GetIsGhost())
        {
            barCtrl.SetHP(20.0f);
            respawnPlayer.Respawn();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Enemy" && !ghostSystem.GetIsGhost())
    //    {
    //        barCtrl.SetHP(20.0f);
    //        respawnPlayer.Respawn();
    //    }
    //}
}
