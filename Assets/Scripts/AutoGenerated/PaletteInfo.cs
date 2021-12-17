using UnityEngine;
using System.Collections.Generic;

public static class PaletteInfo
{
	public const int numColors = 34;

	public static PaletteColor paletteColor0 = new PaletteColor(0, PaletteSwapIndex.Swap0_AutumnRobin, new Color32(190, 074, 047, 255), "Autumn Robin");
	public static PaletteColor paletteColor1 = new PaletteColor(1, PaletteSwapIndex.Swap1_JapaneseKoi, new Color32(215, 118, 067, 255), "Japanese Koi");
	public static PaletteColor paletteColor2 = new PaletteColor(2, PaletteSwapIndex.Swap2_CanaryIsland, new Color32(234, 212, 170, 255), "Canary Island");
	public static PaletteColor paletteColor3 = new PaletteColor(3, PaletteSwapIndex.Swap3_SweetCurry, new Color32(228, 166, 114, 255), "Sweet Curry");
	public static PaletteColor paletteColor4 = new PaletteColor(4, PaletteSwapIndex.Swap4_IcedTea, new Color32(184, 111, 080, 255), "Iced Tea");
	public static PaletteColor paletteColor5 = new PaletteColor(5, PaletteSwapIndex.Swap5_SingleOrigin, new Color32(115, 062, 057, 255), "Single Origin");
	public static PaletteColor paletteColor6 = new PaletteColor(6, PaletteSwapIndex.Swap6_Toledo, new Color32(062, 039, 049, 255), "Toledo");
	public static PaletteColor paletteColor7 = new PaletteColor(7, PaletteSwapIndex.Swap7_JapaneseCarmine, new Color32(162, 038, 051, 255), "Japanese Carmine");
	public static PaletteColor paletteColor8 = new PaletteColor(8, PaletteSwapIndex.Swap8_VermilionCinnabar, new Color32(228, 059, 068, 255), "Vermilion Cinnabar");
	public static PaletteColor paletteColor9 = new PaletteColor(9, PaletteSwapIndex.Swap9_OrneryTangerine, new Color32(247, 119, 034, 255), "Ornery Tangerine");
	public static PaletteColor paletteColor10 = new PaletteColor(10, PaletteSwapIndex.Swap10_RipePumpkin, new Color32(254, 174, 052, 255), "Ripe Pumpkin");
	public static PaletteColor paletteColor11 = new PaletteColor(11, PaletteSwapIndex.Swap11_PrimrosePath, new Color32(254, 231, 097, 255), "Primrose Path");
	public static PaletteColor paletteColor12 = new PaletteColor(12, PaletteSwapIndex.Swap12_RangeLand, new Color32(099, 199, 077, 255), "Range Land");
	public static PaletteColor paletteColor13 = new PaletteColor(13, PaletteSwapIndex.Swap13_Greenery, new Color32(062, 137, 072, 255), "Greenery");
	public static PaletteColor paletteColor14 = new PaletteColor(14, PaletteSwapIndex.Swap14_Greenbriar, new Color32(038, 092, 066, 255), "Greenbriar");
	public static PaletteColor paletteColor15 = new PaletteColor(15, PaletteSwapIndex.Swap15_Nordic, new Color32(025, 060, 062, 255), "Nordic");
	public static PaletteColor paletteColor16 = new PaletteColor(16, PaletteSwapIndex.Swap16_BaleineBlue, new Color32(018, 078, 137, 255), "Baleine Blue");
	public static PaletteColor paletteColor17 = new PaletteColor(17, PaletteSwapIndex.Swap17_Atmosphere, new Color32(000, 153, 219, 255), "Atmosphere");
	public static PaletteColor paletteColor18 = new PaletteColor(18, PaletteSwapIndex.Swap18_SparkyBlue, new Color32(044, 232, 245, 255), "Sparky Blue");
	public static PaletteColor paletteColor19 = new PaletteColor(19, PaletteSwapIndex.Swap19_White, new Color32(254, 254, 254, 255), "White");
	public static PaletteColor paletteColor20 = new PaletteColor(20, PaletteSwapIndex.Swap20_IcyBrook, new Color32(192, 203, 220, 255), "Icy Brook");
	public static PaletteColor paletteColor21 = new PaletteColor(21, PaletteSwapIndex.Swap21_BlueberryBuckle, new Color32(139, 155, 180, 255), "Blueberry Buckle");
	public static PaletteColor paletteColor22 = new PaletteColor(22, PaletteSwapIndex.Swap22_Allegiance, new Color32(090, 105, 136, 255), "Allegiance");
	public static PaletteColor paletteColor23 = new PaletteColor(23, PaletteSwapIndex.Swap23_BonneNuit, new Color32(058, 068, 102, 255), "Bonne Nuit");
	public static PaletteColor paletteColor24 = new PaletteColor(24, PaletteSwapIndex.Swap24_LatinCharm, new Color32(038, 043, 068, 255), "Latin Charm");
	public static PaletteColor paletteColor25 = new PaletteColor(25, PaletteSwapIndex.Swap25_RiverStyx, new Color32(024, 020, 037, 255), "River Styx");
	public static PaletteColor paletteColor26 = new PaletteColor(26, PaletteSwapIndex.Swap26_CherrySoda, new Color32(255, 000, 068, 255), "Cherry Soda");
	public static PaletteColor paletteColor27 = new PaletteColor(27, PaletteSwapIndex.Swap27_Seance, new Color32(104, 056, 108, 255), "Seance");
	public static PaletteColor paletteColor28 = new PaletteColor(28, PaletteSwapIndex.Swap28_SignalPink, new Color32(181, 080, 136, 255), "Signal Pink");
	public static PaletteColor paletteColor29 = new PaletteColor(29, PaletteSwapIndex.Swap29_Begonia, new Color32(246, 117, 122, 255), "Begonia");
	public static PaletteColor paletteColor30 = new PaletteColor(30, PaletteSwapIndex.Swap30_GentleDoe, new Color32(232, 183, 150, 255), "Gentle Doe");
	public static PaletteColor paletteColor31 = new PaletteColor(31, PaletteSwapIndex.Swap31_ToastedNut, new Color32(194, 133, 105, 255), "Toasted Nut");
	public static PaletteColor paletteColor32 = new PaletteColor(32, PaletteSwapIndex.Swap32_PinkOCD, new Color32(107, 054, 255, 255), "Pink OCD");
	public static PaletteColor paletteColor33 = new PaletteColor(33, PaletteSwapIndex.Swap33_LakeRetbaPink, new Color32(244, 090, 228, 255), "Lake Retba Pink");

