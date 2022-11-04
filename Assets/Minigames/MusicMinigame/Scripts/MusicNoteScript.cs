using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNoteScript : MonoBehaviour
{
	public MusicMinigameScript MusicMinigame;
	public InputManagerScript InputManager;

	public GameObject Ripple;

	public GameObject Perfect;
	public GameObject Wrong;
	public GameObject Early;
	public GameObject Late;
	public GameObject Miss;

	public GameObject Rating;

	public string XboxDirection;
	public string Direction;
	public string Tapped;

	public bool GaveInput;
	public bool Proceed;

	public float Speed;

	public int ID;

	void Update()
	{
		transform.localPosition += new Vector3(Speed * Time.deltaTime * -1, 0, 0);

		if (!MusicMinigame.KeyDown)
		{
			GaveInput = false;

			     if (InputManager.TappedUp)    {GaveInput = true; Tapped = "up";}
			else if (InputManager.TappedDown)  {GaveInput = true; Tapped = "down";}
			else if (InputManager.TappedRight) {GaveInput = true; Tapped = "right";}
			else if (InputManager.TappedLeft)  {GaveInput = true; Tapped = "left";}

			if (Input.GetKeyDown(Direction) || GaveInput && Tapped == Direction)
			{
				if (MusicMinigame.CurrentNote == ID)
				{
					if (transform.localPosition.x > -.6f && transform.localPosition.x < -.4f)
					{
						Rating = Instantiate(Perfect, transform.position, Quaternion.identity);
						Proceed = true;

						MusicMinigame.Health++;
						MusicMinigame.CringeTimer = 0;
						MusicMinigame.UpdateHealthBar();
					}
					else if (transform.localPosition.x > -.4f && transform.localPosition.x < -.2f)
					{
						Rating = Instantiate(Early, transform.position, Quaternion.identity);
						MusicMinigame.CringeTimer = 0;
						Proceed = true;
					}
					else if (transform.localPosition.x > -.8f && transform.localPosition.x < -.4f)
					{
						Rating = Instantiate(Late, transform.position, Quaternion.identity);
						MusicMinigame.CringeTimer = 0;
						Proceed = true;
					}
				}
			}
			else
			{
				if (Input.anyKeyDown)
				{
					if (transform.localPosition.x > -.8f && transform.localPosition.x < -.2f)
					{
						if (!MusicMinigame.GameOver)
						{
							Rating = Instantiate(Wrong, transform.position, Quaternion.identity);
							Proceed = true;

							MusicMinigame.Cringe();

							if (!MusicMinigame.LockHealth)
							{
								MusicMinigame.Health -= 10;
							}

							MusicMinigame.UpdateHealthBar();
						}
					}
				}
			}
		}

		if (Proceed)
		{
			GameObject NewRipple = Instantiate(Ripple, transform.position, Quaternion.identity);

			NewRipple.transform.parent = transform.parent;
			NewRipple.transform.localScale = new Vector3(.2f, .2f, .2f);

			Rating.transform.parent = transform.parent;
			Rating.transform.localPosition = new Vector3(-.5f, .25f, 0);
			Rating.transform.localScale = new Vector3(.3f, .15f, .15f);

			MusicMinigame.CurrentNote++;
			MusicMinigame.KeyDown = true;

			Destroy(gameObject);
		}
		else
		{
			if (transform.localPosition.x < -.65f)
			{
				if (MusicMinigame.CurrentNote == ID)
				{
					MusicMinigame.CurrentNote++;
				}
			}
		}

		if (transform.localPosition.x < -.94f)
		{
			if (!MusicMinigame.GameOver)
			{
				Rating = Instantiate(Miss, transform.position, Quaternion.identity);

				Rating.transform.parent = transform.parent;
				Rating.transform.localPosition = new Vector3(-.94f, .25f, 0);
				Rating.transform.localScale = new Vector3(.3f, .15f, .15f);

				Destroy(gameObject);
			
				MusicMinigame.Cringe();

				if (!MusicMinigame.LockHealth)
				{
					MusicMinigame.Health -= 10;
				}

				MusicMinigame.UpdateHealthBar();
			}
		}
	}
}