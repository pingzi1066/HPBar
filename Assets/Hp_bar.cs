using UnityEngine;
using System.Collections;

public class Hp_bar : MonoBehaviour
{
    private Mesh mesh;
    private Vector2[] originUV = new Vector2[4];
    private Vector3[] originVertices;
    private float edgeUVX = 0.03f;
    private float maxUVX = .515f;

    public Vector2 size = new Vector2(0.2f, 0.01f);
    public float edgeX = 0.1f;

    public float barUVH = 0.25f; //0.75蓝色0.5灰色0.25绿色0红色

    public int max = 100;
    public int cur = 15;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.RecalculateNormals();

        float left = -size.x - edgeX;
        float right = size.x + edgeX;

        //顶点坐标
        originVertices = new Vector3[]
		{
			new Vector3(left, size.y, 0) * 0.5f,
            new Vector3(-size.x, size.y, 0) * 0.5f, 
            new Vector3(size.x, size.y, 0) * 0.5f, 
            new Vector3(right, size.y, 0) * 0.5f,
            new Vector3(right, size.y, 0) * 0.5f,

            new Vector3(left, -size.y, 0) * 0.5f,
			new Vector3(-size.x, -size.y, 0) * 0.5f, 
            new Vector3(size.x, -size.y, 0) * 0.5f, 
            new Vector3(right, -size.y, 0) * 0.5f,
            new Vector3(right, -size.y, 0) * 0.5f
		};

        //UV分布坐标
        originUV = new Vector2[]
		{
			new Vector2(0, barUVH), 
            new Vector2(edgeUVX, barUVH),
            new Vector2(maxUVX - edgeUVX, barUVH),
            new Vector2(maxUVX, barUVH),
            new Vector2(maxUVX + .1f, barUVH),


			new Vector2(0, barUVH - .25f), 
            new Vector2(edgeUVX, barUVH - .25f),
            new Vector2(maxUVX - edgeUVX, barUVH - .25f),
            new Vector2(maxUVX,barUVH - .25f),
            new Vector2(maxUVX + .1f, barUVH - .25f)
		};

        mesh.vertices = originVertices;
        mesh.uv = originUV;

        //三角片连接
        mesh.triangles = new int[]
        {
            0, 1, 5,        5, 1, 6,
            1, 2, 6,        6, 2, 7,
            2, 3 ,7,        7, 3, 8,
            3, 4, 8,        8, 4, 9
        };
    }

    void UpdateVertices(int _hp, int _maxhp)
    {
        _hp = Mathf.Clamp(_hp, 0, _maxhp);
        float x = (((float)_hp / (float)_maxhp) * 2 - 1) * size.x;

        originVertices[2] = new Vector3(x, size.y, 0) * 0.5f;
        originVertices[7] = new Vector3(x, -size.y, 0) * 0.5f;

        if (_hp > 0)
        {
            originVertices[1] = new Vector3(-size.x, size.y, 0) * 0.5f;
            originVertices[3] = new Vector3(x + edgeX, size.y, 0) * 0.5f;
            originVertices[6] = new Vector3(-size.x, -size.y, 0) * .5f;
            originVertices[8] = new Vector3(x + edgeX, -size.y, 0) * 0.5f;

            originVertices[2] = new Vector3(x, size.y, 0) * 0.5f;
            originVertices[7] = new Vector3(x, -size.y, 0) * 0.5f;
        }
        else
        {
            float left = -size.x - edgeX;
            originVertices[1] = new Vector3(left, size.y, 0) * 0.5f;
            originVertices[3] = new Vector3(left, size.y, 0) * 0.5f;
            originVertices[6] = new Vector3(left, -size.y, 0) * .5f;
            originVertices[8] = new Vector3(left, -size.y, 0) * 0.5f;

            originVertices[2] = new Vector3(left, size.y, 0) * 0.5f;
            originVertices[7] = new Vector3(left, -size.y, 0) * 0.5f;
        }

        mesh.vertices = originVertices;
    }

    void Update()
    {
        UpdateVertices(cur, max);
    }
}
