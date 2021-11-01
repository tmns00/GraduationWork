using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    public float rotation;
    public float time;



    // Start is called before the first frame update
    void Start()
    {
        rotation = 0;
        time = 0;


    }

    // Update is called once per frame
    void Update()
    {
        //time -= Time.deltaTime;
        //if (time <= 0)
        //{
        //    time = 0.1f;

        //    for (int i = 0; i < 91; i++)
        //    {
        //        rotation = i;

        //        if (i >= 90)
        //        {
        //            i = 0;
        //        }
        //    }

        rotation += 0.01f;

        if (rotation > 90)
        {
            rotation = 0;
        }

        transform.Rotate(new Vector3(0, rotation, 0));


    }

}   



