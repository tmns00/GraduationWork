using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haunted : MonoBehaviour
{
    float time;
    public float timeEnd;
    public bool isFear;
    public Fear fear;
    [SerializeField]
    BarCtrl barCtrl;
    [SerializeField]
    RespawnPlayer respawnPlayer;
    [SerializeField]
    GhostSystem ghostSystem;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        timeEnd = 3;
        isFear = false;
        barCtrl = GameObject.Find("BarCtrl").GetComponent<BarCtrl>();
        respawnPlayer = GameObject.Find("GameManager").GetComponent<RespawnPlayer>();
        ghostSystem = GameObject.Find("Player").GetComponent<GhostSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(fear.Fearvalue);
        if(isFear)
        {
            time += 1 / 60.0f;
            if(time>=timeEnd)
            {
                isFear = false;
            }
        }
        if (!isFear)
            time = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isFear)
        {
            if (other.gameObject.tag == "Ghost")
            {
                fear.Fearvalue -= 5;
                isFear = true;
            }
        }
        if(fear.FearLevel==2)
        {
            if(other.gameObject.tag== "Ghost" && ghostSystem.GetIsGhost())
            {
                barCtrl.SetHP(20.0f);
                respawnPlayer.Respawn();
            }
        }
        if(other.gameObject.tag=="DeadBody")
        {
            barCtrl.SetHP(20.0f);
            respawnPlayer.Respawn();
        }
    }
}
