using UnityEngine;

public class RivalPhoneScript : MonoBehaviour
{
	public DoorGapScript StolenPhoneDropoff;

	public Renderer MyRenderer;
	public PromptScript Prompt;

	public bool LewdPhotos;
	public bool Stolen;

	public int StudentID;

	public Vector3 OriginalPosition;
	public Quaternion OriginalRotation;
	public Transform OriginalParent;

	void Start()
	{
		this.OriginalParent = this.transform.parent;
		this.OriginalPosition = this.transform.localPosition;
		this.OriginalRotation = this.transform.localRotation;
	}

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
            this.Prompt.Circle[0].fillAmount = 1;

            this.Prompt.Yandere.StudentManager.CanAnyoneSeeYandere();

            if (!this.Prompt.Yandere.StudentManager.YandereVisible)
            {
                //If this is Osana's phone...
                if (this.StudentID == this.Prompt.Yandere.StudentManager.RivalID)
			    {
				    //If we're waiting for Yandere-chan to obtain Osana's phone...
				    if (SchemeGlobals.GetSchemeStage(1) == 4)
				    {
					    SchemeGlobals.SetSchemeStage(1, 5);
					    this.Prompt.Yandere.PauseScreen.Schemes.UpdateInstructions();
				    }
			    }
				
			    this.Prompt.Yandere.RivalPhoneTexture = MyRenderer.material.mainTexture;

			    this.Prompt.Yandere.Inventory.RivalPhone = true;
                this.Prompt.Yandere.Inventory.RivalPhoneID = this.StudentID;

                this.Prompt.enabled = false;
			    this.enabled = false;

			    this.StolenPhoneDropoff.Prompt.enabled = true;
			    this.StolenPhoneDropoff.Phase = 1;
			    this.StolenPhoneDropoff.Timer = 0;
			    this.StolenPhoneDropoff.Prompt.Label[0].text = "     " + "Provide Stolen Phone";

			    this.gameObject.SetActive(false);
			    this.Stolen = true;
            }
            else
            {
                this.Prompt.Yandere.NotificationManager.CustomText = "Someone is watching!";
                this.Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
            }
        }
	}

	public void ReturnToOrigin()
	{
		this.transform.parent = this.OriginalParent;
		this.transform.localPosition = this.OriginalPosition;
		this.transform.localRotation = this.OriginalRotation;

		this.gameObject.SetActive(false);
		this.Prompt.enabled = true;

		this.LewdPhotos = false;
		this.Stolen = false;
		this.enabled = true;
	}
}