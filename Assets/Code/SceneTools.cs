using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTools : MonoBehaviour
{
    public static SceneTools Instance;
    [field: SerializeField] public Material[] voxelMaterials;
    [field: SerializeField] public Texture2D[] voxelIcons;

    private void Awake()
    {
        Instance = this;
    }
}
