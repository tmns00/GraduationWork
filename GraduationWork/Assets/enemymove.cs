using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemymove : MonoBehaviour
{
    [SerializeField]
    private Transform _self;
    [SerializeField]
    private Transform _target1;
    [SerializeField]
    private Transform _target2;

    private Vector3 _forward = Vector3.forward;

    private float second;

    private int target;

    private Vector3 startPosition;


    GameObject Player;

    public hit hit;

    private NavMeshAgent _agent;

    private float overtime;

    // Start is called before the first frame update
    void Start()
    {

        startPosition = transform.position;
        Player = GameObject.FindWithTag("Player");

        _agent = GetComponent<NavMeshAgent>();
        target = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (hit.ishit == false)
        {
            _agent.SetDestination(startPosition);
            _agent.updateRotation = false;

            second += Time.deltaTime;

            //float speed=0.1f;

            //Vector3 relativePos = _target1.transform.position - this.transform.position;
            //Quaternion rotation = Quaternion.LookRotation(relativePos);
            //transform.rotation = Quaternion.Slerp(this.transform.rotation,rotation,speed);

            if (second >= 2 && target == 0)
            {
                _self.LookAt(_target1);
                target += 1;
            }

            if (second >= 4 && target == 1)
            {
                _self.LookAt(_target2);
                target += 1;
            }

            if (second >= 6 && target == 2)
            {
                _self.LookAt(_target1);
                second = 0;
                target = 0;
            }

        }


        if (hit.ishit)
        {


            overtime += 1 / 60f;
            if (overtime >= 4)
            {
                hit.ishit = false;
            }
            _agent.destination = Player.transform.position;
        }
        if (!hit.ishit)
            overtime = 0;





    }



}



