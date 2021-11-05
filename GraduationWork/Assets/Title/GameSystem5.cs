using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem5 : MonoBehaviour
{
    [SerializeField]
    private BarCtrl barCtrl;

    private void Update()
    {
        if (barCtrl.GetHP() >= 100)
            StartGame();
    }

    //　スタートボタンを押したら実行する
    public void StartGame()
    {
        SceneManager.LoadScene("Gameover");
    }
}