using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public static class YanSaveEditorHelpers
{
    public static Type GrabType(string type)
    {
        if (string.IsNullOrEmpty(type)) return null;

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            var grabbedType = assembly.GetType(type);
            if (grabbedType != null)
                return grabbedType;
        }

        return null;
    }
}
#endif
