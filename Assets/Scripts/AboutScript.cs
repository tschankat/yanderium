using UnityEngine;

public class AboutScript : MonoBehaviour
{
	public Transform[] Labels;
	public bool[] SlideOut;
	public bool[] SlideIn;
	public UILabel LinkLabel;
	public UITexture Yuno1;
	public UITexture Yuno2;
	public int SlideID = 0;
	public int ID = 0;
	public float Timer = 0.0f;

	void Start()
	{
		foreach (Transform transform in this.Labels)
		{
			Vector3 localPosition = transform.localPosition;
			localPosition.x = 2000.0f;
			transform.localPosition = localPosition;
		}
	}

	void Update()
	{
		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			if (this.SlideID < this.Labels.Length)
			{
				this.SlideIn[this.SlideID] = true;
			}

			this.SlideID++;
		}

		if (this.SlideID < (this.Labels.Length + 1))
		{
			for (this.ID = 0; this.ID < this.Labels.Length; this.ID++)
			{
				if (this.SlideIn[this.ID])
				{
					Transform transform = this.Labels[this.ID];
					Vector3 localPosition = transform.localPosition;
					localPosition.x = Mathf.Lerp(localPosition.x, 0.0f, Time.deltaTime);
					transform.localPosition = localPosition;
				}
			}
		}
		else
		{
			this.Timer += Time.deltaTime * 10.0f;

			for (this.ID = 0; this.ID < this.Labels.Length; this.ID++)
			{
				if (this.Timer > this.ID)
				{
					this.SlideOut[this.ID] = true;

					Transform transform = this.Labels[this.ID];
					Vector3 localPosition = transform.localPosition;

					if (localPosition.x > 0.0f)
					{
						localPosition.x = -0.10f;
						transform.localPosition = localPosition;
					}
				}
			}

			for (this.ID = 0; this.ID < this.Labels.Length; this.ID++)
			{
				if (this.SlideOut[this.ID])
				{
					Transform transform = this.Labels[this.ID];
					Vector3 localPosition = transform.localPosition;
					localPosition.x += localPosition.x * 0.010f;
					transform.localPosition = localPosition;
				}
			}

			if (this.SlideID > (this.Labels.Length + 1))
			{
				Color color = this.LinkLabel.color;
				color.a += Time.deltaTime;
				this.LinkLabel.color = color;
			}

			if (this.SlideID > (this.Labels.Length + 2))
			{
				Color color = this.Yuno1.color;
				color.a += Time.deltaTime;
				this.Yuno1.color = color;
			}

			if (this.SlideID > (this.Labels.Length + 3))
			{
				Color color = this.Yuno2.color;
				color.a += Time.deltaTime;
				this.Yuno2.color = color;

				Vector3 localScale = this.Yuno2.transform.localScale;
				localScale.x += Time.deltaTime * 0.10f;
				localScale.y += Time.deltaTime * 0.10f;
				this.Yuno2.transform.localScale = localScale;
			}
		}
	}
}