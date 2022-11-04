using System;
using UnityEngine;

public enum BucketContentsType
{
	Water,
	Gas,
	Weights // Dumbbells, etc..
}

// [af] Abstract class for the contents of a bucket. It encompasses the behaviors expected 
// from each kind of content.
public abstract class BucketContents
{
	public abstract BucketContentsType Type { get; }
	public abstract bool IsCleaningAgent { get; }
	public abstract bool IsFlammable { get; }
	public abstract bool CanBeLifted(int strength);
}

[Serializable]
public class BucketWater : BucketContents
{
	[SerializeField] float bloodiness;
	[SerializeField] bool hasBleach;

	public float Bloodiness
	{
		get { return this.bloodiness; }
		set { this.bloodiness = Mathf.Clamp01(value); }
	}

	public bool HasBleach
	{
		get { return this.hasBleach; }
		set { this.hasBleach = value; }
	}

	public override BucketContentsType Type { get { return BucketContentsType.Water; } }
	public override bool IsCleaningAgent { get { return this.hasBleach; } }
	public override bool IsFlammable { get { return false; } }
	public override bool CanBeLifted(int strength) { return true; }
}

[Serializable]
public class BucketGas : BucketContents
{
	public override BucketContentsType Type { get { return BucketContentsType.Gas; } }
	public override bool IsCleaningAgent { get { return false; } }
	public override bool IsFlammable { get { return true; } }
	public override bool CanBeLifted(int strength) { return true; }
}

[Serializable]
public class BucketWeights : BucketContents
{
	[SerializeField] int count;

	public int Count
	{
		get { return this.count; }
		set { this.count = (value < 0) ? 0 : value; }
	}

	public override BucketContentsType Type { get { return BucketContentsType.Weights; } }
	public override bool IsCleaningAgent { get { return false; } }
	public override bool IsFlammable { get { return false; } }

	public override bool CanBeLifted(int strength)
	{
		// [af] In the future, this could be a more specific calculation, like whether
		// the count is less than the strength.
		return strength > 0;
	}
}
