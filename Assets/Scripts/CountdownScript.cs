using UnityEngine;

public class CountdownScript : MonoBehaviour
{
	public UISprite Sprite;

	public float Speed = 0.05f;

	public bool MaskedPhoto;

	void Update()
	{
		this.Sprite.fillAmount = Mathf.MoveTowards(this.Sprite.fillAmount, 0, Time.deltaTime * Speed);
	}
}