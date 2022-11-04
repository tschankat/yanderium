using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TributeScript : MonoBehaviour
{
	public RiggedAccessoryAttacher RiggedAttacher;
	public StudentManagerScript StudentManager;
	public HenshinScript Henshin;
	public YandereScript Yandere;
	public GameObject Rainey;

	public string[] MiyukiLetters;
	public string[] NurseLetters;
	public string[] AzurLane;
	public string[] Letter;

	public int MiyukiID;
	public int NurseID;
	public int AzurID;
	public int ID;

	public Mesh ThiccMesh;

	void Start()
	{
		if (GameGlobals.LoveSick || MissionModeGlobals.MissionMode || GameGlobals.AlphabetMode)
		{
			this.enabled = false;
		}

		Rainey.SetActive(false);
	}

	void Update()
	{
		if (this.RiggedAttacher.gameObject.activeInHierarchy)
		{
			RiggedAttacher.newRenderer.SetBlendShapeWeight(0, 100);
			RiggedAttacher.newRenderer.SetBlendShapeWeight(1, 100);

			enabled = false;
		}
		else
		{
			if (!Yandere.PauseScreen.Show && Yandere.CanMove)
			{
				if (Input.GetKeyDown(Letter[ID]))
				{
					ID++;

					if (ID == Letter.Length)
					{
						Rainey.SetActive(true);
						enabled = false;
					}
				}

				if (Input.GetKeyDown(AzurLane[AzurID]))
				{
					AzurID++;

					if (AzurID == AzurLane.Length)
					{
						Yandere.AzurLane();
						enabled = false;
					}
				}

				if (Input.GetKeyDown(NurseLetters[NurseID]))
				{
					NurseID++;

					if (NurseID == NurseLetters.Length)
					{
						RiggedAttacher.root = StudentManager.Students[90].Hips.parent.gameObject;
						RiggedAttacher.Student = StudentManager.Students[90];
						RiggedAttacher.gameObject.SetActive(true);

						StudentManager.Students[90].MyRenderer.enabled = false;
					}
				}

				if (Yandere.Armed)
				{
					if (Yandere.EquippedWeapon.WeaponID == 14)
					{
						if (Input.GetKeyDown(MiyukiLetters[MiyukiID]))
						{
							MiyukiID++;

							if (MiyukiID == MiyukiLetters.Length)
							{
								Henshin.TransformYandere();
								Yandere.CanMove = false;
								enabled = false;
							}
						}
					}
				}
			}
		}
	}
}