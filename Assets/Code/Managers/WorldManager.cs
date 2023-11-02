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
        _container.Initialize(worldMaterial, Vector3.zero);
        
        // loop for 5x5x5 area around spawnPoint

        
        // example
        for (int x = 0; x < 16; x++)
        {
            for (int z = 0; z < 16; z++)
            {
                int height = Random.Range(1, 17);
                for (int y = 0; y < height; y++)
                {
                    _container[new Vector3(x, y, z)] = new Voxel{Type = voxelToSpawn};
                }
            }
        }
        
        _container.GenerateMesh();
        _container.UploadMesh();
    }
}
