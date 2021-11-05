using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private GhostSystem ghostSystem;

    // Start is called before the first frame update
    void Start()
    {
        ghostSystem = player.GetComponent<GhostSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ghostSystem.GetIsGhost())
            transform.position = player.transform.position + new Vector3(0.0f, 30.0f, -5.0f);
        else if (!ghostSystem.GetIsGhost())
            transform.position = player.transform.position + new Vector3(0.0f, 10.0f, -1.5f);
    }
}
