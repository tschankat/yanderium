using UnityEngine;

public enum CollectibleType
{
	HeadmasterTape,
	BasementTape,
	Manga,
	Tape,
	Key,
    Panty
}

public class CollectibleScript : MonoBehaviour
{
	public PromptScript Prompt;
	public string Name = string.Empty;
	public int Type = 0;
	public int ID = 0;

	void Start()
	{
		// [af] This method with Globals is actually worse than before (because of
		// more code), but maybe there could be a Globals.Get/SetCollectible() pair 
		// of methods that take the collectible type, and then they figure out which 
		// one to get/set for us.
		bool collected = 
			((this.CollectibleType == CollectibleType.BasementTape) && CollectibleGlobals.GetBasementTapeCollected(this.ID)) ||
			((this.CollectibleType == CollectibleType.Manga) && CollectibleGlobals.GetMangaCollected(this.ID)) ||
			((this.CollectibleType == CollectibleType.Tape) && CollectibleGlobals.GetTapeCollected(this.ID)) ||
            ((this.CollectibleType == CollectibleType.Panty) && CollectibleGlobals.GetPantyPurchased(11));

        if (collected)
		{
			Destroy(this.gameObject);
		}

		if (GameGlobals.LoveSick || MissionModeGlobals.MissionMode)
		{
			Destroy(this.gameObject);
		}
	}

	public CollectibleType CollectibleType
	{
		get
		{
			if (this.Name == "HeadmasterTape")
			{
				return CollectibleType.HeadmasterTape;
			}
			else if (this.Name == "BasementTape")
			{
				return CollectibleType.BasementTape;
			}
			else if (this.Name == "Manga")
			{
				return CollectibleType.Manga;
			}
			else if (this.Name == "Tape")
			{
				return CollectibleType.Tape;
			}
			else if (this.Type == 5)
			{
				return CollectibleType.Key;
			}
            else if (this.Type == 6)
            {
                return CollectibleType.Panty;
            }
            else
			{
				Debug.LogError("Unrecognized collectible \"" + this.Name + "\".", this.gameObject);
				return CollectibleType.Tape;
			}
		}
	}

	void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0.0f)
		{
			if (this.CollectibleType == CollectibleType.HeadmasterTape)
			{
				CollectibleGlobals.SetHeadmasterTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.BasementTape)
			{
				CollectibleGlobals.SetBasementTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Manga)
			{
				CollectibleGlobals.SetMangaCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Tape)
			{
				CollectibleGlobals.SetTapeCollected(this.ID, true);
			}
			else if (this.CollectibleType == CollectibleType.Key)
			{
				Prompt.Yandere.Inventory.MysteriousKeys++;
			}
            else if (this.CollectibleType == CollectibleType.Panty)
            {
                CollectibleGlobals.SetPantyPurchased(11, true);
            }
            else
			{
				Debug.LogError("Collectible \"" + this.Name + "\" not implemented.", this.gameObject);
			}

			Destroy(this.gameObject);
		}
	}
}
