using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VoxelManager : MonoBehaviour
{
    public static VoxelManager Instance;
    public VoxelType CurrentVoxelType { get; private set; } = VoxelType.Sand;
    public Action<VoxelType> OnVoxelTypeChanged;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCurrentVoxelType(VoxelType newType)
    {
        CurrentVoxelType = newType;
        OnVoxelTypeChanged?.Invoke(newType);
    }

}
