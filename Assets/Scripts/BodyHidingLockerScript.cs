using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHidingLockerScript : MonoBehaviour
{
    public StudentManagerScript StudentManager;

	public RagdollScript Corpse;

	public PromptScript Prompt;

	public AudioClip LockerClose;
	public AudioClip LockerOpen;

	public float Rotation;
	public float Speed;

	public Transform Door;

    public int StudentID;

    public bool ABC;

	void Update()
	{
		/////////////////////////
		///// DOOR ROTATION /////
		/////////////////////////

		if (Rotation != 0)
		{
			Speed += Time.deltaTime * 100;

			Rotation = Mathf.MoveTowards(Rotation, 0, Speed * Time.deltaTime);

			if (Rotation > -1)
			{
				AudioSource.PlayClipAtPoint(LockerClose, Prompt.Yandere.MainCamera.transform.position);

                if (Corpse != null)
                {
                    Corpse.gameObject.SetActive(false);
                }

                Prompt.enabled = true;
				Rotation = 0;
				Speed = 0;

                if (ABC)
                {
                    Prompt.Hide();
                    Prompt.enabled = false;

                    enabled = false;
                }
			}

			Door.transform.localEulerAngles = new Vector3(0, Rotation, 0);
		}

		/////////////////////////
		///// HIDING CORPSE /////
		/////////////////////////

        if (Corpse == null)
        {
		    if (Prompt.Yandere.Carrying || Prompt.Yandere.Dragging)
		    {
			    Prompt.enabled = true;

			    if (Prompt.Circle[0].fillAmount == 0)
			    {
                    Prompt.Circle[0].fillAmount = 1;

                    AudioSource.PlayClipAtPoint(LockerOpen, Prompt.Yandere.MainCamera.transform.position);

                    if (Prompt.Yandere.Carrying)
                    {
				        Corpse = Prompt.Yandere.CurrentRagdoll;
                    }
                    else
                    {
                        Corpse = Prompt.Yandere.Ragdoll.GetComponent<RagdollScript>();
                    }

                    Prompt.Label[0].text = "     " + "Remove Corpse";
                    Prompt.Hide();
                    Prompt.enabled = false;

                    Prompt.Yandere.EmptyHands();
				    Prompt.Yandere.NearBodies = 0;
				    Prompt.Yandere.NearestCorpseID = 0;
				    Prompt.Yandere.CorpseWarning = false;
				    Prompt.Yandere.StudentManager.UpdateStudents();

				    Corpse.transform.parent = transform;

				    Corpse.transform.position = transform.position + new Vector3(0, .1f, 0);
				    Corpse.transform.localEulerAngles = new Vector3(0, -90, 0);
                    Corpse.Police.HiddenCorpses++;
                    Corpse.enabled = false;
				    Corpse.Hidden = true;

                    StudentID = Corpse.StudentID;

                    if (ABC)
                    {
                        Corpse.DestroyRigidbodies();
                    }
                    else
                    {
                        Corpse.BloodSpawnerCollider.enabled = false;
                        Corpse.Prompt.MyCollider.enabled = false;
                        Corpse.BloodPoolSpawner.enabled = false;
                        Corpse.DisableRigidbodies();
                    }

                    Corpse.Student.CharacterAnimation.enabled = true;
                    Corpse.Student.CharacterAnimation.Play("f02_lockerPose_00");

                    Rotation = -180;
			    }
		    }
		    else
		    {
			    if (Prompt.enabled)
			    {
				    Prompt.Hide();
				    Prompt.enabled = false;
			    }
		    }
        }
        else
        {
            if (Prompt.Circle[0].fillAmount == 0)
            {
                Prompt.Hide();
                Prompt.enabled = false;
                Prompt.Label[0].text = "     " + "Hide Corpse";

                AudioSource.PlayClipAtPoint(LockerOpen, Prompt.Yandere.MainCamera.transform.position);

                Corpse.enabled = true;
                Corpse.gameObject.SetActive(true);
                Corpse.CharacterAnimation.enabled = false;

                Corpse.transform.localPosition = new Vector3(0, 0, .5f);
                Corpse.transform.localEulerAngles = new Vector3(0, -90, .5f);
                Corpse.transform.parent = null;

                Corpse.BloodSpawnerCollider.enabled = true;
                Corpse.Prompt.MyCollider.enabled = true;
                Corpse.BloodPoolSpawner.NearbyBlood = 0;
                //Corpse.Police.HiddenCorpses--;
                Corpse.EnableRigidbodies();

                if (!Corpse.Cauterized)
                {
                    Corpse.BloodPoolSpawner.enabled = true;
                }

                Corpse = null;

                Rotation = -180;
            }
        }
    }

    public void UpdateCorpse()
    {
        Corpse = StudentManager.Students[StudentID].Ragdoll;
        Corpse.transform.parent = this.transform;

        Prompt.Label[0].text = "     " + "Remove Corpse";
        Prompt.enabled = true;
    }
}