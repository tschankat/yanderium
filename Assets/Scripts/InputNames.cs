// [af] This class stores several values used with Unity's virtual buttons.
// The Xbox controller is probably the most widely used and supported one for
// PC gaming, so most of the input is based on its interface.
public static class InputNames
{
	public const int Mouse_LMB = 0;
	public const int Mouse_RMB = 1;
	public const int Mouse_MMB = 2;

	// @todo: Change the strings themselves to be "Xbox_A", etc. here and in the editor
	// so their usage is more clear?
	public const string Xbox_A = "A";
	public const string Xbox_B = "B";
	public const string Xbox_X = "X";
	public const string Xbox_Y = "Y";
	public const string Xbox_LB = "LB";
	public const string Xbox_RB = "RB";
	public const string Xbox_LT = "LT";
	public const string Xbox_RT = "RT";
	public const string Xbox_LS = "LS";
	public const string Xbox_RS = "RS";
	public const string Xbox_Start = "Start";
	public const string Xbox_Back = "Back";
	// @todo: DPad...?

	public const string Yanvania_Horizontal = "VaniaHorizontal";
	public const string Yanvania_Vertical = "VaniaVertical";
	public const string Yanvania_Attack = "VaniaAttack";
	public const string Yanvania_Jump = "VaniaJump";
}
