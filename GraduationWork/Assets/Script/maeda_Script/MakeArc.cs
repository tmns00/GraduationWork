using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MakeArc : MonoBehaviour
{
    private float areaAngle; //作成する角度
    private float startAngle = 90;  //スタート地点の角度
    public int quality = 100;   //360degのときのtriangle数
    public Color color = new Color(1, 0, 0, 0.5f); //RGBA
    private Vector3 scale;  //大きさ

    private Vector3[] vertices; //頂点
    private int[] triangles;    //index

    private void makeParams()
    {
        List<Vector3> vertList = new List<Vector3>();
        List<Vector3> vertList2 = new List<Vector3>();

        List<int> triList = new List<int>();

        vertList.Add(new Vector3(0, 0, 0));  //原点

        float th, v1, v2;
        float max = quality * areaAngle / 360;
        for (int i = 0; i <= max; i++)
        {
            th = i * areaAngle / max + startAngle;
            v1 = Mathf.Sin(th * Mathf.Deg2Rad);
            v2 = Mathf.Cos(th * Mathf.Deg2Rad);
            vertList.Add(new Vector3(v1, 0, v2));
            if (i <= max - 1)
            {
                triList.Add(0); triList.Add(i + 1); triList.Add(i + 2);
            }
        }
        vertices = vertList.ToArray();
        triangles = triList.ToArray();


    }

    private void setParams()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // 法線とバウンディングの計算
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        transform.localScale = scale;

        GetComponent<MeshFilter>().sharedMesh = mesh;
        //GetComponent<MeshCollider>().sharedMesh = mesh;

        // 色指定
        GetComponent<MeshRenderer>().material.color = color;
    }

    void Start()
    {
        makeParams();
        setParams();
    }
}