	public static PaletteColor[] paletteColors = new PaletteColor[] {
		paletteColor0,
		paletteColor1,
		paletteColor2,
		paletteColor3,
		paletteColor4,
		paletteColor5,
		paletteColor6,
		paletteColor7,
		paletteColor8,
		paletteColor9,
		paletteColor10,
		paletteColor11,
		paletteColor12,
		paletteColor13,
		paletteColor14,
		paletteColor15,
		paletteColor16,
		paletteColor17,
		paletteColor18,
		paletteColor19,
		paletteColor20,
		paletteColor21,
		paletteColor22,
		paletteColor23,
		paletteColor24,
		paletteColor25,
		paletteColor26,
		paletteColor27,
		paletteColor28,
		paletteColor29,
		paletteColor30,
		paletteColor31,
		paletteColor32,
		paletteColor33,
	};

	public static Color32[] colors = new Color32[] {
		new Color32(190, 074, 047, 255),
		new Color32(215, 118, 067, 255),
		new Color32(234, 212, 170, 255),
		new Color32(228, 166, 114, 255),
		new Color32(184, 111, 080, 255),
		new Color32(115, 062, 057, 255),
		new Color32(062, 039, 049, 255),
		new Color32(162, 038, 051, 255),
		new Color32(228, 059, 068, 255),
		new Color32(247, 119, 034, 255),
		new Color32(254, 174, 052, 255),
		new Color32(254, 231, 097, 255),
		new Color32(099, 199, 077, 255),
		new Color32(062, 137, 072, 255),
		new Color32(038, 092, 066, 255),
		new Color32(025, 060, 062, 255),
		new Color32(018, 078, 137, 255),
		new Color32(000, 153, 219, 255),
		new Color32(044, 232, 245, 255),
		new Color32(254, 254, 254, 255),
		new Color32(192, 203, 220, 255),
		new Color32(139, 155, 180, 255),
		new Color32(090, 105, 136, 255),
		new Color32(058, 068, 102, 255),
		new Color32(038, 043, 068, 255),
		new Color32(024, 020, 037, 255),
		new Color32(255, 000, 068, 255),
		new Color32(104, 056, 108, 255),
		new Color32(181, 080, 136, 255),
		new Color32(246, 117, 122, 255),
		new Color32(232, 183, 150, 255),
		new Color32(194, 133, 105, 255),
		new Color32(107, 054, 255, 255),
		new Color32(244, 090, 228, 255),
	};

