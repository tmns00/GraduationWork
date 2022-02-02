using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemyMove : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public bool isAttack;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    float searchAngle = 45f;
    [SerializeField]
    float trackingstop;
    float time;
    private bool isTracking;
    public bool isShot;
    [SerializeField]
    float shottime;
    float times;
    public Fear fear;
    public bool PlayerCatch;
    //�����蔻��̔��a��ύX
    SphereCollider sphereCollider;
    public Haunted ht;
    Vector3 positionDiff;
    Vector3 DeadBodyDiff;
    float angle;
    float Deadangle;
    bool isSerch;
    //�ǂ��ђʂ��Ȃ��悤�ɂ��郌�C
    Ray ray;
    RaycastHit hit;
    [SerializeField]
    private GameObject DeadBody;
    bool isRunaway;
    int choose;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        // autoBraking �𖳌��ɂ���ƁA�ڕW�n�_�̊Ԃ��p���I�Ɉړ����܂�
        //(�܂�A�G�[�W�F���g�͖ڕW�n�_�ɋ߂Â��Ă�
        // ���x�����Ƃ��܂���)
        agent.autoBraking = false;
        //�G�𔭌�
        isAttack = false;
        //�G�̎��E���瓦�ꂽ���1�b�ԒǐՂ���
        isTracking = false;
        time = 0;
        GotoNextPoint();
        isShot = false;
        sphereCollider = GetComponent<SphereCollider>();
        isSerch = false;
        PlayerCatch = false;
        isRunaway = false;
        choose = Random.Range(0, 2);
        trackingstop = 3;
    }


    void GotoNextPoint()
    {
        // �n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ��܂�
        if (points.Length == 0)
            return;

        // �G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ肵�܂�
        agent.destination = points[destPoint].position;

        // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A
        // �K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�
        destPoint = (destPoint + 1) % points.Length;

    }


    void Update()
    {

        //Debug.Log(fear.FearLevel);
        if (isSerch)
        {
            positionDiff = Player.transform.position - transform.position; //�G�ƃv���C���[�̋���
            angle = Vector3.Angle(transform.forward, positionDiff); //�G����݂��v���C���[�̕���

        }

        switch (choose)
        {
            case 0:
                Patrol();
                isShot = ht.isFear;
                break;
            case 1:
                RunAway();
                break;
        }




    }

    private void OnTriggerStay(Collider other)
    {
        var distance = positionDiff.magnitude;
        var direction = positionDiff.normalized;


        if (other.gameObject.tag == "Player")
        {

            isSerch = true;
            if (angle <= searchAngle)
            {
                if (Physics.Raycast(transform.position, direction, out hit, distance))
                {
                    if (hit.collider.gameObject.tag == ("Wall"))
                    {
                        isAttack = false;
                    }
                    else
                    {
                        isAttack = true;
                    }
                }
            }

            if (other.gameObject.tag == "Player" && isAttack)
            {
                if (angle >= searchAngle)
                {
                    isTracking = true;
                }
            }
        }
        if (fear.FearLevel >= 1 && choose == 0)
        {

            if (other.gameObject.tag == "Ghost")
            {


                if (angle <= searchAngle)
                {

                    if (Physics.Raycast(transform.position, direction, out hit, distance))
                    {
                        if (hit.collider.gameObject.tag == ("Wall"))
                        {
                            isAttack = false;
                        }
                        else
                        {
                            isAttack = true;
                        }
                    }

                }

                if (other.gameObject.tag == "Ghost" && isAttack)
                {
                    if (angle >= searchAngle)
                    {
                        isTracking = true;
                    }
                }
            }
        }
        if (choose == 1)
        {
            if (other.gameObject.tag == "Ghost")
            {
                isSerch = true;
                if (angle <= searchAngle)
                {

                    if (Physics.Raycast(transform.position, direction, out hit, distance))
                    {
                        if (hit.collider.gameObject.tag == ("Wall"))
                        {
                            isRunaway = false;
                        }
                        else
                        {
                            isRunaway = true;
                        }
                    }

                }

                if (other.gameObject.tag == "Ghost" && isAttack)
                {
                    if (angle >= searchAngle)
                    {
                        isTracking = true;
                    }
                }
            }
            if (other.gameObject.tag == "DeadBody")
            {
                DeadBody = GameObject.Find("DeadBody(Clone)");
                DeadBodyDiff = DeadBody.transform.position - transform.position; //�G�ƃv���C���[�̋���
                Deadangle = Vector3.Angle(transform.forward, positionDiff); //�G����݂��v���C���[�̕���
                if (Deadangle <= searchAngle)
                {

                    agent.SetDestination(DeadBody.transform.position);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isAttack)
            isTracking = true;


    }
    void RunAway()
    {
        if (!isAttack && !isRunaway)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
        //���G�͈͓���Player���������炨��
        if (isAttack && !isRunaway)
            agent.SetDestination(Player.transform.position);
        //���G�͈͊O�ɍs������1�b�ԒǐՂ���
        if (isTracking)
        {
            time += 1 / 60f;
            if (time >= trackingstop)
            {
                isAttack = false;
                isTracking = false;
                isSerch = false;
            }
        }
      

        if (isRunaway)
        {
            Debug.Log(agent.speed);
            time += 1 / 60f;
            agent.speed = 13;
            if (Player.transform.position.x > transform.position.x)
            {
                agent.SetDestination(Player.transform.position * -1);
            }
            if (Player.transform.position.x < transform.position.x)
            {
                agent.SetDestination(Player.transform.position * 3);
            }
            if (time >= trackingstop)
            {
                isRunaway = false;
            }
        }
        if (!isTracking && !isRunaway)
            time = 0;
      
           
        if (!isRunaway)
           agent.speed = 3.5f;
    }
    void Patrol()
    {
        switch (fear.FearLevel)
        {
            case 0:
                // �G�[�W�F���g�����ڕW�n�_�ɋ߂Â��Ă�����A
                // ���̖ڕW�n�_��I�����܂�
                if (!isAttack)
                {
                    if (!agent.pathPending && agent.remainingDistance < 0.5f)
                        GotoNextPoint();
                }
                //���G�͈͓���Player���������炨��
                if (isAttack)
                    agent.SetDestination(Player.transform.position);
                //���G�͈͊O�ɍs������1�b�ԒǐՂ���
                if (isTracking)
                {
                    time += 1 / 60f;
                    if (time >= trackingstop)
                    {
                        isAttack = false;
                        isTracking = false;
                        isSerch = false;
                    }
                }
                if (!isTracking)
                    time = 0;

                if (isShot)
                {
                    times += 1 / 60f;
                    if (times >= shottime)
                    {
                        times = 0;
                        ht.isFear = false;
                    }
                }
                if (isShot)
                {
                    agent.speed = 1.5f;
                    sphereCollider.radius = 3;
                }
                if (!isShot)
                {
                    agent.speed = 3.5f;
                    sphereCollider.radius = 6;
                }


                break;


            case 1:
                // �G�[�W�F���g�����ڕW�n�_�ɋ߂Â��Ă�����A
                // ���̖ڕW�n�_��I�����܂�
                if (!isAttack)
                {
                    if (!agent.pathPending && agent.remainingDistance < 0.5f)
                        GotoNextPoint();
                }
                //���G�͈͓���Player���������炨��
                if (isAttack)
                    agent.SetDestination(Player.transform.position);
                //���G�͈͊O�ɍs������1�b�ԒǐՂ���
                if (isTracking)
                {
                    time += 1 / 60f;
                    if (time >= trackingstop)
                    {
                        isAttack = false;
                        isTracking = false;
                        isSerch = false;
                    }
                }
                if (!isTracking)
                    time = 0;

                if (isShot)
                {
                    times += 1 / 60f;
                    if (times >= shottime)
                    {
                        times = 0;
                        ht.isFear = false;
                    }
                }
                if (isShot)
                {
                    agent.speed = 2.5f;
                    sphereCollider.radius = 4.5f;
                }
                if (!isShot)
                {
                    agent.speed = 3.5f;
                    sphereCollider.radius = 6;
                }

                break;
            case 2:
                PlayerCatch = true;
                // �G�[�W�F���g�����ڕW�n�_�ɋ߂Â��Ă�����A
                // ���̖ڕW�n�_��I�����܂�
                if (!isAttack)
                {
                    if (!agent.pathPending && agent.remainingDistance < 0.5f)
                        GotoNextPoint();
                }
                //���G�͈͓���Player���������炨��
                if (isAttack)
                    agent.SetDestination(Player.transform.position);
                //���G�͈͊O�ɍs������1�b�ԒǐՂ���
                if (isTracking)
                {
                    time += 1 / 60f;
                    if (time >= trackingstop)
                    {
                        isAttack = false;
                        isTracking = false;
                        isSerch = false;
                    }
                }
                if (!isTracking)
                    time = 0;

                break;
        }
    }
}
