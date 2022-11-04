using System;
using System.Collections;
using UnityEngine;

// [af] A wrapper for C# arrays so 2D arrays can be serialized by Unity. This class 
// shouldn't be used directly because Unity can't serialize generic classes (which is 
// a pretty big deal), so unfortunately we have to define a derived class and serialize 
// that instead.
public class ArrayWrapper<T> : IEnumerable
{
	[SerializeField] T[] elements;

	public ArrayWrapper(int size)
	{
		this.elements = new T[size];
	}

	public ArrayWrapper(T[] elements)
	{
		this.elements = elements;
	}

	public T this[int i]
	{
		get { return this.elements[i]; }
		set { this.elements[i] = value; }
	}

	public int Length
	{
		get { return this.elements.Length; }
	}

	// [af] Use this when the caller needs to be assigned the array itself.
	public T[] Get()
	{
		return this.elements;
	}

	public IEnumerator GetEnumerator()
	{
		return this.elements.GetEnumerator();
	}
}

[Serializable]
public class AudioClipArrayWrapper : ArrayWrapper<AudioClip>
{
	public AudioClipArrayWrapper(int size)
		: base(size) { }

	public AudioClipArrayWrapper(AudioClip[] elements)
		: base(elements) { }
}

[Serializable]
public class ScheduleBlockArrayWrapper : ArrayWrapper<ScheduleBlock>
{
	public ScheduleBlockArrayWrapper(int size)
		: base(size) { }

	public ScheduleBlockArrayWrapper(ScheduleBlock[] elements)
		: base(elements) { }
}

[Serializable]
public class StringArrayWrapper : ArrayWrapper<string>
{
	public StringArrayWrapper(int size)
		: base(size) { }

	public StringArrayWrapper(string[] elements)
		: base(elements) { }
}
