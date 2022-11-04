using UnityEngine;

public class InventoryScript : MonoBehaviour
{
	public SchemesScript Schemes;

	public bool ModifiedUniform = false;
	public bool DirectionalMic = false;
	public bool DuplicateSheet = false;
	public bool AnswerSheet = false;
	public bool MaskingTape = false;
	public bool RivalPhone = false;
	public bool LockPick = false;
	public bool Headset = false;
	public bool FakeID = false;
	public bool IDCard = false;
	public bool Book = false;

	//Bombs
	public bool AmnesiaBomb = false;
	public bool SmokeBomb = false;
	public bool StinkBomb = false;

	//Lethal Poisons
	public bool LethalPoison = false;
	public bool ChemicalPoison = false;

	//Emetic Poisons
	public bool EmeticPoison = false;
	public bool RatPoison = false;

	//Headache Poisons
	public bool HeadachePoison = false;

	//Sedatives
	public bool Tranquilizer = false;
	public bool Sedative = false;

	public bool Cigs = false;
	public bool Ring = false;
	public bool Rose = false;
	public bool Sake = false;
	public bool Soda = false;
	public bool Bra = false;

	public bool CabinetKey = false;
	public bool CaseKey = false;
	public bool SafeKey = false;
	public bool ShedKey = false;

	public int MysteriousKeys = 0;
    public int LethalPoisons = 0;
    public int RivalPhoneID = 0;
    public int PantyShots = 0;

	public float Money;

	public bool[] ShrineCollectibles;

	public UILabel MoneyLabel;

	void Start()
	{
		PantyShots = PlayerGlobals.PantyShots;
		Money = PlayerGlobals.Money;

		UpdateMoney();
	}

	public void UpdateMoney()
	{
		//Debug.Log("Inventory says that money is " + PlayerGlobals.Money);

		MoneyLabel.text = "$" + Money.ToString("F2", System.Globalization.NumberFormatInfo.InvariantInfo);
	}
}