using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    [Header("フェード")] public Fadeimage fade;

    public string sceneName;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ToScene") && SceneManager.GetActiveScene().name != "Game")
        {
            SceneChange();
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
