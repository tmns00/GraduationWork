using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            audioSource.PlayOneShot(sound);
        }
    }
}
