using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum VoxelType
{
    Base,
    Sand,
    Water,
    Wood,
    Grass
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
            default:
                throw new NotImplementedException();
            case VoxelType.Sand:
                material = SceneTools.Instance.voxelMaterials[0];
                _density = 1f;
                _flammable = false;
                break;
            case VoxelType.Water:
                material = SceneTools.Instance.voxelMaterials[1];
                _density = 0.5f;
                _flammable = false;
                break;
            case VoxelType.Wood:
                material = SceneTools.Instance.voxelMaterials[2];
                _density = 1f;
                _flammable = true;
                break;
            case VoxelType.Grass:
                material = SceneTools.Instance.voxelMaterials[3];
                _density = 1f;
                _flammable = true;
                break;
        }
        _meshRenderer.material = material;
    }
}

