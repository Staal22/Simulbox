using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVoxelGroup : ICommand
{
    private GameObject _voxelGroup;
    private VoxelSpawner _voxelSpawner;
    private VoxelType _voxelType;
    private Vector3 _spawnPoint;
    
    public AddVoxelGroup(VoxelSpawner voxelSpawner, Vector3 spawnPoint)
    {
        _voxelSpawner = voxelSpawner;
        _spawnPoint = spawnPoint;
        _voxelType = VoxelManager.Instance.CurrentVoxelType;
    }

    public void Execute()
    {
        _voxelGroup = WorldManager.Instance.SpawnVoxelChunk(_spawnPoint, _voxelType);
    }

    public void Undo()
    {
        Debug.Log("Undoing spawn voxel group");
        Object.Destroy(_voxelGroup);
    }
}
