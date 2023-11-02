using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Container : MonoBehaviour
{
    public Vector3 containerPosition;
    
    private MeshData _meshData;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;

    public void Initialize(Material mat, Vector3 position)
    {
        ConfigureComponents();
        containerPosition = position;
        _meshRenderer.material = mat;
    }
    
    private void ConfigureComponents()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
    }

    public void GenerateMesh()
    {
        _meshData.ClearData();

        Vector3 blockPos = new Vector3(8, 8, 8);
        Voxel block = new Voxel{ID = 1};

        int counter = 0;
        Vector3[] faceVertices = new Vector3[4];
        Vector2[] faceUVs = new Vector2[4];
        
        // Iterate through all 6 faces of the cube
        for (int i = 0; i < 6; i++)
        {
            // Draw this face
            
            // Collect the vertices of this face from the default set and add block position
            for (int j = 0; j < 4; j++)
            {
                faceVertices[j] = VoxelVertices[VoxelVertexIndex[i, j]] + blockPos;
                faceUVs[j] = VoxelUVs[j];
            }
            // Iterate through the 6 vertices of this face (2 triangles)
            for (int j = 0; j < 6; j++)
            {
                _meshData.Vertices.Add(faceVertices[VoxelTris[i, j]]);
                _meshData.UVs.Add(faceUVs[VoxelTris[i, j]]);
                _meshData.Triangles.Add(counter++);
            }
        }
    }

    public void UploadMesh()
    {
        _meshData.UploadMesh();
        
        if (_meshRenderer == null)
            ConfigureComponents();
        
        _meshFilter.mesh = _meshData.Mesh;
        
        if (_meshData.Vertices.Count > 3)
            _meshCollider.sharedMesh = _meshData.Mesh;
    }
    
    #region Mesh Data
    public struct MeshData
    {
        public Mesh Mesh;
        public List<Vector3> Vertices;
        public List<int> Triangles;
        public List<Vector2> UVs;

        public bool Initialized;
        
        public void ClearData()
        {
            if (!Initialized)
            {
                Vertices = new List<Vector3>();
                Triangles = new List<int>();
                UVs = new List<Vector2>();
                
                Initialized = true;
                Mesh = new Mesh();
            }
            else
            {
                Vertices.Clear();
                Triangles.Clear();
                UVs.Clear();
                Mesh.Clear();
            }
        }
        public void UploadMesh(bool sharedVertices = false)
        {
            Mesh.SetVertices(Vertices);
            Mesh.SetTriangles(Triangles, 0, false);
            Mesh.SetUVs(0, UVs);
            
            Mesh.Optimize();
            Mesh.RecalculateNormals();
            Mesh.RecalculateBounds();
            Mesh.UploadMeshData(false);
        }
    }
    #endregion
    
    #region Voxel Statics
    private static readonly Vector3[] VoxelVertices = new Vector3[8]
    {
        new (0, 0, 0),
        new (1, 0, 0),
        new (0, 1, 0),
        new (1, 1, 0),
        
        new (0, 0, 1),
        new (1, 0, 1),
        new (0, 1, 1),
        new (1, 1, 1)

    };
    private static readonly int[,] VoxelVertexIndex = new int[6, 4]
    {
        { 0, 1, 2, 3 },
        { 4, 5, 6, 7 },
        { 4, 0, 6, 2 },
        { 5, 1, 3, 7 },
        { 0, 1, 4, 5 },
        { 2, 3, 6, 7 }
    };
    private static readonly Vector2[] VoxelUVs = new Vector2[4]
    {
        new (0, 0),
        new (0, 1),
        new (1, 0),
        new (1, 1)
    };
    private static readonly int[,] VoxelTris = new int[6, 6]
    {
        { 0, 2, 3, 0, 3, 1 },
        { 0, 1, 2, 1, 3, 2 },
        { 0, 2, 3, 0, 3, 1 },
        { 0, 1, 2, 1, 3, 2 },
        { 0, 1, 2, 1, 3, 2 },
        { 0, 2, 3, 0, 3, 1 }
    };
    #endregion



}
