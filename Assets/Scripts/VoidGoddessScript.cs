using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidGoddessScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public InputManagerScript InputManager;
	public PromptScript Prompt;

	public GameObject BloodyUniform;
	public GameObject SeveredLimb;
	public GameObject NewPortrait;
	public GameObject BloodPool;
	public GameObject Portrait;
	public GameObject Goddess;

	public Transform BloodParent;
	public Transform Highlight;
	public Transform Window;
	public Transform Head;

	public UITexture[] Portraits;
	public Animation[] Legs;

	public bool PassingJudgement;
	public bool Disabled;
	public bool Follow;

	public int Selected;
	public int Column;
	public int Row;
	public int ID;

	public Texture Headmaster;
	public Texture Counselor;
	public Texture Infochan;

	void Start ()
	{
        #if !UNITY_EDITOR
		if (GameGlobals.AlphabetMode)
		{
            gameObject.SetActive(false);
        }
		else
		{
        #endif
			Legs[1]["SpiderLegWiggle"].speed = 1.00f;
			Legs[2]["SpiderLegWiggle"].speed = 0.95f;
			Legs[3]["SpiderLegWiggle"].speed = 0.90f;
			Legs[4]["SpiderLegWiggle"].speed = 0.85f;
			Legs[5]["SpiderLegWiggle"].speed = 0.80f;
			Legs[6]["SpiderLegWiggle"].speed = 0.75f;
			Legs[7]["SpiderLegWiggle"].speed = 0.70f;
			Legs[8]["SpiderLegWiggle"].speed = 0.65f;

			//Head.localEulerAngles = new Vector3(0, 180, 180);

			ID = 1;

			while (ID < 101)
			{
				NewPortrait = Instantiate(Portrait, transform.position, Quaternion.identity);

				NewPortrait.transform.parent = Window;

				NewPortrait.transform.localScale = new Vector3(1, 1, 1);

				NewPortrait.transform.localPosition = new Vector3(
					-450 + (Column * 100),
					450 - (Row * 100),
					0);

				Portraits[ID] = NewPortrait.GetComponent<UITexture>();

				if (ID > 11 && ID < 20)
				{
					NewPortrait.GetComponent<UITexture>().mainTexture = Prompt.Yandere.PauseScreen.StudentInfoMenu.RivalPortraits[ID];
				}
				else if (ID < 98)
				{
					string path = "file:///" + Application.streamingAssetsPath +
						"/Portraits/Student_" + ID.ToString() + ".png";

					WWW www = new WWW(path);

					NewPortrait.GetComponent<UITexture>().mainTexture = www.texture;
				}
				else if (ID == 98)
				{
					NewPortrait.GetComponent<UITexture>().mainTexture = Counselor;
				}
				else if (ID == 99)
				{
					NewPortrait.GetComponent<UITexture>().mainTexture = Headmaster;
				}
				else if (ID == 100)
				{
					NewPortrait.GetComponent<UITexture>().mainTexture = Infochan;
				}

				Column++;

				if (Column == 10)
				{
					Column = 0;
					Row++;
				}

				ID++;
			}

			Window.parent.gameObject.SetActive(false);

			Selected = 1;
			Column = 0;
			Row = 0;

			UpdatePortraits();
        #if !UNITY_EDITOR
		}
        #endif
	}

	void Update ()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
			Prompt.Circle[0].fillAmount = 1;

			if (!Goddess.activeInHierarchy)
			{
                Prompt.Yandere.Police.Invalid = true;

				Prompt.Label[0].text = "     " + "Pass Judgement";
				Prompt.Label[1].text = "     " + "Dismiss";
				Prompt.Label[2].text = "     " + "Follow";

				Prompt.HideButton[1] = false;
				Prompt.HideButton[2] = false;

				Prompt.OffsetX[0] = -1;

				Goddess.SetActive(true);
			}
			else
			{
				Window.parent.gameObject.SetActive(true);
				Prompt.Yandere.CanMove = false;
				PassingJudgement = true;
			}
		}

		if (Prompt.Circle[1].fillAmount == 0)
		{
			Prompt.Circle[1].fillAmount = 1;

			Prompt.Label[0].text = "     " + "Summon An Ancient Evil";
			Prompt.Label[1].text = "";
			Prompt.Label[2].text = "";

			Prompt.HideButton[1] = true;
			Prompt.HideButton[2] = true;

			Prompt.OffsetX[0] = 0;

			transform.position = new Vector3(-9.5f, 1, -75);
			Goddess.SetActive(false);
			Follow = false;
		}

		if (Prompt.Circle[2].fillAmount == 0)
		{
			Prompt.Circle[2].fillAmount = 1;

			Follow = !Follow;
		}

		if (Follow)
		{
			if (Vector3.Distance(Prompt.Yandere.transform.position + new Vector3(0, 1, 0), transform.position) > 1)
			{
				transform.position = Vector3.Lerp(transform.position, Prompt.Yandere.transform.position + new Vector3(0, 1, 0), Time.deltaTime);
			}
		}

		if (PassingJudgement)
		{
			if (InputManager.TappedUp)
			{
				Row--;
				UpdateHighlight();
			}
			else if (InputManager.TappedDown)
			{
				Row++;
				UpdateHighlight();
			}
			if (InputManager.TappedLeft)
			{
				Column--;
				UpdateHighlight();
			}
			else if (InputManager.TappedRight)
			{
				Column++;
				UpdateHighlight();
			}

			if (Input.GetButtonDown("A"))
			{
				StudentManager.DisableStudent(Selected);
				UpdatePortraits();
			}

			//Toggle All
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				if (!Disabled)
				{
					this.StudentManager.DisableEveryone();
					this.Disabled = true;
				}
				else
				{
					ID = 1;

					while (ID < 101)
					{
						StudentManager.DisableStudent(ID);
						ID++;
					}
				}

				UpdatePortraits();
			}
			//Toggle Clubless
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				ID = 1;

				while (ID < 11)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Rivals
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				ID = 11;

				while (ID < 21)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Cooking
			else if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				ID = 21;

				while (ID < 26)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Drama
			else if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				ID = 26;

				while (ID < 31)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Occult
			else if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				ID = 31;

				while (ID < 36)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Martial Arts
			else if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				ID = 36;

				while (ID < 41)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Art
			else if (Input.GetKeyDown(KeyCode.Alpha8))
			{
				ID = 41;

				while (ID < 46)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Martial Arts
			else if (Input.GetKeyDown(KeyCode.Alpha9))
			{
				ID = 46;

				while (ID < 51)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Music
			else if (Input.GetKeyDown(KeyCode.Alpha0))
			{
				ID = 51;

				while (ID < 56)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Photography
			else if (Input.GetKeyDown("-"))
			{
				ID = 56;

				while (ID < 61)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Science
			else if (Input.GetKeyDown("="))
			{
				ID = 61;

				while (ID < 66)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Sports
			else if (Input.GetKeyDown("r"))
			{
				ID = 66;

				while (ID < 71)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Gardening
			else if (Input.GetKeyDown("t"))
			{
				ID = 71;

				while (ID < 76)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Delinquents
			else if (Input.GetKeyDown("y"))
			{
				ID = 76;

				while (ID < 81)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Bullies
			else if (Input.GetKeyDown("u"))
			{
				ID = 81;

				while (ID < 86)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Council
			else if (Input.GetKeyDown("i"))
			{
				ID = 86;

				while (ID < 90)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Faculty
			else if (Input.GetKeyDown("o"))
			{
				ID = 90;

				while (ID < 98)
				{
					StudentManager.DisableStudent(ID);
					ID++;
				}

				UpdatePortraits();
			}
			//Toggle Club Leaders
			else if (Input.GetKeyDown("p"))
			{
				/*
				StudentManager.DisableStudent(21);
				StudentManager.DisableStudent(26);
				StudentManager.DisableStudent(31);
				StudentManager.DisableStudent(36);
				StudentManager.DisableStudent(41);
				StudentManager.DisableStudent(46);
				StudentManager.DisableStudent(51);
				StudentManager.DisableStudent(56);
				StudentManager.DisableStudent(61);
				StudentManager.DisableStudent(66);
				StudentManager.DisableStudent(71);

				UpdatePortraits();
			}
			//Toggle Non-Leaders
			else if (Input.GetKeyDown("["))
			{
				*/
				ID = 1;

				while (ID < 101)
				{
					if (ID != 21 && ID != 26 && ID != 31 && 
						ID != 36 && ID != 41 && ID != 46 && 
						ID != 51 && ID != 56 && ID != 61 && 
						ID != 66 && ID != 71)
					{
						StudentManager.DisableStudent(ID);
					}

					ID++;
				}

				UpdatePortraits();
			}
			//Toggle everyone but Senpai+Raibaru+Osana
			else if (Input.GetKeyDown("space"))
			{
				ID = 2;

				while (ID < 101)
				{
					if (ID != 1 && ID != 2 && ID != 3 && ID != 6 && ID != 10 && ID != 11 && ID != 39 &&
						ID != 41 && ID != 42 &&ID != 43 && ID != 44 && ID != 45 && ID != 81 && ID != 82 &&
						ID != 83 && ID != 84 && ID != 85)
					{
						StudentManager.DisableStudent(ID);
					}

					ID++;
				}

				if (StudentManager.Students[7].gameObject.activeInHierarchy)
				{
					StudentManager.DisableStudent(7);
				}

				UpdatePortraits();
			}

			if (Input.GetButtonDown("B"))
			{
				Window.parent.gameObject.SetActive(false);
				Prompt.Yandere.CanMove = true;
				Prompt.Yandere.Shoved = false;
				PassingJudgement = false;
			}

			if (Input.GetKeyDown(KeyCode.Z))
			{
				StudentManager.Students[Selected].transform.position = Prompt.Yandere.transform.position + Prompt.Yandere.transform.forward;
				Physics.SyncTransforms();
			}

			if (Input.GetKeyDown(KeyCode.X))
			{
				CounselorGlobals.DeleteAll();

				StudentGlobals.SetStudentExpelled(76, false);
				StudentGlobals.SetStudentExpelled(77, false);
				StudentGlobals.SetStudentExpelled(78, false);
				StudentGlobals.SetStudentExpelled(79, false);
				StudentGlobals.SetStudentExpelled(80, false);
			}
		}

#if UNITY_EDITOR
		if (Input.GetKey(KeyCode.LeftControl))
		{
			if (Input.GetKeyDown("z"))
			{
				GameObject NewBlood = Instantiate(BloodPool, Prompt.Yandere.transform.position + Prompt.Yandere.transform.forward + new Vector3(0, .0001f, 0), Quaternion.identity);

				NewBlood.transform.parent = BloodParent;
				NewBlood.transform.localEulerAngles = new Vector3(90, 0, 0);
			}
			else if (Input.GetKeyDown("x"))
			{
				GameObject NewLimb = Instantiate(SeveredLimb, Prompt.Yandere.transform.position + Prompt.Yandere.transform.forward + new Vector3(0, 1, 0), Quaternion.identity);

				NewLimb.transform.parent = Prompt.Yandere.LimbParent;
				NewLimb.transform.localEulerAngles = new Vector3(0, 0, 0);
			}
			else if (Input.GetKeyDown("c"))
			{
				GameObject NewUniform = Instantiate(BloodyUniform, Prompt.Yandere.transform.position + Prompt.Yandere.transform.forward + new Vector3(0, 1, 0), Quaternion.identity);

				NewUniform.transform.parent = BloodParent;
				NewUniform.transform.localEulerAngles = new Vector3(0, 0, 0);
			}
            else if (Input.GetKeyDown("v"))
            {
                for (this.ID = 21; this.ID < 86; this.ID++)
                {
                    StudentScript student = this.StudentManager.Students[this.ID];

                    if (student != null)
                    {
                        student.BecomeRagdoll();
                        student.DeathType = DeathType.Weapon;
                    }
                }
            }
        }
#endif
	}

	void UpdateHighlight()
	{
		if (this.Row < 0)
		{
			this.Row = 9;
		}
		else if (this.Row > 9)
		{
			this.Row = 0;
		}

		if (this.Column < 0)
		{
			this.Column = 9;
		}
		else if (this.Column > 9)
		{
			this.Column = 0;
		}

		Highlight.localPosition = new Vector3(
			-450 + (100 * Column),
			450 - (100 * Row),
			Highlight.localPosition.z);

		Selected = 1 + (Row * 10) + Column;
	}

	void UpdatePortraits()
	{
		ID = 1;

		while (ID < 98)
		{
			if (StudentManager.Students[ID] != null)
			{
				if (StudentManager.Students[ID].gameObject.activeInHierarchy)
				{
					Portraits[ID].color = new Color(1, 1, 1, 1);
				}
				else
				{
					Portraits[ID].color = new Color(1, 1, 1, .5f);
				}
			}
			else
			{
				Portraits[ID].color = new Color(1, 1, 1, .5f);
			}

			ID++;
		}
	}
}