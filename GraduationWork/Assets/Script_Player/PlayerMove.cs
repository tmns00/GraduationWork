using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    private float moveSpeed;

    private float moveVert;
    private float moveHol;
    private Vector3 velocity;

    private Vector3 playerPos;

    [SerializeField]
    GhostSystem ghostSystem;

    void Start()
    {
        playerPos = transform.position;

        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = new Vector3(moveHol, 0.0f, moveVert);

        if (ghostSystem.GetResetFlag())
        {
            velocity = Vector3.zero;
            ghostSystem.SetResetFlag(false);
        }

        if (!ghostSystem.GetMoveFlag())
            velocity = Vector3.zero;

        transform.position += velocity;

        DecideSpeed();

        MoveInput();

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
            moveSpeed = 0.25f;
        else
            moveSpeed = speed;
    }
}
