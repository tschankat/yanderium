using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bayat.Json.Linq;
using Bayat.Json.Serialization;
using Bayat.Json.Utilities;

namespace Bayat.Json.Converters
{

    public abstract class UnityBehaviourConverter : UnityComponentConverter
    {

        public override bool IsGenericConverter
        {
            get
            {
                return true;
            }
        }

        public UnityBehaviourConverter()
        {
        }

        public override string[] GetObjectProperties()
        {
            return new string[] { "enabled" };
        }

        public override List<string> GetSerializedProperties()
        {
            var list = new List<string>(base.GetSerializedProperties());
            list.AddRange(GetObjectProperties());
            return list;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(UnityEngine.Behaviour).IsAssignableFrom(objectType) && base.CanConvert(objectType);
        }

        public override void WriteProperties(JsonObjectContract contract, JsonWriter writer, object value, Type objectType, JsonSerializerWriter internalWriter)
        {
            base.WriteProperties(contract, writer, value, objectType, internalWriter);
            var instance = (UnityEngine.Behaviour)value;
            writer.WriteProperty("enabled", instance.enabled);
        }

        public override object PopulateMember(string memberName, JsonContract contract, JsonReader reader, Type objectType, object targetObject, JsonSerializerReader internalReader)
        {
            var instance = (UnityEngine.Behaviour)targetObject;
            switch (memberName)
            {
                case "enabled":
                    instance.enabled = reader.ReadProperty<bool>();
                    break;
                default:
                    base.PopulateMember(memberName, contract, reader, objectType, targetObject, internalReader);
                    break;
            }
            return instance;
        }

    }

}