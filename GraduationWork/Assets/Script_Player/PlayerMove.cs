using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;

    private float moveVert;
    private float moveHol;

    private Vector3 playerPos;

    void Start()
    {
        playerPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

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
}
