using UnityEngine;

public class StudentPortraitScript : MonoBehaviour
{
	public GameObject DeathShadow;
	public GameObject PrisonBars;
	public GameObject Panties;
	public GameObject Friend;
	public UITexture Portrait;

	void Start()
	{
		this.DeathShadow.SetActive(false);
		this.PrisonBars.SetActive(false);
		this.Panties.SetActive(false);
		this.Friend.SetActive(false);
	}
}
