using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTools : MonoBehaviour
{
    private static SceneTools instance;
    public static SceneTools Instance
    {
        get
        {
            if (instance == null) 
            {
                instance = FindObjectOfType<SceneTools>();

                if (instance == null) 
                {
                    Debug.Log("SceneTools object must exist in scene.");
                }
            }
            return instance;
        }
    }
    [field: SerializeField] public Material[] voxelMaterials;
    [field: SerializeField] public Texture2D[] voxelIcons;
    
    public Color burntWood;
    public Color burntGrass;

    private void Awake()
    {
        if (instance != null && instance != this) 
        {
            Debug.Log("Multiple SceneTools objects detected: removing this one.");
            Destroy(this.gameObject);
        } 
        else 
        {
            instance = this;
        }
    }
}
