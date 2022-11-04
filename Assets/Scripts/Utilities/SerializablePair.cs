using System;

// [af] This class is necessary as a base class for serializing key-value pairs in Unity. 
// The KeyValuePair type is not inherited because structs cannot be inherited.
public class SerializablePair<T, U>
{
	public T first;
	public U second;

	public SerializablePair(T first, U second)
	{
		this.first = first;
		this.second = second;
	}

	public SerializablePair()
		: this(default(T), default(U)) { }
}

[Serializable]
public class IntAndIntPair : SerializablePair<int, int>
{
	public IntAndIntPair(int first, int second) : base(first, second) { }
	public IntAndIntPair() : base(default(int), default(int)) { }
}
