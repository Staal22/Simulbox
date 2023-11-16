using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    private readonly List<FlammableObject> _flammables = new();
    private float _burnCounter;

    private void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0,2,0), 2f);
        
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == gameObject) continue;
            var flammable = hitCollider.gameObject.GetComponent<FlammableObject>();
            if (flammable == null) continue;
            if (flammable.Burning) continue;
            
            _flammables.Add(flammable);
            flammable.OnDisintegrated += Extinguish;
        }
        
        InvokeRepeating(nameof(BurnFlammables), 0.3f, 0.3f);
    }
    
    private void Extinguish()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var flammable = other.gameObject.GetComponent<FlammableObject>();
        if (flammable == null) return;
        if (other.gameObject.GetComponent<FlammableObject>().Burning) return;
        _flammables.Add(flammable);
        flammable.OnDisintegrated += Extinguish;
    }

    private void BurnFlammables()
    {
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
            }
        }
    }
}