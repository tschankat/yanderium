using UnityEngine;

public class BlendshapeScript : MonoBehaviour
{
	public SkinnedMeshRenderer MyMesh;
	public float Happiness = 0.0f;
	public float Blink = 0.0f;

	void LateUpdate()
	{
		this.Happiness += Time.deltaTime * 10.0f;
		this.MyMesh.SetBlendShapeWeight(0, this.Happiness);
		this.Blink += Time.deltaTime * 10.0f;
		this.MyMesh.SetBlendShapeWeight(8, 100.0f);
	}
}
