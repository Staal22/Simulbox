using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IndicatorObject : MonoBehaviour
{
    [field: SerializeField] private Mesh[] outlineMeshes;

    private MeshFilter _meshFilter;
    
    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        VoxelManager.Instance.OnVoxelTypeChanged += HandleVoxelTypeChanged;
    }

    private void OnDestroy()
    {
        VoxelManager.Instance.OnVoxelTypeChanged -= HandleVoxelTypeChanged;
    }

    private void HandleVoxelTypeChanged(VoxelType voxelType)
    {
        switch (voxelType)
        {
            case VoxelType.Base:
                default:
                throw new System.ArgumentOutOfRangeException(nameof(voxelType), voxelType, null);
            case VoxelType.Grass:
                _meshFilter.mesh = outlineMeshes[(int)VoxelType.Grass];
                break;
            case VoxelType.Sand:
                _meshFilter.mesh = outlineMeshes[(int)VoxelType.Sand];
                break;
            case VoxelType.Wood:
                break;
            case VoxelType.Water:
                break;
        }
    }
}
