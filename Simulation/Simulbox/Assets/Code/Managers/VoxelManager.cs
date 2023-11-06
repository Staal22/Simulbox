using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class VoxelManager : MonoBehaviour
{
    public static VoxelManager Instance;
    
    [SerializeField] private GameObject voxelPrefab;
    [SerializeField] private GameObject fireEffectPrefab;
    
    public Mesh[] indicatorMeshes;
    
    public VoxelType CurrentVoxelType { get; private set; } = VoxelType.Grass;
    public Action<VoxelType> OnVoxelTypeChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        indicatorMeshes = new Mesh[Enum.GetNames(typeof(VoxelType)).Length];
        for (var i = 1; i < indicatorMeshes.Length; i++)
        {
            indicatorMeshes[i] = GetIndicatorMesh((VoxelType) i);
        }
    }

    public void SetCurrentVoxelType(VoxelType newType)
    {
        CurrentVoxelType = newType;
        OnVoxelTypeChanged?.Invoke(newType);
    }

    public GameObject SpawnVoxel(Vector3 spawnPoint, VoxelType voxelToSpawn)
    {
        var voxel = Instantiate(voxelPrefab, spawnPoint, Quaternion.identity);
        var voxelComponent = voxel.GetComponent<Voxel>();
        voxelComponent.Init(voxelToSpawn);
        return voxel;
    }
    
    public List<GameObject> SpawnVoxelGroup(Vector3 spawnPoint, VoxelType voxelToSpawn)
    {
        var voxelGroup = new List<GameObject>();
        switch (voxelToSpawn)
        {
            case VoxelType.Base:
            default:
                throw new ArgumentOutOfRangeException(nameof(voxelToSpawn), voxelToSpawn, null);
            case VoxelType.Grass:
                // Random height box
                for (var x = spawnPoint.x -2.5f; x < spawnPoint.x + 2.5f; x++)
                {
                    for (var z = spawnPoint.z -2.5f; z < spawnPoint.z + 2.5f; z++)
                    {
                        var height = Random.Range(1, 5);
                        for (var y = spawnPoint.y + 1f; y < spawnPoint.y + 1f + height; y++)
                        {
                            var voxel = Instantiate(voxelPrefab, new Vector3(x, y, z), Quaternion.identity);
                            var voxelComponent = voxel.GetComponent<Voxel>();
                            voxelComponent.Init(voxelToSpawn);
                            voxelGroup.Add(voxel);
                        }
                    }
                }
                break;
            case VoxelType.Sand:
                // Uniform sphere
                for (var x = spawnPoint.x -2.5f; x < spawnPoint.x + 2.5f; x++)
                {
                    for (var z = spawnPoint.z -2.5f; z < spawnPoint.z + 2.5f; z++)
                    {
                        for (var y = spawnPoint.y -2.5f; y < spawnPoint.y + 2.5f; y++)
                        {
                            if (Vector3.Distance(new Vector3(x, y, z), spawnPoint) < 2.5f)
                            {
                                var voxel = Instantiate(voxelPrefab, new Vector3(x, y, z), Quaternion.identity);
                                var voxelComponent = voxel.GetComponent<Voxel>();
                                voxelComponent.Init(voxelToSpawn);
                                voxelGroup.Add(voxel);
                            }
                        }
                    }
                }
                break;
            case VoxelType.Wood:
                break;
            case VoxelType.Water:
                break;
            case VoxelType.Fire:
                Instantiate(fireEffectPrefab, spawnPoint, Quaternion.identity);
                break;
        }
        
        return voxelGroup;
    }

    private Mesh GetIndicatorMesh(VoxelType voxelType)
    {
         var voxelGroup = SpawnVoxelGroup(Vector3.zero, voxelType);

        var count = voxelGroup.Count;
        // Create CombineInstance from the amount of voxels
        var cInstance = new CombineInstance[count];
        
        // Initialize CombineInstance from MeshFilter of each voxel
        for (int i = 0; i < count; i++)
        {
            // Get current Mesh Filter and initialize each CombineInstance 
            MeshFilter cFilter = voxelGroup[i].GetComponent<MeshFilter>();

            // Get each Mesh and position
            cInstance[i].mesh = cFilter.sharedMesh;
            cInstance[i].transform = cFilter.transform.localToWorldMatrix;
            // Clean up voxel
            Destroy(cFilter.gameObject);
        }

        // Create combined mesh
        var mesh = new Mesh();
        mesh.CombineMeshes(cInstance);
        
        return mesh;
    }
}
