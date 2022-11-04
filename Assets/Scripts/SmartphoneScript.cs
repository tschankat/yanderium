using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartphoneScript : MonoBehaviour
{
	public Transform PhoneCrushingSpot;

	public GameObject EmptyGameObject;
	public Texture SmashedTexture;
	public GameObject PhoneSmash;
	public Renderer MyRenderer;
	public PromptScript Prompt;
	public MeshFilter MyMesh;
	public Mesh SmashedMesh;

	void Update ()
	{
		if (Prompt.Circle[0].fillAmount == 0)
		{
            if (Prompt.Yandere.Dragging || Prompt.Yandere.Carrying)
            {
                Prompt.Yandere.EmptyHands();
            }

			Prompt.Yandere.CrushingPhone = true;
			Prompt.Yandere.PhoneToCrush = this;
			Prompt.Yandere.CanMove = false;

			GameObject NewObject = Instantiate(EmptyGameObject, transform.position, Quaternion.identity);

			PhoneCrushingSpot = NewObject.transform;

			PhoneCrushingSpot.position = new Vector3(
				PhoneCrushingSpot.position.x,
				Prompt.Yandere.transform.position.y,
				PhoneCrushingSpot.position.z);

			PhoneCrushingSpot.LookAt(Prompt.Yandere.transform.position);

			PhoneCrushingSpot.Translate(Vector3.forward * .5f);
		}
	}
}