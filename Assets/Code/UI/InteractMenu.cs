using UnityEngine;
using System;

public class InteractMenu : MonoBehaviour
{
    private static InteractMenu _instance;
    public static InteractMenu Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InteractMenu>();
            }
            return _instance;
        }
    }
    
    public Action<bool> OnPaintModeChanged;
    private Action<VoxelType> _onNewVoxelTypeSelected;
    private VoxelType _selectedVoxelType = VoxelType.Sand;

    private void Awake()
    {
        if (Instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Multiple InteractMenu objects detected: removing this one.");
            Destroy(this);
        }
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