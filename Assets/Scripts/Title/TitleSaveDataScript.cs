using UnityEngine;

public class TitleSaveDataScript : MonoBehaviour
{
	public GameObject EmptyFile;
	public GameObject Data;

	public Texture[] Bloods;

	public UITexture Blood;

	public UILabel Kills;
	public UILabel Mood;
	public UILabel Alerts;
	public UILabel Week;
	public UILabel Day;
	public UILabel Rival;
	public UILabel Rep;
	public UILabel Club;
	public UILabel Friends;

	public int ID;

	public void Start()
	{
		//Debug.Log("ProfileCreated_" + ID + " is: " + PlayerPrefs.GetInt("ProfileCreated_" + ID));

		if (PlayerPrefs.GetInt("ProfileCreated_" + ID) == 1)
		{
			GameGlobals.Profile = ID;

			this.EmptyFile.SetActive(false);
			this.Data.SetActive(true);

			this.Kills.text = "Kills: " + PlayerGlobals.Kills;
			this.Mood.text = "Mood: " + Mathf.RoundToInt(SchoolGlobals.SchoolAtmosphere * 100.0f);
			this.Alerts.text = "Alerts: " + PlayerGlobals.Alerts;

			this.Week.text = "Week: " + 1;
			this.Day.text = "Day: " + DateGlobals.Weekday;
			this.Rival.text = "Rival: " + "Osana";

			this.Rep.text = "Rep: " + PlayerGlobals.Reputation;
			this.Club.text = "Club: " + ClubGlobals.Club;
			this.Friends.text = "Friends: " + PlayerGlobals.Friends;

			     if (PlayerGlobals.Kills == 0) {Blood.mainTexture = null;}
			else if (PlayerGlobals.Kills > 0)  {Blood.mainTexture = Bloods[1];}
			else if (PlayerGlobals.Kills > 5)  {Blood.mainTexture = Bloods[2];}
			else if (PlayerGlobals.Kills > 10) {Blood.mainTexture = Bloods[3];}
			else if (PlayerGlobals.Kills > 15) {Blood.mainTexture = Bloods[4];}
			else if (PlayerGlobals.Kills > 20) {Blood.mainTexture = Bloods[5];}
		}
		else
		{
			this.EmptyFile.SetActive(true);
			this.Data.SetActive(false);
			this.Blood.enabled = false;
		}
	}
}