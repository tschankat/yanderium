using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
	public Transform StartPos;
	public Transform EndPos;

	public Transform RightDoor;
	public Transform LeftDoor;

	public Transform Target;

	public bool OpenDoors = false;
	public bool Begin = false;

	public float Speed = 0.0f;

	public float Timer = 0.0f;

	void Start()
	{
		this.transform.position = this.StartPos.position;
		this.transform.rotation = this.StartPos.rotation;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.Begin = true;
		}

		if (this.Begin)
		{
			this.Timer += Time.deltaTime * this.Speed;

			if (this.Timer > 0.10f)
			{
				this.OpenDoors = true;

				if (this.LeftDoor != null)
				{
					this.LeftDoor.transform.localPosition = new Vector3(
						Mathf.Lerp(this.LeftDoor.transform.localPosition.x, 1.0f, Time.deltaTime),
						this.LeftDoor.transform.localPosition.y,
						this.LeftDoor.transform.localPosition.z);

					this.RightDoor.transform.localPosition = new Vector3(
						Mathf.Lerp(this.RightDoor.transform.localPosition.x, -1.0f, Time.deltaTime),
						this.RightDoor.transform.localPosition.y,
						this.RightDoor.transform.localPosition.z);
				}
			}

			this.transform.position = Vector3.Lerp(
				this.transform.position, this.EndPos.position, Time.deltaTime * this.Timer);
			this.transform.rotation = Quaternion.Lerp(
				this.transform.rotation, this.EndPos.rotation, Time.deltaTime * this.Timer);
		}
	}

	void LateUpdate()
	{
		if (this.Target != null)
		{
			this.transform.LookAt(this.Target);
		}
	}
}
