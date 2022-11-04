using UnityEngine;

public class ParticleDeathScript : MonoBehaviour
{
	public ParticleSystem Particles;

	void LateUpdate()
	{
		if (this.Particles.isPlaying && (this.Particles.particleCount == 0))
		{
			Destroy(this.gameObject);
		}
	}
}
