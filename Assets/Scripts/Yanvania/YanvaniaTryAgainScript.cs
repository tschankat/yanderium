using UnityEngine;
using UnityEngine.SceneManagement;

public class YanvaniaTryAgainScript : MonoBehaviour
{
	public InputManagerScript InputManager;

	public GameObject ButtonEffect;

	public Transform Highlight;

	public UISprite Darkness;

	public bool FadeOut = false;

	public int Selected = 1;

	void Start()
	{
		this.transform.localScale = Vector3.zero;
	}

	void Update()
	{
		if (!this.FadeOut)
		{
			if (this.transform.localScale.x > 0.90f)
			{
				if (this.InputManager.TappedLeft)
				{
					this.Selected = 1;
				}

				if (this.InputManager.TappedRight)
				{
					this.Selected = 2;
				}

				if (this.Selected == 1)
				{
					this.Highlight.localPosition = new Vector3(
						Mathf.Lerp(this.Highlight.localPosition.x, -100.0f, Time.deltaTime * 10.0f),
						this.Highlight.localPosition.y,
						this.Highlight.localPosition.z);

					this.Highlight.localScale = new Vector3(
						Mathf.Lerp(this.Highlight.localScale.x, -1.0f, Time.deltaTime * 10.0f),
						this.Highlight.localScale.y,
						this.Highlight.localScale.z);
				}
				else
				{
					this.Highlight.localPosition = new Vector3(
						Mathf.Lerp(this.Highlight.localPosition.x, 100.0f, Time.deltaTime * 10.0f),
						this.Highlight.localPosition.y,
						this.Highlight.localPosition.z);

					this.Highlight.localScale = new Vector3(
						Mathf.Lerp(this.Highlight.localScale.x, 1.0f, Time.deltaTime * 10.0f),
						this.Highlight.localScale.y,
						this.Highlight.localScale.z);
				}

				if (Input.GetButtonDown(InputNames.Xbox_A) || Input.GetKeyDown("z") || Input.GetKeyDown("x"))
				{
					GameObject NewEffect = Instantiate(this.ButtonEffect,
						this.Highlight.position, Quaternion.identity);
					NewEffect.transform.parent = this.Highlight;
					NewEffect.transform.localPosition = Vector3.zero;
					this.FadeOut = true;
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				this.Darkness.color.a + Time.deltaTime);

			if (this.Darkness.color.a >= 1.0f)
			{
				if (this.Selected == 1)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				else
				{
					SceneManager.LoadScene(SceneNames.YanvaniaTitleScene);
				}
			}
		}
	}
}
