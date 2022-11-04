using UnityEngine;

public class CharacterScript : MonoBehaviour
{
	public Transform RightBreast;
	public Transform LeftBreast;
	public Transform ItemParent;
	public Transform PelvisRoot;
	public Transform RightEye;
	public Transform LeftEye;
	public Transform Head;

	public Transform[] Spine;
	public Transform[] Arm;

	public SkinnedMeshRenderer MyRenderer;
	public Renderer RightYandereEye;
	public Renderer LeftYandereEye;

	void SetAnimations()
	{
		Animation animation = this.GetComponent<Animation>();

		animation[AnimNames.FemaleYanderePose].layer = 1;
		animation[AnimNames.FemaleYanderePose].weight = 0.0f;
		animation.Play(AnimNames.FemaleYanderePose);

		animation[AnimNames.FemaleShy].layer = 2;
		animation[AnimNames.FemaleShy].weight = 0.0f;
		animation.Play(AnimNames.FemaleShy);

		animation[AnimNames.FemaleFist].layer = 3;
		animation[AnimNames.FemaleFist].weight = 0.0f;
		animation.Play(AnimNames.FemaleFist);

		animation[AnimNames.FemaleMopping].layer = 4;
		animation[AnimNames.FemaleMopping].weight = 0.0f;
		animation[AnimNames.FemaleMopping].speed = 2.0f;
		animation.Play(AnimNames.FemaleMopping);

		animation[AnimNames.FemaleCarry].layer = 5;
		animation[AnimNames.FemaleCarry].weight = 0.0f;
		animation.Play(AnimNames.FemaleCarry);

		animation[AnimNames.FemaleMopCarry].layer = 6;
		animation[AnimNames.FemaleMopCarry].weight = 0.0f;
		animation.Play(AnimNames.FemaleMopCarry);

		animation[AnimNames.FemaleBucketCarry].layer = 7;
		animation[AnimNames.FemaleBucketCarry].weight = 0.0f;
		animation.Play(AnimNames.FemaleBucketCarry);

		animation[AnimNames.FemaleCameraPose].layer = 8;
		animation[AnimNames.FemaleCameraPose].weight = 0.0f;
		animation.Play(AnimNames.FemaleCameraPose);

		animation[AnimNames.FemaleDipping].speed = 2.0f;

		animation[AnimNames.FemaleCameraPose].weight = 0.0f;
		animation[AnimNames.FemaleShy].weight = 0.0f;
	}
}
