using UnityEngine;
using System;

public class InteractMenu : MonoBehaviour
{
    private Action<VoxelType> _onNewVoxelTypeSelected;
    private VoxelType _selectedVoxelType = VoxelType.Sand;
    
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
}