using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InfrareSensor : MonoBehaviour
{
    [SerializeField] bool laserIrradiation = true;//レーザー照射
    [SerializeField]GameObject laser = null;//赤外線マテリアル
    [SerializeField] Material laserMaterial;
    static bool playerLaserHit;//プレイヤーがレーザーに触れた
    [SerializeField] float setRebootTime;//再起動にかかる時間
    [SerializeField] AudioClip audioClip;
    AudioSource audioSource;
    BoxCollider boxCol;
    float alpha = 1;
    float laserRebootTime;
    bool gimkPower = true;//ギミックの電力

    [SerializeField]
    private BarCtrl barCtrl;

    private void Start()
    {
        boxCol = GameObject.Find("Sasers").GetComponentInChildren<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        laserRebootTime = setRebootTime;
        laser.GetComponent<Renderer>().material = laserMaterial;


    }

    // Update is called once per frame
    void Update()
    {
        OnLaserIrradiation();
        laser.GetComponent<Renderer>().material.color = laserMaterial.color;

        if (!gimkPower)
        {
            boxCol.enabled = false;
            alpha = 0;
            return; //シャットダウンしたら下の処理しない
        }

        //if (playerLaserHit)
        //{
        //    Debug.Log("感知: ");
        //}

    
        if (laserIrradiation) return;

        if(laserRebootTime <= 3.0f)
        {
            StartCoroutine("ColorCoroutin");
        }

        Reboot();
    }

    /// <summary>
    /// 赤外線レーザーの照射
    /// </summary>
    void OnLaserIrradiation()
    {
        boxCol.enabled = laserIrradiation;
        laserMaterial.color = new Color(
            laserMaterial.color.r,
            laserMaterial.color.g,
            laserMaterial.color.b,
            alpha);


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            barCtrl.SetHP(10.0f);
            playerLaserHit = true;
            audioSource.PlayOneShot(audioClip);

        }

        if (other.CompareTag("PlayerAttack"))
        {
            laserIrradiation = false;
            alpha = 0;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerLaserHit = false;
    }

    /// <summary>
    /// プレイヤー感知
    /// </summary>
    /// <returns></returns>
    public static bool GetPlayerHit()
    {
        return playerLaserHit;
    }

    /// <summary>
    /// 赤外線レーザーの照射スイッチ
    /// </summary>
    /// <param name="IrradiationSwitch"></param>
    public void InfraredIrradiation(bool _irradiationSwitch)
    {
        laserIrradiation = _irradiationSwitch;
    }

    /// <summary>
    /// 再起動
    /// </summary>
    private void Reboot()
    {
        laserRebootTime -= Time.deltaTime;

        //Debug.Log(laserRebootTime);
        
        //再起動
        if (laserRebootTime <= 0.0)
        {
            alpha = 1;
            laserRebootTime = setRebootTime;
            StopCoroutine("ColorCoroutin");
            InfraredIrradiation(true);
        }

    }

    public void ShutDown()
    {
        gimkPower = false;
    }


    /// <summary>
    /// 点滅コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator ColorCoroutin()
    {

        while (true)
        {
            yield return new WaitForEndOfFrame();
            alpha = Mathf.Abs( Mathf.Sin(Time.time / laserRebootTime)) ;

            Color _color = laserMaterial.color;

            _color.a = alpha;

            laserMaterial.color = _color;
            
        }
    }

}
