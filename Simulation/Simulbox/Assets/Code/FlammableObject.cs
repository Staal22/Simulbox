using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableObject : MonoBehaviour, IFlammable
{
    private GameObject _fireEffectPrefab;
    private MeshRenderer _meshRenderer;
    [NonSerialized] public bool Burning;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _fireEffectPrefab = VoxelManager.Instance.fireEffectPrefab;
    }

    public void Ignite(float timeToLive)
    {
        Burning = true;
        // change material color to reflect being burnt
        _meshRenderer.material.color = Color.black;
        // destroy self after a few seconds
        Destroy(gameObject, timeToLive);
        Instantiate(_fireEffectPrefab, transform.position, Quaternion.identity);
    }
}
