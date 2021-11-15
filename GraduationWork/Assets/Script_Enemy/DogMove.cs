using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogMove : MonoBehaviour
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
    //あたり判定の半径を変更
    [SerializeField]
    SphereCollider sphereCollider;
    //霊体を見つけたら吠える
    [SerializeField]
    GameObject barkCollider;
    [SerializeField]
    float barkTime;
    float timeb;
    bool bark;
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
        barkCollider.SetActive(false);
        timeb = 0;
        bark = false;
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
                isShot = false;
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
        if(!bark)
        {
            timeb += 1 / 60f;
            if (timeb >= barkTime)
            {
                barkCollider.SetActive(false);
            }
        }
       
    }
    private void OnTriggerStay(Collider other)
    {
        var positionDiff = other.transform.position - transform.position; //敵とプレイヤーの距離
        var angle = Vector3.Angle(transform.forward, positionDiff); //敵からみたプレイヤーの方向
        if (other.gameObject.tag == "Player")
        {
           
            if (angle <= searchAngle)
            {
                isAttack = true;
            }

            if (other.gameObject.tag == "Player" && isAttack)
            {
                if (angle >= searchAngle)
                {
                    isTracking = true;
                }
            }
        }
        if (other.gameObject.tag == "playerattack")
        {
            isShot = true;
        }
        if (other.gameObject.tag == "Ghost")
        {
            if (angle <= searchAngle)
            {
                barkCollider.SetActive(true);
                bark = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isAttack)
                isTracking = true;
        }
        if(other.gameObject.tag=="Ghost")
        {
            bark = false;
        }
      
    }
}
