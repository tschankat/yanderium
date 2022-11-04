public enum Direction
{
	North, East, South, West
}

// Simple direction class for any compass-related functions.
public static class CardinalDirection
{
	// Gets the direction opposite of the given one.
	public static Direction Reversed(Direction direction)
	{
		if (direction == Direction.North)
		{
			return Direction.South;
		}
		else if (direction == Direction.East)
		{
			return Direction.West;
		}
		else if (direction == Direction.South)
		{
			return Direction.North;
		}
		else
		{
			return Direction.East;
		}
	}

	// Gets the direction 90 degrees to the left of the given one.
	public static Direction LeftPerp(Direction direction)
	{
		if (direction == Direction.North)
		{
			return Direction.West;
		}
		else if (direction == Direction.East)
		{
			return Direction.North;
		}
		else if (direction == Direction.South)
		{
			return Direction.East;
		}
		else
		{
			return Direction.South;
		}
	}

	// Gets the direction 90 degrees to the right of the given one.
	public static Direction RightPerp(Direction direction)
	{
		if (direction == Direction.North)
		{
			return Direction.East;
		}
		else if (direction == Direction.East)
		{
			return Direction.South;
		}
		else if (direction == Direction.South)
		{
			return Direction.West;
		}
		else
		{
			return Direction.North;
		}
	}
}
