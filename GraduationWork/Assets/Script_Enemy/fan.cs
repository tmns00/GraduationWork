using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fan : MonoBehaviour
{
    //扇形のギズモを作る
    public GameObject CreateGizmo(GameObject parent, Vector3 loc, Vector3 rot, Material mat)
    {
        GameObject g = Instantiate(new GameObject(), loc + parent.transform.position, Quaternion.Euler(rot)) as GameObject;
        g.transform.parent = parent.transform;
        g.AddComponent<MeshRenderer>();
        g.AddComponent<MeshFilter>();
        g.GetComponent<MeshRenderer>().material = mat;
        return g;
    }

    //ギズモを指定した角度、長さに変形する
    public void RefreshGizumo(ref GameObject g,GameObject parent,float angle,float range)
    {
        var mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        float x, y;
        int i = 0;
        vertices.Add(new Vector3(0, 0, 0));

        for(float d=-angle/2f;d<=angle/2f;d++)
        {
            x = Mathf.Sin(d * Mathf.Deg2Rad) * range;
            y = Mathf.Cos(d * Mathf.Deg2Rad) * range;
            vertices.Add(new Vector3(x, 0, y));
            triangles.AddRange(new int[] { 0, i + 1, i + 2 });
            i++;
        }
        triangles.RemoveRange(triangles.Count - 3, 3);
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);

        mesh.RecalculateNormals();
        g.GetComponent<MeshFilter>().sharedMesh = mesh;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

   
}
