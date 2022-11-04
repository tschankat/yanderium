using UnityEngine;

public class TrashCompactorScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public JsonScript JSON;
	public UIPanel HUD;

	public GameObject Jukebox;

	public Transform TrashCompactorObject;
	public Transform Yandere;
	
	public float Speed = 0.0f;

	void Start()
	{
#if !UNITY_EDITOR
		//If we are NOT in the editor...
		if (this.StudentManager.Students[10] != null || this.StudentManager.Students[11] != null)
		{
			this.CompactTrash();
		}
		else
		{
			// [af] Converted while loop to for loop.
			for (int ID = 1; ID < 101; ID++)
			{
				if (this.StudentManager.Students[ID] != null)
				{
					if (!this.StudentManager.Students[ID].Male)
					{
						if (this.StudentManager.Students[ID].Cosmetic.Hairstyle == 20 ||
							this.StudentManager.Students[ID].Cosmetic.Hairstyle == 21 ||
							this.StudentManager.Students[ID].Persona == PersonaType.Protective)
						{
							this.CompactTrash();
						}
					}
				}
			}
		}
#else
		//If we ARE in the editor...
		//Debug.Log("This object's name is " + name);
		this.enabled = false;
#endif
	}

	void Update()
	{
		// [af] Added "gameObject" for C# compatibility.
		if (this.TrashCompactorObject.gameObject.activeInHierarchy)
		{
			this.Speed += Time.deltaTime * 0.010f;

			this.TrashCompactorObject.position = Vector3.MoveTowards(
				this.TrashCompactorObject.position, this.Yandere.position, Time.deltaTime * this.Speed);
			this.TrashCompactorObject.LookAt(this.Yandere.position);

			if (Vector3.Distance(this.TrashCompactorObject.position, this.Yandere.position) < 0.50f)
			{
				Application.Quit();
			}
		}
	}

	void CompactTrash()
	{
		Debug.Log("Taking out the garbage.");

		if (!this.TrashCompactorObject.gameObject.activeInHierarchy)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 0.0f;
			this.StudentManager.SetAtmosphere();

			// [af] Converted while loop to foreach loop.
			foreach (StudentScript student in this.StudentManager.Students)
			{
				if (student != null)
				{
					// [af] Added "gameObject" for C# compatibility.
					student.gameObject.SetActive(false);
				}
			}

			this.Yandere.gameObject.GetComponent<YandereScript>().NoDebug = true;

			this.TrashCompactorObject.gameObject.SetActive(true);
			this.Jukebox.SetActive(false);
			this.HUD.enabled = false;
		}
	}
}