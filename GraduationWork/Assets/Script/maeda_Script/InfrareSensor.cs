using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InfrareSensor : MonoBehaviour
{
    [SerializeField] bool laserIrradiation = true;//レーザー照射
    [SerializeField] GameObject laserObj = null;//赤外線オブジェ
    bool playerLaserHit;//プレイヤーがレーザーに触れた

    // Update is called once per frame
    void Update()
    {
        OnLaserIrradiation();
    }

    /// <summary>
    /// 赤外線レーザーの照射
    /// </summary>
    void OnLaserIrradiation()
    {
        laserObj.SetActive(laserIrradiation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerLaserHit = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerLaserHit = false;
    }

    /// <summary>
    /// プレイヤー感知
    /// </summary>
    /// <returns></returns>
    public bool PlayerHit()
    {
        return playerLaserHit;
    }

    /// <summary>
    /// 赤外線レーザーの照射スイッチ
    /// </summary>
    /// <param name="IrradiationSwitch"></param>
    public void InfraredIrradiation(bool irradiationSwitch)
    {
        laserIrradiation = irradiationSwitch;
    }

}
