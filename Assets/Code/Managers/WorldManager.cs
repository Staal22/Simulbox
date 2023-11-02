using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public Material worldMaterial;
    
    private Container _container;

    private void Start()
    {
        GameObject cont = new GameObject("Container");
        cont.transform.parent = transform;
        _container = cont.AddComponent<Container>();
        
        _container.Initialize(worldMaterial, Vector3.zero);
        _container.GenerateMesh();
        _container.UploadMesh();
    }
}
