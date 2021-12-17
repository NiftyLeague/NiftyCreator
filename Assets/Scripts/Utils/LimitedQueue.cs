using System.Collections.Generic;

public class LimitedQueue<T> : Queue<T>
{
	private int limit;
	public LimitedQueue(int limit) : base(limit)
	{
		this.limit = limit;
	}

	public new T Enqueue(T item)
	{
		T dequeued = default;
		if (Count == limit)
		{
			dequeued = Dequeue();
		}
		base.Enqueue(item);
		return dequeued;
	}
}
