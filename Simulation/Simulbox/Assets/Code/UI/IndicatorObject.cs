using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IndicatorObject : MonoBehaviour
{
    // [SerializeField] private Texture2D fireCursor;
    
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    
    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        HandleVoxelTypeChanged(VoxelType.Grass);
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
                throw new ArgumentOutOfRangeException(nameof(voxelType), voxelType, null);
            case VoxelType.Grass:
                _meshFilter.mesh = VoxelManager.Instance.IndicatorMeshes[(int)VoxelType.Grass];
                _meshRenderer.material = SceneTools.Instance.voxelMaterials[(int)VoxelType.Grass];
                // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                break;
            case VoxelType.Sand:
                // TODO - merge scene tools and voxel manager?
                _meshFilter.mesh = VoxelManager.Instance.IndicatorMeshes[(int)VoxelType.Sand];
                _meshRenderer.material = SceneTools.Instance.voxelMaterials[(int)VoxelType.Sand];
                // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                break;
            case VoxelType.Wood:
                _meshFilter.mesh = VoxelManager.Instance.IndicatorMeshes[(int)VoxelType.Wood];
                _meshRenderer.material = SceneTools.Instance.voxelMaterials[(int)VoxelType.Wood];
                // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                break;
            case VoxelType.Water:
                _meshFilter.mesh = VoxelManager.Instance.IndicatorMeshes[(int)VoxelType.Water];
                _meshRenderer.material = SceneTools.Instance.voxelMaterials[(int)VoxelType.Water];
                // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                break;
            case VoxelType.Fire:
                _meshFilter.mesh = null;
                // Cursor.SetCursor(fireCursor, Vector2.zero, CursorMode.Auto);
                break;
        }
    }
}
