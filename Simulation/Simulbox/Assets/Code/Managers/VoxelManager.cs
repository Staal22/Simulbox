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

    public VoxelType CurrentVoxelType { get; private set; } = VoxelType.Sand;
    public Action<VoxelType> OnVoxelTypeChanged;

    private void Awake()
    {
        Instance = this;
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
            case VoxelType.Sand:
                // uniform sphere
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
            case VoxelType.Water:
                break;
            case VoxelType.Wood:
                break;
            case VoxelType.Grass:
                // random height box
                for (var x = spawnPoint.x -2.5f; x < spawnPoint.x + 2.5f; x++)
                {
                    for (var z = spawnPoint.z -2.5f; z < spawnPoint.z + 2.5f; z++)
                    {
                        var height = Random.Range(1, 5);
                        for (var y = spawnPoint.y; y < spawnPoint.y + height; y++)
                        {
                            var voxel = Instantiate(voxelPrefab, new Vector3(x, y, z), Quaternion.identity);
                            var voxelComponent = voxel.GetComponent<Voxel>();
                            voxelComponent.Init(voxelToSpawn);
                            voxelGroup.Add(voxel);
                        }
                    }
                }
                break;
        }

        return voxelGroup;
    }
    
}
