using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haunted : MonoBehaviour
{
    float time;
    float timeEnd;
    public bool isFear;
    public Fear fear;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        timeEnd = 3;
        isFear = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(fear.Fearvalue);
        if(isFear)
        {
            time += 1 / 60.0f;
            if(time>=timeEnd)
            {
                isFear = false;
            }
        }
        if (!isFear)
            time = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isFear)
        {
            if (other.gameObject.tag == "Ghost")
            {
                fear.Fearvalue -= 10;
                isFear = true;
            }
        }
    }
}
