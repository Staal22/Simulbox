using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableObject : MonoBehaviour, IFlammable
{
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Ignite(float timeToLive)
    {
        // change material color to reflect being burnt
        _meshRenderer.material.color = Color.black;
        // destroy self after a few seconds
        Destroy(gameObject, timeToLive);
    }
}
