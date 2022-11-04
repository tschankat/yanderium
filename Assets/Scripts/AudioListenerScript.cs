using UnityEngine;

public class AudioListenerScript : MonoBehaviour
{
	public Transform Target;
	public Camera mainCamera;

	void Start()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		//if (mainCamera != null)
		//{
			this.transform.position = this.Target.position;
			this.transform.eulerAngles = mainCamera.transform.eulerAngles;
		//}
	}
}