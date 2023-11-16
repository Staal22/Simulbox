using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    private readonly List<FlammableObject> _flammables = new();
    private const float BurnTimeSeconds = 5;
    private float _burnCounter;

    private void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0,2,0), 2.5f);
        
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == gameObject) continue;
            var flammable = hitCollider.gameObject.GetComponent<FlammableObject>();
            if (flammable == null) continue;
            if (flammable.Burning) continue;
            
            _flammables.Add(flammable);
        }
    }

    private void FixedUpdate()
    {
        _burnCounter += Time.deltaTime;
        BurnFlammables();
        if (_burnCounter >= BurnTimeSeconds)
        {
            Extinguish();
        }
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
    }

    private void OnTriggerExit(Collider other)
    {
        var flammable = other.gameObject.GetComponent<FlammableObject>();
        if (flammable == null) return;
        if (other.gameObject.GetComponent<FlammableObject>().Burning) return;
        _flammables.Remove(flammable);
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
                _flammables[i].Ignite(BurnTimeSeconds);
            }
        }
    }
}