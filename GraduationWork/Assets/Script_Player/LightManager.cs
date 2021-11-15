using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private Light playerLight;
    [SerializeField]
    private GhostSystem ghostSystem;

    // Start is called before the first frame update
    void Start()
    {
        playerLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!ghostSystem.GetIsGhost())
        {
            playerLight.type = LightType.Spot;
        }
        else if(ghostSystem.GetIsGhost())
        {
            playerLight.type = LightType.Directional;
        }
    }
}
