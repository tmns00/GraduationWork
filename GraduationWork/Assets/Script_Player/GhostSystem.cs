using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSystem : MonoBehaviour
{
    [SerializeField]
    private bool isGhost = false; //幽体かどうか
    [SerializeField]
    private bool isReturn = false; //戻れるかどうか

    [SerializeField]
    private GameObject deadBody;
    [SerializeField]
    private GameObject body;

    // Update is called once per frame
    void Update()
    {
        TurnGhost();

        if (Input.GetButtonDown("GhostButton") && isReturn && isGhost)
            ReturnToBody();
    }

    void TurnGhost()
    {
        if (Input.GetButtonDown("GhostButton") && !isGhost)
        {
            isGhost = true;
            Instantiate(
                deadBody,
                transform.position,
                deadBody.gameObject.transform.rotation
                );
            gameObject.tag = "Ghost";
        }
    }

    void ReturnToBody()
    {
        isGhost = false;
        Destroy(body);
        isReturn = false;
        gameObject.tag = "Player";
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

    //幽体フラグの取得
    public bool GetIsGhost()
    {
        return isGhost;
    }

    //幽体フラグの設定
    public void SetIsGhost(bool flag)
    {
        isGhost = flag;
    }
}
