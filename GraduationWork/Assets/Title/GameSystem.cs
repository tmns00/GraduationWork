using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{

    //�@�X�^�[�g�{�^��������������s����
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}