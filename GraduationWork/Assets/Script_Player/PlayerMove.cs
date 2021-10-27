using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    private float moveSpeed;

    private float moveVert;
    private float moveHol;

    private Vector3 playerPos;

    [SerializeField]
    GhostSystem ghostSystem;

    void Start()
    {
        playerPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DecideSpeed();

        MoveInput();

        transform.position += new Vector3(moveHol, 0.0f, moveVert);

        Vector3 diff = transform.position - playerPos;

        if (diff.magnitude > 0.01f)
            transform.rotation = Quaternion.LookRotation(diff);

        playerPos = transform.position;
    }

    private void MoveInput()
    {
        moveVert = Input.GetAxis("Vertical") * moveSpeed;
        moveHol = Input.GetAxis("Horizontal") * moveSpeed;
    }

    private void DecideSpeed()
    {
        if (ghostSystem.GetIsGhost())
            moveSpeed = 0.5f;
        else
            moveSpeed = speed;
    }
}
