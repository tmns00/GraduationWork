using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarCtrl : MonoBehaviour
{
    [SerializeField]
    Slider _slider;
    [SerializeField]
    float _hp = 0;

    [SerializeField]
    private SceneSystem sceneSystem;

    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GameObject.Find("Gauge").GetComponent<Slider>();
        once = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hp >= 100)
            GameOver();

        if (once && RoundTripManager.GetIsBackWay())
        {
            _hp = 95.0f;
            once = false;
        }
        
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
        sceneSystem.sceneName = "Gameover";
        sceneSystem.SceneChange();
    }
}
