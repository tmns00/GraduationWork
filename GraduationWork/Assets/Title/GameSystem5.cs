using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem5 : MonoBehaviour
{

    //�@�X�^�[�g�{�^��������������s����
    public void StartGame()
    {
        SceneManager.LoadScene("Gameover");
    }
}