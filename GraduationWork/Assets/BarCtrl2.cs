using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarCtrl2 : MonoBehaviour
{
    [SerializeField]
    Slider _slider;
    [SerializeField]
    float _hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        //_slider = GameObject.Find("Gauge2").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _hp;
    }

    public void SetHP(float hp)
    {
        _hp -= hp;
    }

    public float GetHP()
    {
        return _hp;
    }
}
