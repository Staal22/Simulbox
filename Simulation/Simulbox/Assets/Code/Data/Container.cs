using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Container : MonoBehaviour
{
    // public Vector3 containerPosition;
    
    private Dictionary<Vector3, Voxel> _data;
    private MeshData _meshData;
    
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;

    public void Initialize(Material mat, Vector3 position)
    {
        ConfigureComponents();
        _data = new Dictionary<Vector3, Voxel>();
        // containerPosition = position;
        _meshRenderer.material = mat;
    }

    public void ClearData()
    {
        _data.Clear();
    }

    public void GenerateMesh()
    {
        _meshData.ClearData();
        
        var counter = 0;
        var faceVertices = new Vector3[4];
        var faceUVs = new Vector2[4];

        foreach (var kvp in _data)
        {
            // Only draw solid blocks
            if (kvp.Value.Empty)
                continue;
            
            var blockPos = kvp.Key;
            var block = kvp.Value;
            
            var voxelColor = WorldManager.Instance.worldColors[(int)block.Type - 1];
            var voxelColorAlpha = voxelColor.color;
            voxelColorAlpha.a = 1;
            var voxelMetallicSmoothness = new Vector2(voxelColor.metallic, voxelColor.smoothness);
            
            // Iterate through all 6 faces of the cube
            for (int i = 0; i < 6; i++)
            {
                if (!this[blockPos + VoxelFaceChecks[i]].Empty)
                    continue;
                
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
                    _meshData.UVs2.Add(voxelMetallicSmoothness);
                    _meshData.Colors.Add(voxelColorAlpha);
                    _meshData.Triangles.Add(counter++);
                }
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
    
    public Voxel this[Vector3 index]
    {
        get => _data.TryGetValue(index, out var item) ? item : EmptyVoxel;
        set => _data[index] = value;
    }
    
    private void ConfigureComponents()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
    }
    
    private static readonly Voxel EmptyVoxel = new Voxel{Type = VoxelType.Base};
    
    #region Mesh Data
    private struct MeshData
    {
        public Mesh Mesh;
        public List<Vector3> Vertices;
        public List<int> Triangles;
        public List<Vector2> UVs;
        public List<Vector2> UVs2;
        public List<Color> Colors;

        private bool _initialized;
        
        public void ClearData()
        {
            if (!_initialized)
            {
                Vertices = new List<Vector3>();
                Triangles = new List<int>();
                UVs = new List<Vector2>();
                UVs2 = new List<Vector2>();
                Colors = new List<Color>();
                
                _initialized = true;
                Mesh = new Mesh();
            }
            else
            {
                Vertices.Clear();
                Triangles.Clear();
                UVs.Clear();
                UVs2.Clear();
                Colors.Clear();
                
                Mesh.Clear();
            }
        }
        public void UploadMesh()
        {
            Mesh.SetVertices(Vertices);
            Mesh.SetTriangles(Triangles, 0, false);
            Mesh.SetColors(Colors);
            
            Mesh.SetUVs(0, UVs);
            Mesh.SetUVs(2, UVs2);
            
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
    private static readonly Vector3[] VoxelFaceChecks =
    {
        new (0, 0, -1),
        new (0, 0, 1),
        new (-1, 0, 0),
        new (1, 0, 0),
        new (0, -1, 0),
        new (0, 1, 0)
    };
    private static readonly int[,] VoxelVertexIndex =
    {
        { 0, 1, 2, 3 },
        { 4, 5, 6, 7 },
        { 4, 0, 6, 2 },
        { 5, 1, 7, 3 },
        { 0, 1, 4, 5 },
        { 2, 3, 6, 7 }
    };
    private static readonly Vector2[] VoxelUVs =
    {
        new (0, 0),
        new (0, 1),
        new (1, 0),
        new (1, 1)
    };
    private static readonly int[,] VoxelTris =
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
