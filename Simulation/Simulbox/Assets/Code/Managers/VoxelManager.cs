using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    // public void SpawnVoxelGroup(Vector3 spawnPoint, VoxelType voxelToSpawn)
    // {
    //     var voxel = Instantiate(voxelPrefab, spawnPoint, Quaternion.identity);
    //     var voxelComponent = voxel.GetComponent<Voxel>();
    //     voxelComponent.Init(voxelToSpawn);
    // }
    
}
