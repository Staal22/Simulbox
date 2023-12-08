using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableObject : MonoBehaviour, IFlammable
{
    private Color _burntWood;
    private Color _burntGrass;
    
    private GameObject _fireEffectPrefab;
    private MeshRenderer _meshRenderer;
    private Voxel _voxel;
    public float timeToLive = 5f;
    [NonSerialized] public bool Burning;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _voxel = GetComponent<Voxel>();
        timeToLive = _voxel.burnTime;
    }

    private void Start()
    {
        _fireEffectPrefab = VoxelManager.Instance.fireEffectPrefab;
        _burntWood = SceneTools.Instance.burntWood;
        _burntGrass = SceneTools.Instance.burntGrass;
    }

    public void Ignite()
    {
        Burning = true;
        // _meshRenderer.material.color = Color.black;
        _meshRenderer.material.color = timeToLive > 2f ? _burntWood : _burntGrass;
        Invoke(nameof(Disintegrate), timeToLive);
        Instantiate(_fireEffectPrefab, transform.position, Quaternion.identity);
    }
    
    public void StopBurning()
    {
        Burning = false;
        CancelInvoke(nameof(Disintegrate));
    }
    
    private void Disintegrate()
    {
        Destroy(gameObject);
    }
}
