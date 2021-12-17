using System.Collections.Generic;

public static class AnimationTags
{
	public static AnimationTag Accessories = new AnimationTag(AnimationTagType.Accessories, 0, 0);
	public static AnimationTag Turnaround = new AnimationTag(AnimationTagType.Turnaround, 1, 8);
	public static AnimationTag Run = new AnimationTag(AnimationTagType.Run, 9, 16);
	public static AnimationTag RunDirChange = new AnimationTag(AnimationTagType.RunDirChange, 17, 18);
	public static AnimationTag BatCharge = new AnimationTag(AnimationTagType.BatCharge, 19, 24);
	public static AnimationTag Bat = new AnimationTag(AnimationTagType.Bat, 25, 25);
	public static AnimationTag BatRecover = new AnimationTag(AnimationTagType.BatRecover, 27, 28);
	public static AnimationTag BatDiagCharge = new AnimationTag(AnimationTagType.BatDiagCharge, 29, 34);
	public static AnimationTag BatDiag = new AnimationTag(AnimationTagType.BatDiag, 35, 35);
	public static AnimationTag BatDiagRecover = new AnimationTag(AnimationTagType.BatDiagRecover, 37, 38);
	public static AnimationTag BatUpCharge = new AnimationTag(AnimationTagType.BatUpCharge, 39, 44);
	public static AnimationTag BatUp = new AnimationTag(AnimationTagType.BatUp, 45, 45);
	public static AnimationTag BatUpRecover = new AnimationTag(AnimationTagType.BatUpRecover, 47, 48);
	public static AnimationTag BatDiagDown = new AnimationTag(AnimationTagType.BatDiagDown, 49, 49);
	public static AnimationTag BatDiagDownRecover = new AnimationTag(AnimationTagType.BatDiagDownRecover, 51, 52);
	public static AnimationTag BatDownCharge = new AnimationTag(AnimationTagType.BatDownCharge, 53, 53);
	public static AnimationTag BatDown = new AnimationTag(AnimationTagType.BatDown, 54, 54);
	public static AnimationTag BatDownRecover = new AnimationTag(AnimationTagType.BatDownRecover, 55, 56);
	public static AnimationTag JumpUpRight = new AnimationTag(AnimationTagType.JumpUpRight, 57, 58);
	public static AnimationTag JumpUp = new AnimationTag(AnimationTagType.JumpUp, 59, 60);
	public static AnimationTag JumpLaunch = new AnimationTag(AnimationTagType.JumpLaunch, 59, 59);
	public static AnimationTag JumpUpToDownTransition = new AnimationTag(AnimationTagType.JumpUpToDownTransition, 61, 61);
	public static AnimationTag JumpDownArmsOut = new AnimationTag(AnimationTagType.JumpDownArmsOut, 62, 63);
	public static AnimationTag JumpDown = new AnimationTag(AnimationTagType.JumpDown, 64, 65);
	public static AnimationTag JumpSomersault = new AnimationTag(AnimationTagType.JumpSomersault, 66, 73);
	public static AnimationTag StandRight = new AnimationTag(AnimationTagType.StandRight, 74, 74);
	public static AnimationTag Wallslide = new AnimationTag(AnimationTagType.Wallslide, 75, 76);
	public static AnimationTag WallslideJump = new AnimationTag(AnimationTagType.WallslideJump, 77, 77);
	public static AnimationTag HitRecovered = new AnimationTag(AnimationTagType.HitRecovered, 78, 79);
	public static AnimationTag Impact = new AnimationTag(AnimationTagType.Impact, 80, 81);
	public static AnimationTag HitFly = new AnimationTag(AnimationTagType.HitFly, 82, 83);
	public static AnimationTag HitComet = new AnimationTag(AnimationTagType.HitComet, 84, 87);
	public static AnimationTag SkidLand = new AnimationTag(AnimationTagType.SkidLand, 88, 88);
	public static AnimationTag Skid = new AnimationTag(AnimationTagType.Skid, 89, 90);
	public static AnimationTag SkidRecover = new AnimationTag(AnimationTagType.SkidRecover, 91, 91);
	public static AnimationTag HitSpin = new AnimationTag(AnimationTagType.HitSpin, 92, 99);
	public static AnimationTag HitRotate = new AnimationTag(AnimationTagType.HitRotate, 100, 107);
	public static AnimationTag Idle = new AnimationTag(AnimationTagType.Idle, 108, 112);
	public static AnimationTag IdleTransition = new AnimationTag(AnimationTagType.IdleTransition, 108, 108);
	public static AnimationTag IdleLoop = new AnimationTag(AnimationTagType.IdleLoop, 109, 112);
	public static AnimationTag WinTransition = new AnimationTag(AnimationTagType.WinTransition, 113, 114);
	public static AnimationTag Win = new AnimationTag(AnimationTagType.Win, 115, 117);
	public static AnimationTag BurpStart = new AnimationTag(AnimationTagType.BurpStart, 118, 121);
	public static AnimationTag BurpLoop = new AnimationTag(AnimationTagType.BurpLoop, 122, 124);
	public static AnimationTag ThrowCharge = new AnimationTag(AnimationTagType.ThrowCharge, 125, 126);
	public static AnimationTag Throw = new AnimationTag(AnimationTagType.Throw, 127, 128);
	public static AnimationTag ThrowUpRightCharge = new AnimationTag(AnimationTagType.ThrowUpRightCharge, 129, 130);
	public static AnimationTag ThrowUpRight = new AnimationTag(AnimationTagType.ThrowUpRight, 131, 132);
	public static AnimationTag ThrowUpCharge = new AnimationTag(AnimationTagType.ThrowUpCharge, 133, 134);
	public static AnimationTag ThrowUp = new AnimationTag(AnimationTagType.ThrowUp, 135, 136);
	public static AnimationTag ThrowDownRightCharge = new AnimationTag(AnimationTagType.ThrowDownRightCharge, 137, 138);
	public static AnimationTag ThrowDownRight = new AnimationTag(AnimationTagType.ThrowDownRight, 139, 140);
	public static AnimationTag ThrowDownRightAirCharge = new AnimationTag(AnimationTagType.ThrowDownRightAirCharge, 141, 142);
	public static AnimationTag ThrowDownRightAir = new AnimationTag(AnimationTagType.ThrowDownRightAir, 143, 144);
	public static AnimationTag ThrowDownCharge = new AnimationTag(AnimationTagType.ThrowDownCharge, 145, 146);
	public static AnimationTag ThrowDown = new AnimationTag(AnimationTagType.ThrowDown, 147, 148);
	public static AnimationTag ThrowDownAirCharge = new AnimationTag(AnimationTagType.ThrowDownAirCharge, 149, 150);
	public static AnimationTag ThrowDownAir = new AnimationTag(AnimationTagType.ThrowDownAir, 151, 152);
	public static AnimationTag FrogTongue = new AnimationTag(AnimationTagType.FrogTongue, 153, 153);
	public static AnimationTag FrogTongueJumpUp = new AnimationTag(AnimationTagType.FrogTongueJumpUp, 154, 154);
	public static AnimationTag FrogTongueJumpDown = new AnimationTag(AnimationTagType.FrogTongueJumpDown, 155, 155);
	public static AnimationTag FrogTongueStunned = new AnimationTag(AnimationTagType.FrogTongueStunned, 156, 156);
	public static AnimationTag FrogBlush = new AnimationTag(AnimationTagType.FrogBlush, 157, 157);
	public static AnimationTag DogeCoinTransition = new AnimationTag(AnimationTagType.DogeCoinTransition, 158, 158);
	public static AnimationTag DogeCoinPowerup = new AnimationTag(AnimationTagType.DogeCoinPowerup, 159, 162);
	public static AnimationTag DogeCoinSpin = new AnimationTag(AnimationTagType.DogeCoinSpin, 163, 170);
	public static AnimationTag DogeSwoleTransition = new AnimationTag(AnimationTagType.DogeSwoleTransition, 171, 172);
	public static AnimationTag DogeSwole = new AnimationTag(AnimationTagType.DogeSwole, 173, 174);
	public static AnimationTag AlienTeaStart = new AnimationTag(AnimationTagType.AlienTeaStart, 175, 175);
	public static AnimationTag AlienTea = new AnimationTag(AnimationTagType.AlienTea, 176, 179);
	public static AnimationTag AlienTeaTurned = new AnimationTag(AnimationTagType.AlienTeaTurned, 180, 183);
	public static AnimationTag AlienTeaSip = new AnimationTag(AnimationTagType.AlienTeaSip, 184, 184);
	public static AnimationTag ApeScratchHeadStart = new AnimationTag(AnimationTagType.ApeScratchHeadStart, 185, 185);
	public static AnimationTag ApeScratchHeadLookFwd = new AnimationTag(AnimationTagType.ApeScratchHeadLookFwd, 186, 187);
	public static AnimationTag ApeScratchHeadLookSide = new AnimationTag(AnimationTagType.ApeScratchHeadLookSide, 188, 189);
	public static AnimationTag HumanSquatStart = new AnimationTag(AnimationTagType.HumanSquatStart, 190, 190);
	public static AnimationTag HumanSquatLoop = new AnimationTag(AnimationTagType.HumanSquatLoop, 191, 192);
	public static AnimationTag FrogSit = new AnimationTag(AnimationTagType.FrogSit, 193, 193);
	public static AnimationTag FrogCroak = new AnimationTag(AnimationTagType.FrogCroak, 194, 196);
	public static AnimationTag CatWiggleStart = new AnimationTag(AnimationTagType.CatWiggleStart, 197, 197);
	public static AnimationTag CatWiggle = new AnimationTag(AnimationTagType.CatWiggle, 198, 201);
	public static AnimationTag CatIdleExcited = new AnimationTag(AnimationTagType.CatIdleExcited, 202, 205);
	public static AnimationTag Empty = new AnimationTag(AnimationTagType.Empty, 206, 206);

