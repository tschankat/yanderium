using UnityEngine;

public class FootprintScript : MonoBehaviour
{
	public YandereScript Yandere;

	public Texture Footprint;
	public Texture Flower;

	void Start()
	{
		if (this.Yandere.Schoolwear == 0 || this.Yandere.Schoolwear == 2 ||
			this.Yandere.ClubAttire && this.Yandere.Club == ClubType.MartialArts ||
			this.Yandere.Hungry || this.Yandere.LucyHelmet.activeInHierarchy)
		{
			this.GetComponent<Renderer>().material.mainTexture = this.Footprint;
		}

		if (GameGlobals.CensorBlood)
		{
			this.GetComponent<Renderer>().material.mainTexture = this.Flower;
			this.transform.localScale = new Vector3(.2f, .2f, 1);
		}

		Destroy(this);
	}
}