using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movehit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.root.gameObject.transform.position;
        transform.rotation = transform.root.gameObject.transform.rotation;

    }
}
