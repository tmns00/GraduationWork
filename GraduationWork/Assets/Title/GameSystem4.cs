using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem4 : MonoBehaviour
{
    [Header("�t�F�[�h")] public Fadeimage fade;

    private bool firstPush = false;
    private bool goNextScene = false;
    //�X�^�[�g�{�^��������������s����
    public void StartGame()
    {
        fade.StartFadeOut();
    }
    private void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene("Clear");
            goNextScene = true;
        }
    }
}