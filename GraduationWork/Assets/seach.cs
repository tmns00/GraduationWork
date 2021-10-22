using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class seach : MonoBehaviour
{
    [SerializeField]
    private enemymove enemyMove;
    [SerializeField]
    private SphereCollider searchArea;
    [SerializeField]
    private float searchAngle = 130f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            var playerDirection = other.transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, playerDirection);
            if (angle <= searchAngle)
            {
                Debug.Log("hakken");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }

    //private void OnDrawGizmos()
    //{
    //    Handles.color = Color.red;
    //    Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -searchArea,0f) * transform.forward, searchAngle * 2f, searchArea.radius);
    //}
}
