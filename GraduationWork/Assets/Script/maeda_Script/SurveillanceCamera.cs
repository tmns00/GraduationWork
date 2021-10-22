using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Sprites;

public class SurveillanceCamera : MonoBehaviour
{
    [SerializeField] bool rotMove; //監視カメラを動かすか
    [SerializeField] bool posMove;

    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField, Range(0, 90.0f)] float searchAngle;//サーチ範囲
    [SerializeField] float serchRadius;　//サーチ半径
    [SerializeField, Range(0, 90.0f)] float rotAngle = 0;//回転アングル

    [SerializeField] float MoveDist = 0;//監視カメラを動かす距離
    [SerializeField] Vector2 vec;//監視カメラを動かす方向ベクトル

    float RotSpeed = 0.5f;
    float MoveSpeed = 0.3f;
    float sec;
    bool playerSearchHit;//プレイヤーが監視カメラの範囲に触れた
    Quaternion defaultRotation;

    // Start is called before the first frame update
    void Start()
    {
        playerSearchHit = false;
        defaultRotation = transform.rotation;
    }

    private void Update()
    {
        circleCollider.radius = serchRadius;

        if (playerSearchHit)
        {
            Debug.Log("主人公発見: ");

        }

        RotCamera();
        CameraMove();

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //プレイヤーが範囲に入ったら
        if (other.tag == "Player")
        {
            const float Search_Adjust = 2.5f;//サーチ範囲調整
            //　主人公の方向
            var playerDirection = other.transform.position - transform.position;
            //　敵の前方からの主人公の方向
            var angle = Vector2.Angle(transform.right, playerDirection);
            //　サーチする角度内だったら発見
            if (angle <= searchAngle* Search_Adjust ||
               angle <= searchAngle * -Search_Adjust)
            {
                playerSearchHit = true;
            }
            else if(angle >= searchAngle * Search_Adjust ||
               angle >= searchAngle * -Search_Adjust)
            {
                playerSearchHit = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerSearchHit = false;
        }
    }

    /// <summary>
    /// 監視カメラの首振り
    /// </summary>
    private void RotCamera()
    {
        if (rotMove)
        {
            sec += Time.deltaTime;

            var RotAngle = Mathf.Sin(sec * RotSpeed) * rotAngle;

            transform.rotation = Quaternion.AngleAxis(
                RotAngle,
                Vector3.forward) * defaultRotation;
        }
    }

    /// <summary>
    /// 監視カメラ移動
    /// </summary>
    private void CameraMove()
    {
        if (posMove)
        {
            sec += Time.deltaTime;

            var move = Mathf.Sin(sec * MoveSpeed) * MoveDist;

            transform.position = new Vector2(
               transform.position.x,
                vec.y + move);
        }
    }



    /// <summary>
    /// プレイヤーが監視カメラに引っかかったか
    /// </summary>
    /// <returns></returns>
    private bool SearchHit()
    {
        return playerSearchHit;
    }


#if UNITY_EDITOR
    //　サーチする角度表示
    private void OnDrawGizmos()
    {
        circleCollider.radius = serchRadius;

        Handles.color = new Color(255,0,0,0.2f);
   
        Handles.DrawSolidArc(
            transform.position,
            transform.forward,
            (Quaternion.Euler(0,0, 0f) * transform.right ),
            searchAngle * 2f,
            serchRadius);

        Handles.DrawSolidArc(
            transform.position,
            transform.forward,
            (Quaternion.Euler(0, 0, 0f) * transform.right),
            searchAngle * -2f,
            serchRadius);
               
    }
#endif

}
