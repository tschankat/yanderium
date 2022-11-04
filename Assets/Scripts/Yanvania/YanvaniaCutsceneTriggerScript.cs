using UnityEngine;

public class YanvaniaCutsceneTriggerScript : MonoBehaviour
{
	public YanvaniaYanmontScript Yanmont;
	public GameObject BossBattleWall;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "YanmontChan")
		{
			this.BossBattleWall.SetActive(true);
			this.Yanmont.EnterCutscene = true;

			Destroy(this.gameObject);
		}
	}
}
