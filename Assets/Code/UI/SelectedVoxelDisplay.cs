using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedVoxelDisplay : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textMeshProUgui;
    
    private void Start()
    {
        VoxelManager.Instance.OnVoxelTypeChanged += UpdateDisplay;
    }
    
    private void UpdateDisplay(VoxelType newType)
    {
        image.sprite = Sprite.Create(SceneTools.Instance.voxelIcons[(int)newType], new Rect(0, 0, 64, 64), Vector2.zero);
        textMeshProUgui.text = newType.ToString();
    }
}
