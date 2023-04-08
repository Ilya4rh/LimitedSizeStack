using System;
using System.Collections.Generic;

namespace LimitedSizeStack;

public class ActionStack<TItem>
{
	public TItem Element;
    public bool OperationIsAdd;
    public int Index;
	public bool OperationIsMoveUp;
}

public class ListModel<TItem> : ActionStack<TItem>
{
	public List<TItem> Items { get; }
	private readonly LimitedSizeStack<ActionStack<TItem>> actionStack;

	

    public ListModel(int undoLimit) : this(new List<TItem>(), undoLimit)
	{
		
	}

	public ListModel(List<TItem> items, int undoLimit)
	{
		Items = items;
		actionStack = new LimitedSizeStack<ActionStack<TItem>>(undoLimit);

    }

	public void AddItem(TItem item)
	{
		var action = new ActionStack<TItem> { Element = item, OperationIsAdd = true };

        actionStack.Push(action);
		Items.Add(item);
	}

	public void RemoveItem(int index)
	{
		var action = new ActionStack<TItem> { Element = Items[index], OperationIsAdd = false, Index = index };

        actionStack.Push(action);
        Items.RemoveAt(index);
	}

	public bool CanUndo()
	{
		return actionStack.Count > 0;
	}

	public void Undo()
	{
		var action = actionStack.Pop();
		if (action.OperationIsMoveUp)
		{
			Items.Remove(action.Element);
			Items.Insert(action.Index, action.Element);
		}
		else if (action.OperationIsAdd)
			Items.Remove(action.Element);
		else
			Items.Insert(action.Index, action.Element);
	}

	public void MoveUpItem(int index)
	{
        var action = new ActionStack<TItem> { Element = Items[index], OperationIsMoveUp = true, Index = index };
		actionStack.Push(action);

        var element = Items[index];
		Items.RemoveAt(index);
		Items.Insert(--index, element);
	}
}