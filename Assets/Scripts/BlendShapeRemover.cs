using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeRemover : MonoBehaviour
{
    public SkinnedMeshRenderer SelectedMesh;

    private void Awake()
    {
        if (SystemInfo.supportsComputeShaders == false)
        {
            SelectedMesh.sharedMesh.ClearBlendShapes();
        }
    }
}
