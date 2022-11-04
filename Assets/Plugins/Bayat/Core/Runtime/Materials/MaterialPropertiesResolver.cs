using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Bayat.Core
{

#if BAYAT_DEVELOPER
    [CreateAssetMenu(menuName = "Bayat/Core/Material Properties Resolver")]
#endif
    public class MaterialPropertiesResolver : ScriptableObject
    {

        private static MaterialPropertiesResolver current;

        /// <summary>
        /// Gets the current material properties reference resolver.
        /// </summary>
        public static MaterialPropertiesResolver Current
        {
            get
            {
                if (current == null)
                {
                    MaterialPropertiesResolver instance = null;
#if UNITY_EDITOR
                    string[] instanceGuids = AssetDatabase.FindAssets("t: MaterialPropertiesResolver");
                    if (instanceGuids.Length > 1)
                    {
                        Debug.LogError("There is more than one MaterialPropertiesResolver in this project, but there must only be one.");
                        Debug.Log("Deleting other instances of MaterialPropertiesResolver and keeping only one instance.");
                        Debug.LogFormat("The selected instance is: {0}", AssetDatabase.GUIDToAssetPath(instanceGuids[0]));
                        instance = AssetDatabase.LoadAssetAtPath<MaterialPropertiesResolver>(AssetDatabase.GUIDToAssetPath(instanceGuids[0]));

                        // Delete other instances
                        for (int i = 1; i < instanceGuids.Length; i++)
                        {
                            string instanceGuid = instanceGuids[i];
                            AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(instanceGuid));
                        }
                    }
                    else if (instanceGuids.Length == 0)
                    {
                        instance = ScriptableObject.CreateInstance<MaterialPropertiesResolver>();
                        instance.CollectMaterials();
                        System.IO.Directory.CreateDirectory("Assets/Resources/Bayat/Core");
                        AssetDatabase.CreateAsset(instance, "Assets/Resources/Bayat/Core/MaterialPropertiesResolver.asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                    else
                    {
                        instance = AssetDatabase.LoadAssetAtPath<MaterialPropertiesResolver>(AssetDatabase.GUIDToAssetPath(instanceGuids[0]));
                    }
#else
                    instance = Resources.Load<MaterialPropertiesResolver>("Bayat/Core/MaterialPropertiesResolver");
#endif
                    current = instance;
                }
                return current;
            }
        }

        [SerializeField]
        protected List<Material> materials = new List<Material>();
        [SerializeField]
        protected List<RuntimeMaterialProperties> materialsProperties = new List<RuntimeMaterialProperties>();

        /// <summary>
        /// Gets the materials.
        /// </summary>
        public virtual List<Material> Materials
        {
            get
            {
                return this.materials;
            }
        }

        /// <summary>
        /// Gets the materials properties.
        /// </summary>
        public virtual List<RuntimeMaterialProperties> MaterialsProperties
        {
            get
            {
                return this.materialsProperties;
            }
        }

        public virtual void Reset()
        {
#if UNITY_EDITOR
            CollectMaterials();
#endif
        }

#if UNITY_EDITOR
        /// <summary>
        /// Collects the project materials and their properties.
        /// </summary>
        public void CollectMaterials()
        {

            // Remove NULL values from Dictionary.
            if (RemoveNullReferences() > 0)
            {
                Undo.RecordObject(this, "Update MaterialPropertiesResolver List");
            }

            string[] assetGuids = AssetDatabase.FindAssets("t: Object");
            UnityEngine.Object[] builtInExtraAssets = AssetDatabase.LoadAllAssetsAtPath("Resources/unity_builtin_extra");
            UnityEngine.Object[] defaultAssets = AssetDatabase.LoadAllAssetsAtPath("Library/unity default resources");
            Dictionary<string, UnityEngine.Object> allAssetsByDatabase = new Dictionary<string, UnityEngine.Object>();
            for (int i = 0; i < builtInExtraAssets.Length; i++)
            {
                UnityEngine.Object assetObj = builtInExtraAssets[i];
                string guid = Guid.NewGuid().ToString("N");
                allAssetsByDatabase.Add(guid, assetObj);
            }
            for (int i = 0; i < defaultAssets.Length; i++)
            {
                UnityEngine.Object assetObj = defaultAssets[i];
                string guid = Guid.NewGuid().ToString("N");
                allAssetsByDatabase.Add(guid, assetObj);
            }
            for (int i = 0; i < assetGuids.Length; i++)
            {
                string assetGuid = assetGuids[i];
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                UnityEngine.Object assetObj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
                if (allAssetsByDatabase.ContainsValue(assetObj))
                {
                    continue;
                }
                string guid = Guid.NewGuid().ToString("N");
                allAssetsByDatabase.Add(guid, assetObj);
            }

            foreach (var item in allAssetsByDatabase)
            {
                var asset = item.Value;
                if (asset is Material)
                {
                    Material material = (Material)asset;
                    MaterialProperty[] properties = MaterialEditor.GetMaterialProperties(new UnityEngine.Object[] { material });
                    RuntimeMaterialProperties runtimeProperties = null;
                    if (!this.materials.Contains(material))
                    {
                        this.materials.Add(material);
                        runtimeProperties = new RuntimeMaterialProperties();
                        this.materialsProperties.Add(runtimeProperties);
                    }
                    else
                    {
                        int index = this.materials.IndexOf(material);

                        if (index >= this.materialsProperties.Count)
                        {
                            runtimeProperties = new RuntimeMaterialProperties();

                            this.materialsProperties.Add(runtimeProperties);
                        }
                        else
                        {
                            runtimeProperties = this.materialsProperties[index];
                        }
                    }
                    runtimeProperties.Name = material.name;
                    for (int j = 0; j < properties.Length; j++)
                    {
                        MaterialProperty property = properties[j];
                        RuntimeMaterialProperty runtimeProperty = new RuntimeMaterialProperty(property.name, property.type);
                        int index = runtimeProperties.Properties.FindIndex(theProperty => theProperty.Name == property.name);
                        if (index == -1)
                        {
                            runtimeProperties.Properties.Add(runtimeProperty);
                        }
                        else
                        {
                            runtimeProperties.Properties[index] = runtimeProperty;
                        }
                    }
                }
            }
        }
#endif

        /// <summary>
        /// Gets the material properties.
        /// </summary>
        /// <param name="material">The material</param>
        /// <returns>The material properties <see cref="RuntimeMaterialProperties"/></returns>
        public virtual RuntimeMaterialProperties GetMaterialProperties(Material material)
        {
            int index = this.materials.IndexOf(material);
            if (index == -1)
            {
                return null;
            }
            else
            {
                return this.materialsProperties[index];
            }
        }

        /// <summary>
        /// Checks whether if has any null references or not.
        /// </summary>
        /// <returns>True if has null references otherwise false</returns>
        public virtual bool HasNullReferences()
        {
            int index = 0;
            while (index < this.materials.Count)
            {
                if (this.materials[index] == null)
                {
                    return true;
                }
                else
                {
                    index++;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes the null references.
        /// </summary>
        /// <returns>The count of removed references</returns>
        public virtual int RemoveNullReferences()
        {
            int index = 0;
            int removedCount = 0;
            while (index < this.materials.Count)
            {
                if (this.materials[index] == null)
                {
                    this.materials.RemoveAt(index);
                    this.materialsProperties.RemoveAt(index);
                    removedCount++;
                }
                else
                {
                    index++;
                }
            }
            return removedCount;
        }

    }

}