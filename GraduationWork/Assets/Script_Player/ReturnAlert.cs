using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnAlert : MonoBehaviour
{
    public float rate;
    public GameObject panel;

    private float countTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        countTime = 0;
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!RoundTripManager.GetIsBackWay())
            return;

        if (countTime >= rate)
            ChangeActive();

        countTime += Time.deltaTime;   
    }

    void ChangeActive()
    {
        panel.SetActive(!panel.activeSelf);
        countTime = 0;
    }
}
