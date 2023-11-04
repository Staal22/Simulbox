using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVoxelGroup : ICommand
{
    private GameObject _voxelGroup;
    private VoxelType _voxelType;
    private Vector3 _spawnPoint;
    
    public AddVoxelGroup(Vector3 spawnPoint)
    {
        _spawnPoint = spawnPoint;
        _voxelType = VoxelManager.Instance.CurrentVoxelType;
    }

    public void Execute()
    {
        // _voxelGroup = WorldManager.Instance.SpawnVoxelChunk(_spawnPoint, _voxelType);
    }

    public void Undo()
    {
        Debug.Log("Undoing spawn voxel group");
        Object.Destroy(_voxelGroup);
    }
}
