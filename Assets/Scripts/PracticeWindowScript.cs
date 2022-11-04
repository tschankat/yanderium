using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PracticeWindowScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public DialogueWheelScript DialogueWheel;
	public InputManagerScript InputManager;
	public StudentScript SparringPartner;
	public PromptBarScript PromptBar;
	public YandereScript Yandere;
	public WeaponScript Baton;

	public Transform[] KneelSpot;
	public Transform[] SparSpot;

	public string[] Difficulties;
	public Texture[] AlbumCovers;

	public UITexture[] Texture;
	public UILabel[] Label;

	public Transform Highlight;
	public GameObject Window;
	public UISprite Darkness;

	public int Selected = 0;
	public int ClubID = 0;
	public int ID = 1;

	public ClubType Club;

	public bool PlayedRhythmMinigame;
	public bool ButtonUp;
	public bool FadeOut;
	public bool FadeIn;

	public float Timer;

	void Start()
	{
		Window.SetActive(false);
	}

	void Update()
	{
		if (Window.activeInHierarchy)
		{
			if (InputManager.TappedUp)
			{
				Selected--;
				UpdateHighlight();
			}
			else if (InputManager.TappedDown)
			{
				Selected++;
				UpdateHighlight();
			}

			if (ButtonUp)
			{
				if (Input.GetButtonDown("A"))
				{
					UpdateWindow();

					if (Texture[Selected].color.r == 1)
					{
						Yandere.TargetStudent.Interaction = StudentInteractionType.ClubPractice;
						Yandere.TargetStudent.TalkTimer = 100.0f;
						Yandere.TargetStudent.ClubPhase = 2;

						if (Club == ClubType.MartialArts)
						{
							StudentManager.Students[ClubID - Selected].Distracted = true;
						}

						PromptBar.ClearButtons();
						PromptBar.Show = false;

						Window.SetActive(false);

						ButtonUp = false;
					}
				}
				else if (Input.GetButtonDown("B"))
				{
					Yandere.TargetStudent.Interaction = StudentInteractionType.ClubPractice;
					Yandere.TargetStudent.TalkTimer = 100.0f;
					Yandere.TargetStudent.ClubPhase = 3;

					PromptBar.ClearButtons();
					PromptBar.Show = false;

					Window.SetActive(false);

					ButtonUp = false;
				}
			}
			else
			{
				if (Input.GetButtonUp("A"))
				{
					ButtonUp = true;
				}
			}
		}

		if (FadeOut)
		{
			Darkness.enabled = true;

			Darkness.color = new Color(
				Darkness.color.r,
				Darkness.color.g,
				Darkness.color.b,
				Mathf.MoveTowards(Darkness.color.a, 1, Time.deltaTime));

			if (Darkness.color.a == 1.0f)
			{
				if (DialogueWheel.ClubLeader)
				{
					DialogueWheel.End();
				}

				if (Club == ClubType.LightMusic)
				{
					if (!PlayedRhythmMinigame)
					{
						int TempID = 52;

						while (TempID < 56)
						{
							StudentManager.Students[TempID].transform.position = StudentManager.Clubs.List[TempID].position;
							StudentManager.Students[TempID].EmptyHands();
							TempID++;
						}

						Physics.SyncTransforms();

						PlayerPrefs.SetFloat("TempReputation", StudentManager.Reputation.Reputation);

						PlayedRhythmMinigame = true;
						FadeOut = false;
						FadeIn = true;

						SceneManager.LoadScene("RhythmMinigameScene", LoadSceneMode.Additive);

						foreach(GameObject g in SceneManager.GetActiveScene().GetRootGameObjects())
						{
							g.SetActive(false);
						}
					}
				}
				else if (Club == ClubType.MartialArts)
				{
					if (Yandere.CanMove)
					{
						StudentManager.CombatMinigame.Practice = true;

						StudentManager.Students[46].CharacterAnimation.CrossFade(StudentManager.Students[46].IdleAnim);
						StudentManager.Students[46].transform.eulerAngles = new Vector3(0, 0, 0);
						StudentManager.Students[46].transform.position = KneelSpot[0].position;

						StudentManager.Students[46].Pathfinding.canSearch = false;
						StudentManager.Students[46].Pathfinding.canMove = false;
						StudentManager.Students[46].Distracted = true;
						StudentManager.Students[46].enabled = false;
						StudentManager.Students[46].Routine = false;
						StudentManager.Students[46].Hearts.Stop();

						int TempID = 1;

						while (TempID < 5)
						{
							if (StudentManager.Students[46 + TempID] != null)
							{
								if (StudentManager.Students[46 + TempID].Alive)
								{
									StudentManager.Students[46 + TempID].transform.position = KneelSpot[TempID].position;
									StudentManager.Students[46 + TempID].transform.eulerAngles = KneelSpot[TempID].eulerAngles;

									StudentManager.Students[46 + TempID].Pathfinding.canSearch = false;
									StudentManager.Students[46 + TempID].Pathfinding.canMove = false;
									StudentManager.Students[46 + TempID].Distracted = true;
									StudentManager.Students[46 + TempID].enabled = false;
									StudentManager.Students[46 + TempID].Routine = false;

									if (StudentManager.Students[46 + TempID].Male)
									{
										StudentManager.Students[46 + TempID].CharacterAnimation.CrossFade(AnimNames.MaleSit04);
									}
									else
									{
										StudentManager.Students[46 + TempID].CharacterAnimation.CrossFade(AnimNames.FemaleSit05);
									}
								}
							}

							TempID++;
						}

						Yandere.transform.eulerAngles = SparSpot[1].eulerAngles;
						Yandere.transform.position = SparSpot[1].position;
						Yandere.CanMove = false;

						SparringPartner = StudentManager.Students[ClubID - Selected];

						SparringPartner.CharacterAnimation.CrossFade(SparringPartner.IdleAnim);
						SparringPartner.transform.eulerAngles = SparSpot[2].eulerAngles;
						SparringPartner.transform.position = SparSpot[2].position;

						SparringPartner.MyWeapon = Baton;

						SparringPartner.MyWeapon.transform.parent = SparringPartner.WeaponBagParent;
						SparringPartner.MyWeapon.transform.localEulerAngles = new Vector3(0, 0, 0);
						SparringPartner.MyWeapon.transform.localPosition = new Vector3(0, 0, 0);

						SparringPartner.MyWeapon.GetComponent<Rigidbody>().useGravity = false;
						SparringPartner.MyWeapon.FingerprintID = SparringPartner.StudentID;
						SparringPartner.MyWeapon.MyCollider.enabled = false;

						Physics.SyncTransforms();

						FadeOut = false;
						FadeIn = true;
					}
				}
			}
		}

		if (FadeIn)
		{
			Darkness.color = new Color(
				Darkness.color.r,
				Darkness.color.g,
				Darkness.color.b,
				Mathf.MoveTowards(Darkness.color.a, 0, Time.deltaTime));

			if (Darkness.color.a == 0.0f)
			{
				if (Club == ClubType.LightMusic)
				{
					Timer += Time.deltaTime;

					if (Timer > 1)
					{
						Yandere.SetAnimationLayers();

						StudentManager.UpdateAllAnimLayers();
						StudentManager.Reputation.PendingRep += PlayerPrefs.GetFloat("TempReputation");
						PlayerPrefs.SetFloat("TempReputation", 0);

						FadeIn = false;
						Timer = 0;
					}
				}
				else if (Club == ClubType.MartialArts)
				{
					SparringPartner.Pathfinding.canSearch = false;
					SparringPartner.Pathfinding.canMove = false;

					Timer += Time.deltaTime;

					if (Timer > 1)
					{
							 if (Selected == 1){StudentManager.CombatMinigame.Difficulty = 0.50f;}
						else if (Selected == 2){StudentManager.CombatMinigame.Difficulty = 0.75f;}
						else if (Selected == 3){StudentManager.CombatMinigame.Difficulty = 1.00f;}
						else if (Selected == 4){StudentManager.CombatMinigame.Difficulty = 1.50f;}
						else if (Selected == 5){StudentManager.CombatMinigame.Difficulty = 2.00f;}

						StudentManager.Students[ClubID - Selected].Threatened = true;
						StudentManager.Students[ClubID - Selected].Alarmed = true;
						StudentManager.Students[ClubID - Selected].enabled = true;

						FadeIn = false;

						Timer = 0;
					}
				}
			}
		}
	}

	public void Finish()
	{
		int TempID = 1;

		while (TempID < 6)
		{
			if (StudentManager.Students[45 + TempID] != null)
			{
				StudentManager.Students[45 + TempID].Pathfinding.canSearch = true;
				StudentManager.Students[45 + TempID].Pathfinding.canMove = true;
				StudentManager.Students[45 + TempID].Distracted = false;
				StudentManager.Students[45 + TempID].enabled = true;
				StudentManager.Students[45 + TempID].Routine = true;
			}

			TempID++;
		}
	}

	public void UpdateWindow()
	{
		PromptBar.ClearButtons();
		PromptBar.Label[0].text = "Confirm";
		PromptBar.Label[1].text = "Back";
		PromptBar.Label[4].text = "Choose";
		PromptBar.UpdateButtons();
		PromptBar.Show = true;

		if (Club == ClubType.LightMusic)
		{
			Texture[1].mainTexture = AlbumCovers[1];
			Texture[2].mainTexture = AlbumCovers[2];
			Texture[3].mainTexture = AlbumCovers[3];
			Texture[4].mainTexture = AlbumCovers[4];
			Texture[5].mainTexture = AlbumCovers[5];

			Label[1].text = "Panther" + "\n" + Difficulties[1];
			Label[2].text = "?????" + "\n" + Difficulties[2];
			Label[3].text = "?????" + "\n" + Difficulties[3];
			Label[4].text = "?????" + "\n" + Difficulties[4];
			Label[5].text = "?????" + "\n" + Difficulties[5];

			Texture[2].color = new Color(.5f, .5f, .5f, 1);
			Texture[3].color = new Color(.5f, .5f, .5f, 1);
			Texture[4].color = new Color(.5f, .5f, .5f, 1);
			Texture[5].color = new Color(.5f, .5f, .5f, 1);

			Label[2].color = new Color(0, 0, 0, .5f);
			Label[3].color = new Color(0, 0, 0, .5f);
			Label[4].color = new Color(0, 0, 0, .5f);
			Label[5].color = new Color(0, 0, 0, .5f);
		}
		else if (Club == ClubType.MartialArts)
		{
			ClubID = 51;
			ID = 1;

			while (ID < 6)
			{
				string path = "file:///" + Application.streamingAssetsPath +
						"/Portraits/Student_" + (ClubID - ID).ToString() + ".png";

				WWW www = new WWW(path);

				Texture[ID].mainTexture = www.texture;

				Label[ID].text = StudentManager.JSON.Students[ClubID - ID].Name + "\n" + Difficulties[ID];

				if (StudentManager.Students[ClubID - ID] != null)
				{
					if (!StudentManager.Students[ClubID - ID].Routine)
					{
						Debug.Log("A student is not doing their routine.");

						Texture[ID].color = new Color(.5f, .5f, .5f, 1);
						Label[ID].color = new Color(0, 0, 0, .5f);
					}
					else
					{
						Texture[ID].color = new Color(1, 1, 1, 1);
						Label[ID].color = new Color(0, 0, 0, 1);
					}
				}
				else
				{
					Texture[ID].color = new Color(.5f, .5f, .5f, 1);
					Label[ID].color = new Color(0, 0, 0, .5f);
				}

				ID++;
			}

			Texture[5].color = new Color(1, 1, 1, 1);
			Label[5].color = new Color(0, 0, 0, 1);
		}

		Window.SetActive(true);

		UpdateHighlight();
	}

	public void UpdateHighlight()
	{
		if (Selected < 1)
		{
			Selected = 5;
		}
		else if (Selected > 5)
		{
			Selected = 1;
		}

		Highlight.localPosition = new Vector3(
			0,
			660 - (220 * Selected),
			0);
	}
}