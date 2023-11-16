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
    private float _yOffset;
    private bool _paintMode;

    private void Awake()
    {
        _commandInvoker = new CommandInvoker();
        outlineIndicatorPrefab = Instantiate(outlineIndicatorPrefab);
    }

    private void Start()
    {
        InteractMenu.Instance.OnPaintModeChanged += SetPaintMode;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Reset position
            _yOffset = 0f;
        }
        // Adjust zOffset based on mouse scroll wheel
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _yOffset += Input.mouseScrollDelta.y * -3f;
        }
        else
        {
            _yOffset += Input.mouseScrollDelta.y * -1f;
        }
        // TODO - clamp yOffset to a reasonable range
        // TODO - fix initial placement to be on the ground
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            var point = hit.point + Vector3.up * _yOffset;
            
            outlineIndicatorPrefab.transform.position = point;
            if (Input.GetMouseButtonDown(0))
            {
                ICommand command;
                if (_paintMode)
                    command = new AddVoxel(point);
                else
                    command = new AddVoxelGroup(point);
                _commandInvoker.ExecuteCommand(command);
                _yOffset = 0f;
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
    
    private void SetPaintMode(bool paintMode)
    {
        _paintMode = paintMode;
    }
}
