using UnityEngine;

public class MirrorScript : MonoBehaviour
{
	public PromptScript Prompt;

	public string[] Personas;
	public string[] Idles;
	public string[] Walks;

	public int Limit = 0;

	void Start()
	{
		Limit = Idles.Length - 1;

		if (this.Prompt.Yandere.Club == ClubType.Delinquent)
		{
            Prompt.Yandere.PersonaID = 10;

			if (Prompt.Yandere.Persona != YanderePersonaType.Tough)
			{
				UpdatePersona();
			}
		}
	}

	void Update()
	{
		if (Prompt.Circle[0].fillAmount == 0.0f)
		{
			if (Prompt.Yandere.Health > 0)
			{
				Prompt.Circle[0].fillAmount = 1;

				Prompt.Yandere.PersonaID++;
				if (Prompt.Yandere.PersonaID == Limit){ Prompt.Yandere.PersonaID = 0;}
				UpdatePersona();
			}
		}
		else if (Prompt.Circle[1].fillAmount == 0.0f)
		{
			if (Prompt.Yandere.Health > 0)
			{
				Prompt.Circle[1].fillAmount = 1;

                Prompt.Yandere.PersonaID--;
				if (Prompt.Yandere.PersonaID < 0){ Prompt.Yandere.PersonaID = Limit - 1;}
				UpdatePersona();
			}
		}
	}

	public void UpdatePersona()
	{
        int ID = Prompt.Yandere.PersonaID;

        if (!Prompt.Yandere.Carrying)
		{
			Prompt.Yandere.NotificationManager.PersonaName = Personas[ID];
			Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Persona);

			Prompt.Yandere.IdleAnim = Idles[ID];
			Prompt.Yandere.WalkAnim = Walks[ID];

			Prompt.Yandere.UpdatePersona(ID);
		}

		Prompt.Yandere.OriginalIdleAnim = Idles[ID];
		Prompt.Yandere.OriginalWalkAnim = Walks[ID];

		Prompt.Yandere.StudentManager.UpdatePerception();
	}
}