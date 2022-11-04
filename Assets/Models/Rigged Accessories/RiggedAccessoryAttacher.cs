using UnityEngine;
using System.Collections;

public class RiggedAccessoryAttacher : MonoBehaviour
{
	public StudentScript Student;
    public GameObject root;
    public GameObject accessory;
    public Material[] accessoryMaterials;
	public Material[] okaMaterials;
	public Material[] ribaruMaterials;
	public Material[] defaultMaterials;
	public Material[] painterMaterials;
	public Material[] painterMaterialsFlipped;

	public GameObject[] Panties;
	public Material[] PantyMaterials;

	public SkinnedMeshRenderer newRenderer;

	public bool UpdateBounds;
	public bool Initialized;
	public bool CookingClub;
	public bool ScienceClub;
	public bool ArtClub;
	public bool Gentle;
	public int PantyID;
	public int ID;

	// Use this for initialization
	public void Start ()
	{
		this.Initialized = true;

		if (PantyID == 99)
		{
			//Debug.Log("The game believes that Yandere-chan should be wearing Panties #" + PlayerGlobals.PantiesEquipped);

			PantyID = PlayerGlobals.PantiesEquipped;
		}

		if (CookingClub)
		{
			if (Student.Male)
			{
				accessory = GameObject.Find("MaleCookingApron");
			}
			else
			{
				accessory = GameObject.Find("FemaleCookingApron");
			}
		}
		else if (ArtClub)
		{
			if (Student.Male)
			{
				accessory = GameObject.Find("PainterApron");
				accessoryMaterials = painterMaterials;
			}
			else
			{
				accessory = GameObject.Find("PainterApronFemale");
				accessoryMaterials = painterMaterials;
			}
		}
		else if (Gentle)
		{
			accessory = GameObject.Find("GentleEyes");
			accessoryMaterials = defaultMaterials;
		}
		else
		{
			if (ID == 1)
			{
				accessory = GameObject.Find("LabcoatFemale");
			}

			if (ID == 2)
			{
				accessory = GameObject.Find("LabcoatMale");
			}

			if (ID == 26)
			{
				accessory = GameObject.Find("OkaBlazer");
				accessoryMaterials = okaMaterials;
			}

			if (ID == 100)
			{
				accessory = Panties[PantyID];
				accessoryMaterials[0] = PantyMaterials[PantyID];
			}

			#if UNITY_EDITOR
			if (ID == 6)
			{
				accessory = GameObject.Find("Ribaru");
				accessoryMaterials = ribaruMaterials;
			}
			#endif
		}
			
		this.AttachAccessory();
	}

	public void AttachAccessory()
	{
		AddLimb(accessory, root, accessoryMaterials);

		if (ID == 100)
		{
			newRenderer.updateWhenOffscreen = true;
		}
	}

	#if UNITY_EDITOR
	void Update()
	{
		if (ID == 100)
		{
			if (Input.GetKeyDown("p"))
			{
				PantyID++;

				if (PantyID > 11)
				{
					PantyID = 0;
				}

				Destroy(newRenderer);

				Start();
			}
		}
	}
	#endif

	public void RemoveAccessory()
	{
		Destroy(newRenderer);
	}

	void AddLimb(GameObject bonedObj, GameObject rootObj, Material[] bonedObjMaterials)
    {
        SkinnedMeshRenderer[] bonedObjects = bonedObj.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer skinnedRenderer in bonedObjects)
        {
            ProcessBonedObject(skinnedRenderer, rootObj, bonedObjMaterials);
        }
    }

    void ProcessBonedObject(SkinnedMeshRenderer thisRenderer, GameObject rootObj, Material[] thisRendererMaterials)
    {
        /*      Create the SubObject      */
        var newObj = new GameObject(thisRenderer.gameObject.name);
        newObj.transform.parent = rootObj.transform;
		newObj.layer = rootObj.layer;

        /*      Add the renderer      */
        newObj.AddComponent<SkinnedMeshRenderer>();
        newRenderer = newObj.GetComponent<SkinnedMeshRenderer>();

        /*      Assemble Bone Structure      */
        var myBones = new Transform[thisRenderer.bones.Length];
        for (int i = 0; i < thisRenderer.bones.Length; i++)
        {
            myBones[i] = FindChildByName(thisRenderer.bones[i].name, rootObj.transform);
        }

        /*      Assemble Renderer      */
        newRenderer.bones = myBones;
        newRenderer.sharedMesh = thisRenderer.sharedMesh;
        if (thisRendererMaterials == null)
        {
            newRenderer.materials = thisRenderer.sharedMaterials;
        }
        else
        {
            newRenderer.materials = thisRendererMaterials;
        }

		if (UpdateBounds)
		{
			newRenderer.updateWhenOffscreen = true;
		}
    }
 
    Transform FindChildByName(string thisName, Transform thisGameObj)
    {
        Transform returnObj;
        if(thisGameObj.name == thisName)
            return thisGameObj.transform;
        foreach (Transform child in thisGameObj)
        {
            returnObj = FindChildByName(thisName, child);
            if (returnObj)
            {
                return returnObj;
            }
        }
        return null;
    }
}
