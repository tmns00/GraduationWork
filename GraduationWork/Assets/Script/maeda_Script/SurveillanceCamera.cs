using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Sprites;

public class SurveillanceCamera : MonoBehaviour
{
    [SerializeField] bool rotMove; //監視カメラを動かすか
    [SerializeField] bool posMove;

    [SerializeField] SphereCollider searchCollider = null;
    [SerializeField, Range(0, 90.0f)] float searchAngle = 0;//サーチ範囲
    [SerializeField] float serchRadius;　//サーチ半径
    [SerializeField, Range(0, 90.0f)] float rotAngle = 0;//回転アングル

    [SerializeField] float MoveDist = 0;//監視カメラを動かす距離
    [SerializeField] Vector3 comMoveVec;//監視カメラを動かす方向ベクトル

    float sec;
    float hitSec = 0;//発見された時一定間隔でゲージ増加させる
    private static bool playerSearchHit;//プレイヤーが監視カメラの範囲に触れた
    bool cameraStop = false; //憑依された際の機能ストップ
    private bool camGimkPower = true;//ギミックの電力

    Quaternion defaultRotation;
    AudioSource alertAudio; //アラート音
    [SerializeField] float setRebootTime;//再起動にかかる時間

    [SerializeField] BarCtrl barCtrl =null;


    // Start is called before the first frame update
    void Start()
    {
        playerSearchHit = false;
        defaultRotation = transform.rotation;
        alertAudio = GetComponent<AudioSource>();

        if (barCtrl == null) return;
    }

    private void Update()
    {
        if (!camGimkPower)
        {
            searchCollider.enabled = false;
            return; //シャットダウンしたら下の処理しない
        }

        if (playerSearchHit)
        {
            alertAudio.Play();
            hitSec += 0.05f;
            if (hitSec >= 10)
            {
                barCtrl.SetHP(10);
                hitSec = 0;
            }
        }

        if (cameraStop) return;

        RotCamera();
        CameraMove();

    }

    private void OnTriggerStay(Collider other)
    {

        RaycastHit rayHit;
        const float Search_Adjust = 2.0f;//サーチ範囲調整

        //プレイヤーが範囲に入ったら
        if (other.CompareTag("Player"))
        {
            //　主人公の方向
            Vector3 playerDirection = other.transform.position - transform.position;
            //　敵の前方からの主人公の方向
            var angle = Vector2.Angle(transform.forward, playerDirection);

            Ray ray = new Ray(transform.position, playerDirection);

            if (Physics.Raycast(ray.origin, ray.direction * serchRadius, out rayHit))
            {
                //Rayが最初に当たった物体
                if (rayHit.collider.CompareTag("Player"))
                {
                    //　サーチする角度内だったら発見
                    if (angle <= searchAngle * Search_Adjust)
                    {
                        playerSearchHit = true;
                        cameraStop = true;

                    }
                    else if (angle >= searchAngle * Search_Adjust)
                    {
                        playerSearchHit = false;
                        cameraStop = false;
                        hitSec = 0;

                    }

                }

            }

        }
 
        if (other.CompareTag("PlayerAttack"))
        {
            cameraStop = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerSearchHit = false;
            cameraStop = false;
            hitSec = 0;

        }
    }

    /// <summary>
    /// 監視カメラの首振り
    /// </summary>
    private void RotCamera()
    {
        const float RotSpeed = 0.5f;

        if (rotMove)
        {
            sec += Time.deltaTime;

            var RotAngle = Mathf.Sin(sec * RotSpeed) * rotAngle;

            transform.rotation = Quaternion.AngleAxis(
                RotAngle,
                Vector3.up) * defaultRotation;
        }
    }

    /// <summary>
    /// 監視カメラ移動
    /// </summary>
    private void CameraMove()
    {
        const float MoveSpeed = 0.3f;

        if (posMove)
        {
            sec += Time.deltaTime;

            var move = Mathf.Sin(sec * MoveSpeed) * MoveDist;

            transform.position = new Vector2(
               transform.position.x,
                comMoveVec.z + move);
        }
    }

    /// <summary>
    /// 再起動
    /// </summary>
    private void Reboot()
    {
     
        //laserRebootTime -= Time.deltaTime;

        //Debug.Log(laserRebootTime);

        ////再起動
        //if (laserRebootTime <= 0.0)
        //{
        //    alpha = 1;
        //    laserRebootTime = setRebootTime;
        //    StopCoroutine("ColorCoroutin");
        //    InfraredIrradiation(true);
        //}
    }

    public void ShutDown()
    {
        camGimkPower = false;
    }

    /// <summary>
    /// プレイヤーが監視カメラに引っかかったか
    /// </summary>
    /// <returns></returns>
    public static bool GetSearchHit()
    {
        return playerSearchHit;
    }


#if UNITY_EDITOR
    //　サーチする角度表示
    private void OnDrawGizmos()
    {
        searchCollider.radius = serchRadius;

        Handles.color = new Color(255,0,0,0.2f);

        Handles.DrawSolidArc(
            transform.position,
            transform.up,
            (Quaternion.Euler(0, 0, 0f) * transform.forward),
            searchAngle * 2f,
            serchRadius);

        Handles.DrawSolidArc(
            transform.position,
            transform.up,
            (Quaternion.Euler(0, 0, 0f) * transform.forward),
            searchAngle * -2f,
            serchRadius);


        Handles.DrawSolidArc(
           transform.position,
           transform.right,
           (Quaternion.Euler(0, 0, 0f) * transform.forward),
           searchAngle * 2f,
           serchRadius);

        Handles.DrawSolidArc(
            transform.position,
            transform.right,
            (Quaternion.Euler(0, 0, 0f) * transform.forward),
            searchAngle * -2f,
            serchRadius);
    }
#endif

}
