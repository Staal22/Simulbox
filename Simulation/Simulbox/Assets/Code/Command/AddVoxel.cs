using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AddVoxel : ICommand
{
    private GameObject _voxel;
    private readonly VoxelType _voxelType;
    private readonly Vector3 _spawnPoint;
    
    public AddVoxel(Vector3 spawnPoint)
    {
        _spawnPoint = spawnPoint;
        _voxelType = VoxelManager.Instance.CurrentVoxelType;
    }

    public void Execute()
    {
        _voxel = VoxelManager.Instance.SpawnVoxel(_spawnPoint, _voxelType);
    }

    public void Undo()
    {
        Object.Destroy(_voxel);
    }
}