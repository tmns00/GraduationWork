using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem2 : MonoBehaviour
{

    //�@�X�^�[�g�{�^��������������s����
    public void StartGame()
    {
        SceneManager.LoadScene("Title");
    }
}