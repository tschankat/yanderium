using UnityEngine;

public class DumpScript : MonoBehaviour
{
	public SkinnedMeshRenderer MyRenderer;

	public IncineratorScript Incinerator;

	public float Timer = 0.0f;

	void Update()
	{
		this.Timer += Time.deltaTime;

		if (this.Timer > 5.0f)
		{
			this.Incinerator.Corpses++;

			Destroy(this.gameObject);
		}
	}
}
