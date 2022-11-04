using UnityEngine;

public class YanvaniaDoubleFireballScript : MonoBehaviour
{
	public GameObject Lavaball;
	public GameObject FirstLavaball;
	public GameObject SecondLavaball;
	public GameObject LightningEffect;

	public Transform Dracula;

	public bool SpawnedFirst = false;
	public bool SpawnedSecond = false;

	public float FirstPosition = 0.0f;
	public float SecondPosition = 0.0f;

	public int Direction = 0;

	public float Timer = 0.0f;
	public float Speed = 0.0f;

	void Start()
	{
		Instantiate(this.LightningEffect,
			new Vector3(this.transform.position.x, 8.0f, 0.0f), Quaternion.identity);

		// [af] Replaced if/else statement with ternary expression.
		this.Direction = (this.Dracula.position.x > this.transform.position.x) ? -1 : 1;
	}

	void Update()
	{
		if (this.Timer > 1.0f)
		{
			if (!this.SpawnedFirst)
			{
				Instantiate(this.LightningEffect,
					new Vector3(this.transform.position.x, 7.0f, 0.0f), Quaternion.identity);
				this.FirstLavaball = Instantiate(this.Lavaball,
					new Vector3(this.transform.position.x, 8.0f, 0.0f), Quaternion.identity);
				this.FirstLavaball.transform.localScale = Vector3.zero;
				this.SpawnedFirst = true;
			}
		}

		if (this.FirstLavaball != null)
		{
			this.FirstLavaball.transform.localScale = Vector3.Lerp(
				this.FirstLavaball.transform.localScale,
				new Vector3(1.0f, 1.0f, 1.0f), Time.deltaTime * 10.0f);

			// [af] Replaced if/else statement with ternary expression.
			this.FirstPosition += (this.FirstPosition == 0.0f) ?
				Time.deltaTime : (this.FirstPosition * this.Speed);

			this.FirstLavaball.transform.position = new Vector3(
				this.FirstLavaball.transform.position.x + (this.FirstPosition * this.Direction),
				this.FirstLavaball.transform.position.y,
				this.FirstLavaball.transform.position.z);

			this.FirstLavaball.transform.eulerAngles = new Vector3(
				this.FirstLavaball.transform.eulerAngles.x,
				this.FirstLavaball.transform.eulerAngles.y,
				this.FirstLavaball.transform.eulerAngles.z - (this.FirstPosition * this.Direction * 36.0f));
		}

		if (this.Timer > 2.0f)
		{
			if (!this.SpawnedSecond)
			{
				this.SecondLavaball = Instantiate(this.Lavaball,
					new Vector3(this.transform.position.x, 7.0f, 0.0f), Quaternion.identity);
				this.SecondLavaball.transform.localScale = Vector3.zero;
				this.SpawnedSecond = true;
			}
		}

		if (this.SecondLavaball != null)
		{
			this.SecondLavaball.transform.localScale = Vector3.Lerp(
				this.SecondLavaball.transform.localScale,
				new Vector3(1.0f, 1.0f, 1.0f),
				Time.deltaTime * 10.0f);

			if (this.SecondPosition == 0.0f)
			{
				this.SecondPosition += Time.deltaTime;
			}
			else
			{
				this.SecondPosition += this.SecondPosition * this.Speed;
			}

			this.SecondLavaball.transform.position = new Vector3(
				this.SecondLavaball.transform.position.x + (this.SecondPosition * this.Direction),
				this.SecondLavaball.transform.position.y,
				this.SecondLavaball.transform.position.z);

			this.SecondLavaball.transform.eulerAngles = new Vector3(
				this.SecondLavaball.transform.eulerAngles.x,
				this.SecondLavaball.transform.eulerAngles.y,
				this.SecondLavaball.transform.eulerAngles.z - (this.SecondPosition * this.Direction * 36.0f));
		}

		this.Timer += Time.deltaTime;

		if (this.Timer > 10.0f)
		{
			if (this.FirstLavaball != null)
			{
				Destroy(this.FirstLavaball);
			}

			if (this.SecondLavaball != null)
			{
				Destroy(this.SecondLavaball);
			}

			Destroy(this.gameObject);
		}
	}
}
