using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

// [af] Similar to SerializableDictionary but for hash sets.
public class SerializableHashSet<T> : HashSet<T>, 
	ISerializationCallbackReceiver, IXmlSerializable
{
	[SerializeField] List<T> elements;

	public SerializableHashSet()
	{
		this.elements = new List<T>();
	}

	public void OnBeforeSerialize()
	{
		this.elements.Clear();

		foreach (var element in this)
		{
			this.elements.Add(element);
		}
	}

	public void OnAfterDeserialize()
	{
		this.Clear();

		for (int i = 0; i < this.elements.Count; i++)
		{
			this.Add(this.elements[i]);
		}
	}
	
	const string XML_Element = "Element";

	public XmlSchema GetSchema()
	{
		return null;
	}

	public void ReadXml(XmlReader reader)
	{
		XmlSerializer elementSerializer = new XmlSerializer(typeof(T));

		bool isEmpty = reader.IsEmptyElement;
		reader.Read();

		// [af] Return early if the XML is empty.
		if (isEmpty)
		{
			return;
		}

		while (reader.NodeType != XmlNodeType.EndElement)
		{
			reader.ReadStartElement(XML_Element);
			T element = (T)elementSerializer.Deserialize(reader);
			reader.ReadEndElement();

			this.Add(element);
			
			reader.MoveToContent();
		}
	}

	public void WriteXml(XmlWriter writer)
	{
		XmlSerializer elementSerializer = new XmlSerializer(typeof(T));

		// [af] Enumerate all set elements.
		foreach (T element in this)
		{
			writer.WriteStartElement(XML_Element);
			elementSerializer.Serialize(writer, element);
			writer.WriteEndElement();
		}
	}
}

[Serializable]
public class ClubTypeHashSet : SerializableHashSet<ClubType>
{
	public ClubTypeHashSet() : base() { }
}

[Serializable]
public class IntAndIntPairHashSet : SerializableHashSet<IntAndIntPair>
{
	public IntAndIntPairHashSet() : base() { }
}

[Serializable]
public class IntHashSet : SerializableHashSet<int>
{
	public IntHashSet() : base() { }
}

[Serializable]
public class StringHashSet : SerializableHashSet<string>
{
	public StringHashSet() : base() { }
}
