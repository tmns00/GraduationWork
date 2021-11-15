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
        //‚¨•óæ“¾ó‘Ô‚ÅƒvƒŒƒCƒ„[‚ªN“ü
        if(other.gameObject.tag == "Player" && treasure.IsGetFlag())
        {
            sceneSystem.sceneName = "Clear";
            sceneSystem.SceneChange();
        }
    }
}
