using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InfrareSensor : MonoBehaviour
{
    [SerializeField] bool laserIrradiation = true;//レーザー照射
    [SerializeField] MeshRenderer laserMesh = null;//赤外線オブジェ
    static bool playerLaserHit;//プレイヤーがレーザーに触れた
    [SerializeField] float setRebootTime;//再起動にかかる時間
    [SerializeField] AudioClip audioClip;

    AudioSource audioSource;
    BoxCollider boxCol;
    float alpha = 1;
    float laserRebootTime;

    [SerializeField]
    private BarCtrl barCtrl;

    private void Start()
    {
        boxCol = GameObject.Find("laser").GetComponentInChildren<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        laserRebootTime = setRebootTime;
    }

    // Update is called once per frame
    void Update()
    {
        OnLaserIrradiation();

        if (playerLaserHit)
        {
            Debug.Log("感知: ");

        }

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
        laserMesh.material.color = new Color(
            laserMesh.material.color.r,
            laserMesh.material.color.g,
            laserMesh.material.color.b,
            alpha);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            barCtrl.SetHP(10.0f);
            //laserIrradiation = false;
            //alpha = 0;

            playerLaserHit = true;
            audioSource.PlayOneShot(audioClip);
        }

        if (other.tag == "PlayerAttack")
        {
            laserIrradiation = false;
            alpha = 0;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
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

        Debug.Log(laserRebootTime);
        
        //再起動
        if (laserRebootTime <= 0.0)
        {
            alpha = 1;
            laserRebootTime = setRebootTime;
            StopCoroutine("ColorCoroutin");
            InfraredIrradiation(true);
        }

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

            Color _color = laserMesh.material.color;

            _color.a = alpha;

            laserMesh.material.color = _color;
            
        }
    }

}
