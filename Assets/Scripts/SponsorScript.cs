using UnityEngine;
using UnityEngine.SceneManagement;

public class SponsorScript : MonoBehaviour
{
	public GameObject[] Set;
	public UISprite Darkness;
	public float Timer = 0.0f;
	public int ID = 0;

	void Start()
	{
		Time.timeScale = 1;

		this.Set[1].SetActive(true);
		this.Set[2].SetActive(false);

		this.Darkness.color = new Color(
			this.Darkness.color.r,
			this.Darkness.color.g,
			this.Darkness.color.b,
			1.0f);
	}

	void Update()
	{
		this.Timer += Time.deltaTime;

		if (this.Timer < 3.2f)
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 0, Time.deltaTime));
		}
		else
		{
			this.Darkness.color = new Color(
				this.Darkness.color.r,
				this.Darkness.color.g,
				this.Darkness.color.b,
				Mathf.MoveTowards(this.Darkness.color.a, 1, Time.deltaTime));

			if (this.Darkness.color.a == 1)
			{
				SceneManager.LoadScene(SceneNames.TitleScene);
			}
		}
	}
}
