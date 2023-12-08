using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IndicatorObject : MonoBehaviour
{
    private VoxelManager _voxelManager;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private InteractMenu _interactMenu;
    private VoxelType _oldVoxelType = VoxelType.Grass;
    private bool _singleVoxel;
    
    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _interactMenu = InteractMenu.Instance;
        _voxelManager = VoxelManager.Instance;
        _voxelManager.OnVoxelTypeChanged += HandleVoxelTypeChanged;
        _interactMenu.OnPaintModeChanged += SetDrawMode;
        HandleVoxelTypeChanged(_voxelManager.CurrentVoxelType);
    }

    private void OnDestroy()
    {
        _voxelManager.OnVoxelTypeChanged -= HandleVoxelTypeChanged;
        _interactMenu.OnPaintModeChanged -= SetDrawMode;
    }

    private void SetDrawMode(bool singleVoxel)
    {
        _singleVoxel = singleVoxel;
        HandleVoxelTypeChanged(_oldVoxelType);
    }
    
    private void HandleVoxelTypeChanged(VoxelType voxelType)
    {
        switch (voxelType)
        {
            case VoxelType.Base:
                default:
                throw new ArgumentOutOfRangeException(nameof(voxelType), voxelType, null);
            case VoxelType.Grass:
                _meshFilter.mesh = _singleVoxel ? _voxelManager.IndicatorSingleMeshes[(int)VoxelType.Grass] : _voxelManager.IndicatorGroupMeshes[(int)VoxelType.Grass];
                _meshRenderer.material = SceneTools.Instance.voxelMaterials[(int)VoxelType.Grass];
                break;
            case VoxelType.Sand:
                _meshFilter.mesh = _singleVoxel ? _voxelManager.IndicatorSingleMeshes[(int)VoxelType.Sand] : _voxelManager.IndicatorGroupMeshes[(int)VoxelType.Sand];
                _meshRenderer.material = SceneTools.Instance.voxelMaterials[(int)VoxelType.Sand];
                break;
            case VoxelType.Wood:
                _meshFilter.mesh = _singleVoxel ? _voxelManager.IndicatorSingleMeshes[(int)VoxelType.Wood] : _voxelManager.IndicatorGroupMeshes[(int)VoxelType.Wood];
                _meshRenderer.material = SceneTools.Instance.voxelMaterials[(int)VoxelType.Wood];
                break;
            case VoxelType.Water:
                _meshFilter.mesh = _singleVoxel ? _voxelManager.IndicatorSingleMeshes[(int)VoxelType.Water] : _voxelManager.IndicatorGroupMeshes[(int)VoxelType.Water];
                _meshRenderer.material = SceneTools.Instance.voxelMaterials[(int)VoxelType.Water];
                break;
            case VoxelType.Fire:
                _meshFilter.mesh = null;
                break;
        }
        _oldVoxelType = voxelType;
    }
}
