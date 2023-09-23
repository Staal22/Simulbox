using UnityEngine;

public class Voxel : MonoBehaviour
{
    public Color color;     // Color for rendering
    public float density;   // Density of the voxel
    public bool flammable;  // Can the voxel catch fire?
    
    // Add other properties and methods as needed for interactions and behavior
}

public enum VoxelType
{
    Sand,
    Water,
    Wood,
    Grass
    // Add more voxel types as needed
}