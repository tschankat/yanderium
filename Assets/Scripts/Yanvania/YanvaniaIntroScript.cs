using UnityEngine;

public class YanvaniaIntroScript : MonoBehaviour
{
	public YanvaniaZombieSpawnerScript ZombieSpawner;
	public YanvaniaYanmontScript Yanmont;

	public GameObject Jukebox;

	public Transform BlackRight;
	public Transform BlackLeft;

	public UILabel FinalStage;
	public UILabel Heartbreak;

	public UITexture Triangle;

	public float LeaveTime = 0.0f;
	public float Position = 0.0f;
	public float Timer = 0.0f;

	void Start()
	{
		this.BlackRight.gameObject.SetActive(true);
		this.BlackLeft.gameObject.SetActive(true);
		this.FinalStage.gameObject.SetActive(true);
		this.Heartbreak.gameObject.SetActive(true);
		this.Triangle.gameObject.SetActive(true);

		this.Triangle.transform.localScale = Vector3.zero;

		this.Heartbreak.transform.localPosition = new Vector3(
			1300.0f,
			this.Heartbreak.transform.localPosition.y,
			this.Heartbreak.transform.localPosition.z);

		this.FinalStage.transform.localPosition = new Vector3(
			-1300.0f,
			this.FinalStage.transform.localPosition.y,
			this.FinalStage.transform.localPosition.z);
	}

	void Update()
	{
		this.Timer += Time.deltaTime;

		if ((this.Timer > 1.0f) && (this.Timer < 3.0f))
		{
			if (!this.Jukebox.activeInHierarchy)
			{
				this.Jukebox.SetActive(true);
			}

			this.Triangle.transform.localScale = Vector3.Lerp(
				this.Triangle.transform.localScale,
				new Vector3(1.0f, 1.0f, 1.0f),
				Time.deltaTime * 10.0f);

			this.Heartbreak.transform.localPosition = new Vector3(
				Mathf.Lerp(this.Heartbreak.transform.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
				this.Heartbreak.transform.localPosition.y,
				this.Heartbreak.transform.localPosition.z);

			this.FinalStage.transform.localPosition = new Vector3(
				Mathf.Lerp(this.FinalStage.transform.localPosition.x, 0.0f, Time.deltaTime * 10.0f),
				this.FinalStage.transform.localPosition.y,
				this.FinalStage.transform.localPosition.z);
		}
		else if (this.Timer > 3.0f)
		{
			if (!this.Jukebox.activeInHierarchy)
			{
				this.Jukebox.SetActive(true);
			}

			this.Triangle.transform.localEulerAngles = new Vector3(
				this.Triangle.transform.localEulerAngles.x,
				this.Triangle.transform.localEulerAngles.y,
				this.Triangle.transform.localEulerAngles.z + (36.0f * Time.deltaTime));

			this.Triangle.color = new Color(
				this.Triangle.color.r,
				this.Triangle.color.g,
				this.Triangle.color.b,
				this.Triangle.color.a - Time.deltaTime);

			this.Heartbreak.color = new Color(
				this.Heartbreak.color.r,
				this.Heartbreak.color.g,
				this.Heartbreak.color.b,
				this.Heartbreak.color.a - Time.deltaTime);

			this.FinalStage.color = new Color(
				this.FinalStage.color.r,
				this.FinalStage.color.g,
				this.FinalStage.color.b,
				this.FinalStage.color.a - Time.deltaTime);
		}

		if (this.Timer > 4.0f)
		{
			this.Finish();
		}

		if (this.Timer > this.LeaveTime)
		{
			// [af] Replaced if/else statement with ternary expression.
			this.Position += (this.Position == 0.0f) ? Time.deltaTime : (this.Position * 0.10f);

			if (this.BlackLeft.localPosition.x > -2100.0f)
			{
				this.BlackRight.localPosition = new Vector3(
					this.BlackRight.localPosition.x + this.Position,
					this.BlackRight.localPosition.y,
					this.BlackRight.localPosition.z);

				this.BlackLeft.localPosition = new Vector3(
					this.BlackLeft.localPosition.x - this.Position,
					this.BlackLeft.localPosition.y,
					this.BlackLeft.localPosition.z);
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			this.Finish();
		}
	}

	void Finish()
	{
		if (!this.Jukebox.activeInHierarchy)
		{
			this.Jukebox.SetActive(true);
		}

		this.ZombieSpawner.enabled = true;
		this.Yanmont.CanMove = true;
		Destroy(this.gameObject);
	}
}
