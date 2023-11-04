using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum VoxelType : int
{
     Base = 0,
     Grass = 1,
     Sand = 2,
     Wood = 3,
     Water = 4
}

public class Voxel : MonoBehaviour
{
    [SerializeField] private VoxelType type;

    private MeshRenderer _meshRenderer;
    private float _density;
    private bool _flammable;

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
                _density = 1f;
                _flammable = true;
                break;
            case VoxelType.Sand:
                material = SceneTools.Instance.voxelMaterials[(int)type];
                _density = 1f;
                _flammable = false;
                break;
            case VoxelType.Wood:
                material = SceneTools.Instance.voxelMaterials[(int)type];
                _density = 1f;
                _flammable = true;
                break;
            case VoxelType.Water:
                material = SceneTools.Instance.voxelMaterials[(int)type];
                _density = 0.5f;
                _flammable = false;
                break;
        }
        _meshRenderer.material = material;
    }


}

