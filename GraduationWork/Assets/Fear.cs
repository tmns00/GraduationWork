using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fear : MonoBehaviour
{
    Slider fear;
    public Image slidercolor;
    public float Fearvalue;
    public int FearLevel;
    // Start is called before the first frame update
    void Start()
    {
        fear = GameObject.Find("Fear").GetComponent<Slider>();
        Fearvalue = 0;
        FearLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        fear.value = Fearvalue;
        if (Fearvalue >= 0&&Fearvalue<40)
        {
            FearLevel = 0;
            slidercolor.color = new Color32(0, 0, 255, 255);
        }
        if (Fearvalue >= 40 && Fearvalue < 80)
        {
            FearLevel = 1;
            slidercolor.color = new Color32(125, 125, 0, 255);
        }
        if (Fearvalue >= 80)
        {
            FearLevel = 2;
            slidercolor.color = new Color32(255, 0, 0, 255);
        }
       
        
    }
    
}

