using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Voxel
{
    public byte ID;
    
    public bool IsSolid => ID != 0;
}
