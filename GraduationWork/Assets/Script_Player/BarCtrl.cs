using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarCtrl : MonoBehaviour
{
    Slider _slider;
    float _hp = 0;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GameObject.Find("Gauge").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _hp += 0.01f;
        if(_hp > 1)
        {
            _hp = 0;
        }
        _slider.value = _hp;
    }
}
