using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    private const float BurnTimeSeconds = 5;
    private float _burnCounter;

    private void Start()
    {
        Ignite();
    }

    private void FixedUpdate()
    {
        _burnCounter += Time.deltaTime;
        BurnAdjacentObjects();
        if (_burnCounter >= BurnTimeSeconds)
        {
            Extinguish();
        }
    }

    private void Ignite()
    {
        // TODO - code to ignite the fire, like playing an animation
    }
    
    private void Extinguish()
    {
        // TODO - code to extinguish the fire, like playing an animation and then disabling the gameObject
        Destroy(gameObject);
    }

    private void BurnAdjacentObjects()
    {
        // TODO - fix spreading so it works properly!
        // Check for adjacent flammable objects and ignite them
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != gameObject)
            {
                if (hitCollider.gameObject.GetComponent<FireEffect>() != null)
                {
                    continue;
                }
                var flammable = hitCollider.gameObject.GetComponent<FlammableObject>();
                if (flammable != null)
                {
                    flammable.Ignite(BurnTimeSeconds);
                    hitCollider.gameObject.AddComponent<FireEffect>();
                }
            }
        }
    }

}