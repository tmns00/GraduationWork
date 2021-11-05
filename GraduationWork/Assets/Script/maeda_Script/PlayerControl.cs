using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;             //移動速度

    private Vector3 vec3;
    //private new Rigidbody2D rigidbody;

    private void Start()
    {
        //rigidbody =GetComponent<Rigidbody2D>();

    }
    
    private void FixedUpdate()
    {
        
        float axisX = Input.GetAxisRaw("Horizontal") 
            * moveSpeed * Time.deltaTime;

        float axisZ = Input.GetAxisRaw("Vertical")
           * moveSpeed * Time.deltaTime;

        transform.position += new Vector3(axisX, 0, axisZ);
                     

    }

}
