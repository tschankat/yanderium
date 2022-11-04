using UnityEngine;

public class ExpressionMaskScript : MonoBehaviour
{
	public Renderer Mask;
	public int ID = 0;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			if (this.ID < 3)
			{
				this.ID++;
			}
			else
			{
				this.ID = 0;
			}

			switch (this.ID)
			{
				case 0:
					this.Mask.material.mainTextureOffset = Vector2.zero;
					break;
				// Class 1-1.
				case 1:
					this.Mask.material.mainTextureOffset = new Vector2(0.0f, 0.50f);
					break;
				// Class 2-1.
				case 2:
					this.Mask.material.mainTextureOffset = new Vector2(0.50f, 0.0f);
					break;
				// Class 2-2.
				case 3:
					this.Mask.material.mainTextureOffset = new Vector2(0.50f, 0.50f);
					break;
			}
		}
	}
}
