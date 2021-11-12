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

    private void Start()
    {
        ghostSlider.maxValue = maxHP;
        ghostHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    //戻る
    void ReturnToBody()
    {
        isGhost = false;
        Destroy(body);
        isReturn = false;
        //タグを変更
        gameObject.tag = "Player";
        PauseSystem.SetPauseFlag(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeadBody")
        {
            isReturn = true;
            body = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DeadBody")
            isReturn = false;     
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
            ForcedReturn();
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
        float interpolateValue = (Time.time - startTime) / returnDist * returnSpeed;
        transform.position = Vector3.Lerp(forcedPosition, body.transform.position, interpolateValue);

        if (transform.position == body.transform.position)
            ReturnToBody();
    }
}
