using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Sprites;

public class SurveillanceCamera : MonoBehaviour
{
    [SerializeField] bool rotMove; //�Ď��J�����𓮂�����
    [SerializeField] bool posMove;

    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField, Range(0, 90.0f)] float searchAngle;//�T�[�`�͈�
    [SerializeField] float serchRadius;�@//�T�[�`���a
    [SerializeField, Range(0, 90.0f)] float rotAngle = 0;//��]�A���O��

    [SerializeField] float MoveDist = 0;//�Ď��J�����𓮂�������
    [SerializeField] Vector2 vec;//�Ď��J�����𓮂��������x�N�g��

    float RotSpeed = 0.5f;
    float MoveSpeed = 0.3f;
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
        circleCollider.radius = serchRadius;

        if (playerSearchHit)
        {
            Debug.Log("��l������: ");

        }

        RotCamera();
        CameraMove();

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
    /// �Ď��J�����̎�U��
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
    /// �Ď��J�����ړ�
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
    /// �v���C���[���Ď��J�����Ɉ�������������
    /// </summary>
    /// <returns></returns>
    private bool SearchHit()
    {
        return playerSearchHit;
    }


#if UNITY_EDITOR
    //�@�T�[�`����p�x�\��
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
