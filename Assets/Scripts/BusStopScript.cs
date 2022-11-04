using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class BusStopScript : MonoBehaviour
{
	#if UNITY_EDITOR
	public PostProcessingProfile Profile;

	public SkinnedMeshRenderer BakerSenpaiRenderer;
	public SkinnedMeshRenderer SenpaiRenderer;
	public CosmeticScript Cosmetic;
	public Texture CasualClothes;
	public MeshRenderer Renderer;
	public Animation SenpaiAnim;
	public AudioSource Jukebox;
	public UILabel Subtitle;

	public Animation BakerySenpai;
	public Animation BakeryAmai;
	public Animation SecondAmai;
	public Animation ThirdAmai;
	public Animation InfoChan;

	public Transform AmaiRightHand;
	public Transform AmaiLeftHand;
	public Transform DonutLid;
	public Transform Target;

	public Transform[] SenpaiBrow;
	public Transform[] SenpaiLip;

	public GameObject UtilityPole;
	public GameObject Amai;

	public Mesh CasualMesh;

	public int RivalEliminationID = 0;
	public int SpeechID = 0;
	public int Phase = 1;

	public float BakeryFocus;
	public float ExtraTimer;
	public float TimeLimit;
	public float Alpha;
	public float Speed;
	public float Timer;
	public float DOF;

	public bool InBakery;
	public bool TreeShot;
	public bool CloseUp;
	public bool NoAnim;

	public AudioClip[] OsanaEliminations;
	public AudioClip[] Speech;
	public AudioSource Audio;

	public string[] EliminationDescriptions;
	public string[] Subtitles;

	void Start ()
	{
		Renderer.material.color = new Color(0, 0, 0, 1);

		transform.position = new Vector3(.375f, .5f, 2.5f);
		transform.eulerAngles = new Vector3(0, 180, 0);

		SecondAmai.gameObject.SetActive(false);
		ThirdAmai.gameObject.SetActive(false);

		SenpaiAnim["sadFace_00"].layer = 1;
		SenpaiAnim.Play("sadFace_00");

		DepthOfFieldModel.Settings DepthSettings = Profile.depthOfField.settings;
		DepthSettings.focusDistance = 1.2f;
		DepthSettings.aperture = 5.6f;
		Profile.depthOfField.settings = DepthSettings;

		RivalEliminationID = GameGlobals.RivalEliminationID;

		Subtitle.text = "";

		if (RivalEliminationID == 0)
		{
			RivalEliminationID = 1;
		}
	}

	void Update ()
	{
		if (Input.GetKeyDown("z"))
		{
			Phase = 99;

			UpdateDOF(.5f);

			transform.position = new Vector3(.1f, 1, 1.33333f);
			transform.eulerAngles = new Vector3(0, -135, 0);

			SecondAmai.gameObject.SetActive(false);
			ThirdAmai.gameObject.SetActive(true);
			Amai.gameObject.SetActive(false);

			if (NoAnim)
			{
				ThirdAmai["FriendshipRival"].speed = 0;
			}

			ThirdAmai["f02_carryBox_00"].layer = 1;
			ThirdAmai.Play("f02_carryBox_00");

			SpeechID = 0;
			Alpha = 0;
			Timer = 0;

			Renderer.material.color = new Color(0, 0, 0, Alpha);
		}

		if (Input.GetKeyDown("x"))
		{
			UpdateDOF(.5f);

			transform.position = new Vector3(.1f, 1, 1.33333f);
			transform.eulerAngles = new Vector3(0, -135, 0);

			SecondAmai.gameObject.SetActive(false);
			ThirdAmai.gameObject.SetActive(true);

			ThirdAmai["FriendshipRival"].time = 0;

			ThirdAmai["f02_carryBox_00"].layer = 1;
			ThirdAmai.Play("f02_carryBox_00");

			Subtitle.text = "";

			SpeechID = 0;
			Timer = 0;
			Phase = 0;
		}

		if (Phase == 1)
		{
			Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime * .2f);
			Renderer.material.color = new Color(0, 0, 0, Alpha);

			if (Alpha == 0)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = Subtitles[0];
					Audio.clip = Speech[0];
					Audio.Play();
					SpeechID++;
				}
				else
				{
					Timer += Time.deltaTime;

					if (Timer > 6)
					{
						Subtitle.text = "";
					}
				}
			}

			if (SenpaiRenderer.sharedMesh != CasualMesh)
			{
				SenpaiRenderer.sharedMesh = CasualMesh;
				SenpaiRenderer.materials[0].mainTexture = CasualClothes;
				SenpaiRenderer.materials[1].mainTexture = Cosmetic.SkinTextures[Cosmetic.SkinID];
			}

			if (BakerSenpaiRenderer.sharedMesh != CasualMesh)
			{
				BakerSenpaiRenderer.sharedMesh = CasualMesh;
				BakerSenpaiRenderer.materials[0].mainTexture = CasualClothes;
				BakerSenpaiRenderer.materials[1].mainTexture = Cosmetic.SkinTextures[Cosmetic.SkinID];
			}

			transform.position += new Vector3(0, 0, Speed * Time.deltaTime);

			Amai.transform.position -= new Vector3(1 * Time.deltaTime, 0, 0);

			if (Amai.transform.position.x < -2)
			{
				SecondAmai.gameObject.SetActive(true);

				if (SecondAmai["f02_motherRecipe_00"].speed == 1)
				{
					SecondAmai["f02_motherRecipe_00"].speed = .66666f;
					SecondAmai["f02_motherRecipe_00"].time = 16;
					SecondAmai["f02_carry_00"].layer = 1;
					SecondAmai.Play("f02_carry_00");
				}
			}

			if (Input.GetKeyDown("space"))
			{
				Renderer.material.color = new Color(0, 0, 0, 0);
				Amai.transform.position = new Vector3(-11, 0, 0);
			}

			//Switch to Senpai
			if (Amai.transform.position.x < -10f)
			{
				UpdateDOF(.45f);

				transform.position = new Vector3(-.1f, 1, 1.66666f);
				transform.eulerAngles = new Vector3(0, 135, 0);

				Amai.SetActive(false);

				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"Um...excuse me..."
		else if (Phase == 2)
		{
			Timer += Time.deltaTime;

			if (Timer > 1)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = Subtitles[1];

					Audio.clip = Speech[1];
					Audio.Play();
					SpeechID++;
				}
			}

			if (Timer > 2.275f)
			{
				SenpaiAnim.CrossFade("noticeAmai_00");
				SenpaiAnim["sadFace_00"].weight = 0;
			}

			if (Input.GetKeyDown("space"))
			{
				SenpaiAnim.CrossFade("noticeAmai_00");
				SenpaiAnim["sadFace_00"].weight = 0;

				Timer = 8;
			}

			//Switch to Amai
			if (Timer > 7)
			{
				UpdateDOF(.5f);

				transform.position = new Vector3(.1f, 1, 1.33333f);
				transform.eulerAngles = new Vector3(0, -135, 0);

				SecondAmai.gameObject.SetActive(false);
				ThirdAmai.gameObject.SetActive(true);

				ThirdAmai["FriendshipRival"].time = 2;

				if (NoAnim)
				{
					ThirdAmai["FriendshipRival"].speed = 0;
				}

				ThirdAmai["f02_carryBox_00"].layer = 1;
				ThirdAmai.Play("f02_carryBox_00");

				Subtitle.text = "";

				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"I couldn't help but notice that you seem quite sad about something. Would you like to talk about it?"
		else if (Phase == 3)
		{
			Timer += Time.deltaTime;

			if (Timer > .5f)
			{
				Subtitle.text = Subtitles[2];
			}

			if (Input.GetKeyDown("space")){Timer = 8;}

			//Switch to Senpai
			if (Timer > 7.5f)
			{
				UpdateDOF(.45f);

				transform.position = new Vector3(-.1f, 1, 1.66666f);
				transform.eulerAngles = new Vector3(0, 135, 0);

				Subtitle.text = "";
				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"Huh? Um...have we...met before?"
		else if (Phase == 4)
		{
			Timer += Time.deltaTime;

			if (Timer > .5f)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = Subtitles[3];

					Audio.clip = Speech[2];
					Audio.Play();
					SpeechID++;
				}
			}

			if (Input.GetKeyDown("space")){Timer = 7;}

			//Switch to Amai
			if (Timer > 6)
			{
				UpdateDOF(.5f);

				transform.position = new Vector3(.1f, 1, 1.33333f);
				transform.eulerAngles = new Vector3(0, -135, 0);

				ThirdAmai["FriendshipRival"].time = 77;

				DonutLid.parent.parent = null;
				ThirdAmai["f02_carryBox_00"].weight = 0;

				Subtitle.text = "";
				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"Oh! I'm sorry, my name is Amai Odayaka..."
		else if (Phase == 5)
		{
			Timer += Time.deltaTime;

			if (Timer > 0)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = Subtitles[4];

					Audio.clip = Speech[3];
					Audio.Play();
					SpeechID++;
				}
			}

			if (Timer > 2)
			{
				if (SpeechID == 1)
				{
					Subtitle.text = Subtitles[5];

					ThirdAmai["FriendshipRival"].time = 80;
					SpeechID++;
				}
			}

			if (Timer > 6.75f)
			{
				if (SpeechID == 2)
				{
					Subtitle.text = Subtitles[6];

					ThirdAmai["FriendshipRival"].time = 81;
					SpeechID++;
				}
			}

			if (Timer > 16.75f)
			{
				if (SpeechID == 3)
				{
					Subtitle.text = Subtitles[7];

					ThirdAmai["FriendshipRival"].time = 94;
					SpeechID++;
				}
			}

			if (Input.GetKeyDown("space")){Timer = 21;}

			//Switch to Senpai
			if (Timer > 20)
			{
				UpdateDOF(.45f);

				transform.position = new Vector3(-.1f, 1, 1.66666f);
				transform.eulerAngles = new Vector3(0, 135, 0);

				ThirdAmai["FriendshipRival"].time = 92;

				ThirdAmai["f02_carryBox_00"].weight = 1;
				DonutLid.parent.parent = AmaiRightHand;
				DonutLid.parent.localPosition = new Vector3 (.125f, -.19f, -.03f);
				DonutLid.parent.localEulerAngles = new Vector3 (-15, 90, 90);

				SenpaiAnim.Play("returnToSad_00");

				UtilityPole.SetActive(false);

				Subtitle.text = "";
				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//Senpai talks about Osana's death.
		else if (Phase == 6)
		{
			Timer += Time.deltaTime;

			if (Timer > 5)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = EliminationDescriptions[RivalEliminationID];

					Audio.clip = OsanaEliminations[RivalEliminationID];
					Audio.Play();

					TimeLimit = OsanaEliminations[RivalEliminationID].length;
					SpeechID++;
				}
			}

			Speed += Time.deltaTime * .01f;

			transform.position = Vector3.Lerp(
				transform.position,
				new Vector3(.375f, .55f, 1.75f),
				Time.deltaTime * Speed);

			transform.LookAt(Target);

			if (Input.GetKeyDown("space")){Timer = TimeLimit + 6;}

			//Switch to Amai
			if (Timer > TimeLimit + 6)
			{
				UpdateDOF(.5f);

				transform.position = new Vector3(.1f, 1, 1.33333f);
				transform.eulerAngles = new Vector3(0, -135, 0);

				UtilityPole.SetActive(true);

				Subtitle.text = "";
				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"Oh...I...I'm so sorry to hear about that..."
		else if (Phase == 7)
		{
			Timer += Time.deltaTime;

			if (Timer > 2)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = Subtitles[8];

					ThirdAmai["FriendshipRival"].time = 135;
					Audio.clip = Speech[4];
					Audio.Play();

					SpeechID++;
				}
			}

			if (Input.GetKeyDown("space")){Timer = 15;}

			//Switch to Senpai
			if (Timer > 14)
			{
				UpdateDOF(.45f);

				transform.position = new Vector3(-.1f, 1, 1.66666f);
				transform.eulerAngles = new Vector3(0, 135, 0);

				SenpaiAnim.Play("noticeDonuts_00");

				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"Hey...it's not much, but...maybe this will take your mind off of it for a moment?"
		else if (Phase == 8)
		{
			Timer += Time.deltaTime;

			if (Timer > .5f)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = Subtitles[9];

					Audio.clip = Speech[5];
					Audio.Play();

					SpeechID++;
				}
			}

			if (Input.GetKeyDown("space")){Timer = 10;}

			//Switch to donut box
			if (Timer > 8.5f)
			{
				CloseUp = true;
				UpdateDOF(.4f);

				transform.position = new Vector3(.1f, 1, 1.33333f);
				transform.eulerAngles = new Vector3(0, -135, 0);

				ThirdAmai.transform.position = new Vector3(.5f, 0, .81f);
				ThirdAmai.transform.eulerAngles = new Vector3(0, 45, 0);
				DonutLid.localEulerAngles = new Vector3(-6, 180, 180);

				transform.parent = DonutLid.parent;
				transform.localPosition = new Vector3(0, 2, 1.75f);
				transform.localEulerAngles = new Vector3(45, 180, 0);

				Subtitle.text = "";
				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"Oh...um...thank you."
		//"Go ahead, take one!"
		else if (Phase == 9)
		{
			transform.Translate(0, 0, Time.deltaTime * -.01f);

			Timer += Time.deltaTime;

			if (Timer > .5f)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = Subtitles[10];

					Audio.clip = Speech[6];
					Audio.Play();

					SpeechID++;
				}
			}

			if (Timer > 5f)
			{
				if (SpeechID == 1)
				{
					Subtitle.text = Subtitles[11];

					Audio.clip = Speech[7];
					Audio.Play();

					SpeechID++;
				}
			}

			if (Input.GetKeyDown("space")){Timer = 10;}

			//Switch to artistic shot of trees
			if (Timer > 8.5f)
			{
				CloseUp = false;
				TreeShot = true;
				UpdateDOF(1);

				transform.parent = null;
				transform.position = new Vector3(0, 2, 0);
				transform.eulerAngles = new Vector3(-35, 180, 0);

				Subtitle.text = "";
				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"Mm...it's good!"
		//"Of course! Nobody makes better pastries than my mom and dad!"
		else if (Phase == 10)
		{
			Timer += Time.deltaTime;

			if (Timer > 1)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = Subtitles[12];

					Audio.clip = Speech[8];
					Audio.Play();

					SpeechID++;
				}
			}

			if (Timer > 4.5f)
			{
				if (SpeechID == 1)
				{
					Subtitle.text = Subtitles[13];

					Audio.clip = Speech[9];
					Audio.Play();

					SpeechID++;
				}
			}

			if (Input.GetKeyDown("space")){Timer = 11;}

			//Switch to Amai
			if (Timer > 9.5f)
			{
				TreeShot = false;
				UpdateDOF(.5f);

				transform.position = new Vector3(.1f, 1, 1.33333f);
				transform.eulerAngles = new Vector3(0, -135, 0);

				ThirdAmai.transform.position = new Vector3(.33333f, 0, 1.5f);
				ThirdAmai.transform.eulerAngles = new Vector3(0, 0, 0);

				DonutLid.localEulerAngles = new Vector3(-90, 0, 0);

				ThirdAmai["FriendshipRival"].time = 145;

				Subtitle.text = Subtitles[14];

				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"Actually, I'm heading to my parents' bakery right now. Would you like to come with me?
		//It'll take your mind off things for a while!"
		else if (Phase == 11)
		{
			Timer += Time.deltaTime;

			if (Input.GetKeyDown("space")){Timer = 10;}

			//Switch to Senpai
			if (Timer > 9)
			{
				UpdateDOF(.45f);

				transform.position = new Vector3(-.1f, 1, 1.66666f);
				transform.eulerAngles = new Vector3(0, 135, 0);

				SenpaiAnim.Play("noticeAmai_00");
				SenpaiAnim["noticeAmai_00"].time = SenpaiAnim["noticeAmai_00"].length;

				Subtitle.text = "";
				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"Oh...no, it's okay...I wouldn't want to be a bother..."
		else if (Phase == 12)
		{
			Timer += Time.deltaTime;

			if (Timer > .5f)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = Subtitles[15];

					Audio.clip = Speech[10];
					Audio.Play();

					SpeechID++;
				}
			}

			if (Input.GetKeyDown("space")){Timer = 8;}

			//Switch to Amai
			if (Timer > 7)
			{
				UpdateDOF(.5f);

				transform.position = new Vector3(.1f, 1, 1.33333f);
				transform.eulerAngles = new Vector3(0, -135, 0);

				ThirdAmai["FriendshipRival"].time = 2;

				Subtitle.text = "";
				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"It wouldn't be a bother at all! If I just leave you here, I'll worry about you!"
		else if (Phase == 13)
		{
			Timer += Time.deltaTime;

			if (Timer > .5f)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = Subtitles[16];

					Audio.clip = Speech[11];
					Audio.Play();

					SpeechID++;
				}
			}

			if (Input.GetKeyDown("space")){Timer = 8;}

			//Switch to Senpai
			if (Timer > 6.5f)
			{
				UpdateDOF(.45f);

				transform.position = new Vector3(-.1f, 1, 1.66666f);
				transform.eulerAngles = new Vector3(0, 135, 0);

				SenpaiAnim.Play("draggedByAmai_00");
				SenpaiAnim["sadFace_00"].weight = 0;

				Subtitle.text = "";
				SpeechID = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"Heh. Well, I guess it would be better than moping around."
		else if (Phase == 14)
		{
			Timer += Time.deltaTime;

			if (Timer > .5f)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = Subtitles[17];

					Audio.clip = Speech[12];
					Audio.Play();

					SpeechID++;
				}
			}

			if (Input.GetKeyDown("space")){Timer = 12;}

			//Switch to shot of both characters at bench
			/*
			if (Timer > 11)
			{
				UpdateDOF(1.2f);

				transform.position = new Vector3(0, 1, 3.2f);
				transform.eulerAngles = new Vector3(0, 180, 0);

				SenpaiAnim.Play("draggedByAmai_00");
				SenpaiAnim["draggedByAmai_00"].time = 11;

				ThirdAmai.transform.position = SenpaiAnim.transform.position;
				ThirdAmai.transform.eulerAngles = SenpaiAnim.transform.eulerAngles;
				ThirdAmai["f02_carryBox_00"].weight = 0;
				ThirdAmai.Play("f02_amaiDrag_00");

				DonutLid.parent.parent = AmaiLeftHand;
				DonutLid.parent.localScale = new Vector3(.175f, .175f, .175f);
				DonutLid.parent.localPosition = new Vector3(-.1f, -.19f, -.015f);
				DonutLid.parent.localEulerAngles = new Vector3(90, 90, 0);

				SpeechID = 0;
				Alpha = 0;
				Timer = 0;
				Phase++;
			}
			*/

			//Switch to Amai
			if (Timer > 11)
			{
				UpdateDOF(.5f);

				transform.position = new Vector3(.1f, 1, 1.33333f);
				transform.eulerAngles = new Vector3(0, -135, 0);

				SenpaiAnim.gameObject.SetActive(false);

				ThirdAmai["FriendshipRival"].time = 145;

				Subtitle.text = "";
				SpeechID = 0;
				Alpha = 0;
				Timer = 0;
				Phase++;
			}
		}
		//"Then let's get going!"
		else if (Phase == 15)
		{
			Timer += Time.deltaTime;

			if (Timer > .5f)
			{
				if (SpeechID == 0)
				{
					Subtitle.text = Subtitles[18];

					Audio.clip = Speech[13];
					Audio.Play();

					SpeechID++;
				}
			}

			if (Input.GetKeyDown("space")){Timer = 3;}

			//Fade to black
			if (Timer > 3)
			{
				Alpha = Mathf.MoveTowards(Alpha, 1, Time.deltaTime * .5f);

				Renderer.material.color = new Color(0, 0, 0, Alpha);

				Subtitle.text = "";

				if (Timer > 6)
				{
					Phase = 20;
				}
			}
		}

		//////////////////////////////////////////////////
		//////////////////////////////////////////////////
		//////////////////////////////////////////////////
		//////////////////////////////////////////////////
		//////////////////////////////////////////////////
		///// Senpai and Amai talk in Amai's bakery. /////
		//////////////////////////////////////////////////
		//////////////////////////////////////////////////
		//////////////////////////////////////////////////
		//////////////////////////////////////////////////
		//////////////////////////////////////////////////
		 
		else if (Phase == 20)
		{
			transform.position = new Vector3(-.75f, 1.1f, 7.75f);
			transform.eulerAngles = new Vector3(0, 30, 0);

			Renderer.material.color = new Color(0, 0, 0, 1);
			Alpha = 1;

			InBakery = true;

			BakerySenpai.Play();
			BakeryAmai.Play();

			Jukebox.Play();

			UpdateDOF(1);

			Speed = 0;
			Timer = 0;

			Phase++;
		}
		else if (Phase == 21)
		{
			Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime * .5f);

			Renderer.material.color = new Color(0, 0, 0, Alpha);

			Timer += Time.deltaTime;

			if (Timer > 13.5f)
			{
				LipValue = SenpaiLip[0].localPosition.y;
				Smile = true;
			}

			if (Timer > 15)
			{
				//BakerySenpai["bakeryTalk_00"].speed = 1;
				//BakeryAmai["f02_bakeryTalk_00"].speed = 1;

				Speed += Time.deltaTime * .1f;
			}

			BakeryFocus = Mathf.Lerp(
				BakeryFocus,
				1.5f,
				Speed * Time.deltaTime);

			UpdateDOF(1);

			transform.position = Vector3.Lerp(
				transform.position,
				new Vector3(-1.939f, 1.4f, 5.69f),
				Speed * Time.deltaTime);

			if (Speed > 1)
			{
				InfoChan.CrossFade("f02_infoSnapPhoto_00", 1);
			}

			if (Timer > 30.5f)
			{
				Alpha = 1;
			}

			if (BakerySenpai["bakeryTalk_00"].time >= BakerySenpai["bakeryTalk_00"].length - 1)
			{
				if (Alpha < 1)
				{
					ExtraTimer += Time.deltaTime;
					//Debug.Log(ExtraTimer);
				}

				BakerySenpai.CrossFade("carefreeTalk_02", 1);
				BakeryAmai.CrossFade("f02_carefreeTalk_02", 1);

				BakerySenpai["f02_smile_00"].layer = 1;
				BakerySenpai.Play("f02_smile_00");
			}

			if (Timer > 35)
			{
				DateGlobals.Week = 2;
				DateGlobals.Weekday = DayOfWeek.Saturday;
				SceneManager.LoadScene(SceneNames.CalendarScene);
			}
		}

		/////////////////////
		/////////////////////
		/////////////////////
		/////////////////////
		/////////////////////
		///// Debugging /////
		/////////////////////
		/////////////////////
		/////////////////////
		/////////////////////
		/////////////////////

		else if (Phase == 99)
		{
			if (Input.GetKeyDown("right"))
			{
				ThirdAmai["FriendshipRival"].time++;
				Debug.Log("Time is: " + ThirdAmai["FriendshipRival"].time);
			}

			if (Input.GetKeyDown("left"))
			{
				ThirdAmai["FriendshipRival"].time--;
				Debug.Log("Time is: " + ThirdAmai["FriendshipRival"].time);
			}

			if (Input.GetKeyDown("x"))
			{
				if (DonutLid.parent.parent != null)
				{
					DonutLid.parent.parent = null;
					ThirdAmai["f02_carryBox_00"].weight = 0;
				}
				else
				{
					ThirdAmai["f02_carryBox_00"].weight = 1;
					DonutLid.parent.parent = AmaiRightHand;
					DonutLid.parent.localPosition = new Vector3 (.125f, -.19f, -.03f);
					DonutLid.parent.localEulerAngles = new Vector3 (-15, 90, 90);
				}
			}
		}

		if (Input.GetKeyDown("-"))
		{
			Time.timeScale--;
		}

		if (Input.GetKeyDown("="))
		{
			Time.timeScale++;
		}

		Jukebox.pitch = Time.timeScale;
	}

	public bool Smile = false;
	public float Strength = 0;
	public float LipValue = -.08f;

	void LateUpdate()
	{
		SenpaiBrow[0].localPosition = new Vector3(-.025f, .025f, 0);
		SenpaiBrow[0].localEulerAngles = new Vector3(0, 0, 22.5f);

		SenpaiBrow[1].localPosition = new Vector3(.025f, .025f, 0);
		SenpaiBrow[1].localEulerAngles = new Vector3(0, 0, -22.5f);

		if (Smile)
		{
			Strength += Time.deltaTime;
			LipValue = Mathf.Lerp(LipValue, -.06f, Time.deltaTime * Strength);

			SenpaiLip[0].localPosition = new Vector3(SenpaiLip[0].localPosition.x, LipValue, SenpaiLip[0].localPosition.z);
			SenpaiLip[1].localPosition = new Vector3(SenpaiLip[1].localPosition.x, LipValue, SenpaiLip[1].localPosition.z);
		}
	}

	void UpdateDOF(float Focus)
	{
		DepthOfFieldModel.Settings DepthSettings = Profile.depthOfField.settings;

		if (CloseUp)
		{
			DepthSettings.focusDistance = .33333f;//Focus;
		}
		else if (TreeShot)
		{
			DepthSettings.focusDistance = 1;//Focus;
		}
		else if (InBakery)
		{
			DepthSettings.focusDistance = BakeryFocus;//Focus;
		}
		else
		{
			DepthSettings.focusDistance = DOF;//Focus;
		}

		//DepthSettings.focusDistance = Focus;

		Profile.depthOfField.settings = DepthSettings;
	}
	#endif
}