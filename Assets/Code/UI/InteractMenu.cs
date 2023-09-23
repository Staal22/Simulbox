using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class InteractMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI selectedTypeText;
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
        selectedTypeText.text = newType.ToString(); // Update UI to display the selected type
        _onNewVoxelTypeSelected?.Invoke(_selectedVoxelType);
    }
}