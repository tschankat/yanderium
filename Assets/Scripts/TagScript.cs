using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagScript : MonoBehaviour
{
	public UISprite Sprite;

	public Camera UICamera;
	public Camera MainCameraCamera;

	public Transform MainCamera;
	public Transform Target;

	void Start()
	{
		this.Sprite.color = new Color (1, 0, 0, 0);

		this.MainCameraCamera = MainCamera.GetComponent<Camera>();
	}

	void Update ()
	{
		if (this.Target != null)
		{
			float Angle = Vector3.Angle(MainCamera.forward, MainCamera.position - Target.position);

			if (Angle > 90)
			{
				Vector2 TargetPosition = MainCameraCamera.WorldToScreenPoint(Target.position);
				transform.position = UICamera.ScreenToWorldPoint(new Vector3(TargetPosition.x, TargetPosition.y, 1.0f));
			}
		}
	}
}