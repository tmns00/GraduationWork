using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem5 : MonoBehaviour
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
            SceneManager.LoadScene("Gameover");
            goNextScene = true;
        }
    }
}