using UnityEngine;
using System;

public class InteractMenu : MonoBehaviour
{
    public static InteractMenu Instance;
    
    public Action<bool> OnPaintModeChanged;
    private Action<VoxelType> _onNewVoxelTypeSelected;
    private VoxelType _selectedVoxelType = VoxelType.Sand;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetSelectedVoxelType(_selectedVoxelType);
        _onNewVoxelTypeSelected += VoxelManager.Instance.SetCurrentVoxelType;
    }

    public void SetSelectedVoxelType(VoxelType newType)
    {
        _selectedVoxelType = newType;
        _onNewVoxelTypeSelected?.Invoke(_selectedVoxelType);
    }

    public void ResetSimulation()
    {
        VoxelManager.Instance.ClearVoxels();
    }
    
    public void SetPaintMode(bool paintMode)
    {
        OnPaintModeChanged?.Invoke(paintMode);
    }
}