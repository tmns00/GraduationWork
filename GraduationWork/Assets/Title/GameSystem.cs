using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("ToScene"))
            StartGame();
    }
    //　スタートボタンを押したら実行する
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}