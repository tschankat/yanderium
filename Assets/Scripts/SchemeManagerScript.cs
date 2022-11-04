using UnityEngine;

public class SchemeManagerScript : MonoBehaviour
{
	public SchemesScript Schemes;
	public ClockScript Clock;

	public bool ClockCheck;
	public float Timer;

	void Update()
	{
		if (Clock.HourTime > 15.5f)
		{
			SchemeGlobals.SetSchemeStage(SchemeGlobals.CurrentScheme, 100);

			Clock.Yandere.NotificationManager.CustomText = "Scheme failed! You were too slow.";
			Clock.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);

			Schemes.UpdateInstructions();

			enabled = false;
		}

		if (ClockCheck)
		{
			if (Clock.HourTime > 8.25f)
			{
				Timer += Time.deltaTime;

				if (Timer > 1)
				{
					Timer = 0;

					//If we're waiting for the clock to advance past 8:15 AM...
					if (SchemeGlobals.GetSchemeStage(5) == 1)
					{
						Debug.Log("It's past 8:15 AM, so we're advancing to Stage 2 of Scheme 5.");

						SchemeGlobals.SetSchemeStage(5, 2);
						Schemes.UpdateInstructions();

						ClockCheck = false;
					}
				}
			}
		}
	}
}