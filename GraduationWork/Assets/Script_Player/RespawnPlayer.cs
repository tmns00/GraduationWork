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

    //�t�F�[�h�X�s�[�h
    public float fadeSpeed;

    //���o�p�t�F�[�h
    public Image fade;
    //�t�F�[�h�C�����t���O
    private bool isFadeIn = false;

    //���o�p�C���X�g
    public RawImage image;

    //�ꎞ��~�m�F�p�̃t���O
    private bool isStop = false;
    //�ꎞ��~����
    public float stopTime;
    //�J�E���g�p�ϐ�
    [SerializeField]
    private float count = 0.0f;

    //�t�F�[�h�A�E�g���t���O
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
    /// �O������̌Ăяo��
    /// </summary>
    public void Respawn()
    {
        fade.fillAmount = 1.0f;

        player.transform.position = resPosition.transform.position;
        player.transform.rotation = Quaternion.identity;

        isFadeIn = true;
    }
}
