using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InfraredLaser : MonoBehaviour
{
    [SerializeField] bool LaserIrradiation = true;//���[�U�[�Ǝ�
    [SerializeField] GameObject LaserObj = null;
    bool playerLaserHit;//�v���C���[�����[�U�[�ɐG�ꂽ

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
