using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

class YanSaveResolver : DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);

        PropertyInfo readableInfo = null;

        foreach (var member in type.GetMember("isReadable"))
        {
            if (member.MemberType == MemberTypes.Property)
                readableInfo = (PropertyInfo)member;
        }

        foreach (var property in props)
        {
            property.ShouldSerialize = instance =>
            {
                // Currently materials are broken
                if (type == typeof(Material)) return false;

                if (readableInfo != null)
                    return (bool)readableInfo.GetValue(instance);

                return true;
            };
        }

        return props.Where(p => p.Writable).ToList();
    }
}
