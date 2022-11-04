// [af] Moved here from Globals.cs. I didn't see any school-related files, so I made this one.
public enum SchoolAtmosphereType
{
	High,
	Medium,
	Low
}

public static class SchoolAtmosphere
{
	public static SchoolAtmosphereType Type
	{
		get
		{
			float atmosphere = SchoolGlobals.SchoolAtmosphere;

			if (atmosphere > (2.0f / 3.0f))
			{
				return SchoolAtmosphereType.High;
			}
			else if (atmosphere > (1.0f / 3.0f))
			{
				return SchoolAtmosphereType.Medium;
			}
			else
			{
				return SchoolAtmosphereType.Low;
			}
		}
	}
}