	public const int totalFrameCount = 207;
	public static AnimationTag[] tagsArray = new AnimationTag[] {
		Accessories,
		Turnaround,
		Run,
		RunDirChange,
		BatCharge,
		Bat,
		BatRecover,
		BatDiagCharge,
		BatDiag,
		BatDiagRecover,
		BatUpCharge,
		BatUp,
		BatUpRecover,
		BatDiagDown,
		BatDiagDownRecover,
		BatDownCharge,
		BatDown,
		BatDownRecover,
		JumpUpRight,
		JumpUp,
		JumpLaunch,
		JumpUpToDownTransition,
		JumpDownArmsOut,
		JumpDown,
		JumpSomersault,
		StandRight,
		Wallslide,
		WallslideJump,
		HitRecovered,
		Impact,
		HitFly,
		HitComet,
		SkidLand,
		Skid,
		SkidRecover,
		HitSpin,
		HitRotate,
		Idle,
		IdleTransition,
		IdleLoop,
		WinTransition,
		Win,
		BurpStart,
		BurpLoop,
		ThrowCharge,
		Throw,
		ThrowUpRightCharge,
		ThrowUpRight,
		ThrowUpCharge,
		ThrowUp,
		ThrowDownRightCharge,
		ThrowDownRight,
		ThrowDownRightAirCharge,
		ThrowDownRightAir,
		ThrowDownCharge,
		ThrowDown,
		ThrowDownAirCharge,
		ThrowDownAir,
		FrogTongue,
		FrogTongueJumpUp,
		FrogTongueJumpDown,
		FrogTongueStunned,
		FrogBlush,
		DogeCoinTransition,
		DogeCoinPowerup,
		DogeCoinSpin,
		DogeSwoleTransition,
		DogeSwole,
		AlienTeaStart,
		AlienTea,
		AlienTeaTurned,
		AlienTeaSip,
		ApeScratchHeadStart,
		ApeScratchHeadLookFwd,
		ApeScratchHeadLookSide,
		HumanSquatStart,
		HumanSquatLoop,
		FrogSit,
		FrogCroak,
		CatWiggleStart,
		CatWiggle,
		CatIdleExcited,
		Empty,
	};

