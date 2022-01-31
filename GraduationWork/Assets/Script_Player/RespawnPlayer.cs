using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject resPosition;

    //フェードスピード
    public float fadeSpeed;

    //演出用フェード
    public Image fade;
    //フェードイン中フラグ
    private bool isFadeIn = false;

    //演出用イラスト
    public RawImage image;

    //一時停止確認用のフラグ
    private bool isStop = false;
    //一時停止時間
    public float stopTime;
    //カウント用変数
    [SerializeField]
    private float count = 0.0f;

    //フェードアウト中フラグ
    private bool isFadeOut = false;

    void Start()
    {
        fade.fillAmount = 0.0f;
        image.rectTransform.position = image.rectTransform.position + new Vector3(0, 275.0f, 0);

        isFadeIn = false;
        isStop = false;
        isFadeOut = false;
    }

    void Update()
    {
        if (isFadeIn)
        {
            image.rectTransform.position -= new Vector3(0, 600 / fadeSpeed, 0);
        }

        if (isFadeIn && image.rectTransform.position.y <= 360)
        {
            isFadeIn = false;
            isStop = true;
        }

        if (isStop)
        {
            count += Time.deltaTime;
        }

        if (count >= stopTime)
        {
            count = 0.0f;
            isStop = false;
            isFadeOut = true;
        }

        if(isFadeOut)
        {
            image.rectTransform.position += new Vector3(0, 600 / fadeSpeed, 0);
            fade.fillAmount -= 1 / fadeSpeed;
        }

        if(isFadeOut && fade.fillAmount <=0.0f)
        {
            isFadeOut = false;
        }
    }

    /// <summary>
    /// 外部からの呼び出し
    /// </summary>
    public void Respawn()
    {
        fade.fillAmount = 1.0f;

        player.transform.position = resPosition.transform.position;
        player.transform.rotation = Quaternion.identity;

        isFadeIn = true;
    }
}
