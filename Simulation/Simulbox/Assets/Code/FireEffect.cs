using System;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    // private readonly List<Voxel> _voxels = new();
    private readonly List<FlammableObject> _flammables = new();
    private bool _beingDestroyed;

    private void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0, 2, 0), 2f);

        foreach (var hitCollider in hitColliders)
        {
            AddVoxel(hitCollider.gameObject);
        }
        if (_flammables.Count == 0)
        {
            _beingDestroyed = true;
            Invoke(nameof(Extinguish), 3f);
        }
        InvokeRepeating(nameof(BurnFlammables), 0.3f, 0.3f);
    }

    private void Extinguish()
    {
        if (_beingDestroyed)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_flammables.Count == 0 && !_beingDestroyed)
        {
            _beingDestroyed = true;
            Invoke(nameof(Extinguish), 3f);
        }
    }

    private void OnTriggerEnter(Collider other) => AddVoxel(other.gameObject);

    private void AddVoxel(GameObject potentialVoxel)
    {
        var voxel = potentialVoxel.GetComponent<Voxel>();
        if (voxel == null) return;
        if (voxel.type == VoxelType.Water)
        {
            Extinguish();            
        }
        else
        {
            var flammable = voxel.GetComponent<FlammableObject>();
            if (flammable == null || flammable.Burning) return;
            
            _flammables.Add(flammable);
            _beingDestroyed = false;
        }
    }

    private void BurnFlammables()
    {
        if (_flammables.Count == 0 && !_beingDestroyed)
        {
            Extinguish();
        }
        if (_flammables.Count == 0) return;

        for (int i = _flammables.Count - 1; i >= 0; i--)
        {
            if (_flammables[i].Burning)
            {
                _flammables.RemoveAt(i);
            }
            else
            {
                _flammables[i].Ignite();
                if (!_beingDestroyed)
                {
                    _beingDestroyed = true;
                    Invoke(nameof(Extinguish), _flammables[i].timeToLive);
                }
            }
        }
    }
}