using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    private static readonly Stack<ICommand> UndoStack = new();
    private static readonly Stack<ICommand> RedoStack = new();

    public void ExecuteCommand(ICommand command)
    {
        // Debug.Log("Executing command" + command.GetType().Name);
        command.Execute();
        UndoStack.Push(command);
        RedoStack.Clear();
    }

    public void Undo()
    {
        if (UndoStack.Count <= 0) return;
        var command = UndoStack.Pop();
        command.Undo();
        RedoStack.Push(command);
    }

    public void Redo()
    {
        if (RedoStack.Count <= 0) return;
        var command = RedoStack.Pop();
        command.Execute();
        UndoStack.Push(command);
    }
}