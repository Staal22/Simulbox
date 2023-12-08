using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    private readonly HashSet<FlammableObject> _flammables = new();
    private bool _beingDestroyed;
    private float _burnTimer = 0.3f;

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
            ExtinguishAfterDelay(3f);
        }
    }

    private void Update()
    {
        _burnTimer -= Time.deltaTime;
        if (_burnTimer <= 0)
        {
            BurnFlammables();
            _burnTimer = 0.3f;
        }

        if (_flammables.Count == 0 && !_beingDestroyed)
        {
            _beingDestroyed = true;
            ExtinguishAfterDelay(3f);
        }
    }

    private void OnTriggerEnter(Collider other) => AddVoxel(other.gameObject);

    private void AddVoxel(GameObject potentialVoxel)
    {
        var voxel = potentialVoxel.GetComponent<Voxel>();
        if (voxel == null) return;
        if (voxel.type == VoxelType.Water)
        {
            foreach (var flammable in _flammables)
            {
                flammable.StopBurning();
            }
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
    
        var toRemove = new List<FlammableObject>();
    
        foreach (var flammable in _flammables)
        {
            if (flammable.Burning)
            {
                toRemove.Add(flammable);
            }
            else
            {
                flammable.Ignite();
                if (!_beingDestroyed)
                {
                    _beingDestroyed = true;
                    ExtinguishAfterDelay(flammable.timeToLive);
                }
            }
        }
        foreach (var flammable in toRemove)
        {
            _flammables.Remove(flammable);
        }
    }
    
    private void Extinguish()
    {
        if (_beingDestroyed)
        {
            Destroy(gameObject);
        }
    }
    private void ExtinguishAfterDelay(float delay)
    {
        StartCoroutine(ExtinguishCoroutine(delay));
    }

    private IEnumerator ExtinguishCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Extinguish();
    }
}