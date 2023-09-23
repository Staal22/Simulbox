using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VoxelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sandPrefab;
    [SerializeField] private GameObject grassPrefab;
    [SerializeField] private GameObject waterPrefab;
    private VoxelType _voxelToSpawn;
    
    private void OnEnable()
    {
        VoxelManager.Instance.OnVoxelTypeChanged += SetType;
    }

    private void OnDisable()
    {
        VoxelManager.Instance.OnVoxelTypeChanged -= SetType;
    }

    private void SetType(VoxelType newType)
    {
        _voxelToSpawn = newType;
    }
    
    private void SpawnVoxel(VoxelType selectedType)
    {
        switch (selectedType)
        {
            default:
                break;
            case VoxelType.Sand:
                Instantiate(sandPrefab, transform.position, Quaternion.identity);
                break;
            case VoxelType.Water:
                Instantiate(waterPrefab, transform.position, Quaternion.identity);
                break;
            case VoxelType.Grass:
                Instantiate(waterPrefab, transform.position, Quaternion.identity);
                break;
            
            
        }
    }
}

