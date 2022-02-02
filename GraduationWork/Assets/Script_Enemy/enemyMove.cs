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
    //あたり判定の半径を変更
    SphereCollider sphereCollider;
    public Haunted ht;
    Vector3 positionDiff;
    Vector3 DeadBodyDiff;
    float angle;
    float Deadangle;
    bool isSerch;
    //壁を貫通しないようにするレイ
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
        // autoBraking を無効にすると、目標地点の間を継続的に移動します
        //(つまり、エージェントは目標地点に近づいても
        // 速度をおとしません)
        agent.autoBraking = false;
        //敵を発見
        isAttack = false;
        //敵の視界から逃れた後に1秒間追跡する
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
        // 地点がなにも設定されていないときに返します
        if (points.Length == 0)
            return;

        // エージェントが現在設定された目標地点に行くように設定します
        agent.destination = points[destPoint].position;

        // 配列内の次の位置を目標地点に設定し、
        // 必要ならば出発地点にもどります
        destPoint = (destPoint + 1) % points.Length;

    }


    void Update()
    {

        //Debug.Log(fear.FearLevel);
        if (isSerch)
        {
            positionDiff = Player.transform.position - transform.position; //敵とプレイヤーの距離
            angle = Vector3.Angle(transform.forward, positionDiff); //敵からみたプレイヤーの方向

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
                DeadBodyDiff = DeadBody.transform.position - transform.position; //敵とプレイヤーの距離
                Deadangle = Vector3.Angle(transform.forward, positionDiff); //敵からみたプレイヤーの方向
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
        //索敵範囲内にPlayerが入ったらおう
        if (isAttack && !isRunaway)
            agent.SetDestination(Player.transform.position);
        //索敵範囲外に行った後1秒間追跡する
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
                // エージェントが現目標地点に近づいてきたら、
                // 次の目標地点を選択します
                if (!isAttack)
                {
                    if (!agent.pathPending && agent.remainingDistance < 0.5f)
                        GotoNextPoint();
                }
                //索敵範囲内にPlayerが入ったらおう
                if (isAttack)
                    agent.SetDestination(Player.transform.position);
                //索敵範囲外に行った後1秒間追跡する
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
                // エージェントが現目標地点に近づいてきたら、
                // 次の目標地点を選択します
                if (!isAttack)
                {
                    if (!agent.pathPending && agent.remainingDistance < 0.5f)
                        GotoNextPoint();
                }
                //索敵範囲内にPlayerが入ったらおう
                if (isAttack)
                    agent.SetDestination(Player.transform.position);
                //索敵範囲外に行った後1秒間追跡する
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
                // エージェントが現目標地点に近づいてきたら、
                // 次の目標地点を選択します
                if (!isAttack)
                {
                    if (!agent.pathPending && agent.remainingDistance < 0.5f)
                        GotoNextPoint();
                }
                //索敵範囲内にPlayerが入ったらおう
                if (isAttack)
                    agent.SetDestination(Player.transform.position);
                //索敵範囲外に行った後1秒間追跡する
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
