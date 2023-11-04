using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVoxelGroup : ICommand
{
    private List<GameObject> _voxelGroup;
    private readonly VoxelType _voxelType;
    private readonly Vector3 _spawnPoint;
    
    public AddVoxelGroup(Vector3 spawnPoint)
    {
        _spawnPoint = spawnPoint;
        _voxelType = VoxelManager.Instance.CurrentVoxelType;
    }

    public void Execute()
    {
        _voxelGroup = VoxelManager.Instance.SpawnVoxelGroup(_spawnPoint, _voxelType);
    }

    public void Undo()
    {
        Debug.Log("Undoing spawn voxel group");
        foreach (var gameObject in _voxelGroup) Object.Destroy(gameObject);
    }
}
