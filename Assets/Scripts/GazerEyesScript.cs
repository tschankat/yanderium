using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazerEyesScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
	public YandereScript Yandere;

	public GameObject FemaleBloodyScream;
	public GameObject MaleBloodyScream;
	public GameObject ParticleEffect;
	public GameObject Laser;

	public SkinnedMeshRenderer[] Eyes;

	public float[] BlinkStrength;

	public Texture[] EyeTextures;

	public bool[] Blink;

	public float RandomNumber;
	public float AnimTime;

	public bool Attacking;

	public int Effect;
	public int ID;

	void Start()
	{
		GetComponent<Animation>()["Eyeballs_Run"].speed = 0;
		GetComponent<Animation>()["Eyeballs_Walk"].speed = 0;
		GetComponent<Animation>()["Eyeballs_Idle"].speed = 0;
	}

	void Update ()
	{
		this.StudentManager.UpdateStudents();

		if (Attacking == false)
		{
			AnimTime += Time.deltaTime;

			if (AnimTime > 144)
			{
				AnimTime = 0;
			}
		}
		else
		{
			if (AnimTime < 72)
			{
				AnimTime = Mathf.Lerp(AnimTime, 0, Time.deltaTime * 1.44f * 5);
			}
			else
			{
				AnimTime = Mathf.Lerp(AnimTime, 144, Time.deltaTime * 1.44f * 5);
			}
		}

		GetComponent<Animation>()["Eyeballs_Run"].time = AnimTime;
		GetComponent<Animation>()["Eyeballs_Walk"].time = AnimTime;
		GetComponent<Animation>()["Eyeballs_Idle"].time = AnimTime;

		ID = 0;

		while (ID < Eyes.Length)
		{
			if (BlinkStrength[ID] == 0)
			{
				RandomNumber = Random.Range(1, 101);
			}

			if (RandomNumber == 1)
			{
				Blink[ID] = true;
			}

			if (Blink[ID] == true)
			{
				BlinkStrength [ID] = Mathf.MoveTowards(BlinkStrength[ID], 100, Time.deltaTime * 1000);
				Eyes[ID].SetBlendShapeWeight(0, BlinkStrength[ID]);

				if (BlinkStrength[ID] == 100)
				{
					Blink[ID] = false;
				}
			}
			else
			{
				if (BlinkStrength[ID] > 0)
				{
					BlinkStrength [ID] = Mathf.MoveTowards(BlinkStrength[ID], 0, Time.deltaTime * 1000);
					Eyes[ID].SetBlendShapeWeight(0, BlinkStrength[ID]);
				}
			}

			ID++;
		}

		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");

		if (Yandere.CanMove)
		{
			if ((v != 0.0f) || (h != 0.0f))
			{
				if (Input.GetButton(InputNames.Xbox_LB))
				{
					GetComponent<Animation>().CrossFade("Eyeballs_Run", 1);
				}
				else
				{
					GetComponent<Animation>().CrossFade("Eyeballs_Walk", 1);
				}
			}
			else
			{
				GetComponent<Animation>().CrossFade("Eyeballs_Idle", 1);
			}
		}
	}

	public void ChangeEffect()
	{
		Effect++;

		if (Effect == EyeTextures.Length)
		{
			Effect = 0;
		}

		ID = 0;

		while (ID < Eyes.Length)
		{
			Instantiate(ParticleEffect, Eyes [ID].transform.position, Quaternion.identity);
			Eyes[ID].material.mainTexture = EyeTextures[Effect];
			ID++;
		}
	}

	public bool Shinigami;

	public void Attack()
	{
		if (!this.Shinigami)
		{
			ID = 0;

			while (ID < Eyes.Length)
			{
				GameObject NewLaser = Instantiate(Laser, Eyes[ID].transform.position, Quaternion.identity);
				NewLaser.transform.LookAt(Yandere.TargetStudent.Hips.position + new Vector3(0, .33333f, 0));
				NewLaser.transform.localScale = new Vector3(1, 1, Vector3.Distance(Eyes[ID].transform.position, Yandere.TargetStudent.Hips.position + new Vector3(0, .33333f, 0)) * .5f);

				ID++;
			}
		}

		//Red eyes. Combustion.
		if (this.Effect == 0)
		{
			this.Yandere.TargetStudent.Combust();
		}
		//Yellow eyes. Electrocute.
		else if (this.Effect == 1)
		{
			this.ElectrocuteStudent(Yandere.TargetStudent);

			//Target.ElectroSteam[0].SetActive(true);
			//Target.ElectroSteam[1].SetActive(true);
			//Target.ElectroSteam[2].SetActive(true);
			//Target.ElectroSteam[3].SetActive(true);
		}
		//Blue eyes. Explosion.
		else if (this.Effect == 2)
		{
			Instantiate(this.Yandere.FalconPunch, this.Yandere.TargetStudent.transform.position + new Vector3(0, .5f, 0) - (this.Yandere.transform.forward * .5f), Quaternion.identity);
		}
		//Purple eyes. Ebola.
		else if (this.Effect == 3)
		{
			Instantiate(this.Yandere.EbolaEffect,
				this.Yandere.TargetStudent.transform.position + Vector3.up, Quaternion.identity);
			this.Yandere.TargetStudent.SpawnAlarmDisc();
			this.Yandere.TargetStudent.DeathType = DeathType.Poison;

			this.Yandere.TargetStudent.BecomeRagdoll();
		}
		//Pink eyes. Dismemberment.
		else if (this.Effect == 4)
		{
			if (this.Yandere.TargetStudent.Male)
			{
				Instantiate(MaleBloodyScream, this.Yandere.TargetStudent.transform.position + new Vector3 (0, 1, 0), Quaternion.identity);
			}
			else
			{
				Instantiate(FemaleBloodyScream, this.Yandere.TargetStudent.transform.position + new Vector3 (0, 1, 0), Quaternion.identity);
			}

			this.Yandere.TargetStudent.BecomeRagdoll();
			this.Yandere.TargetStudent.Ragdoll.Dismember();
		}
		//Grey eyes. Petrification.
		else if (this.Effect == 5)
		{
			this.Yandere.TargetStudent.TurnToStone();
		}
		//Black eyes. Suicide.
		/*
		else if (this.Effect == 6)
		{
			this.Yandere.TargetStudent.MurderSuicidePhase = 1;
			this.Yandere.TargetStudent.Pathfinding.speed = 1;

			this.Yandere.TargetStudent.Routine = false;
			this.Yandere.TargetStudent.Suicide = true;
		}

		this.Yandere.TargetStudent.Prompt.Hide();
		this.Yandere.TargetStudent.Prompt.enabled = false;
		*/
	}

	public void ElectrocuteStudent(StudentScript Target)
	{
		Target.EmptyHands();

		if (Target.Investigating)
		{
			Target.StopInvestigating();
		}

		Target.CharacterAnimation[Target.ElectroAnim].speed = 0.85f;
		Target.CharacterAnimation[Target.ElectroAnim].time = 2;
		Target.CharacterAnimation.CrossFade(Target.ElectroAnim);

        Target.CharacterAnimation[Target.WetAnim].weight = 0.0f;

        Target.Pathfinding.canSearch = false;
		Target.Pathfinding.canMove = false;
		Target.EatingSnack = false;
		Target.Electrified = true;
		Target.Fleeing = false;
		Target.Routine = false;
		Target.Dying = true;

		if (Target.Following)
		{
            Target.Yandere.Follower = null;
            Target.Yandere.Followers--;
			Target.Following = false;
		}

		Target.Police.CorpseList[Target.Police.Corpses] = Target.Ragdoll;
		Target.Police.Corpses++;

		GameObjectUtils.SetLayerRecursively(Target.gameObject, 11);
		Target.tag = "Blood";

		Target.Ragdoll.ElectrocutionAnimation = true;
		Target.Ragdoll.Disturbing = true;

		Target.MurderSuicidePhase = 100;
		Target.SpawnAlarmDisc();

		GameObject NewElectricity1 = Instantiate(StudentManager.LightSwitch.Electricity, Target.transform.position, Quaternion.identity);
		NewElectricity1.transform.parent = Target.BoneSets.RightArm;
		NewElectricity1.transform.localPosition = Vector3.zero;

		GameObject NewElectricity2 = Instantiate(StudentManager.LightSwitch.Electricity, Target.transform.position, Quaternion.identity);
		NewElectricity2.transform.parent = Target.BoneSets.LeftArm;
		NewElectricity2.transform.localPosition = Vector3.zero;

		GameObject NewElectricity3 = Instantiate(StudentManager.LightSwitch.Electricity, Target.transform.position, Quaternion.identity);
		NewElectricity3.transform.parent = Target.BoneSets.RightLeg;
		NewElectricity3.transform.localPosition = Vector3.zero;

		GameObject NewElectricity4 = Instantiate(StudentManager.LightSwitch.Electricity, Target.transform.position, Quaternion.identity);
		NewElectricity4.transform.parent = Target.BoneSets.LeftLeg;
		NewElectricity4.transform.localPosition = Vector3.zero;

		GameObject NewElectricity5 = Instantiate(StudentManager.LightSwitch.Electricity, Target.transform.position, Quaternion.identity);
		NewElectricity5.transform.parent = Target.BoneSets.Head;
		NewElectricity5.transform.localPosition = Vector3.zero;

		GameObject NewElectricity6 = Instantiate(StudentManager.LightSwitch.Electricity, Target.transform.position, Quaternion.identity);
		NewElectricity6.transform.parent = Target.Hips;
		NewElectricity6.transform.localPosition = Vector3.zero;

		AudioSource.PlayClipAtPoint(StudentManager.LightSwitch.Flick[2], Target.transform.position + Vector3.up);
	}
}