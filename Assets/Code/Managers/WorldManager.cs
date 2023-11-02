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
        
        for (int x = 0; x < 16; x++)
        {
            for (int z = 0; z < 16; z++)
            {
                int height = UnityEngine.Random.Range(1, 16);
                for (int y = 0; y < height; y++)
                {
                    _container[new Vector3(x, y, z)] = new Voxel{ID = 1};
                }
            }
        }
        
        _container.GenerateMesh();
        _container.UploadMesh();
    }
}
