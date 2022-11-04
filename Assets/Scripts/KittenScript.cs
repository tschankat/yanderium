using UnityEngine;

public class KittenScript : MonoBehaviour
{
	public YandereScript Yandere;

	public GameObject Character;

	public string[] AnimationNames;

	public Transform Target;
	public Transform Head;

	public string CurrentAnim = string.Empty;
	public string IdleAnim = string.Empty;

	public bool Wait = false;

	public float Timer = 0.0f;

	void LateUpdate()
	{
		if (Vector3.Distance(this.transform.position, Yandere.transform.position) < 5)
		{
			if (!this.Yandere.Aiming)
			{
				Vector3 lerpEnd = (this.Yandere.Head.transform.position.x < this.transform.position.x) ?
					this.Yandere.Head.transform.position :
					(this.transform.position + this.transform.forward + (this.transform.up * 0.139854f));

				this.Target.position = Vector3.Lerp(this.Target.position, lerpEnd, Time.deltaTime * 5.0f);

				this.Head.transform.LookAt(this.Target);
			}
			else
			{
				this.Head.transform.LookAt(this.Yandere.transform.position + (Vector3.up * this.Head.position.y));
			}
		}
	}
}