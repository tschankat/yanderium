using UnityEngine;

// [af] Various static methods for playing an audio clip.
public static class AudioClipPlayer
{
	// [af] Used most of the time.
	public static void Play(AudioClip clip, Vector3 position, float minDistance,
		float maxDistance, out GameObject clipOwner, float playerY)
	{
		GameObject tempGO = new GameObject("AudioClip_" + clip.name);
		tempGO.transform.position = position;

		AudioSource aSource = tempGO.AddComponent<AudioSource>();
		aSource.clip = clip;
		aSource.Play();
		Object.Destroy(tempGO, clip.length);

		aSource.rolloffMode = AudioRolloffMode.Linear;
		aSource.minDistance = minDistance;
		aSource.maxDistance = maxDistance;
		aSource.spatialBlend = 1.0f;

		clipOwner = tempGO;

		float tempY = tempGO.transform.position.y;
		aSource.volume = (playerY < (tempY - 2.0f)) ? 0.0f : 1.0f;
	}

	// [af] Plays a clip attached to some transform.
	public static void PlayAttached(AudioClip clip, Vector3 position, Transform attachment,
		float minDistance, float maxDistance, out GameObject clipOwner, float playerY)
	{
		GameObject tempGO = new GameObject("AudioClip_" + clip.name);
		tempGO.transform.position = position;
		tempGO.transform.parent = attachment;

		AudioSource aSource = tempGO.AddComponent<AudioSource>();
		aSource.clip = clip;
		aSource.Play();
		Object.Destroy(tempGO, clip.length);

		aSource.rolloffMode = AudioRolloffMode.Linear;
		aSource.minDistance = minDistance;
		aSource.maxDistance = maxDistance;
		aSource.spatialBlend = 1.0f;

		clipOwner = tempGO;

		float tempY = tempGO.transform.position.y;
		aSource.volume = (playerY < (tempY - 2.0f)) ? 0.0f : 1.0f;
	}

	// [af] Used rarely.
	public static void PlayAttached(AudioClip clip, Transform attachment, float minDistance, 
		float maxDistance)
	{
		GameObject tempGO = new GameObject("AudioClip_" + clip.name);
		tempGO.transform.parent = attachment;
		tempGO.transform.localPosition = Vector3.zero;

		AudioSource aSource = tempGO.AddComponent<AudioSource>();
		aSource.clip = clip;
		aSource.Play();
		Object.Destroy(tempGO, clip.length);

		aSource.rolloffMode = AudioRolloffMode.Linear;
		aSource.minDistance = minDistance;
		aSource.maxDistance = maxDistance;
		aSource.spatialBlend = 1.0f;
	}

	// [af] Used on a few scripts.
	public static void Play(AudioClip clip, Vector3 position, float minDistance,
		float maxDistance, out GameObject clipOwner, out float clipLength)
	{
		GameObject tempGO = new GameObject("AudioClip_" + clip.name);
		tempGO.transform.position = position;

		AudioSource aSource = tempGO.AddComponent<AudioSource>();
		aSource.clip = clip;
		aSource.Play();
		Object.Destroy(tempGO, clip.length);

		clipLength = clip.length;

		aSource.rolloffMode = AudioRolloffMode.Linear;
		aSource.minDistance = minDistance;
		aSource.maxDistance = maxDistance;
		aSource.spatialBlend = 1.0f;

		clipOwner = tempGO;
	}

	// [af] Used on a couple scripts.
	public static void Play(AudioClip clip, Vector3 position, float minDistance,
		float maxDistance, out GameObject clipOwner)
	{
		GameObject tempGO = new GameObject("AudioClip_" + clip.name);
		tempGO.transform.position = position;

		AudioSource aSource = tempGO.AddComponent<AudioSource>();
		aSource.clip = clip;
		aSource.Play();
		Object.Destroy(tempGO, clip.length);

		aSource.rolloffMode = AudioRolloffMode.Linear;
		aSource.minDistance = minDistance;
		aSource.maxDistance = maxDistance;
		aSource.spatialBlend = 1.0f;

		clipOwner = tempGO;
	}

	// [af] Used in Yanvania.
	public static void Play2D(AudioClip clip, Vector3 position)
	{
		GameObject tempGO = new GameObject("AudioClip_" + clip.name);
		tempGO.transform.position = position;

		AudioSource aSource = tempGO.AddComponent<AudioSource>();
		aSource.clip = clip;
		aSource.Play();
		Object.Destroy(tempGO, clip.length);
	}

	public static void Play2D(AudioClip clip, Vector3 position, float pitch)
	{
		GameObject tempGO = new GameObject("AudioClip_" + clip.name);
		tempGO.transform.position = position;

		AudioSource aSource = tempGO.AddComponent<AudioSource>();
		aSource.clip = clip;
		aSource.Play();
		Object.Destroy(tempGO, clip.length);

		aSource.pitch = pitch;
	}
}
