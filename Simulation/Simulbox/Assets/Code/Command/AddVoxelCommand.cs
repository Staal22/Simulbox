using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AddVoxelCommand : ICommand
{
    private GameObject _voxel;
    private readonly VoxelType _voxelType;
    private readonly Vector3 _spawnPoint;
    
    public AddVoxelCommand(Vector3 spawnPoint)
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
        Debug.Log("Undoing spawn voxel");
        Object.Destroy(_voxel);
    }
}