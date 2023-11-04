using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject outlineIndicatorPrefab;
    
    private CommandInvoker _commandInvoker;

    private void Start()
    {
        _commandInvoker = new CommandInvoker();
        outlineIndicatorPrefab = Instantiate(outlineIndicatorPrefab);
    }

    private void Update()
    {
        // if we're not hovering over a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            outlineIndicatorPrefab.SetActive(false);
            return;
        }
        outlineIndicatorPrefab.SetActive(true);
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            outlineIndicatorPrefab.transform.position = hit.point;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out var hitInfo))
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
