using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class StalkerPromptScript : MonoBehaviour
{
    public StalkerYandereScript Yandere;
    public SmoothLookAtScript Cat;
    public StalkerScript Stalker;

    public GameObject StairBlocker;
    public GameObject FrontDoor;
    public GameObject Father;
    public GameObject Mother;

    public AudioSource MyAudio;

	public UISprite MySprite;

    public Renderer Darkness;

    public Transform CatCage;
    public Transform Door;

    public bool ServedPurpose;
    public bool OpenDoor;

    public float TargetRotation = 5.5f;
    public float Rotation;
    public float Alpha;
    public float Speed;

    public int ID;

	void Update()
	{
		transform.LookAt(Yandere.MainCamera.transform);

		if (Vector3.Distance(transform.position, Yandere.transform.position) < 5)
		{
            if (!ServedPurpose)
            {
			    Alpha = Mathf.MoveTowards(Alpha, 1, Time.deltaTime);
            }
            else
            {
                Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, Yandere.transform.position) < 2 && !ServedPurpose)
			{
				if (Input.GetButtonDown(InputNames.Xbox_A))
				{
					if (ID == 1)
					{
						Yandere.MyAnimation.CrossFade("f02_climbTrellis_00");
						Yandere.Climbing = true;
						Yandere.CanMove = false;
						Destroy(gameObject);
						Destroy(MySprite);
					}
                    else if (ID == 2)
                    {
                        Stalker.enabled = true;
                        ServedPurpose = true;
                        OpenDoor = true;
                        MyAudio.Play();
                    }
                    else if (ID == 3)
                    {
                        Yandere.MyAnimation["f02_grip_00"].layer = 1;
                        Yandere.MyAnimation.Play("f02_grip_00");
                        Yandere.Object = CatCage;

                        CatCage.parent = Yandere.RightHand;
                        CatCage.localEulerAngles = new Vector3(0, 0, 0);
                        CatCage.localPosition = new Vector3(0.075f, -0.025f, 0.0125f);

                        StairBlocker.SetActive(true);
                        FrontDoor.SetActive(true);

                        Father.SetActive(false);
                        Mother.SetActive(false);

                        Cat.transform.localEulerAngles = new Vector3(0, 0, 0);
                        Cat.enabled = false;

                        MyAudio.Play();

                        Destroy(gameObject);
                        Destroy(MySprite);

                        Stalker.Limit = 10;
                    }
                    else if (ID == 4)
                    {
                        //This is actually the prompt with an ID of 5.
                        CatCage.gameObject.SetActive(true);
                        ServedPurpose = true;
                        OpenDoor = true;
                        MyAudio.Play();
                    }
                    else if (ID == 5)
                    {
                        Yandere.CanMove = false;
                        ServedPurpose = true;
                        OpenDoor = true;
                        MyAudio.Play();
                    }
                }
			}
		}
		else
		{
			Alpha = Mathf.MoveTowards(Alpha, 0, Time.deltaTime);
		}

        if (OpenDoor)
        {
            Speed += Time.deltaTime * .1f;

            Rotation = Mathf.Lerp(Rotation, TargetRotation, Time.deltaTime * Speed);

            Door.transform.localEulerAngles = new Vector3(
                Door.transform.localEulerAngles.x,
                Rotation,
                Door.transform.localEulerAngles.z);

            if (ID == 5)
            {
                Darkness.material.color = new Color(0, 0, 0, Darkness.material.color.a + Time.deltaTime * .33333f);

                if (Darkness.material.color.a >= 1)
                {
                    EventGlobals.OsanaConversation = true;
                    SceneManager.LoadScene(SceneNames.PhoneScene);
                }
            }
        }

		MySprite.color = new Color(1, 1, 1, Alpha);
	}
}