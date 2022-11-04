using UnityEngine;

public class HomePantiesScript : MonoBehaviour
{
	public HomePantyChangerScript PantyChanger;
	public float RotationSpeed = 0.0f;
	public Material Unselectable;
	public Renderer MyRenderer;
	public int ID = 0;

	void Start()
	{
		if (ID > 0)
		{
			if (!CollectibleGlobals.GetPantyPurchased(ID))
			{
				MyRenderer.material = Unselectable;
				MyRenderer.material.color = new Color(0, 0, 0, .5f);
			}
		}
	}

	void Update()
	{
		float eulerY = (this.PantyChanger.Selected == this.ID) ?
			(this.transform.eulerAngles.y + (Time.deltaTime * this.RotationSpeed)) : 0.0f;
				
		this.transform.eulerAngles = new Vector3(
			this.transform.eulerAngles.x, eulerY, this.transform.eulerAngles.z);
	}
}