using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlphabetScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;
    public InventoryScript Inventory;
    public ClassScript Class;

    public GameObject BodyHidingLockers;
	public GameObject AmnesiaBombBox;
	public GameObject SmokeBombBox;
	public GameObject StinkBombBox;
	public GameObject AmnesiaBomb;
	public GameObject PuzzleCube;
	public GameObject SuperRobot;
	public GameObject SmokeBomb;
	public GameObject StinkBomb;
	public GameObject WeaponBag;
    public GameObject Jukebox;

    public UILabel ChallengeFailed;
	public UILabel TargetLabel;
	public UILabel BombLabel;

    public UITexture BombTexture;

    public Transform LocalArrow;

	public Transform Yandere;

	public int RemainingBombs;
	public int CurrentTarget;

    public bool StopMusic;

    public float LastTime;
    public float Timer;

	public int[] IDs;

    public AudioSource Music;

    void Start()
	{
		if (GameGlobals.AlphabetMode)
		{
			TargetLabel.transform.parent.gameObject.SetActive(true);
			StudentManager.Yandere.NoDebug = true;
			BodyHidingLockers.SetActive(true);
			AmnesiaBombBox.SetActive(true);
			SmokeBombBox.SetActive(true);
			StinkBombBox.SetActive(true);
			SuperRobot.SetActive(true);
			PuzzleCube.SetActive(true);
			WeaponBag.SetActive(true);
            Jukebox.SetActive(false);

            Class.PhysicalGrade = 5;

            Music.Play();

            UpdateText();
        }
		else
		{
            TargetLabel.transform.parent.gameObject.SetActive(false);
			BombLabel.transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
			enabled = false;
		}
	}

	void Update ()
	{
		if (CurrentTarget < IDs.Length)
		{
            if (Input.GetKeyDown("m"))
            {
                if (Music.isPlaying)
                {
                    StopMusic = true;
                    Music.Stop();
                }
                else
                {
                    StopMusic = false;
                    Music.Play();
                }
            }

            if (Music.time < 600 && Music.time > LastTime)
            {
                LastTime = Music.time;
            }

            if (!Music.isPlaying && !StopMusic)
            {
                Music.Play();
                Music.time = LastTime;
            }

            #if UNITY_EDITOR

            if (Input.GetKeyDown("z"))
			{
				StudentManager.Students[IDs[CurrentTarget]].transform.position = new Vector3(60, 5, 130);
				StudentManager.Students[IDs[CurrentTarget]].BecomeRagdoll();
				StudentManager.Students[IDs[CurrentTarget]].DeathType = DeathType.Weapon;
                StudentManager.Students[IDs[CurrentTarget]].Ragdoll.BloodPoolSpawner.enabled = false;
            }

			#endif

			if (this.StudentManager.Yandere.CanMove)
			{
				if (Input.GetButtonDown(InputNames.Xbox_LS) || Input.GetKeyDown(KeyCode.T))
				{
					if (StudentManager.Yandere.Inventory.SmokeBomb)
					{
						Instantiate(SmokeBomb, Yandere.position, Quaternion.identity);
						RemainingBombs--;

						BombLabel.text = "" + RemainingBombs;

						if (RemainingBombs == 0)
						{
							StudentManager.Yandere.Inventory.SmokeBomb = false;
						}
					}
					else if (StudentManager.Yandere.Inventory.StinkBomb)
					{
						Instantiate(StinkBomb, Yandere.position, Quaternion.identity);
						RemainingBombs--;

						BombLabel.text = "" + RemainingBombs;

						if (RemainingBombs == 0)
						{
							StudentManager.Yandere.Inventory.StinkBomb = false;
						}
					}
					else if (StudentManager.Yandere.Inventory.AmnesiaBomb)
					{
						Instantiate(AmnesiaBomb, Yandere.position, Quaternion.identity);
						RemainingBombs--;

						BombLabel.text = "" + RemainingBombs;

						if (RemainingBombs == 0)
						{
							StudentManager.Yandere.Inventory.AmnesiaBomb = false;
						}
					}
				}
			}

			LocalArrow.LookAt(StudentManager.Students[IDs[CurrentTarget]].transform.position);
			transform.eulerAngles = LocalArrow.eulerAngles - new Vector3(0, StudentManager.MainCamera.transform.eulerAngles.y, 0);

			if (StudentManager.Yandere.Attacking && StudentManager.Yandere.TargetStudent.StudentID != IDs[CurrentTarget] ||
                StudentManager.Yandere.Struggling && StudentManager.Yandere.TargetStudent.StudentID != IDs[CurrentTarget] ||
                StudentManager.Police.Show)
			{
				//Debug.Log ("StudentID is: " + StudentManager.Yandere.TargetStudent.StudentID + " and CurrentTarget is: " + IDs[CurrentTarget]);
				ChallengeFailed.enabled = true;
			}

			if (!StudentManager.Students[IDs[CurrentTarget]].Alive)
			{
				CurrentTarget++;

                if (CurrentTarget > 77)
				{
					TargetLabel.text = "Challenge Complete!";

					SceneManager.LoadScene("OsanaJokeScene");
				}
				else
				{
                    UpdateText();
				}
			}

			if (ChallengeFailed.enabled)
			{
				Timer += Time.deltaTime;

				if (Timer > 3)
				{
					SceneManager.LoadScene("LoadingScene");
				}
			}
		}
	}

    public void UpdateText()
    {
        TargetLabel.text = "(" + CurrentTarget + "/77) Current Target: " + StudentManager.JSON.Students[IDs[CurrentTarget]].Name;

        if (RemainingBombs > 0)
        {
            BombLabel.transform.parent.gameObject.SetActive(true);

            if (BombTexture.color.a < 1)
            {
                if (Inventory.StinkBomb)
                {
                    BombTexture.color = new Color(0, .5f, 0, 1);
                }
                else if (Inventory.AmnesiaBomb)
                {
                    BombTexture.color = new Color(1, .5f, 1, 1);
                }
                else
                {
                    BombTexture.color = new Color(.5f, .5f, .5f, 1);
                }
            }
        }
    }
}