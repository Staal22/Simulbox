using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum VoxelType : int
{
    Base = 0,
    Sand = 1,
    Water = 2,
    Wood = 3,
    Grass = 4
}

public struct Voxel
{
    public VoxelType Type;
    
    public bool IsSolid => Type != VoxelType.Base;
    
    

}
