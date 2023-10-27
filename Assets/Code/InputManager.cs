using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    // public static InputManager Instance;
    
    [SerializeField] private Camera mainCamera;
    private VoxelSpawner _voxelSpawner;
    private CommandInvoker _commandInvoker;
    // private void Awake()
    // {
    //     Instance = this;
    // }

    private void Start()
    {
        //find voxel spawner in scene
        _voxelSpawner = FindObjectOfType<VoxelSpawner>();
        _commandInvoker = new CommandInvoker();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // if we didn't hit UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                ICommand command = new AddVoxelCommand(_voxelSpawner, hitInfo.point);
                _commandInvoker.ExecuteCommand(command);
            }
        }
        if (/*Input.GetKey(KeyCode.LeftControl) && */Input.GetKeyDown(KeyCode.Z))
        {
            _commandInvoker.Undo();
        }
        if (/*Input.GetKey(KeyCode.LeftControl) && */Input.GetKeyDown(KeyCode.Y))
        {
            _commandInvoker.Redo();
        }
    }
}
