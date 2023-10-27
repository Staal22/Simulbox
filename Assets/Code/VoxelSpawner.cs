using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VoxelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject voxelPrefab;
    // [SerializeField] private GameObject sandPrefab;
    // [SerializeField] private GameObject grassPrefab;
    // [SerializeField] private GameObject waterPrefab;
    // private VoxelType _voxelToSpawn;
    
    // private void Start()
    // {
    //     VoxelManager.Instance.OnVoxelTypeChanged += SetType;
    // }
    //
    // private void SetType(VoxelType newType)
    // {
    //     _voxelToSpawn = newType;
    // }
    
    public GameObject SpawnVoxel(Vector3 spawnPoint, VoxelType voxelToSpawn)
    {
        // Debug.Log("Spawning voxel");
        // switch (voxelToSpawn)
        // {
        //     default:
        //         break;
        //     case VoxelType.Sand:
        //         _voxel = Instantiate(sandPrefab, spawnPoint, Quaternion.identity);
        //         break;
        //     case VoxelType.Grass:
        //         _voxel = Instantiate(grassPrefab, spawnPoint, Quaternion.identity);
        //         break;
        //     case VoxelType.Water:
        //         _voxel = Instantiate(waterPrefab, spawnPoint, Quaternion.identity);
        //         break;
        // }
        var voxel = Instantiate(voxelPrefab, spawnPoint, Quaternion.identity);
        var voxelComponent = voxel.GetComponent<Voxel>();
        voxelComponent.Init(voxelToSpawn);
        return voxel;
    }
}

