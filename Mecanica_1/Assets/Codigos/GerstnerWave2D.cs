using System.Collections.Generic;
using UnityEngine;

public class GerstnerWave2D : MonoBehaviour
{
    public int meshPoints;
    public Vector2 rangeX;
    public Vector2 rangeZ;
    public Material[] material;

    public float amplitude, steepness, waveNumber;
    public Vector2 direction;
    float time;

    private float Lx, Lz;

    private Mesh mesh0, mesh1;
    private Vector3[] vertices;
    private int[] triangles;

    private List<GameObject> side;


    void Start()
    {
        InitSidesProperties();
        CreateMesh();
    }

    void FixedUpdate()
    {
        CreateMesh();
        time += Time.fixedDeltaTime;
    }

    Vector3 Function2D(float x, float z)
    {
        float A = amplitude;
        float k = waveNumber;
        float w = Mathf.Sqrt(9.81f * k);
        float Q = steepness;
        Vector2 D = direction.normalized;


        Vector2 p = new Vector2(x, z);
        float arg = k * Vector2.Dot(D, p) - w * time ;

        float xComp = x - Q * A * D.x * Mathf.Cos(arg);
        float yComp = -A * Mathf.Sin(arg);
        float zComp = z - Q * A * D.y * Mathf.Cos(arg);

        return new Vector3(xComp, yComp, zComp) ;
    }

    // No mires el c digo :u
    void CreateMesh()
    {
        vertices = new Vector3[(meshPoints + 1) * (meshPoints + 1)];

        int n = 0;
        float deltaX = Lx / (float)meshPoints;
        float deltaZ = Lz / (float)meshPoints;
        for (int j = 0; j <= meshPoints; j++)
        {
            for (int i = 0; i <= meshPoints; i++)
            {
                float x = i * deltaX + rangeX.x;
                float z = j * deltaZ + rangeZ.x;

                Vector3 vertex = Function2D(x, z);

                vertices[n] = vertex;
                n++;
            }
        }

        triangles = new int[meshPoints * meshPoints * 6];
        int vert = 0;
        int tris = 0;
        for (int j = 0; j < meshPoints; j++)
        {
            for (int i = 0; i < meshPoints; i++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + meshPoints + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + meshPoints + 1;
                triangles[tris + 5] = vert + meshPoints + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }

        mesh0.Clear();
        mesh0.vertices = vertices;
        mesh0.triangles = triangles;
        mesh0.RecalculateNormals();
        mesh0.RecalculateTangents();

        System.Array.Reverse(triangles);
        Vector3[] invertedNormals = new Vector3[mesh0.normals.Length];
        Vector4[] tangents = mesh0.tangents;
        Vector4[] invertedTangents = new Vector4[tangents.Length];
        for (int i = 0; i < invertedNormals.Length; i++)
        {
            invertedNormals[i] = -mesh0.normals[i];
            invertedTangents[i] = tangents[i];
            invertedTangents[i].w = -invertedTangents[i].w;
        }

        mesh1.Clear();
        mesh1.vertices = vertices;
        mesh1.triangles = triangles;
        mesh1.normals = invertedNormals;
        mesh1.tangents = invertedTangents;
    }

    private void InitSidesProperties()
    {
        side = new List<GameObject>();

        side.Add(transform.GetChild(0).gameObject);
        side.Add(transform.GetChild(1).gameObject);

        side[0].AddComponent<MeshFilter>();
        side[1].AddComponent<MeshFilter>();

        side[0].AddComponent<MeshRenderer>();
        side[1].AddComponent<MeshRenderer>();

        side[0].GetComponent<MeshRenderer>().material = material[0];
        side[1].GetComponent<MeshRenderer>().material = material[1];

        mesh0 = new Mesh();
        mesh1 = new Mesh();

        side[0].GetComponent<MeshFilter>().mesh = mesh0;
        side[1].GetComponent<MeshFilter>().mesh = mesh1;
        Lx = rangeX.y - rangeX.x;
        Lz = rangeZ.y - rangeZ.x;



    }

}
