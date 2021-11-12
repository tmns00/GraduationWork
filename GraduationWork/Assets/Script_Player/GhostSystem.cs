using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostSystem : MonoBehaviour
{
    //�H�̂��ǂ���
    [SerializeField]
    private bool isGhost = false;
    //�߂�邩�ǂ���
    private bool isReturn = false;

    //������
    [SerializeField]
    private GameObject deadBody;
    //�����̂̈ꎞ�ۑ��p
    [SerializeField]
    private GameObject body;

    //�H�̉��Q�[�W�̐��l
    [SerializeField]
    private float ghostHP;
    //�H�̉��Q�[�W�̍ő�l
    private const float maxHP = 100.0f;
    //�H�̉��Q�[�W�̏���x
    public float useRate;
    //�H�̉��Q�[�W�̉񕜑��x
    public float chargeRate;
    //�Q�[�W�\���p�X���C�_�[
    [SerializeField]
    private Slider ghostSlider;
    //�����I�ɖ߂����Ƃ��̈ʒu
    private Vector3 forcedPosition;
    //�߂鑬��
    public float returnSpeed = 1;
    //�߂�Ƃ��̎���
    private float startTime;
    //�߂鋗��
    private float returnDist;

    private void Start()
    {
        ghostSlider.maxValue = maxHP;
        ghostHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        //�H�̉�/�߂�{�^�������ʂ̂��߃t���O�ŏ�������
        if (Input.GetButtonDown("GhostButton") && !isGhost)
            TurnGhost();
        else if (Input.GetButtonDown("GhostButton") && isReturn && isGhost)
            ReturnToBody();

        GaugeUpdate();
    }

    //�H�̉�
    void TurnGhost()
    {
        isGhost = true;
        //�����̂����݈ʒu�ɐ���
        Instantiate(
            deadBody,
            transform.position,
            deadBody.gameObject.transform.rotation
            );
        //�^�O��ύX
        gameObject.tag = "Ghost";
    }

    //�߂�
    void ReturnToBody()
    {
        isGhost = false;
        Destroy(body);
        isReturn = false;
        //�^�O��ύX
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

    //�H�̃t���O�̎擾
    public bool GetIsGhost()
    {
        return isGhost;
    }

    //�H�̃t���O�̐ݒ�
    public void SetIsGhost(bool flag)
    {
        isGhost = flag;
    }

    //�Q�[�W�̍X�V
    void GaugeUpdate()
    {
        if (ghostHP < 0)
            ghostHP = 0;

        //�Q�[�W��0��
        if (ghostHP <= 0)
        {
            forcedPosition = transform.position;
            returnDist = Vector3.Distance(forcedPosition, body.transform.position);
            PauseSystem.SetPauseFlag(true);
            ForcedReturn();
        }

        //�ő�l���傫���Ȃ��Ă��܂������ɐ؂�̂�
        if (ghostHP >= maxHP)
            ghostHP = maxHP;

        //�����̏�������
        if (isGhost)
            ghostHP -= useRate;
        else if (!isGhost && ghostHP <= maxHP)
            ghostHP += chargeRate;
        else
            return;

        //�Q�[�W�X�V
        ghostSlider.value = ghostHP;
    }

    //�����I�ɖ߂�
    void ForcedReturn()
    {
        //transform.position = body.transform.position;
        float interpolateValue = (Time.time - startTime) / returnDist * returnSpeed;
        transform.position = Vector3.Lerp(forcedPosition, body.transform.position, interpolateValue);

        if (transform.position == body.transform.position)
            ReturnToBody();
    }
}