	public static Dictionary<AnimationTagType, AnimationTag> tags = new Dictionary<AnimationTagType, AnimationTag>() {
		{ AnimationTagType.Accessories, Accessories },
		{ AnimationTagType.Turnaround, Turnaround },
		{ AnimationTagType.Run, Run },
		{ AnimationTagType.RunDirChange, RunDirChange },
		{ AnimationTagType.BatCharge, BatCharge },
		{ AnimationTagType.Bat, Bat },
		{ AnimationTagType.BatRecover, BatRecover },
		{ AnimationTagType.BatDiagCharge, BatDiagCharge },
		{ AnimationTagType.BatDiag, BatDiag },
		{ AnimationTagType.BatDiagRecover, BatDiagRecover },
		{ AnimationTagType.BatUpCharge, BatUpCharge },
		{ AnimationTagType.BatUp, BatUp },
		{ AnimationTagType.BatUpRecover, BatUpRecover },
		{ AnimationTagType.BatDiagDown, BatDiagDown },
		{ AnimationTagType.BatDiagDownRecover, BatDiagDownRecover },
		{ AnimationTagType.BatDownCharge, BatDownCharge },
		{ AnimationTagType.BatDown, BatDown },
		{ AnimationTagType.BatDownRecover, BatDownRecover },
		{ AnimationTagType.JumpUpRight, JumpUpRight },
		{ AnimationTagType.JumpUp, JumpUp },
		{ AnimationTagType.JumpLaunch, JumpLaunch },
		{ AnimationTagType.JumpUpToDownTransition, JumpUpToDownTransition },
		{ AnimationTagType.JumpDownArmsOut, JumpDownArmsOut },
		{ AnimationTagType.JumpDown, JumpDown },
		{ AnimationTagType.JumpSomersault, JumpSomersault },
		{ AnimationTagType.StandRight, StandRight },
		{ AnimationTagType.Wallslide, Wallslide },
		{ AnimationTagType.WallslideJump, WallslideJump },
		{ AnimationTagType.HitRecovered, HitRecovered },
		{ AnimationTagType.Impact, Impact },
		{ AnimationTagType.HitFly, HitFly },
		{ AnimationTagType.HitComet, HitComet },
		{ AnimationTagType.SkidLand, SkidLand },
		{ AnimationTagType.Skid, Skid },
		{ AnimationTagType.SkidRecover, SkidRecover },
		{ AnimationTagType.HitSpin, HitSpin },
		{ AnimationTagType.HitRotate, HitRotate },
		{ AnimationTagType.Idle, Idle },
		{ AnimationTagType.IdleTransition, IdleTransition },
		{ AnimationTagType.IdleLoop, IdleLoop },
		{ AnimationTagType.WinTransition, WinTransition },
		{ AnimationTagType.Win, Win },
		{ AnimationTagType.BurpStart, BurpStart },
		{ AnimationTagType.BurpLoop, BurpLoop },
		{ AnimationTagType.ThrowCharge, ThrowCharge },
		{ AnimationTagType.Throw, Throw },
		{ AnimationTagType.ThrowUpRightCharge, ThrowUpRightCharge },
		{ AnimationTagType.ThrowUpRight, ThrowUpRight },
		{ AnimationTagType.ThrowUpCharge, ThrowUpCharge },
		{ AnimationTagType.ThrowUp, ThrowUp },
		{ AnimationTagType.ThrowDownRightCharge, ThrowDownRightCharge },
		{ AnimationTagType.ThrowDownRight, ThrowDownRight },
		{ AnimationTagType.ThrowDownRightAirCharge, ThrowDownRightAirCharge },
		{ AnimationTagType.ThrowDownRightAir, ThrowDownRightAir },
		{ AnimationTagType.ThrowDownCharge, ThrowDownCharge },
		{ AnimationTagType.ThrowDown, ThrowDown },
		{ AnimationTagType.ThrowDownAirCharge, ThrowDownAirCharge },
		{ AnimationTagType.ThrowDownAir, ThrowDownAir },
		{ AnimationTagType.FrogTongue, FrogTongue },
		{ AnimationTagType.FrogTongueJumpUp, FrogTongueJumpUp },
		{ AnimationTagType.FrogTongueJumpDown, FrogTongueJumpDown },
		{ AnimationTagType.FrogTongueStunned, FrogTongueStunned },
		{ AnimationTagType.FrogBlush, FrogBlush },
		{ AnimationTagType.DogeCoinTransition, DogeCoinTransition },
		{ AnimationTagType.DogeCoinPowerup, DogeCoinPowerup },
		{ AnimationTagType.DogeCoinSpin, DogeCoinSpin },
		{ AnimationTagType.DogeSwoleTransition, DogeSwoleTransition },
		{ AnimationTagType.DogeSwole, DogeSwole },
		{ AnimationTagType.AlienTeaStart, AlienTeaStart },
		{ AnimationTagType.AlienTea, AlienTea },
		{ AnimationTagType.AlienTeaTurned, AlienTeaTurned },
		{ AnimationTagType.AlienTeaSip, AlienTeaSip },
		{ AnimationTagType.ApeScratchHeadStart, ApeScratchHeadStart },
		{ AnimationTagType.ApeScratchHeadLookFwd, ApeScratchHeadLookFwd },
		{ AnimationTagType.ApeScratchHeadLookSide, ApeScratchHeadLookSide },
		{ AnimationTagType.HumanSquatStart, HumanSquatStart },
		{ AnimationTagType.HumanSquatLoop, HumanSquatLoop },
		{ AnimationTagType.FrogSit, FrogSit },
		{ AnimationTagType.FrogCroak, FrogCroak },
		{ AnimationTagType.CatWiggleStart, CatWiggleStart },
		{ AnimationTagType.CatWiggle, CatWiggle },
		{ AnimationTagType.CatIdleExcited, CatIdleExcited },
		{ AnimationTagType.Empty, Empty },
	};
}

