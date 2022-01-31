using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InfrareSensor : MonoBehaviour
{
    bool laserIrradiation = true;//レーザー照射
    [SerializeField]GameObject[] laser = new GameObject[3];//赤外線オブジェ
    [SerializeField] Material laserMaterial;
    static bool playerLaserHit;//プレイヤーがレーザーに触れた
    [SerializeField] float setRebootTime;//再起動にかかる時間
    [SerializeField] AudioClip audioClip;
    AudioSource audioSource;
    [SerializeField]BoxCollider boxCol;
    float alpha = 1;
    float laserRebootTime;
    bool gimkPower;//ギミックの電力
    
    [SerializeField]
    private BarCtrl barCtrl;

    private void Start()
    {
        gimkPower = true;
        boxCol = transform.Find("Sasers").GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        laserRebootTime = setRebootTime;

        for(int i = 0; i < 2; i++)
        {
            laser[i].GetComponent<Renderer>().material.color = 
                laserMaterial.color;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        OnLaserIrradiation();
        laser[0].GetComponent<Renderer>().material.color = laserMaterial.color;
        laser[1].GetComponent<Renderer>().material.color = laserMaterial.color;
        laser[2].GetComponent<Renderer>().material.color = laserMaterial.color;

        if (!gimkPower) //シャットダウン
        {
            LaserPowerOFF();
            return; //シャットダウンしたら下の処理しない
        }

        //デバッグ用
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    LaserPowerOFF();
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
        if (!gimkPower) return;

        if(other.CompareTag("Player"))
        {
            barCtrl.SetHP(10.0f);
            playerLaserHit = true;
            audioSource.PlayOneShot(audioClip);

        }

        if (other.CompareTag("PlayerAttack"))
        {
            LaserPowerOFF();
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
    public static bool GetLaserHit()
    {
        return playerLaserHit;
    }

    /// <summary>
    /// 赤外線レーザーの照射スイッチ
    /// </summary>
    /// <param name="IrradiationSwitch"></param>
    public void SetInfraredIrradiation(bool _irradiationSwitch)
    {
        laserIrradiation = _irradiationSwitch;
    }

    /// <summary>
    /// 再起動
    /// </summary>
    private void Reboot()
    {
        laserRebootTime -= Time.deltaTime;
        
        //再起動
        if (laserRebootTime <= 0.0)
        {
            alpha = 1;
            laserRebootTime = setRebootTime;
            StopCoroutine("ColorCoroutin");
            SetInfraredIrradiation(true);
        }

    }

    public void ShutDown()
    {
        gimkPower = false;
    }

    public bool GetPower()
    {
        return gimkPower;
    }

    /// <summary>
    /// レーザー消滅時の処理
    /// </summary>
    private void LaserPowerOFF()
    {
        boxCol.enabled = false;
        laserIrradiation = false;
        alpha = 0;
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
