using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAttack : MonoBehaviour
{
    public enum ItemType
    {
        None,
        Battery,
    }

    [SerializeField]
    private GhostSystem ghostSystem;

    [SerializeField]
    private GameObject attackArea;

    [SerializeField]
    private bool isPick = false;
    [SerializeField]
    private GameObject pickItem;
    [SerializeField]
    private ItemType currentHoldItem;
    [SerializeField]
    private GameObject[] items;
    [SerializeField]
    private Item itemScript;

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

        if (currentHoldItem != ItemType.None)
        {
            Instantiate(
                items[(int)currentHoldItem - 1],
                transform.position,
                Quaternion.identity
                );

        }

        itemScript = pickItem.GetComponent<Item>();
        currentHoldItem = (ItemType)itemScript.GetTypeInt();
        Destroy(pickItem);
        isPick = false;
        pickItem = null;
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
        {
            isPick = true;
            pickItem = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            isPick = false;
            pickItem = other.gameObject;
        }
    }
}