	public static Dictionary<int, PaletteColor> indexMap = new Dictionary<int, PaletteColor> {
		{ 0, paletteColor26 },
		{ 20, paletteColor25 },
		{ 38, paletteColor7 },
		{ 39, paletteColor6 },
		{ 43, paletteColor24 },
		{ 54, paletteColor32 },
		{ 56, paletteColor27 },
		{ 59, paletteColor8 },
		{ 60, paletteColor15 },
		{ 62, paletteColor5 },
		{ 68, paletteColor23 },
		{ 74, paletteColor0 },
		{ 78, paletteColor16 },
		{ 80, paletteColor28 },
		{ 90, paletteColor33 },
		{ 92, paletteColor14 },
		{ 105, paletteColor22 },
		{ 111, paletteColor4 },
		{ 117, paletteColor29 },
		{ 118, paletteColor1 },
		{ 119, paletteColor9 },
		{ 133, paletteColor31 },
		{ 137, paletteColor13 },
		{ 153, paletteColor17 },
		{ 155, paletteColor21 },
		{ 166, paletteColor3 },
		{ 174, paletteColor10 },
		{ 183, paletteColor30 },
		{ 199, paletteColor12 },
		{ 203, paletteColor20 },
		{ 212, paletteColor2 },
		{ 231, paletteColor11 },
		{ 232, paletteColor18 },
		{ 254, paletteColor19 },
	};

	public static Dictionary<string, PaletteColor> nameMap = new Dictionary<string, PaletteColor> {
		{ "Allegiance", paletteColor22 },
		{ "Atmosphere", paletteColor17 },
		{ "Autumn Robin", paletteColor0 },
		{ "Baleine Blue", paletteColor16 },
		{ "Begonia", paletteColor29 },
		{ "Blueberry Buckle", paletteColor21 },
		{ "Bonne Nuit", paletteColor23 },
		{ "Canary Island", paletteColor2 },
		{ "Cherry Soda", paletteColor26 },
		{ "Gentle Doe", paletteColor30 },
		{ "Greenbriar", paletteColor14 },
		{ "Greenery", paletteColor13 },
		{ "Iced Tea", paletteColor4 },
		{ "Icy Brook", paletteColor20 },
		{ "Japanese Carmine", paletteColor7 },
		{ "Japanese Koi", paletteColor1 },
		{ "Lake Retba Pink", paletteColor33 },
		{ "Latin Charm", paletteColor24 },
		{ "Nordic", paletteColor15 },
		{ "Ornery Tangerine", paletteColor9 },
		{ "Pink OCD", paletteColor32 },
		{ "Primrose Path", paletteColor11 },
		{ "Range Land", paletteColor12 },
		{ "Ripe Pumpkin", paletteColor10 },
		{ "River Styx", paletteColor25 },
		{ "Seance", paletteColor27 },
		{ "Signal Pink", paletteColor28 },
		{ "Single Origin", paletteColor5 },
		{ "Sparky Blue", paletteColor18 },
		{ "Sweet Curry", paletteColor3 },
		{ "Toasted Nut", paletteColor31 },
		{ "Toledo", paletteColor6 },
		{ "Vermilion Cinnabar", paletteColor8 },
		{ "White", paletteColor19 },
	};

