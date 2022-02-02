using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private const string key_isRun = "isRun";

    public GhostSystem ghostSystem;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ghostSystem.GetIsGhost())
            return;

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            animator.SetBool(key_isRun, true);
        else
            animator.SetBool(key_isRun, false);
    }
}
