using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AddVoxelCommand : ICommand
{
    private GameObject _voxel;
    private VoxelSpawner _voxelSpawner;
    private VoxelType _voxelType;
    private Vector3 _spawnPoint;
    
    public AddVoxelCommand(VoxelSpawner voxelSpawner, Vector3 spawnPoint)
    {
        _voxelSpawner = voxelSpawner;
        _spawnPoint = spawnPoint;
        _voxelType = VoxelManager.Instance.CurrentVoxelType;
    }

    public void Execute()
    {
        // _voxel = _voxelSpawner.SpawnVoxel(_spawnPoint, _voxelType);
    }

    public void Undo()
    {
        Debug.Log("Undoing spawn voxel");
        Object.Destroy(_voxel);
    }
}