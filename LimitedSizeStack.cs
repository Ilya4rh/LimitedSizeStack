using System;
using System.Collections.Generic;

namespace LimitedSizeStack;

public class LimitedSizeStack<T>
{
    private LinkedList<T> listValues;
	private int limitList;

    public LimitedSizeStack(int undoLimit)
	{
		listValues = new LinkedList<T>();

		limitList = undoLimit;
	}

	public void Push(T item)
	{
		if (limitList == 0)
			return;

		else if (listValues.Count == limitList)
			listValues.RemoveFirst();

		listValues.AddLast(item);
	}

	public T Pop()
	{
		var lastValue = listValues.Last.Value;
		listValues.RemoveLast();

		return lastValue;
	}

	public int Count
	{
		get { return listValues.Count; }
	}
}