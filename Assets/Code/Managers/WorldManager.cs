using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;
    
    public Material worldMaterial;
    public VoxelColor[] worldColors;
    
    private Container _container;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"Multiple {nameof(WorldManager)}s in scene!");
            Destroy(this);
        }
    }

    public void SpawnVoxelChunk(Vector3 spawnPoint, VoxelType voxelToSpawn)
    {
        var cont = new GameObject("Container")
        {
            transform =
            {
                parent = transform
            }
        };
        _container = cont.AddComponent<Container>();
        _container.Initialize(worldMaterial, Vector3.zero); //TODO initialize container with spawn point

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
                                _container[new Vector3(x, y, z)] = new Voxel{Type = VoxelType.Sand};
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
                            _container[new Vector3(x, y, z)] = new Voxel{Type = VoxelType.Grass};
                        }
                    }
                }
                break;
        }
        
        _container.GenerateMesh();
        _container.UploadMesh();
    }
}
