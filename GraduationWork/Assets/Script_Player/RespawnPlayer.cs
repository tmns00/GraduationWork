using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject resPosition;

    public void Respawn()
    {
        player.transform.position = resPosition.transform.position;
    }
}
