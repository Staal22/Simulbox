using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var voxel = other.GetComponent<Voxel>();
        if (voxel != null)
        {
            Destroy(voxel.gameObject);
        }
    }
}
