using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

// [af] Same idea as the ArrayWrapper class. This class works around the fact that
// Unity cannot serialize dictionaries, although you still need to write a derived 
// class and use that instead of this.
public class SerializableDictionary<K, V> : Dictionary<K, V>,
	ISerializationCallbackReceiver, IXmlSerializable
{
	[SerializeField] List<K> keys;
	[SerializeField] List<V> values;

	public SerializableDictionary()
	{
		this.keys = new List<K>();
		this.values = new List<V>();
	}

	public void OnBeforeSerialize()
	{
		this.keys.Clear();
		this.values.Clear();

		foreach (var pair in this)
		{
			this.keys.Add(pair.Key);
			this.values.Add(pair.Value);
		}
	}

	public void OnAfterDeserialize()
	{
		this.Clear();

		Debug.Assert(this.keys.Count == this.values.Count);

		for (int i = 0; i < this.keys.Count; i++)
		{
			this.Add(this.keys[i], this.values[i]);
		}
	}

	const string XML_Item = "Item";
	const string XML_Key = "Key";
	const string XML_Value = "Value";

	public XmlSchema GetSchema()
	{
		return null;
	}

	public void ReadXml(XmlReader reader)
	{
		XmlSerializer keySerializer = new XmlSerializer(typeof(K));
		XmlSerializer valueSerializer = new XmlSerializer(typeof(V));

		bool isEmpty = reader.IsEmptyElement;
		reader.Read();

		// [af] Return early if the XML is empty.
		if (isEmpty)
		{
			return;
		}

		while (reader.NodeType != XmlNodeType.EndElement)
		{
			reader.ReadStartElement(XML_Item);

			reader.ReadStartElement(XML_Key);
			K key = (K)keySerializer.Deserialize(reader);
			reader.ReadEndElement();

			reader.ReadStartElement(XML_Value);
			V value = (V)valueSerializer.Deserialize(reader);
			reader.ReadEndElement();

			this.Add(key, value);

			reader.ReadEndElement();
			reader.MoveToContent();
		}

		reader.ReadEndElement();
	}

	public void WriteXml(XmlWriter writer)
	{
		XmlSerializer keySerializer = new XmlSerializer(typeof(K));
		XmlSerializer valueSerializer = new XmlSerializer(typeof(V));

		// [af] Enumerate all dictionary elements.
		foreach (var pair in this)
		{
			writer.WriteStartElement(XML_Item);

			writer.WriteStartElement(XML_Key);
			keySerializer.Serialize(writer, pair.Key);
			writer.WriteEndElement();

			writer.WriteStartElement(XML_Value);
			valueSerializer.Serialize(writer, pair.Value);
			writer.WriteEndElement();

			writer.WriteEndElement();
		}
	}
}

[Serializable]
public class ClubTypeAndBoolDictionary : SerializableDictionary<ClubType, bool>
{
	public ClubTypeAndBoolDictionary() : base() { }
}

[Serializable]
public class ClubTypeAndStringDictionary : SerializableDictionary<ClubType, string>
{
	public ClubTypeAndStringDictionary() : base() { }
}

[Serializable]
public class IntAndBoolDictionary : SerializableDictionary<int, bool>
{
	public IntAndBoolDictionary() : base() { }
}

[Serializable]
public class IntAndColorDictionary : SerializableDictionary<int, Color>
{
	public IntAndColorDictionary() : base() { }
}

[Serializable]
public class IntAndFloatDictionary : SerializableDictionary<int, float>
{
	public IntAndFloatDictionary() : base() { }
}

[Serializable]
public class IntAndIntDictionary : SerializableDictionary<int, int>
{
	public IntAndIntDictionary() : base() { }
}

[Serializable]
public class IntAndIntPairAndBoolDictionary : SerializableDictionary<IntAndIntPair, bool>
{
	public IntAndIntPairAndBoolDictionary() : base() { }
}

[Serializable]
public class IntAndStringDictionary : SerializableDictionary<int, string>
{
	public IntAndStringDictionary() : base() { }
}

[Serializable]
public class IntAndVector2Dictionary : SerializableDictionary<int, Vector2>
{
	public IntAndVector2Dictionary() : base() { }
}

[Serializable]
public class NotificationTypeAndStringDictionary :
	SerializableDictionary<NotificationType, string>
{
	public NotificationTypeAndStringDictionary() : base() { }
}

[Serializable]
public class PersonaTypeAndStringDictionary : SerializableDictionary<PersonaType, string>
{
	public PersonaTypeAndStringDictionary() : base() { }
}

[Serializable]
public class SubtitleTypeAndAudioClipArrayDictionary :
	SerializableDictionary<SubtitleType, AudioClipArrayWrapper>
{
	public SubtitleTypeAndAudioClipArrayDictionary() : base() { }
}

[Serializable]
public class StringAndBoolDictionary : SerializableDictionary<string, bool>
{
	public StringAndBoolDictionary() : base() { }
}

[Serializable]
public class StringAndStringArrayDictionary : SerializableDictionary<string, string[]>
{
	public StringAndStringArrayDictionary() : base() { }
}
