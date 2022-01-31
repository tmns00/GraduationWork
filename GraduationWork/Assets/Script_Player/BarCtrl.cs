using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarCtrl : MonoBehaviour
{
    Slider _slider;
    [SerializeField]
    float _hp = 0;

    [SerializeField]
    private SceneSystem sceneSystem;

    //AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GameObject.Find("Gauge").GetComponent<Slider>();
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hp >= 100)
            GameOver();

        //_hp += 0.01f;
        //if(_hp > 1)
        //{
        //    _hp = 0;
        //}
        _slider.value = _hp;
    }

    public void SetHP(float hp)
    {
        _hp += hp;
    }

    public float GetHP()
    {
        return _hp;
    }

    private void GameOver()
    {
        //audioSource.Stop();
        sceneSystem.sceneName = "Gameover";
        sceneSystem.SceneChange();
    }
}
