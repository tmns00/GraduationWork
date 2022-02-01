using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    [Header("フェード")] public Fadeimage fade;

    public string sceneName;
    AudioSource audioSource;

    private bool startOnce = true;
    private bool playOnce = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startOnce = true;
        playOnce = true;
    }

    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ToScene") && SceneManager.GetActiveScene().name != "Game")
        {
            SceneChange();
        }

        if(startOnce && SceneManager.GetActiveScene().name == "Game")
        {
            audioSource.Stop();
            startOnce = false;
        }

        if(playOnce && RoundTripManager.GetIsBackWay())
        {
            audioSource.Play();
            playOnce = false;
        }

        if (fade.IsFadeOutComplete())
        {
            audioSource.Stop();
            SceneManager.LoadScene(sceneName);
        }
    }

    public void SceneChange()
    {
        fade.StartFadeOut();
    }
}
