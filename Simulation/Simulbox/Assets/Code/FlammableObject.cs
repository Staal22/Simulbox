using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableObject : MonoBehaviour, IFlammable
{
    public event Action OnDisintegrated;
    
    private GameObject _fireEffectPrefab;
    private MeshRenderer _meshRenderer;
    private const float TimeToLive = 5f;
    [NonSerialized] public bool Burning;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _fireEffectPrefab = VoxelManager.Instance.fireEffectPrefab;
    }

    public void Ignite()
    {
        Burning = true;
        // change material color to reflect being burnt
        _meshRenderer.material.color = Color.black;
        // destroy self after a few seconds
        Invoke(nameof(Disintegrate), TimeToLive);
        Instantiate(_fireEffectPrefab, transform.position, Quaternion.identity);
    }
    
    private void Disintegrate()
    {
        OnDisintegrated?.Invoke();
        Destroy(gameObject);
    }
}
