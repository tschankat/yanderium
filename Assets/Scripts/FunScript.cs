using UnityEngine;
using UnityEngine.SceneManagement;

public class FunScript : MonoBehaviour
{
	public TypewriterEffect Typewriter;
	public GameObject Controls;
	public GameObject Skip;
	public Texture[] Portraits;
	public string[] Lines;
	public UITexture Girl;
	public UILabel Label;
	public float OutroTimer = 0.0f;
	public float Timer = 0.0f;
	public int DebugNumber = 0;
	public int ID = 0;

	public bool VeryFun;

	public float R = 1.0f;
	public float G = 1.0f;
	public float B = 1.0f;

	public string Text;

	void Start()
	{
		if (PlayerPrefs.GetInt("DebugNumber") > 0)
		{
			if (PlayerPrefs.GetInt("DebugNumber") > 10)
			{
				PlayerPrefs.SetInt("DebugNumber", 0);
			}

			DebugNumber = PlayerPrefs.GetInt("DebugNumber");
		}

		if (VeryFun)
		{
			if (DebugNumber != -1)
			{
				Text = "" + DebugNumber;
			}
			else
			{
				Text = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/Fun.txt");
			}

				 if (Text == "0"){this.ID = 0;}
			else if (Text == "1"){this.ID = 1;}
			else if (Text == "2"){this.ID = 2;}
			else if (Text == "3"){this.ID = 3;}
			else if (Text == "4"){this.ID = 4;}
			else if (Text == "5"){this.ID = 5;}
			else if (Text == "6"){this.ID = 6;}
			else if (Text == "7"){this.ID = 7;}
			else if (Text == "8"){this.ID = 8;}
			else if (Text == "9"){this.ID = 9;}
			else if (Text == "10"){this.ID = 10;}
			else if (Text == "69")
			{
				Label.text = "( ͡° ͜ʖ ͡°) ";
				this.ID = 8;
			}
			else if (Text == "666")
			{
				Label.text = "Sometimes, I lie. It's just too fun. You eat up everything I say. I wonder what else I can trick you into believing? ";
				Girl.color = new Color(1, 0, 0, 0);
				Label.color = new Color(1, 0, 0, 1);
				this.ID = 5;
			}
			else
			{
				Application.LoadLevel("WelcomeScene");
			}
		}

		if (Text != "666" && Text != "69")
		{
			this.Label.text = this.Lines[this.ID];
		}	

		if (SceneManager.GetActiveScene ().name == SceneNames.MoreFunScene || Text == "666")
		{
			G = 0.0f;
			B = 0.0f;

			Label.color = new Color(R, G, B, 1.0f);
			Skip.SetActive (false);
		}

		if (SceneManager.GetActiveScene ().name == SceneNames.VeryFunScene)
		{
			Skip.SetActive (false);
		}

		this.Controls.SetActive(false);
		this.Label.gameObject.SetActive(false);
		this.Girl.color = new Color(R, G, B, 0.0f);
	}

	void Update()
	{
		if (Input.GetKeyDown(","))
		{
			if (PlayerPrefs.GetInt("DebugNumber") > 0)
			{
				PlayerPrefs.SetInt("DebugNumber", PlayerPrefs.GetInt("DebugNumber") - 1);
				Application.LoadLevel(Application.loadedLevel);
			}
		}

		if (Input.GetKeyDown("."))
		{
			if (PlayerPrefs.GetInt("DebugNumber") < 10)
			{
				PlayerPrefs.SetInt("DebugNumber", PlayerPrefs.GetInt("DebugNumber") + 1);
				Application.LoadLevel(Application.loadedLevel);
			}
		}

		this.Timer += Time.deltaTime;

		if (this.Timer > 3.0f)
		{
			if (!this.Typewriter.mActive)
			{
				this.Controls.SetActive(true);
			}
		}
		else if (this.Timer > 2.0f)
		{
			this.Girl.mainTexture = this.Portraits[this.ID];
			this.Label.gameObject.SetActive(true);
		}
		else if (this.Timer > 1.0f)
		{
			this.Girl.color = new Color(R, G, B, Mathf.MoveTowards(this.Girl.color.a, 1.0f, Time.deltaTime));
		}

		if (this.Controls.activeInHierarchy)
		{
			if (Input.GetButtonDown(InputNames.Xbox_B))
			{
				if (this.Skip.activeInHierarchy)
				{
					this.ID = 19;
					this.Skip.SetActive(false);

					this.Girl.mainTexture = this.Portraits[this.ID];

					this.Typewriter.ResetToBeginning();
					this.Typewriter.mFullText = this.Lines[this.ID];
				}
			}
			else if (Input.GetButtonDown(InputNames.Xbox_A))
			{
				if (this.ID < (this.Lines.Length - 1) && !this.VeryFun)
				{
					if (this.Typewriter.mCurrentOffset < this.Typewriter.mFullText.Length)
					{
						this.Typewriter.Finish();
					}
					else
					{
						this.ID++;

						if (this.ID == 19)
						{
							this.Skip.SetActive(false);
						}

						this.Girl.mainTexture = this.Portraits[this.ID];

						this.Typewriter.ResetToBeginning();
						this.Typewriter.mFullText = this.Lines[this.ID];
					}
				}
				else
				{
					Application.Quit();
				}
			}
		}
	}
}
