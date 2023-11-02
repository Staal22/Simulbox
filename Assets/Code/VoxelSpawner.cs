using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VoxelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject voxelPrefab;
    
    // public GameObject SpawnVoxel(Vector3 spawnPoint, VoxelType voxelToSpawn)
    // {
    //     var voxel = Instantiate(voxelPrefab, spawnPoint, Quaternion.identity);
    //     var voxelComponent = voxel.GetComponent<Voxel>();
    //     voxelComponent.Init(voxelToSpawn);
    //     return voxel;
    // }
    //
    public void SpawnVoxel(Vector3 spawnPoint, VoxelType voxelToSpawn)
    {
        var voxel = Instantiate(voxelPrefab, spawnPoint, Quaternion.identity);
        var voxelComponent = voxel.GetComponent<OLD_Voxel>();
        voxelComponent.Init(voxelToSpawn);
    }
    
    public void SpawnVoxelGroup(Vector3 spawnPoint, VoxelType voxelToSpawn)
    {
        var voxel = Instantiate(voxelPrefab, spawnPoint, Quaternion.identity);
        var voxelComponent = voxel.GetComponent<OLD_Voxel>();
        voxelComponent.Init(voxelToSpawn);
    }
    
}

