using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTools : MonoBehaviour
{
    private static SceneTools _instance;
    public static SceneTools Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneTools>();
            }
            return _instance;
        }
    }
    
    [field: SerializeField] public Material[] voxelMaterials;
    [field: SerializeField] public Texture2D[] voxelIcons;
    
    public Color burntWood;
    public Color burntGrass;

    private void Awake()
    {
        if (Instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Multiple SceneTools objects detected: removing this one.");
            Destroy(this);
        }
    }
}
