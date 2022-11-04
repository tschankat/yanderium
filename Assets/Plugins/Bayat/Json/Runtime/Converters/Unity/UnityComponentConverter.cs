using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bayat.Json.Linq;
using Bayat.Json.Serialization;
using Bayat.Json.Utilities;

namespace Bayat.Json.Converters
{

    public abstract class UnityComponentConverter : UnityObjectConverter
    {

        public override bool IsGenericConverter
        {
            get
            {
                return true;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(UnityEngine.Component).IsAssignableFrom(objectType) && base.CanConvert(objectType);
        }

    }

}