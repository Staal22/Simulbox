using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (_voxelGroup == null) return;
        foreach (var gameObject in _voxelGroup.Where(gameObject => gameObject != null))
        {
            Object.Destroy(gameObject);
        }
    }
}
