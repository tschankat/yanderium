using UnityEngine;

public class KnifeDetectorScript : MonoBehaviour
{
	public BlowtorchScript[] Blowtorches;

	public Transform HeatingSpot;
	public Transform Torches;

	public YandereScript Yandere;
	public PromptScript Prompt;

	public float Timer = 0.0f;

	void Start()
	{
		this.Disable();
	}

	void Update()
	{
		if (this.Blowtorches[1].transform.parent != Torches ||
			this.Blowtorches[2].transform.parent != Torches ||
			this.Blowtorches[3].transform.parent != Torches)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = true;

			this.enabled = false;
		}

		if (this.Yandere.Armed)
		{
			if (this.Yandere.EquippedWeapon.WeaponID == 8)
			{
				this.Prompt.MyCollider.enabled = true;
				this.Prompt.enabled = true;

				if (this.Prompt.Circle[0].fillAmount == 0.0f)
				{
					this.Prompt.Circle[0].fillAmount = 1;

					if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
					{
						this.Yandere.CharacterAnimation.CrossFade(AnimNames.FemaleHeating);
						this.Yandere.CanMove = false;
						this.Timer = 5;

						this.Blowtorches[1].enabled = true;
						this.Blowtorches[2].enabled = true;
						this.Blowtorches[3].enabled = true;

						this.Blowtorches[1].GetComponent<AudioSource>().Play();
						this.Blowtorches[2].GetComponent<AudioSource>().Play();
						this.Blowtorches[3].GetComponent<AudioSource>().Play();
					}
				}
			}
			else
			{
				this.Disable();
			}
		}
		else
		{
			this.Disable();
		}

		if (this.Timer > 0.0f)
		{
			this.Yandere.transform.rotation = Quaternion.Slerp(
				this.Yandere.transform.rotation, this.HeatingSpot.rotation, Time.deltaTime * 10.0f);

			this.Yandere.MoveTowardsTarget(this.HeatingSpot.position);

			WeaponScript yandereWeapon = this.Yandere.EquippedWeapon;
			Material weaponMaterial = yandereWeapon.MyRenderer.material;

			weaponMaterial.color = new Color(
				weaponMaterial.color.r,
				Mathf.MoveTowards(weaponMaterial.color.g, 0.50f, Time.deltaTime * 0.20f),
				Mathf.MoveTowards(weaponMaterial.color.b, 0.50f, Time.deltaTime * 0.20f),
				weaponMaterial.color.a);

			this.Timer = Mathf.MoveTowards(this.Timer, 0.0f, Time.deltaTime);

			if (this.Timer == 0.0f)
			{
				yandereWeapon.Heated = true;
				this.enabled = false;
				this.Disable();
			}
		}
	}

	void Disable()
	{
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.Prompt.MyCollider.enabled = false;
	}
}
