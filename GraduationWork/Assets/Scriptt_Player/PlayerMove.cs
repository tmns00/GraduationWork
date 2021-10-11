using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;

    private float moveVert;
    private float moveHol;

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveInput();
        gameObject.transform.position += new Vector3(moveVert, 0.0f, moveHol);
    }

    private void MoveInput()
    {
        moveVert = Input.GetAxis("Vertical") * moveSpeed;
        moveHol = Input.GetAxis("Horizontal") * moveSpeed;
    }
}
