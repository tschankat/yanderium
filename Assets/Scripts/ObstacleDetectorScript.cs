using UnityEngine;

public class ObstacleDetectorScript : MonoBehaviour
{
	public YandereScript Yandere;
	
	public GameObject ControllerX;
	public GameObject KeyboardX;
	
	public Collider[] ObstacleArray;
	
	public int Obstacles = 0;
	
	public bool Add = false;
	public int ID = 0;
	
	void Start()
	{
		this.ControllerX.SetActive(false);
		this.KeyboardX.SetActive(false);
	}
	
	// [af] Commented in JS code.
	/*
	
	void Update()
	{
		this.ID = 1;
		
		while (this.ID < this.ObstacleArray.Length)
		{
			if (this.ObstacleArray[this.ID] != null)
			{
				if (!this.ObstacleArray[this.ID].enabled)
				{
					this.ObstacleArray[this.ID] = null;
					
					if (this.ID != this.ObstacleArray.Length - 1)
					{
						this.Shuffle(this.ID);
					}
					
					this.Obstacles--;
				}
			}
			
			this.ID++;
		}
	}
	
	void OnTriggerEnter(other:Collider)
	{
		if (other.gameObject.layer != 1 && other.gameObject.layer != 8 && other.gameObject.layer != 13 && other.gameObject.layer != 14)
		{
			this.Add = true;
			this.ID = 1;
			
			while (this.ID < 11)
			{
				if (this.ObstacleArray[this.ID] == other.gameObject.collider)
				{
					this.Add = false;
				}
				
				this.ID++;
			}
			
			if (this.Add)
			{
				this.Obstacles++;
				this.ObstacleArray[this.Obstacles] = other.gameObject.collider;
			}
			
			//Obstacles++;
			
			if (this.Yandere.Container != null)
			{
				this.ControllerX.SetActive(true);
				this.KeyboardX.SetActive(true);
			}
			
			Debug.Log("I am colliding with " + other.name);
		}
	}
	
	void OnTriggerExit(other:Collider)
	{
		if (other.gameObject.layer != 1 && other.gameObject.layer != 8 && other.gameObject.layer != 13 && other.gameObject.layer != 14)
		{
			this.Obstacles--;
			
			if (this.Obstacles == 0)
			{
				this.ControllerX.SetActive(false);
				this.KeyboardX.SetActive(false);
			}
			
			Debug.Log("I am no longer colliding with " + other.name);
		}
	}
	
	void UpdateX()
	{
		if (this.Obstacles > 0)
		{
			this.ControllerX.SetActive(true);
			this.KeyboardX.SetActive(true);
		}
	}
	
	void Shuffle(Start:int)
	{
		this.ShuffleID = this.Start;
		
		while (this.ShuffleID < this.ObstacleArray.Length - 1)
		{
			this.ObstacleArray[this.ShuffleID] = this.ObstacleArray[this.ShuffleID + 1];
			this.ShuffleID++;
		}
	}
	
	*/
}
