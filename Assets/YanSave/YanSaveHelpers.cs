using System;

public static class YanSaveHelpers
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
