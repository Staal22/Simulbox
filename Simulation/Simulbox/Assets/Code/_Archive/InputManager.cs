using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    // private VoxelSpawner _voxelSpawner;
    // private CommandInvoker _commandInvoker;
    private WorldManager _worldManager;

    private void Start()
    {
        // _voxelSpawner = FindObjectOfType<VoxelSpawner>();
        // _commandInvoker = new CommandInvoker();
        _worldManager = WorldManager.Instance;
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
                _worldManager.SpawnVoxelChunk(hitInfo.point, VoxelManager.Instance.CurrentVoxelType);
                // _voxelSpawner.SpawnVoxel(hitInfo.point, VoxelManager.Instance.CurrentVoxelType);
                // ICommand command = new AddVoxelCommand(_voxelSpawner, hitInfo.point);
                // _commandInvoker.ExecuteCommand(command);
            }
        }
        // if (Input.GetKeyDown(KeyCode.Z))
        // {
        //     _commandInvoker.Undo();
        // }
        // if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     _commandInvoker.Redo();
        // }
    }
}
