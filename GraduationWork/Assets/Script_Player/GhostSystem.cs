using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostSystem : MonoBehaviour
{
    //幽体かどうか
    [SerializeField]
    private bool isGhost = false;
    //戻れるかどうか
    private bool isReturn = false;

    //仮死体
    [SerializeField]
    private GameObject deadBody;
    //仮死体の一時保存用
    [SerializeField]
    private GameObject body;

    //幽体化ゲージの数値
    [SerializeField]
    private float ghostHP;
    //幽体化ゲージの最大値
    private const float maxHP = 100.0f;
    //幽体化ゲージの消費速度
    public float useRate;
    //幽体化ゲージの回復速度
    public float chargeRate;
    //ゲージ表示用スライダー
    [SerializeField]
    private Slider ghostSlider;
    //強制的に戻されるときの位置
    private Vector3 forcedPosition;
    //戻る速さ
    public float returnSpeed = 1;
    //戻るときの時間
    private float startTime;
    //戻る距離
    private float returnDist;

    //検知するためのエリア
    public GameObject searchAreaObj;
    //検知エリアスクリプト
    [SerializeField]
    private SearchArea searchArea;

    //警戒度ゲージ
    [SerializeField]
    private BarCtrl barCtrl;
    //ゲージ変更フラグ
    private bool canBar = false;

    //本体の当たり判定
    [SerializeField]
    private CapsuleCollider bodyCol;

    //幽体化関連のUI
    public RawImage toGhostUI;
    public RawImage reBodyUI;

    //アクションUI
    public RawImage actionUI;

    private bool isReset = false;

    private bool canMove = true;

    private void Start()
    {
        ghostSlider.maxValue = maxHP;
        ghostHP = maxHP;
        toGhostUI.enabled = true;
        reBodyUI.enabled = false;
        isReset = false;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canBar)
        {
            barCtrl.SetHP(searchArea.GetEnemyCount() * 5);
            canBar = false;
        }

        //幽体化/戻るボタンが共通のためフラグで条件分け
        if (Input.GetButtonDown("GhostButton") && !isGhost)
            TurnGhost();
        else if (Input.GetButtonDown("GhostButton") && isReturn && isGhost)
            ReturnToBody();

        GaugeUpdate();
    }

    //幽体化
    void TurnGhost()
    {
        isGhost = true;
        //仮死体を現在位置に生成
        Instantiate(
            deadBody,
            transform.position,
            deadBody.gameObject.transform.rotation
            );

        //タグを変更
        gameObject.tag = "Ghost";

        //コライダーのトリガー化
        bodyCol.isTrigger = true;

        //UI切り替え
        toGhostUI.enabled = false;
    }

    //戻る
    void ReturnToBody()
    {
        isGhost = false;
        transform.position = body.transform.position;
        Destroy(body);
        isReturn = false;
        //タグを変更
        gameObject.tag = "Player";
        PauseSystem.SetPauseFlag(false);

        //コライダーのトリガー解除
        bodyCol.isTrigger = false;

        //UI切り替え
        toGhostUI.enabled = true;
        reBodyUI.enabled = false;

        isReset = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeadBody")
        {
            isReturn = true;
            body = other.gameObject;
            reBodyUI.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DeadBody")
        {
            isReturn = false;
            reBodyUI.enabled = false;
        }
    }

    //幽体フラグの取得
    public bool GetIsGhost()
    {
        return isGhost;
    }

    //幽体フラグの設定
    public void SetIsGhost(bool flag)
    {
        isGhost = flag;
    }

    //ゲージの更新
    void GaugeUpdate()
    {
        if (ghostHP < 0)
            ghostHP = 0;

        //ゲージが0か
        if (ghostHP <= 0)
        {
            forcedPosition = transform.position;
            returnDist = Vector3.Distance(forcedPosition, body.transform.position);
            PauseSystem.SetPauseFlag(true);
            //startTime = Time.deltaTime;
            ForcedReturn();
            return;
        }

        //最大値より大きくなってしまった時に切り捨て
        if (ghostHP >= maxHP)
            ghostHP = maxHP;

        //増減の条件分け
        if (isGhost)
            ghostHP -= useRate;
        else if (!isGhost && ghostHP <= maxHP)
            ghostHP += chargeRate;
        else
            return;

        //ゲージ更新
        ghostSlider.value = ghostHP;
    }

    //強制的に戻す
    void ForcedReturn()
    {
        //transform.position = body.transform.position;
        float interpolateValue = (Time.deltaTime - startTime) / returnDist * returnSpeed;
        transform.position = Vector3.Lerp(forcedPosition, body.transform.position, interpolateValue);

        if (transform.position == body.transform.position)
        {
            var createObj = Instantiate(searchAreaObj, transform.position, Quaternion.identity);
            searchArea = createObj.GetComponent<SearchArea>();
            canBar = true;

            ReturnToBody();
        }
    }

    public bool GetResetFlag()
    {
        return isReset;
    }

    public void SetResetFlag(bool flag)
    {
        isReset = flag;
    }

    public bool GetMoveFlag()
    {
        return canMove;
    }

    public void SetMoveFlag(bool flag)
    {
        canMove = flag;
    }
}