public struct AnimationTag
{
	public AnimationTagType tagType;
	public int frameFrom;
	public int frameTo;
	public int[] frames;
	public int frameCount;

	public AnimationTag(AnimationTagType tagType, int frameFrom, int frameTo)
	{
		this.tagType = tagType;
		this.frameFrom = frameFrom;
		this.frameTo = frameTo;
		this.frameCount = frameTo - frameFrom + 1;
		this.frames = new int[this.frameCount];
		int index = 0;
		for (int f = frameFrom; f <= frameTo; f++)
		{
			this.frames[index++] = f;
		}
	}
}

public enum AnimationTagType
{
	Accessories,
	Turnaround,
	Run,
	RunDirChange,
	BatCharge,
	Bat,
	BatRecover,
	BatDiagCharge,
	BatDiag,
	BatDiagRecover,
	BatUpCharge,
	BatUp,
	BatUpRecover,
	BatDiagDown,
	BatDiagDownRecover,
	BatDownCharge,
	BatDown,
	BatDownRecover,
	JumpUpRight,
	JumpUp,
	JumpLaunch,
	JumpUpToDownTransition,
	JumpDownArmsOut,
	JumpDown,
	JumpSomersault,
	StandRight,
	Wallslide,
	WallslideJump,
	HitRecovered,
	Impact,
	HitFly,
	HitComet,
	SkidLand,
	Skid,
	SkidRecover,
	HitSpin,
	HitRotate,
	Idle,
	IdleTransition,
	IdleLoop,
	WinTransition,
	Win,
	BurpStart,
	BurpLoop,
	ThrowCharge,
	Throw,
	ThrowUpRightCharge,
	ThrowUpRight,
	ThrowUpCharge,
	ThrowUp,
	ThrowDownRightCharge,
	ThrowDownRight,
	ThrowDownRightAirCharge,
	ThrowDownRightAir,
	ThrowDownCharge,
	ThrowDown,
	ThrowDownAirCharge,
	ThrowDownAir,
	FrogTongue,
	FrogTongueJumpUp,
	FrogTongueJumpDown,
	FrogTongueStunned,
	FrogBlush,
	DogeCoinTransition,
	DogeCoinPowerup,
	DogeCoinSpin,
	DogeSwoleTransition,
	DogeSwole,
	AlienTeaStart,
	AlienTea,
	AlienTeaTurned,
	AlienTeaSip,
	ApeScratchHeadStart,
	ApeScratchHeadLookFwd,
	ApeScratchHeadLookSide,
	HumanSquatStart,
	HumanSquatLoop,
	FrogSit,
	FrogCroak,
	CatWiggleStart,
	CatWiggle,
	CatIdleExcited,
	Empty,
}
