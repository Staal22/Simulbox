using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VoxelTypeButton : MonoBehaviour
{
    [SerializeField] private VoxelType voxelType;
    private InteractMenu _interactMenu;
    private Button _button;

    private void Awake()
    {
        _interactMenu = GetComponentInParent<InteractMenu>();
    }
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (_interactMenu == null)
        {
            Debug.LogError("No InteractMenu found in parent hierarchy!");
            return;
        }
        _interactMenu.SetSelectedVoxelType(voxelType);
    }
}