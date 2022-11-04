using UnityEngine;

public class YanvaniaCandlestickScript : MonoBehaviour
{
	public GameObject DestroyedCandlestick;

	public bool Destroyed = false;

	public AudioClip Break;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 19)
		{
			if (!this.Destroyed)
			{
				GameObject NewObject = Instantiate(this.DestroyedCandlestick,
					this.transform.position, Quaternion.identity);
				NewObject.transform.localScale = this.transform.localScale;

				this.Destroyed = true;

				AudioClipPlayer.Play2D(this.Break, this.transform.position);

				Destroy(this.gameObject);
			}
		}
	}
}
