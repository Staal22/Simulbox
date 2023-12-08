using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum VoxelType : int
{
     Base = 0,
     Grass = 1,
     Sand = 2,
     Wood = 3,
     Water = 4,
     Fire = 5
}

public class Voxel : MonoBehaviour
{
    public VoxelType type;
    public float burnTime;
    
    private SceneTools _sceneTools;
    private MeshRenderer _meshRenderer;
    
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        _sceneTools = SceneTools.Instance;
    }

    public void Init(VoxelType inType)
    {
        type = inType;
        Material material;
        // Initialize values based on type
        switch (type)
        {
            case VoxelType.Base:
            default:
                throw new NotImplementedException();
            case VoxelType.Grass:
                material = _sceneTools.voxelMaterials[(int)VoxelType.Grass];
                burnTime = 2f;
                gameObject.AddComponent<FlammableObject>();
                break;
            case VoxelType.Sand:
                material = _sceneTools.voxelMaterials[(int)VoxelType.Sand];
                break;
            case VoxelType.Wood:
                material = _sceneTools.voxelMaterials[(int)VoxelType.Wood];
                burnTime = 5f;
                gameObject.AddComponent<FlammableObject>();
                break;
            case VoxelType.Water:
                material = _sceneTools.voxelMaterials[(int)VoxelType.Water];
                break;
        }
        _meshRenderer.material = material;
    }
}