	public static Dictionary<Color32, PaletteColor> colorMap = new Dictionary<Color32, PaletteColor> {
		{ new Color32(190, 074, 047, 255), paletteColor0 },
		{ new Color32(215, 118, 067, 255), paletteColor1 },
		{ new Color32(234, 212, 170, 255), paletteColor2 },
		{ new Color32(228, 166, 114, 255), paletteColor3 },
		{ new Color32(184, 111, 080, 255), paletteColor4 },
		{ new Color32(115, 062, 057, 255), paletteColor5 },
		{ new Color32(062, 039, 049, 255), paletteColor6 },
		{ new Color32(162, 038, 051, 255), paletteColor7 },
		{ new Color32(228, 059, 068, 255), paletteColor8 },
		{ new Color32(247, 119, 034, 255), paletteColor9 },
		{ new Color32(254, 174, 052, 255), paletteColor10 },
		{ new Color32(254, 231, 097, 255), paletteColor11 },
		{ new Color32(099, 199, 077, 255), paletteColor12 },
		{ new Color32(062, 137, 072, 255), paletteColor13 },
		{ new Color32(038, 092, 066, 255), paletteColor14 },
		{ new Color32(025, 060, 062, 255), paletteColor15 },
		{ new Color32(018, 078, 137, 255), paletteColor16 },
		{ new Color32(000, 153, 219, 255), paletteColor17 },
		{ new Color32(044, 232, 245, 255), paletteColor18 },
		{ new Color32(254, 254, 254, 255), paletteColor19 },
		{ new Color32(192, 203, 220, 255), paletteColor20 },
		{ new Color32(139, 155, 180, 255), paletteColor21 },
		{ new Color32(090, 105, 136, 255), paletteColor22 },
		{ new Color32(058, 068, 102, 255), paletteColor23 },
		{ new Color32(038, 043, 068, 255), paletteColor24 },
		{ new Color32(024, 020, 037, 255), paletteColor25 },
		{ new Color32(255, 000, 068, 255), paletteColor26 },
		{ new Color32(104, 056, 108, 255), paletteColor27 },
		{ new Color32(181, 080, 136, 255), paletteColor28 },
		{ new Color32(246, 117, 122, 255), paletteColor29 },
		{ new Color32(232, 183, 150, 255), paletteColor30 },
		{ new Color32(194, 133, 105, 255), paletteColor31 },
		{ new Color32(107, 054, 255, 255), paletteColor32 },
		{ new Color32(244, 090, 228, 255), paletteColor33 },
	};
}

[System.Serializable]
public struct PaletteColor
{
	public int index;
	public PaletteSwapIndex swapIndex;
	public Color32 color;
	public string name;

	public PaletteColor(int index, PaletteSwapIndex swapIndex, Color32 color, string name)
	{
		this.index = index;
		this.swapIndex = swapIndex;
		this.color = color;
		this.name = name;
	}
}

public enum PaletteSwapIndex
{
	Swap0_AutumnRobin = 74,
	Swap1_JapaneseKoi = 118,
	Swap2_CanaryIsland = 212,
	Swap3_SweetCurry = 166,
	Swap4_IcedTea = 111,
	Swap5_SingleOrigin = 62,
	Swap6_Toledo = 39,
	Swap7_JapaneseCarmine = 38,
	Swap8_VermilionCinnabar = 59,
	Swap9_OrneryTangerine = 119,
	Swap10_RipePumpkin = 174,
	Swap11_PrimrosePath = 231,
	Swap12_RangeLand = 199,
	Swap13_Greenery = 137,
	Swap14_Greenbriar = 92,
	Swap15_Nordic = 60,
	Swap16_BaleineBlue = 78,
	Swap17_Atmosphere = 153,
	Swap18_SparkyBlue = 232,
	Swap19_White = 254,
	Swap20_IcyBrook = 203,
	Swap21_BlueberryBuckle = 155,
	Swap22_Allegiance = 105,
	Swap23_BonneNuit = 68,
	Swap24_LatinCharm = 43,
	Swap25_RiverStyx = 20,
	Swap26_CherrySoda = 0,
	Swap27_Seance = 56,
	Swap28_SignalPink = 80,
	Swap29_Begonia = 117,
	Swap30_GentleDoe = 183,
	Swap31_ToastedNut = 133,
	Swap32_PinkOCD = 54,
	Swap33_LakeRetbaPink = 90,
}
