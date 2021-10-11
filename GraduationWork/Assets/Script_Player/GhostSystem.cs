using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSystem : MonoBehaviour
{
    [SerializeField]
    private bool isGhost = false; //óHëÃÇ©Ç«Ç§Ç©
    [SerializeField]
    private bool isReturn = false; //ñﬂÇÍÇÈÇ©Ç«Ç§Ç©

    [SerializeField]
    private GameObject deadBody;
    [SerializeField]
    private GameObject body;

    // Update is called once per frame
    void Update()
    {
        TurnGhost();

        if (Input.GetKeyDown(KeyCode.E) && isReturn && isGhost)
            ReturnToBody();
    }

    void TurnGhost()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isGhost)
        {
            isGhost = true;
            Instantiate(
                deadBody,
                transform.position,
                deadBody.gameObject.transform.rotation
                );
        }
    }

    void ReturnToBody()
    {
        isGhost = false;
        Destroy(body);
        isReturn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeadBody")
        {
            isReturn = true;
            body = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DeadBody")
        {
            isReturn = false;
            body = null;
        }
    }

    //óHëÃÉtÉâÉOÇÃéÊìæ
    public bool GetIsGhost()
    {
        return isGhost;
    }

    //óHëÃÉtÉâÉOÇÃê›íË
    public void SetIsGhost(bool flag)
    {
        isGhost = flag;
    }
}
