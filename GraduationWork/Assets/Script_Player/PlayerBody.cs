using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField]
    private GhostSystem ghostSystem;
    [SerializeField]
    private Material material;
    private Color bodyColor;

    // Start is called before the first frame update
    void Start()
    {
        bodyColor = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (ghostSystem.GetIsGhost())
            ChangeBody();
    }

    void ChangeBody()
    {
        if (bodyColor.a < 1.0f)
            return;
        bodyColor.a = 0.2f;

        material.color = bodyColor;
    }
}
