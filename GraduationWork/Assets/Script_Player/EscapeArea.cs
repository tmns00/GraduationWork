using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeArea : MonoBehaviour
{
    [SerializeField]
    private Treasure treasure;
    [SerializeField]
    private GameSystem4 sceneChange;

    private void OnTriggerEnter(Collider other)
    {
        //����擾��ԂŃv���C���[���N��
        if(other.gameObject.tag == "Player" && treasure.IsGetFlag())
        {
            sceneChange.StartGame();
        }
    }
}
