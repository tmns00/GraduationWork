using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GhostSystem ghostSystem;

    [SerializeField]
    private GameObject attackArea;

    private bool isPick = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            Action();
    }

    private void Action()
    {
        if (!ghostSystem.GetIsGhost())
            PickUp();
        else if (ghostSystem.GetIsGhost())
            Attack();
    }

    private void PickUp()
    {
        if (!isPick)
            return;


    }

    private void Attack()
    {
        Instantiate(
            attackArea,
            gameObject.transform.position,
            Quaternion.identity
            );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
            isPick = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
            isPick = false;
    }
}
