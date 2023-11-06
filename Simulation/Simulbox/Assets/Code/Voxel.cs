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
    [SerializeField] private VoxelType type;

    private MeshRenderer _meshRenderer;
    public float density;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Init(VoxelType inType)
    {
        type = inType;
        Material material;
        // initialize values based on type
        switch (type)
        {
            case VoxelType.Base:
            default:
                throw new NotImplementedException();
            case VoxelType.Grass:
                material = SceneTools.Instance.voxelMaterials[(int)type];
                density = 1f;
                gameObject.AddComponent<FlammableObject>();
                break;
            case VoxelType.Sand:
                material = SceneTools.Instance.voxelMaterials[(int)type];
                density = 1f;
                break;
            case VoxelType.Wood:
                material = SceneTools.Instance.voxelMaterials[(int)type];
                density = 1f;
                break;
            case VoxelType.Water:
                material = SceneTools.Instance.voxelMaterials[(int)type];
                density = 0.5f;
                break;
        }
        _meshRenderer.material = material;
    }

    private void Update()
    {
        switch (type)
        {
            case VoxelType.Base:
            default:
                throw new NotImplementedException();
            case VoxelType.Grass:
                break;
            case VoxelType.Sand:
                break;
            case VoxelType.Wood:
                break;
            case VoxelType.Water:
                break;
        }
    }
}

