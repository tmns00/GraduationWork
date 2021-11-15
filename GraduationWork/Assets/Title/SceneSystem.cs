using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    [Header("フェード")] public Fadeimage fade;

    public string sceneName;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ToScene") && SceneManager.GetActiveScene().name != "Game")
        {
            SceneChange();
        }

        if (fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void SceneChange()
    {
        fade.StartFadeOut();
    }
}
