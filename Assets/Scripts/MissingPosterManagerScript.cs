using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingPosterManagerScript : MonoBehaviour
{
	public GameObject MissingPoster;

	public int RandomID;
	public int ID;

	void Start ()
	{
		while (ID < 101)
		{
			if (StudentGlobals.GetStudentMissing (ID))
			{
				GameObject NewMissingPoster = Instantiate(this.MissingPoster,
					this.transform.position, Quaternion.identity);

				NewMissingPoster.transform.parent = transform;
				NewMissingPoster.transform.localScale = new Vector3(1, 1, 1);
				NewMissingPoster.transform.eulerAngles = new Vector3(0, 0, Random.Range(-15.0f, 15.0f));

				string path = "file:///" + Application.streamingAssetsPath +
					"/Portraits/Student_" + ID.ToString() + ".png";

				WWW www = new WWW(path);

				//NewMissingPoster.GetComponent<UITexture>().mainTexture = www.texture;
				NewMissingPoster.GetComponent<MissingPosterScript>().MyRenderer.material.mainTexture = www.texture;

				this.RandomID = Random.Range(1, 3);

				NewMissingPoster.transform.localPosition = new Vector3(-16300.0f + (ID * 500), Random.Range (1300.0f, 2000.0f), 0);

				if (NewMissingPoster.transform.localPosition.x > -3700)
				{
					NewMissingPoster.transform.localPosition = new Vector3(
						NewMissingPoster.transform.localPosition.x + 7300,
						NewMissingPoster.transform.localPosition.y,
						NewMissingPoster.transform.localPosition.z);
				}

				if (NewMissingPoster.transform.localPosition.x > 15800.0f)
				{
					Destroy(NewMissingPoster);
				}
			}

			ID++;
		}
	}
}