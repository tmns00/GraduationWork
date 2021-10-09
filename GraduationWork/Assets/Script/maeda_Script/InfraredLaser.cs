using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InfraredLaser : MonoBehaviour
{
    [SerializeField] bool LaserIrradiation = true;//レーザー照射
    [SerializeField] GameObject LaserObj = null;
    bool playerLaserHit;//プレイヤーがレーザーに触れた

    // Update is called once per frame
    void Update()
    {
        OnLaserIrradiation();
    }

    void OnLaserIrradiation()
    {
        LaserObj.SetActive(LaserIrradiation);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerLaserHit = true;

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerLaserHit = false;

    }

    public bool PlayerHit()
    {
        return playerLaserHit;
    }
        
}
