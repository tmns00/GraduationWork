using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem5 : MonoBehaviour
{

    //　スタートボタンを押したら実行する
    public void StartGame()
    {
        SceneManager.LoadScene("Gameover");
    }
}