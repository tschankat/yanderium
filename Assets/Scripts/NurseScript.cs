using UnityEngine;

public class NurseScript : MonoBehaviour
{
	public GameObject Character;
	public Transform SkirtCenter;

	void Awake()
	{
		// [af] Moved here from class scope for compatibility with C#.
		Animation charAnimation = this.Character.GetComponent<Animation>();
		charAnimation[AnimNames.FemaleNoBlink].layer = 1;
		charAnimation.Blend(AnimNames.FemaleNoBlink);
	}

	void LateUpdate()
	{
		this.SkirtCenter.localEulerAngles = new Vector3(
			-15.0f,
			this.SkirtCenter.localEulerAngles.y,
			this.SkirtCenter.localEulerAngles.z);
	}
}
