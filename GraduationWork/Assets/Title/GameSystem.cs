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
    //�@�X�^�[�g�{�^��������������s����
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}