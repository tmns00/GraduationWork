using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SurveillanceCamera : MonoBehaviour
{
    [SerializeField] private bool RotMove; //監視カメラを動かすか
    [SerializeField] private bool PosMove;
    [SerializeField, Range(0, 90.0f)]
    private float SearchAngle;//サーチ範囲
    [SerializeField] float SerchRadius;　//サーチ半径
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField, Range(0, 90.0f)] float Angle = 0;//回転アングル

    float RotSpeed = 0.5f;
    Vector3 vec;
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
        circleCollider.radius = SerchRadius;

        if(playerSearchHit)
        {
            Debug.Log("主人公発見: ");

        }

        RotCamera();
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
            if (angle <= SearchAngle* Search_Adjust ||
               angle <= SearchAngle * -Search_Adjust)
            {
                playerSearchHit = true;
            }
            else if(angle >= SearchAngle * Search_Adjust ||
               angle >= SearchAngle * -Search_Adjust)
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

    //監視カメラの首振り
    private void RotCamera()
    {
        if (RotMove)
        {
            sec += Time.deltaTime;

            var RotAngle = Mathf.Sin(sec * RotSpeed) * Angle;

            transform.rotation = Quaternion.AngleAxis(
                RotAngle,
                Vector3.forward) * defaultRotation;
        }
    }

    //監視カメラ移動
    private void CameraMove()
    {
        if (PosMove)
        {

        }
    }




    private bool SearchHit()
    {
        return playerSearchHit;
    }


#if UNITY_EDITOR
    //　サーチする角度表示
    private void OnDrawGizmos()
    {
        circleCollider.radius = SerchRadius;

        Handles.color = new Color(255,0,0,0.2f);
        Handles.DrawSolidArc(
            transform.position,
            transform.forward,
            (Quaternion.Euler(0,0, 0f) * transform.right ),
            SearchAngle * 2f,
            SerchRadius);

        Handles.DrawSolidArc(
            transform.position,
            transform.forward,
            (Quaternion.Euler(0, 0, 0f) * transform.right),
            SearchAngle * -2f,
            SerchRadius);
    }
#endif
    
}
