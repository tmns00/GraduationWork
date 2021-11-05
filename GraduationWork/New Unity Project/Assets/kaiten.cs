using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaiten : MonoBehaviour
{

    public Vector3 gizmoSize =new Vector3(0.3f,0.3f,0.3f);
    public Color gizmoColor = Color.yellow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, gizmoSize);
    }
}
