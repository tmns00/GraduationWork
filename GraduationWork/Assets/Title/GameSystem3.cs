using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem3 : MonoBehaviour
{

    //　スタートボタンを押したら実行する
    public void StartGame()
    {
        SceneManager.LoadScene("Title");
    }
}