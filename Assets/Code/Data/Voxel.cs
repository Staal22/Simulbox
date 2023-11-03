using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VoxelType : int
{
    Base = 0,
    Grass = 1,
    Sand = 2,
    Wood = 3,
    Water = 4
}

public struct Voxel
{
    public VoxelType Type;
    
    public bool IsSolid => Type != VoxelType.Base;
    
    

}
