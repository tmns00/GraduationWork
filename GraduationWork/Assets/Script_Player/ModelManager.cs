using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    public Mesh[] models;
    public Texture[] images;

    [SerializeField]
    private GhostSystem ghostSystem;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ghostSystem.GetIsGhost())
        {
            meshFilter.mesh = models[0];
            //meshRenderer.material.SetTexture("_BaseMap", images[0]);
        }
        else if (!ghostSystem.GetIsGhost())
        {
            meshFilter.mesh = models[1];
            //meshRenderer.material.SetTexture("_BaseMap", images[1]);
        }
    }
}
