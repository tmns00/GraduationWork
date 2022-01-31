using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private GhostSystem ghostSystem;

    private bool isAreaMove = false;

    // Start is called before the first frame update
    void Start()
    {
        ghostSystem = player.GetComponent<GhostSystem>();
        isAreaMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAreaMove)
            return;

        if (ghostSystem.GetIsGhost())
        {
            transform.position = player.transform.position + new Vector3(0.0f, 12.0f, -6.0f);
            transform.LookAt(player.transform);
        }
        else if (!ghostSystem.GetIsGhost())
        {
            transform.position = player.transform.position + new Vector3(0.0f, 10.0f, -5.0f);
            transform.LookAt(player.transform);
        }
    }

    public void SetMoveFlag(bool flag)
    {
        isAreaMove = flag;
    }
}
