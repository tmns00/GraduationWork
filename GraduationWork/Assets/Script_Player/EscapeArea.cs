using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeArea : MonoBehaviour
{
    
    [SerializeField]
    private SceneSystem sceneSystem;

    void Start()
    {
        sceneSystem = GameObject.Find("GameManager").GetComponent<SceneSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //����擾��ԂŃv���C���[���N��
        if(other.gameObject.tag == "Player" && RoundTripManager.GetIsBackWay())
        {
            sceneSystem.sceneName = "Clear";
            sceneSystem.SceneChange();
        }
    }
}
