using System;
using UnityEngine;

public enum StanceType
{
	Standing,
	Crouching,
	Crawling
}

// [af] This class makes it easier to keep track of Yandere-chan's stances that she
// changes between. We no longer need to store the previous stance separately because 
// it's taken care of here automatically.
[Serializable]
public class Stance
{
	[SerializeField] StanceType current;
	[SerializeField] StanceType previous;

	public Stance(StanceType initialStance)
	{
		this.current = initialStance;
		this.previous = initialStance;
	}

	public StanceType Current
	{
		get { return this.current; }
		set
		{
			this.previous = this.current;
			this.current = value;
		}
	}

	public StanceType Previous
	{
		get { return this.previous; }
	}
}
