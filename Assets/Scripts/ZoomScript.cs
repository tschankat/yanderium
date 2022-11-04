using UnityEngine;

public class ZoomScript : MonoBehaviour
{
	public CardboardBoxScript CardboardBox;
	public RPG_Camera CameraScript;
	public YandereScript Yandere;

	public float TargetZoom = 0.0f;
	public float Zoom = 0.0f;

	public float ShakeStrength = 0.0f;

	public float midOffset = 0.25f;

	public float Slender = 0.0f;
	public float Height = 0.0f;
	public float Timer = 0.0f;

	public Vector3 Target;

	public bool OverShoulder = false;

	public GameObject TallHat;

	void Update()
	{
		if (this.Yandere.FollowHips)
		{
			this.transform.position = new Vector3(
				Mathf.MoveTowards(this.transform.position.x, this.Yandere.Hips.position.x, Time.deltaTime),
				this.transform.position.y,
				Mathf.MoveTowards(this.transform.position.z, this.Yandere.Hips.position.z, Time.deltaTime));
		}

		if (this.Yandere.Stance.Current == StanceType.Crawling)
		{
			this.Height = 0.050f;
		}
		else if (this.Yandere.Stance.Current == StanceType.Crouching)
		{
			this.Height = 0.40f;
		}
		else
		{
			this.Height = 1.0f;
		}

		if (!this.Yandere.FollowHips)
		{
			if (this.Yandere.FlameDemonic)
			{
				this.transform.localPosition = new Vector3(
					this.transform.localPosition.x,
					Mathf.Lerp(this.transform.localPosition.y, Height + this.Zoom + 0.40f, Time.deltaTime * 10.0f),
					this.transform.localPosition.z);
			}
			else if (this.Yandere.Slender)
			{
				this.transform.localPosition = new Vector3(
					this.transform.localPosition.x,
					Mathf.Lerp(this.transform.localPosition.y, Height + this.Zoom + this.Slender, Time.deltaTime * 10.0f),
					this.transform.localPosition.z);
			}
			else if (this.Yandere.Stand.Stand.activeInHierarchy)
			{
				this.transform.localPosition = new Vector3(
					this.transform.localPosition.x,
					Mathf.Lerp(this.transform.localPosition.y, Height - (this.Zoom * 0.50f) + (this.Slender * 0.50f), Time.deltaTime * 10.0f),
					this.transform.localPosition.z);
			}
			else
			{
				this.transform.localPosition = new Vector3(
					this.transform.localPosition.x,
					Mathf.Lerp(this.transform.localPosition.y, Height + this.Zoom, Time.deltaTime * 10.0f),
					this.transform.localPosition.z);
			}
		}
		else
		{
			if (!this.Yandere.SithLord)
			{
				this.transform.position = new Vector3(
					this.transform.position.x,
					Mathf.MoveTowards(this.transform.position.y, this.Yandere.Hips.position.y + this.Zoom, Time.deltaTime * 10.0f),
					this.transform.position.z);
			}
			else
			{
				this.transform.position = new Vector3(
					this.transform.position.x,
					Mathf.MoveTowards(this.transform.position.y, this.Yandere.Hips.position.y, Time.deltaTime * 10.0f),
					this.transform.position.z);
			}
		}
			
		if (!this.Yandere.Aiming)
		{
			this.TargetZoom += Input.GetAxis("Mouse ScrollWheel");
		}

		if (this.Yandere.SithLord || this.Yandere.Riding)
		{
			this.Slender = Mathf.Lerp(this.Slender, 2.5f, Time.deltaTime);
		}
		else if (this.Yandere.Slender || this.Yandere.Stand.Stand.activeInHierarchy ||
			this.Yandere.Blasting || this.Yandere.PK || this.Yandere.Shipgirl ||
			this.TallHat.activeInHierarchy || this.Yandere.Man.activeInHierarchy ||
			this.Yandere.Pod.activeInHierarchy || this.Yandere.LucyHelmet.activeInHierarchy ||
			this.Yandere.Kagune[0].activeInHierarchy)
		{
			this.Slender = Mathf.Lerp(this.Slender, 0.50f, Time.deltaTime);
		}
		else
		{
			this.Slender = Mathf.Lerp(this.Slender, 0.0f, Time.deltaTime);
		}

		if (this.TargetZoom < 0.0f)
		{
			this.TargetZoom = 0.0f;
		}

		if (this.Yandere.Stance.Current == StanceType.Crawling)
		{
			if (this.TargetZoom > 0.30f)
			{
				this.TargetZoom = 0.30f;
			}
		}
		else
		{
			if (this.TargetZoom > 0.40f)
			{
				this.TargetZoom = 0.40f;
			}
		}

		this.Zoom = Mathf.Lerp(this.Zoom, this.TargetZoom, Time.deltaTime);

		if (!this.Yandere.Possessed)
		{
			this.CameraScript.distance = 2.0f - (this.Zoom * 3.33333f) + this.Slender;
			this.CameraScript.distanceMax = 2.0f - (this.Zoom * 3.33333f) + this.Slender;
			this.CameraScript.distanceMin = 2.0f - (this.Zoom * 3.33333f) + this.Slender;

			if (this.Yandere.TornadoHair.activeInHierarchy || this.CardboardBox != null && 
				this.CardboardBox.transform.parent == this.Yandere.Hips)
			{
				this.CameraScript.distanceMax += 3.0f;
			}
		}
		//If Yandere-chan is possessed...
		else
		{
			this.CameraScript.distance = 5.0f;
			this.CameraScript.distanceMax = 5.0f;
		}

		if (!this.Yandere.TimeSkipping)
		{
			this.Timer += Time.deltaTime;

			this.ShakeStrength = Mathf.Lerp(this.ShakeStrength, 1.0f - (this.Yandere.Sanity * 0.010f), Time.deltaTime);

			if (this.Timer > (0.10f + (this.Yandere.Sanity * 0.010f)))
			{
				this.Target.x = Random.Range(-this.ShakeStrength, this.ShakeStrength);
				this.Target.y = this.transform.localPosition.y;
				this.Target.z = Random.Range(-this.ShakeStrength, this.ShakeStrength);

				this.Timer = 0.0f;
			}
		}
		else
		{
			this.Target = new Vector3(0.0f, this.transform.localPosition.y, 0.0f);
		}

		if (this.Yandere.RoofPush)
		{
			this.transform.position = new Vector3(
				Mathf.MoveTowards(this.transform.position.x, this.Yandere.Hips.position.x, Time.deltaTime * 10.0f),
				this.transform.position.y,
				Mathf.MoveTowards(this.transform.position.z, this.Yandere.Hips.position.z, Time.deltaTime * 10.0f));
		}
		else
		{
			this.transform.localPosition = Vector3.MoveTowards(
				this.transform.localPosition, this.Target, Time.deltaTime * this.ShakeStrength * 0.10f);
		}
			
		/*
		//Old code that set camera to specific location
		this.transform.localPosition = new Vector3(
			this.OverShoulder ? 0.25f : 0.0f,
			this.transform.localPosition.y,
			this.transform.localPosition.z);
		*/
	}

	public void LateUpdate()
	{
		//Camera Pivot faces always forward.
		transform.eulerAngles = Vector3.zero;

		if (OverShoulder)
		{
			//Storing forward MainCamera direction casted in worldspace.
			Vector3 camDirFor = Yandere.MainCamera.transform.TransformDirection(Vector3.forward);

			//Camera Pivot always follows YandereChan but with an offset.
			//MidOffset is multiplied by the dot product of the MainCamera direction, the global forward and global right drection.
			transform.position = new Vector3(
				Yandere.transform.position.x + (midOffset * Vector3.Dot(camDirFor, Vector3.forward)),
				transform.position.y,
				Yandere.transform.position.z + (midOffset * Vector3.Dot(camDirFor, -Vector3.right)));
		}
		else
        {
            if (this.Yandere.FollowHips)
            {
                this.transform.position = new Vector3(
                    Mathf.MoveTowards(this.transform.position.x, this.Yandere.Hips.position.x, Time.deltaTime),
                    this.transform.position.y,
                    Mathf.MoveTowards(this.transform.position.z, this.Yandere.Hips.position.z, Time.deltaTime));
            }
            else
            {
                transform.localPosition = new Vector3(0f, transform.localPosition.y, 0f);
            }
        }
    }
}