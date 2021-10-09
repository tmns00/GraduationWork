using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SurveillanceCamera : MonoBehaviour
{
    [SerializeField] private bool RotMove; //�Ď��J�����𓮂�����
    [SerializeField] private bool PosMove;
    [SerializeField, Range(0, 90.0f)]
    private float SearchAngle;//�T�[�`�͈�
    [SerializeField] float SerchRadius;�@//�T�[�`���a
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField, Range(0, 90.0f)] float Angle = 0;//��]�A���O��

    float RotSpeed = 0.5f;
    Vector3 vec;
    float sec;
    bool playerSearchHit;//�v���C���[���Ď��J�����͈̔͂ɐG�ꂽ
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
            Debug.Log("��l������: ");

        }

        RotCamera();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //�v���C���[���͈͂ɓ�������
        if (other.tag == "Player")
        {
            const float Search_Adjust = 2.5f;//�T�[�`�͈͒���
            //�@��l���̕���
            var playerDirection = other.transform.position - transform.position;
            //�@�G�̑O������̎�l���̕���
            var angle = Vector2.Angle(transform.right, playerDirection);
            //�@�T�[�`����p�x���������甭��
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

    //�Ď��J�����̎�U��
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

    //�Ď��J�����ړ�
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
    //�@�T�[�`����p�x�\��
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
