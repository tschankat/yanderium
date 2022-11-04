using UnityEngine;

public class YanvaniaTeleportEffectScript : MonoBehaviour
{
	public YanvaniaDraculaScript Dracula;

	public Transform SecondBeamParent;
	public Renderer SecondBeam;
	public Renderer FirstBeam;

	public bool InformedDracula = false;

	public float Timer = 0.0f;

	void Start()
	{
		this.FirstBeam.material.color = new Color(
			this.FirstBeam.material.color.r,
			this.FirstBeam.material.color.g,
			this.FirstBeam.material.color.b,
			0.0f);

		this.SecondBeam.material.color = new Color(
			this.SecondBeam.material.color.r,
			this.SecondBeam.material.color.g,
			this.SecondBeam.material.color.b,
			0.0f);

		this.FirstBeam.transform.localScale = new Vector3(
			0.0f, this.FirstBeam.transform.localScale.y, 0.0f);

		this.SecondBeamParent.transform.localScale = new Vector3(
			this.SecondBeamParent.transform.localScale.x,
			0.0f,
			this.SecondBeamParent.transform.localScale.z);
	}

	void Update()
	{
		// [af] Commented in JS code.
		/*this.Timer += Time.deltaTime;

		if (this.Timer > 5)
		{
			Destroy(gameObject);
		}
		
		if (this.SecondBeamParent.transform.localScale.y < 1)
		{
			if (this.FirstBeam.transform.localScale.x < .50f)
			{
				this.FirstBeam.transform.localScale = Vector3.MoveTowards(this.FirstBeam.transform.localScale, Vector3(.5, 2.75, .50f), Time.deltaTime);
				this.FirstBeam.material.color.a = this.FirstBeam.transform.localScale.x / .50f;
			}
			else
			{
				this.SecondBeamParent.transform.localScale = Vector3.MoveTowards(this.SecondBeamParent.transform.localScale, Vector3(1, 1, 1), Time.deltaTime);
				this.SecondBeam.material.color.a = this.this.SecondBeamParent.transform.localScale.y;
				
				if (this.SecondBeam.material.color.a == 1)
				{
					this.FirstBeam.material.color.a = 0;
					
					if (Dracula.transform.position.y < 0)
					{
						if (!this.InformedDracula)
						{
							this.InformedDracula = true;
							Dracula.Teleport();
						}
					}
				}
			}
		}
		else
		{
			if (this.SecondBeam.material.color.r > 0)
			{
				this.SecondBeam.material.color.r -= Time.deltaTime;
				this.SecondBeam.material.color.g -= Time.deltaTime;
				this.SecondBeam.material.color.b -= Time.deltaTime;
			}
			else
			{
				if (!this.InformedDracula)
				{
					this.InformedDracula = true;
					Dracula.Teleport();
				}
				
				this.SecondBeam.material.color.a = Mathf.MoveTowards(this.SecondBeam.material.color.a, 0, Time.deltaTime);
				
				if (this.SecondBeam.material.color.a == 0)
				{
					Destroy(gameObject);
				}
			}
		}*/
	}
}
