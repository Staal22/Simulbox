using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private CommandInvoker _commandInvoker;

    private void Start()
    {
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
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                // ICommand command = new AddVoxelCommand(hitInfo.point);
                ICommand command = new AddVoxelGroup(hitInfo.point);
                _commandInvoker.ExecuteCommand(command);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _commandInvoker.Undo();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _commandInvoker.Redo();
        }
    }
}
