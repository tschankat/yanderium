using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelinquentVoicesScript : MonoBehaviour
{
	public YandereScript Yandere;
	public RadioScript Radio;

	public SubtitleScript Subtitle;

	public float Timer;

	public int RandomID;
	public int LastID;

	void Start()
	{
		Timer = 5;
	}

	void Update()
	{
		if (Radio.MyAudio.isPlaying)
		{
			if (Yandere.CanMove)
			{
				if (Vector3.Distance(Yandere.transform.position, transform.position) < 5)
				{
					Timer = Mathf.MoveTowards(Timer, 0, Time.deltaTime);

					if (Timer == 0)
					{
						if (Yandere.Club != ClubType.Delinquent)
						{
							if (Yandere.Container == null)
							{
								while (RandomID == LastID)
								{
									RandomID = Random.Range(0, Subtitle.DelinquentAnnoyClips.Length);
								}

								LastID = RandomID;

								this.Subtitle.UpdateLabel(SubtitleType.DelinquentAnnoy, RandomID, 3.0f);
							}
							else
							{
								while (RandomID == LastID)
								{
									RandomID = Random.Range(0, Subtitle.DelinquentCaseClips.Length);
								}

								LastID = RandomID;

								this.Subtitle.UpdateLabel(SubtitleType.DelinquentCase, RandomID, 3.0f);
							}

							Timer = 5;
						}
					}
				}
			}
		}
	}
}