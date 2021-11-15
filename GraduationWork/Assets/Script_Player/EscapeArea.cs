using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeArea : MonoBehaviour
{
    [SerializeField]
    private Treasure treasure;
    [SerializeField]
    private SceneSystem sceneSystem;

    private void OnTriggerEnter(Collider other)
    {
        //お宝取得状態でプレイヤーが侵入
        if(other.gameObject.tag == "Player" && treasure.IsGetFlag())
        {
            sceneSystem.sceneName = "Clear";
            sceneSystem.SceneChange();
        }
    }
}
