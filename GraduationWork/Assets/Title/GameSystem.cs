using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    [Header("フェード")] public Fadeimage fade;

    private bool firstPush = false;
    private bool goNextScene = false;
    　//スタートボタンを押したら実行する
    public void StartGame()
    {
        fade.StartFadeOut();
    }
    private void Update()
    {     
        if (!goNextScene && fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene("Stageselect");
            goNextScene = true;
        }
    }
}