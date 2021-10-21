using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private const float deleteTime = 0.1f;
    private float countTime;

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;

        if (deleteTime <= countTime)
            Delete();
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
