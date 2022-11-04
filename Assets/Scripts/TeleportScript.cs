using UnityEngine;

public class TeleportScript : MonoBehaviour
{
	public PromptScript Prompt;

	public Transform Destination;

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			this.Prompt.Yandere.transform.position = this.Destination.position;
            Physics.SyncTransforms();
		}
	}
}
