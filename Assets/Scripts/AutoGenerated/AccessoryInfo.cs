using System.Collections.Generic;
using System.Linq;

public static class Accessories
{
	public static int version = 92;

	public static AccessoryInfo AlienAfro = new AccessoryInfo("AlienAfro", "a_alien_afro", "afro", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienBangs = new AccessoryInfo("AlienBangs", "a_alien_bangs", "bangs", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienBaseballCap = new AccessoryInfo("AlienBaseballCap", "a_alien_baseball-cap", "baseball cap", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienBeanie = new AccessoryInfo("AlienBeanie", "a_alien_beanie", "beanie", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienBowlerHat = new AccessoryInfo("AlienBowlerHat", "a_alien_bowler-hat", "bowler hat", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienBrokenGlasses = new AccessoryInfo("AlienBrokenGlasses", "a_alien_broken-glasses", "broken glasses", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo AlienBun = new AccessoryInfo("AlienBun", "a_alien_bun", "bun", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienBuzzcut = new AccessoryInfo("AlienBuzzcut", "a_alien_buzzcut", "buzzcut", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienCigarette = new AccessoryInfo("AlienCigarette", "a_alien_cigarette", "cigarette", AccessoryType.Mouth, CharacterType.Alien, 26);
	public static AccessoryInfo AlienCircleBeard = new AccessoryInfo("AlienCircleBeard", "a_alien_circle_beard", "circle beard", AccessoryType.Beard, CharacterType.Alien, 38);
	public static AccessoryInfo AlienConstructionHelmet = new AccessoryInfo("AlienConstructionHelmet", "a_alien_construction-helmet", "construction helmet", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienCowboyHat = new AccessoryInfo("AlienCowboyHat", "a_alien_cowboy-hat", "cowboy hat", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienCrown = new AccessoryInfo("AlienCrown", "a_alien_crown", "crown", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienCurly = new AccessoryInfo("AlienCurly", "a_alien_curly", "curly", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienCurtain = new AccessoryInfo("AlienCurtain", "a_alien_curtain", "curtain", AccessoryType.Beard, CharacterType.Alien, 38);
	public static AccessoryInfo AlienCyborg = new AccessoryInfo("AlienCyborg", "a_alien_cyborg", "cyborg", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo AlienDiamondStuds = new AccessoryInfo("AlienDiamondStuds", "a_alien_diamond_studs", "diamond studs", AccessoryType.Piercing, CharacterType.Alien, 44);
	public static AccessoryInfo AlienEyepatch = new AccessoryInfo("AlienEyepatch", "a_alien_eyepatch", "eyepatch", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo AlienEyepatchSkull = new AccessoryInfo("AlienEyepatchSkull", "a_alien_eyepatch_skull", "eyepatch skull", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo AlienFishHat = new AccessoryInfo("AlienFishHat", "a_alien_fish-hat", "fish hat", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienGoatee = new AccessoryInfo("AlienGoatee", "a_alien_goatee", "goatee", AccessoryType.Beard, CharacterType.Alien, 38);
	public static AccessoryInfo AlienGogglesVr = new AccessoryInfo("AlienGogglesVr", "a_alien_goggles_vr", "goggles vr", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo AlienHamburger = new AccessoryInfo("AlienHamburger", "a_alien_hamburger", "hamburger", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienHandlebar = new AccessoryInfo("AlienHandlebar", "a_alien_handlebar", "handlebar", AccessoryType.Beard, CharacterType.Alien, 38);
	public static AccessoryInfo AlienHelmetViking = new AccessoryInfo("AlienHelmetViking", "a_alien_helmet-viking", "helmet viking", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienHighFlatTop = new AccessoryInfo("AlienHighFlatTop", "a_alien_high_flat_top", "high flat top", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienHoopEarring = new AccessoryInfo("AlienHoopEarring", "a_alien_hoop_earring", "hoop earring", AccessoryType.Piercing, CharacterType.Alien, 44);
	public static AccessoryInfo AlienHugeBeard = new AccessoryInfo("AlienHugeBeard", "a_alien_huge_beard", "huge beard", AccessoryType.Beard, CharacterType.Alien, 38);
	public static AccessoryInfo AlienHugeMustache = new AccessoryInfo("AlienHugeMustache", "a_alien_huge_mustache", "huge mustache", AccessoryType.Beard, CharacterType.Alien, 38);
	public static AccessoryInfo AlienKidPropellerHat = new AccessoryInfo("AlienKidPropellerHat", "a_alien_kid-propeller-hat", "kid propeller hat", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienMask = new AccessoryInfo("AlienMask", "a_alien_mask", "mask", AccessoryType.Mouth, CharacterType.Alien, 26);
	public static AccessoryInfo AlienMohavk = new AccessoryInfo("AlienMohavk", "a_alien_mohavk", "mohavk", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienMonacle = new AccessoryInfo("AlienMonacle", "a_alien_monacle", "monacle", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo AlienMustache = new AccessoryInfo("AlienMustache", "a_alien_mustache", "mustache", AccessoryType.Beard, CharacterType.Alien, 38);
	public static AccessoryInfo AlienMutton = new AccessoryInfo("AlienMutton", "a_alien_mutton", "mutton", AccessoryType.Beard, CharacterType.Alien, 38);
	public static AccessoryInfo AlienNarrowGlasses = new AccessoryInfo("AlienNarrowGlasses", "a_alien_narrow-glasses", "narrow glasses", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo AlienNoseRing = new AccessoryInfo("AlienNoseRing", "a_alien_nose_ring", "nose ring", AccessoryType.Piercing, CharacterType.Alien, 44);
	public static AccessoryInfo AlienPacifier = new AccessoryInfo("AlienPacifier", "a_alien_pacifier", "pacifier", AccessoryType.Mouth, CharacterType.Alien, 26);
	public static AccessoryInfo AlienPartyGlasses = new AccessoryInfo("AlienPartyGlasses", "a_alien_party-glasses", "party glasses", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo AlienPartyHat = new AccessoryInfo("AlienPartyHat", "a_alien_party-hat", "party hat", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienPartyHorn = new AccessoryInfo("AlienPartyHorn", "a_alien_party_horn", "party horn", AccessoryType.Mouth, CharacterType.Alien, 26);
	public static AccessoryInfo AlienPigtails = new AccessoryInfo("AlienPigtails", "a_alien_pigtails", "pigtails", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienPipe = new AccessoryInfo("AlienPipe", "a_alien_pipe", "pipe", AccessoryType.Mouth, CharacterType.Alien, 26);
	public static AccessoryInfo AlienPirateHat = new AccessoryInfo("AlienPirateHat", "a_alien_pirate-hat", "pirate hat", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienPoliceHat = new AccessoryInfo("AlienPoliceHat", "a_alien_police-hat", "police hat", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienPonytail = new AccessoryInfo("AlienPonytail", "a_alien_ponytail", "ponytail", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienPulledBack = new AccessoryInfo("AlienPulledBack", "a_alien_pulled_back", "pulled back", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienRectangularGlasses = new AccessoryInfo("AlienRectangularGlasses", "a_alien_rectangular-glasses", "rectangular glasses", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo AlienRoundGlasses = new AccessoryInfo("AlienRoundGlasses", "a_alien_round-glasses", "round glasses", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo AlienSantaHat = new AccessoryInfo("AlienSantaHat", "a_alien_santa-hat", "santa hat", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienShaggy = new AccessoryInfo("AlienShaggy", "a_alien_shaggy", "shaggy", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienSimple = new AccessoryInfo("AlienSimple", "a_alien_simple", "simple", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienSombrero = new AccessoryInfo("AlienSombrero", "a_alien_sombrero", "sombrero", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienSpiky = new AccessoryInfo("AlienSpiky", "a_alien_spiky", "spiky", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienSteampunkGoggles = new AccessoryInfo("AlienSteampunkGoggles", "a_alien_steampunk-goggles", "steampunk goggles", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo AlienStraight = new AccessoryInfo("AlienStraight", "a_alien_straight", "straight", AccessoryType.Hair, CharacterType.Alien, 32);
	public static AccessoryInfo AlienSunglasses = new AccessoryInfo("AlienSunglasses", "a_alien_sunglasses", "sunglasses", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo AlienTopHat = new AccessoryInfo("AlienTopHat", "a_alien_top-hat", "top hat", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienTrafficCone = new AccessoryInfo("AlienTrafficCone", "a_alien_traffic-cone", "traffic cone", AccessoryType.Hat, CharacterType.Alien, 20);
	public static AccessoryInfo AlienVanDyke = new AccessoryInfo("AlienVanDyke", "a_alien_van-dyke", "van dyke", AccessoryType.Beard, CharacterType.Alien, 38);
	public static AccessoryInfo AlienVape = new AccessoryInfo("AlienVape", "a_alien_vape", "vape", AccessoryType.Mouth, CharacterType.Alien, 26);
	public static AccessoryInfo AlienVisor = new AccessoryInfo("AlienVisor", "a_alien_visor", "visor", AccessoryType.Eyewear, CharacterType.Alien, 14);
	public static AccessoryInfo ApeAfro = new AccessoryInfo("ApeAfro", "a_ape_afro", "afro", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApeBangs = new AccessoryInfo("ApeBangs", "a_ape_bangs", "bangs", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApeBaseballCap = new AccessoryInfo("ApeBaseballCap", "a_ape_baseball-cap", "baseball cap", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeBeanie = new AccessoryInfo("ApeBeanie", "a_ape_beanie", "beanie", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeBowlerHat = new AccessoryInfo("ApeBowlerHat", "a_ape_bowler-hat", "bowler hat", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeBrokenGlasses = new AccessoryInfo("ApeBrokenGlasses", "a_ape_broken-glasses", "broken glasses", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo ApeBun = new AccessoryInfo("ApeBun", "a_ape_bun", "bun", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApeBuzzcut = new AccessoryInfo("ApeBuzzcut", "a_ape_buzzcut", "buzzcut", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApeCigarette = new AccessoryInfo("ApeCigarette", "a_ape_cigarette", "cigarette", AccessoryType.Mouth, CharacterType.Ape, 28);
	public static AccessoryInfo ApeCircleBeard = new AccessoryInfo("ApeCircleBeard", "a_ape_circle_beard", "circle beard", AccessoryType.Beard, CharacterType.Ape, 40);
	public static AccessoryInfo ApeConstructionHelmet = new AccessoryInfo("ApeConstructionHelmet", "a_ape_construction-helmet", "construction helmet", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeCowboyHat = new AccessoryInfo("ApeCowboyHat", "a_ape_cowboy-hat", "cowboy hat", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeCrown = new AccessoryInfo("ApeCrown", "a_ape_crown", "crown", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeCurly = new AccessoryInfo("ApeCurly", "a_ape_curly", "curly", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApeCurtain = new AccessoryInfo("ApeCurtain", "a_ape_curtain", "curtain", AccessoryType.Beard, CharacterType.Ape, 40);
	public static AccessoryInfo ApeCyborg = new AccessoryInfo("ApeCyborg", "a_ape_cyborg", "cyborg", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo ApeDiamondStuds = new AccessoryInfo("ApeDiamondStuds", "a_ape_diamond_studs", "diamond studs", AccessoryType.Piercing, CharacterType.Ape, 46);
	public static AccessoryInfo ApeEyepatch = new AccessoryInfo("ApeEyepatch", "a_ape_eyepatch", "eyepatch", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo ApeEyepatchSkull = new AccessoryInfo("ApeEyepatchSkull", "a_ape_eyepatch_skull", "eyepatch skull", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo ApeFishHat = new AccessoryInfo("ApeFishHat", "a_ape_fish-hat", "fish hat", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeGoatee = new AccessoryInfo("ApeGoatee", "a_ape_goatee", "goatee", AccessoryType.Beard, CharacterType.Ape, 40);
	public static AccessoryInfo ApeGogglesVr = new AccessoryInfo("ApeGogglesVr", "a_ape_goggles_vr", "goggles vr", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo ApeHamburger = new AccessoryInfo("ApeHamburger", "a_ape_hamburger", "hamburger", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeHandlebar = new AccessoryInfo("ApeHandlebar", "a_ape_handlebar", "handlebar", AccessoryType.Beard, CharacterType.Ape, 40);
	public static AccessoryInfo ApeHelmetViking = new AccessoryInfo("ApeHelmetViking", "a_ape_helmet-viking", "helmet viking", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeHighFlatTop = new AccessoryInfo("ApeHighFlatTop", "a_ape_high_flat_top", "high flat top", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApeHoopEarring = new AccessoryInfo("ApeHoopEarring", "a_ape_hoop_earring", "hoop earring", AccessoryType.Piercing, CharacterType.Ape, 46);
	public static AccessoryInfo ApeHugeBeard = new AccessoryInfo("ApeHugeBeard", "a_ape_huge_beard", "huge beard", AccessoryType.Beard, CharacterType.Ape, 40);
	public static AccessoryInfo ApeHugeMustache = new AccessoryInfo("ApeHugeMustache", "a_ape_huge_mustache", "huge mustache", AccessoryType.Beard, CharacterType.Ape, 40);
	public static AccessoryInfo ApeKidPropellerHat = new AccessoryInfo("ApeKidPropellerHat", "a_ape_kid-propeller-hat", "kid propeller hat", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeMask = new AccessoryInfo("ApeMask", "a_ape_mask", "mask", AccessoryType.Mouth, CharacterType.Ape, 28);
	public static AccessoryInfo ApeMohavk = new AccessoryInfo("ApeMohavk", "a_ape_mohavk", "mohavk", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApeMonacle = new AccessoryInfo("ApeMonacle", "a_ape_monacle", "monacle", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo ApeMustache = new AccessoryInfo("ApeMustache", "a_ape_mustache", "mustache", AccessoryType.Beard, CharacterType.Ape, 40);
	public static AccessoryInfo ApeMutton = new AccessoryInfo("ApeMutton", "a_ape_mutton", "mutton", AccessoryType.Beard, CharacterType.Ape, 40);
	public static AccessoryInfo ApeNarrowGlasses = new AccessoryInfo("ApeNarrowGlasses", "a_ape_narrow-glasses", "narrow glasses", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo ApeNoseRing = new AccessoryInfo("ApeNoseRing", "a_ape_nose_ring", "nose ring", AccessoryType.Piercing, CharacterType.Ape, 46);
	public static AccessoryInfo ApePacifier = new AccessoryInfo("ApePacifier", "a_ape_pacifier", "pacifier", AccessoryType.Mouth, CharacterType.Ape, 28);
	public static AccessoryInfo ApePartyGlasses = new AccessoryInfo("ApePartyGlasses", "a_ape_party-glasses", "party glasses", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo ApePartyHat = new AccessoryInfo("ApePartyHat", "a_ape_party-hat", "party hat", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApePartyHorn = new AccessoryInfo("ApePartyHorn", "a_ape_party_horn", "party horn", AccessoryType.Mouth, CharacterType.Ape, 28);
	public static AccessoryInfo ApePigtails = new AccessoryInfo("ApePigtails", "a_ape_pigtails", "pigtails", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApePipe = new AccessoryInfo("ApePipe", "a_ape_pipe", "pipe", AccessoryType.Mouth, CharacterType.Ape, 28);
	public static AccessoryInfo ApePirateHat = new AccessoryInfo("ApePirateHat", "a_ape_pirate-hat", "pirate hat", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApePoliceHat = new AccessoryInfo("ApePoliceHat", "a_ape_police-hat", "police hat", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApePonytail = new AccessoryInfo("ApePonytail", "a_ape_ponytail", "ponytail", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApePulledBack = new AccessoryInfo("ApePulledBack", "a_ape_pulled_back", "pulled back", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApeRectangularGlasses = new AccessoryInfo("ApeRectangularGlasses", "a_ape_rectangular-glasses", "rectangular glasses", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo ApeRoundGlasses = new AccessoryInfo("ApeRoundGlasses", "a_ape_round-glasses", "round glasses", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo ApeSantaHat = new AccessoryInfo("ApeSantaHat", "a_ape_santa-hat", "santa hat", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeShaggy = new AccessoryInfo("ApeShaggy", "a_ape_shaggy", "shaggy", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApeSimple = new AccessoryInfo("ApeSimple", "a_ape_simple", "simple", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApeSombrero = new AccessoryInfo("ApeSombrero", "a_ape_sombrero", "sombrero", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeSpiky = new AccessoryInfo("ApeSpiky", "a_ape_spiky", "spiky", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApeSteampunkGoggles = new AccessoryInfo("ApeSteampunkGoggles", "a_ape_steampunk-goggles", "steampunk goggles", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo ApeStraight = new AccessoryInfo("ApeStraight", "a_ape_straight", "straight", AccessoryType.Hair, CharacterType.Ape, 34);
	public static AccessoryInfo ApeSunglasses = new AccessoryInfo("ApeSunglasses", "a_ape_sunglasses", "sunglasses", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo ApeTopHat = new AccessoryInfo("ApeTopHat", "a_ape_top-hat", "top hat", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeTrafficCone = new AccessoryInfo("ApeTrafficCone", "a_ape_traffic-cone", "traffic cone", AccessoryType.Hat, CharacterType.Ape, 22);
	public static AccessoryInfo ApeVanDyke = new AccessoryInfo("ApeVanDyke", "a_ape_van-dyke", "van dyke", AccessoryType.Beard, CharacterType.Ape, 40);
	public static AccessoryInfo ApeVape = new AccessoryInfo("ApeVape", "a_ape_vape", "vape", AccessoryType.Mouth, CharacterType.Ape, 28);
	public static AccessoryInfo ApeVisor = new AccessoryInfo("ApeVisor", "a_ape_visor", "visor", AccessoryType.Eyewear, CharacterType.Ape, 16);
	public static AccessoryInfo CatAfro = new AccessoryInfo("CatAfro", "a_cat_afro", "afro", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatBangs = new AccessoryInfo("CatBangs", "a_cat_bangs", "bangs", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatBaseballCap = new AccessoryInfo("CatBaseballCap", "a_cat_baseball-cap", "baseball cap", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatBeanie = new AccessoryInfo("CatBeanie", "a_cat_beanie", "beanie", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatBowlerHat = new AccessoryInfo("CatBowlerHat", "a_cat_bowler-hat", "bowler hat", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatBrokenGlasses = new AccessoryInfo("CatBrokenGlasses", "a_cat_broken-glasses", "broken glasses", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo CatBun = new AccessoryInfo("CatBun", "a_cat_bun", "bun", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatBuzzcut = new AccessoryInfo("CatBuzzcut", "a_cat_buzzcut", "buzzcut", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatCigarette = new AccessoryInfo("CatCigarette", "a_cat_cigarette", "cigarette", AccessoryType.Mouth, CharacterType.Cat, 30);
	public static AccessoryInfo CatCircleBeard = new AccessoryInfo("CatCircleBeard", "a_cat_circle_beard", "circle beard", AccessoryType.Beard, CharacterType.Cat, 42);
	public static AccessoryInfo CatConstructionHelmet = new AccessoryInfo("CatConstructionHelmet", "a_cat_construction-helmet", "construction helmet", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatCowboyHat = new AccessoryInfo("CatCowboyHat", "a_cat_cowboy-hat", "cowboy hat", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatCrown = new AccessoryInfo("CatCrown", "a_cat_crown", "crown", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatCurly = new AccessoryInfo("CatCurly", "a_cat_curly", "curly", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatCurtain = new AccessoryInfo("CatCurtain", "a_cat_curtain", "curtain", AccessoryType.Beard, CharacterType.Cat, 42);
	public static AccessoryInfo CatCyborg = new AccessoryInfo("CatCyborg", "a_cat_cyborg", "cyborg", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo CatDiamondStuds = new AccessoryInfo("CatDiamondStuds", "a_cat_diamond_studs", "diamond studs", AccessoryType.Piercing, CharacterType.Cat, 48);
	public static AccessoryInfo CatEyepatch = new AccessoryInfo("CatEyepatch", "a_cat_eyepatch", "eyepatch", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo CatEyepatchSkull = new AccessoryInfo("CatEyepatchSkull", "a_cat_eyepatch_skull", "eyepatch skull", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo CatFishHat = new AccessoryInfo("CatFishHat", "a_cat_fish-hat", "fish hat", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatGoatee = new AccessoryInfo("CatGoatee", "a_cat_goatee", "goatee", AccessoryType.Beard, CharacterType.Cat, 42);
	public static AccessoryInfo CatGogglesVr = new AccessoryInfo("CatGogglesVr", "a_cat_goggles_vr", "goggles vr", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo CatHamburger = new AccessoryInfo("CatHamburger", "a_cat_hamburger", "hamburger", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatHandlebar = new AccessoryInfo("CatHandlebar", "a_cat_handlebar", "handlebar", AccessoryType.Beard, CharacterType.Cat, 42);
	public static AccessoryInfo CatHelmetViking = new AccessoryInfo("CatHelmetViking", "a_cat_helmet-viking", "helmet viking", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatHighFlatTop = new AccessoryInfo("CatHighFlatTop", "a_cat_high_flat_top", "high flat top", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatHoopEarring = new AccessoryInfo("CatHoopEarring", "a_cat_hoop_earring", "hoop earring", AccessoryType.Piercing, CharacterType.Cat, 48);
	public static AccessoryInfo CatHugeBeard = new AccessoryInfo("CatHugeBeard", "a_cat_huge_beard", "huge beard", AccessoryType.Beard, CharacterType.Cat, 42);
	public static AccessoryInfo CatHugeMustache = new AccessoryInfo("CatHugeMustache", "a_cat_huge_mustache", "huge mustache", AccessoryType.Beard, CharacterType.Cat, 42);
	public static AccessoryInfo CatKidPropellerHat = new AccessoryInfo("CatKidPropellerHat", "a_cat_kid-propeller-hat", "kid propeller hat", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatMask = new AccessoryInfo("CatMask", "a_cat_mask", "mask", AccessoryType.Mouth, CharacterType.Cat, 30);
	public static AccessoryInfo CatMohavk = new AccessoryInfo("CatMohavk", "a_cat_mohavk", "mohavk", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatMonacle = new AccessoryInfo("CatMonacle", "a_cat_monacle", "monacle", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo CatMustache = new AccessoryInfo("CatMustache", "a_cat_mustache", "mustache", AccessoryType.Beard, CharacterType.Cat, 42);
	public static AccessoryInfo CatMutton = new AccessoryInfo("CatMutton", "a_cat_mutton", "mutton", AccessoryType.Beard, CharacterType.Cat, 42);
	public static AccessoryInfo CatNarrowGlasses = new AccessoryInfo("CatNarrowGlasses", "a_cat_narrow-glasses", "narrow glasses", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo CatNoseRing = new AccessoryInfo("CatNoseRing", "a_cat_nose_ring", "nose ring", AccessoryType.Piercing, CharacterType.Cat, 48);
	public static AccessoryInfo CatPacifier = new AccessoryInfo("CatPacifier", "a_cat_pacifier", "pacifier", AccessoryType.Mouth, CharacterType.Cat, 30);
	public static AccessoryInfo CatPartyGlasses = new AccessoryInfo("CatPartyGlasses", "a_cat_party-glasses", "party glasses", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo CatPartyHat = new AccessoryInfo("CatPartyHat", "a_cat_party-hat", "party hat", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatPartyHorn = new AccessoryInfo("CatPartyHorn", "a_cat_party_horn", "party horn", AccessoryType.Mouth, CharacterType.Cat, 30);
	public static AccessoryInfo CatPigtails = new AccessoryInfo("CatPigtails", "a_cat_pigtails", "pigtails", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatPipe = new AccessoryInfo("CatPipe", "a_cat_pipe", "pipe", AccessoryType.Mouth, CharacterType.Cat, 30);
	public static AccessoryInfo CatPirateHat = new AccessoryInfo("CatPirateHat", "a_cat_pirate-hat", "pirate hat", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatPoliceHat = new AccessoryInfo("CatPoliceHat", "a_cat_police-hat", "police hat", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatPonytail = new AccessoryInfo("CatPonytail", "a_cat_ponytail", "ponytail", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatPulledBack = new AccessoryInfo("CatPulledBack", "a_cat_pulled_back", "pulled back", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatRectangularGlasses = new AccessoryInfo("CatRectangularGlasses", "a_cat_rectangular-glasses", "rectangular glasses", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo CatRoundGlasses = new AccessoryInfo("CatRoundGlasses", "a_cat_round-glasses", "round glasses", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo CatSantaHat = new AccessoryInfo("CatSantaHat", "a_cat_santa-hat", "santa hat", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatShaggy = new AccessoryInfo("CatShaggy", "a_cat_shaggy", "shaggy", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatSimple = new AccessoryInfo("CatSimple", "a_cat_simple", "simple", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatSombrero = new AccessoryInfo("CatSombrero", "a_cat_sombrero", "sombrero", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatSpiky = new AccessoryInfo("CatSpiky", "a_cat_spiky", "spiky", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatSteampunkGoggles = new AccessoryInfo("CatSteampunkGoggles", "a_cat_steampunk-goggles", "steampunk goggles", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo CatStraight = new AccessoryInfo("CatStraight", "a_cat_straight", "straight", AccessoryType.Hair, CharacterType.Cat, 36);
	public static AccessoryInfo CatSunglasses = new AccessoryInfo("CatSunglasses", "a_cat_sunglasses", "sunglasses", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo CatTopHat = new AccessoryInfo("CatTopHat", "a_cat_top-hat", "top hat", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatTrafficCone = new AccessoryInfo("CatTrafficCone", "a_cat_traffic-cone", "traffic cone", AccessoryType.Hat, CharacterType.Cat, 24);
	public static AccessoryInfo CatVanDyke = new AccessoryInfo("CatVanDyke", "a_cat_van-dyke", "van dyke", AccessoryType.Beard, CharacterType.Cat, 42);
	public static AccessoryInfo CatVape = new AccessoryInfo("CatVape", "a_cat_vape", "vape", AccessoryType.Mouth, CharacterType.Cat, 30);
	public static AccessoryInfo CatVisor = new AccessoryInfo("CatVisor", "a_cat_visor", "visor", AccessoryType.Eyewear, CharacterType.Cat, 18);
	public static AccessoryInfo DogeAfro = new AccessoryInfo("DogeAfro", "a_doge_afro", "afro", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogeBangs = new AccessoryInfo("DogeBangs", "a_doge_bangs", "bangs", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogeBaseballCap = new AccessoryInfo("DogeBaseballCap", "a_doge_baseball-cap", "baseball cap", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeBeanie = new AccessoryInfo("DogeBeanie", "a_doge_beanie", "beanie", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeBowlerHat = new AccessoryInfo("DogeBowlerHat", "a_doge_bowler-hat", "bowler hat", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeBrokenGlasses = new AccessoryInfo("DogeBrokenGlasses", "a_doge_broken-glasses", "broken glasses", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo DogeBun = new AccessoryInfo("DogeBun", "a_doge_bun", "bun", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogeBuzzcut = new AccessoryInfo("DogeBuzzcut", "a_doge_buzzcut", "buzzcut", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogeCigarette = new AccessoryInfo("DogeCigarette", "a_doge_cigarette", "cigarette", AccessoryType.Mouth, CharacterType.Doge, 29);
	public static AccessoryInfo DogeCircleBeard = new AccessoryInfo("DogeCircleBeard", "a_doge_circle_beard", "circle beard", AccessoryType.Beard, CharacterType.Doge, 41);
	public static AccessoryInfo DogeConstructionHelmet = new AccessoryInfo("DogeConstructionHelmet", "a_doge_construction-helmet", "construction helmet", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeCowboyHat = new AccessoryInfo("DogeCowboyHat", "a_doge_cowboy-hat", "cowboy hat", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeCrown = new AccessoryInfo("DogeCrown", "a_doge_crown", "crown", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeCurly = new AccessoryInfo("DogeCurly", "a_doge_curly", "curly", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogeCurtain = new AccessoryInfo("DogeCurtain", "a_doge_curtain", "curtain", AccessoryType.Beard, CharacterType.Doge, 41);
	public static AccessoryInfo DogeCyborg = new AccessoryInfo("DogeCyborg", "a_doge_cyborg", "cyborg", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo DogeDiamondStuds = new AccessoryInfo("DogeDiamondStuds", "a_doge_diamond_studs", "diamond studs", AccessoryType.Piercing, CharacterType.Doge, 47);
	public static AccessoryInfo DogeEyepatch = new AccessoryInfo("DogeEyepatch", "a_doge_eyepatch", "eyepatch", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo DogeEyepatchSkull = new AccessoryInfo("DogeEyepatchSkull", "a_doge_eyepatch_skull", "eyepatch skull", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo DogeFishHat = new AccessoryInfo("DogeFishHat", "a_doge_fish-hat", "fish hat", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeGoatee = new AccessoryInfo("DogeGoatee", "a_doge_goatee", "goatee", AccessoryType.Beard, CharacterType.Doge, 41);
	public static AccessoryInfo DogeGogglesVr = new AccessoryInfo("DogeGogglesVr", "a_doge_goggles_vr", "goggles vr", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo DogeHamburger = new AccessoryInfo("DogeHamburger", "a_doge_hamburger", "hamburger", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeHandlebar = new AccessoryInfo("DogeHandlebar", "a_doge_handlebar", "handlebar", AccessoryType.Beard, CharacterType.Doge, 41);
	public static AccessoryInfo DogeHelmetViking = new AccessoryInfo("DogeHelmetViking", "a_doge_helmet-viking", "helmet viking", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeHighFlatTop = new AccessoryInfo("DogeHighFlatTop", "a_doge_high_flat_top", "high flat top", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogeHoopEarring = new AccessoryInfo("DogeHoopEarring", "a_doge_hoop_earring", "hoop earring", AccessoryType.Piercing, CharacterType.Doge, 47);
	public static AccessoryInfo DogeHugeBeard = new AccessoryInfo("DogeHugeBeard", "a_doge_huge_beard", "huge beard", AccessoryType.Beard, CharacterType.Doge, 41);
	public static AccessoryInfo DogeHugeMustache = new AccessoryInfo("DogeHugeMustache", "a_doge_huge_mustache", "huge mustache", AccessoryType.Beard, CharacterType.Doge, 41);
	public static AccessoryInfo DogeKidPropellerHat = new AccessoryInfo("DogeKidPropellerHat", "a_doge_kid-propeller-hat", "kid propeller hat", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeMask = new AccessoryInfo("DogeMask", "a_doge_mask", "mask", AccessoryType.Mouth, CharacterType.Doge, 29);
	public static AccessoryInfo DogeMohavk = new AccessoryInfo("DogeMohavk", "a_doge_mohavk", "mohavk", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogeMonacle = new AccessoryInfo("DogeMonacle", "a_doge_monacle", "monacle", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo DogeMustache = new AccessoryInfo("DogeMustache", "a_doge_mustache", "mustache", AccessoryType.Beard, CharacterType.Doge, 41);
	public static AccessoryInfo DogeMutton = new AccessoryInfo("DogeMutton", "a_doge_mutton", "mutton", AccessoryType.Beard, CharacterType.Doge, 41);
	public static AccessoryInfo DogeNarrowGlasses = new AccessoryInfo("DogeNarrowGlasses", "a_doge_narrow-glasses", "narrow glasses", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo DogeNoseRing = new AccessoryInfo("DogeNoseRing", "a_doge_nose_ring", "nose ring", AccessoryType.Piercing, CharacterType.Doge, 47);
	public static AccessoryInfo DogePacifier = new AccessoryInfo("DogePacifier", "a_doge_pacifier", "pacifier", AccessoryType.Mouth, CharacterType.Doge, 29);
	public static AccessoryInfo DogePartyGlasses = new AccessoryInfo("DogePartyGlasses", "a_doge_party-glasses", "party glasses", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo DogePartyHat = new AccessoryInfo("DogePartyHat", "a_doge_party-hat", "party hat", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogePartyHorn = new AccessoryInfo("DogePartyHorn", "a_doge_party_horn", "party horn", AccessoryType.Mouth, CharacterType.Doge, 29);
	public static AccessoryInfo DogePigtails = new AccessoryInfo("DogePigtails", "a_doge_pigtails", "pigtails", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogePipe = new AccessoryInfo("DogePipe", "a_doge_pipe", "pipe", AccessoryType.Mouth, CharacterType.Doge, 29);
	public static AccessoryInfo DogePirateHat = new AccessoryInfo("DogePirateHat", "a_doge_pirate-hat", "pirate hat", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogePoliceHat = new AccessoryInfo("DogePoliceHat", "a_doge_police-hat", "police hat", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogePonytail = new AccessoryInfo("DogePonytail", "a_doge_ponytail", "ponytail", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogePulledBack = new AccessoryInfo("DogePulledBack", "a_doge_pulled_back", "pulled back", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogeRectangularGlasses = new AccessoryInfo("DogeRectangularGlasses", "a_doge_rectangular-glasses", "rectangular glasses", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo DogeRoundGlasses = new AccessoryInfo("DogeRoundGlasses", "a_doge_round-glasses", "round glasses", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo DogeSantaHat = new AccessoryInfo("DogeSantaHat", "a_doge_santa-hat", "santa hat", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeShaggy = new AccessoryInfo("DogeShaggy", "a_doge_shaggy", "shaggy", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogeSimple = new AccessoryInfo("DogeSimple", "a_doge_simple", "simple", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogeSombrero = new AccessoryInfo("DogeSombrero", "a_doge_sombrero", "sombrero", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeSpiky = new AccessoryInfo("DogeSpiky", "a_doge_spiky", "spiky", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogeSteampunkGoggles = new AccessoryInfo("DogeSteampunkGoggles", "a_doge_steampunk-goggles", "steampunk goggles", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo DogeStraight = new AccessoryInfo("DogeStraight", "a_doge_straight", "straight", AccessoryType.Hair, CharacterType.Doge, 35);
	public static AccessoryInfo DogeSunglasses = new AccessoryInfo("DogeSunglasses", "a_doge_sunglasses", "sunglasses", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo DogeTopHat = new AccessoryInfo("DogeTopHat", "a_doge_top-hat", "top hat", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeTrafficCone = new AccessoryInfo("DogeTrafficCone", "a_doge_traffic-cone", "traffic cone", AccessoryType.Hat, CharacterType.Doge, 23);
	public static AccessoryInfo DogeVanDyke = new AccessoryInfo("DogeVanDyke", "a_doge_van-dyke", "van dyke", AccessoryType.Beard, CharacterType.Doge, 41);
	public static AccessoryInfo DogeVape = new AccessoryInfo("DogeVape", "a_doge_vape", "vape", AccessoryType.Mouth, CharacterType.Doge, 29);
	public static AccessoryInfo DogeVisor = new AccessoryInfo("DogeVisor", "a_doge_visor", "visor", AccessoryType.Eyewear, CharacterType.Doge, 17);
	public static AccessoryInfo FrogAfro = new AccessoryInfo("FrogAfro", "a_frog_afro", "afro", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogBangs = new AccessoryInfo("FrogBangs", "a_frog_bangs", "bangs", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogBaseballCap = new AccessoryInfo("FrogBaseballCap", "a_frog_baseball-cap", "baseball cap", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogBeanie = new AccessoryInfo("FrogBeanie", "a_frog_beanie", "beanie", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogBowlerHat = new AccessoryInfo("FrogBowlerHat", "a_frog_bowler-hat", "bowler hat", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogBrokenGlasses = new AccessoryInfo("FrogBrokenGlasses", "a_frog_broken-glasses", "broken glasses", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo FrogBun = new AccessoryInfo("FrogBun", "a_frog_bun", "bun", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogBuzzcut = new AccessoryInfo("FrogBuzzcut", "a_frog_buzzcut", "buzzcut", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogCigarette = new AccessoryInfo("FrogCigarette", "a_frog_cigarette", "cigarette", AccessoryType.Mouth, CharacterType.Frog, 25);
	public static AccessoryInfo FrogCircleBeard = new AccessoryInfo("FrogCircleBeard", "a_frog_circle_beard", "circle beard", AccessoryType.Beard, CharacterType.Frog, 37);
	public static AccessoryInfo FrogConstructionHelmet = new AccessoryInfo("FrogConstructionHelmet", "a_frog_construction-helmet", "construction helmet", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogCowboyHat = new AccessoryInfo("FrogCowboyHat", "a_frog_cowboy-hat", "cowboy hat", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogCrown = new AccessoryInfo("FrogCrown", "a_frog_crown", "crown", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogCurly = new AccessoryInfo("FrogCurly", "a_frog_curly", "curly", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogCurtain = new AccessoryInfo("FrogCurtain", "a_frog_curtain", "curtain", AccessoryType.Beard, CharacterType.Frog, 37);
	public static AccessoryInfo FrogCyborg = new AccessoryInfo("FrogCyborg", "a_frog_cyborg", "cyborg", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo FrogDiamondStuds = new AccessoryInfo("FrogDiamondStuds", "a_frog_diamond_studs", "diamond studs", AccessoryType.Piercing, CharacterType.Frog, 43);
	public static AccessoryInfo FrogEyepatch = new AccessoryInfo("FrogEyepatch", "a_frog_eyepatch", "eyepatch", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo FrogEyepatchSkull = new AccessoryInfo("FrogEyepatchSkull", "a_frog_eyepatch_skull", "eyepatch skull", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo FrogFishHat = new AccessoryInfo("FrogFishHat", "a_frog_fish-hat", "fish hat", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogGoatee = new AccessoryInfo("FrogGoatee", "a_frog_goatee", "goatee", AccessoryType.Beard, CharacterType.Frog, 37);
	public static AccessoryInfo FrogGogglesVr = new AccessoryInfo("FrogGogglesVr", "a_frog_goggles_vr", "goggles vr", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo FrogHamburger = new AccessoryInfo("FrogHamburger", "a_frog_hamburger", "hamburger", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogHandlebar = new AccessoryInfo("FrogHandlebar", "a_frog_handlebar", "handlebar", AccessoryType.Beard, CharacterType.Frog, 37);
	public static AccessoryInfo FrogHelmetViking = new AccessoryInfo("FrogHelmetViking", "a_frog_helmet-viking", "helmet viking", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogHighFlatTop = new AccessoryInfo("FrogHighFlatTop", "a_frog_high_flat_top", "high flat top", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogHoopEarring = new AccessoryInfo("FrogHoopEarring", "a_frog_hoop_earring", "hoop earring", AccessoryType.Piercing, CharacterType.Frog, 43);
	public static AccessoryInfo FrogHugeBeard = new AccessoryInfo("FrogHugeBeard", "a_frog_huge_beard", "huge beard", AccessoryType.Beard, CharacterType.Frog, 37);
	public static AccessoryInfo FrogHugeMustache = new AccessoryInfo("FrogHugeMustache", "a_frog_huge_mustache", "huge mustache", AccessoryType.Beard, CharacterType.Frog, 37);
	public static AccessoryInfo FrogKidPropellerHat = new AccessoryInfo("FrogKidPropellerHat", "a_frog_kid-propeller-hat", "kid propeller hat", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogMask = new AccessoryInfo("FrogMask", "a_frog_mask", "mask", AccessoryType.Mouth, CharacterType.Frog, 25);
	public static AccessoryInfo FrogMohavk = new AccessoryInfo("FrogMohavk", "a_frog_mohavk", "mohavk", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogMonacle = new AccessoryInfo("FrogMonacle", "a_frog_monacle", "monacle", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo FrogMustache = new AccessoryInfo("FrogMustache", "a_frog_mustache", "mustache", AccessoryType.Beard, CharacterType.Frog, 37);
	public static AccessoryInfo FrogMutton = new AccessoryInfo("FrogMutton", "a_frog_mutton", "mutton", AccessoryType.Beard, CharacterType.Frog, 37);
	public static AccessoryInfo FrogNarrowGlasses = new AccessoryInfo("FrogNarrowGlasses", "a_frog_narrow-glasses", "narrow glasses", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo FrogNoseRing = new AccessoryInfo("FrogNoseRing", "a_frog_nose_ring", "nose ring", AccessoryType.Piercing, CharacterType.Frog, 43);
	public static AccessoryInfo FrogPacifier = new AccessoryInfo("FrogPacifier", "a_frog_pacifier", "pacifier", AccessoryType.Mouth, CharacterType.Frog, 25);
	public static AccessoryInfo FrogPartyGlasses = new AccessoryInfo("FrogPartyGlasses", "a_frog_party-glasses", "party glasses", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo FrogPartyHat = new AccessoryInfo("FrogPartyHat", "a_frog_party-hat", "party hat", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogPartyHorn = new AccessoryInfo("FrogPartyHorn", "a_frog_party_horn", "party horn", AccessoryType.Mouth, CharacterType.Frog, 25);
	public static AccessoryInfo FrogPigtails = new AccessoryInfo("FrogPigtails", "a_frog_pigtails", "pigtails", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogPipe = new AccessoryInfo("FrogPipe", "a_frog_pipe", "pipe", AccessoryType.Mouth, CharacterType.Frog, 25);
	public static AccessoryInfo FrogPirateHat = new AccessoryInfo("FrogPirateHat", "a_frog_pirate-hat", "pirate hat", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogPoliceHat = new AccessoryInfo("FrogPoliceHat", "a_frog_police-hat", "police hat", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogPonytail = new AccessoryInfo("FrogPonytail", "a_frog_ponytail", "ponytail", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogPulledBack = new AccessoryInfo("FrogPulledBack", "a_frog_pulled_back", "pulled back", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogRectangularGlasses = new AccessoryInfo("FrogRectangularGlasses", "a_frog_rectangular-glasses", "rectangular glasses", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo FrogRoundGlasses = new AccessoryInfo("FrogRoundGlasses", "a_frog_round-glasses", "round glasses", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo FrogSantaHat = new AccessoryInfo("FrogSantaHat", "a_frog_santa-hat", "santa hat", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogShaggy = new AccessoryInfo("FrogShaggy", "a_frog_shaggy", "shaggy", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogSimple = new AccessoryInfo("FrogSimple", "a_frog_simple", "simple", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogSombrero = new AccessoryInfo("FrogSombrero", "a_frog_sombrero", "sombrero", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogSpiky = new AccessoryInfo("FrogSpiky", "a_frog_spiky", "spiky", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogSteampunkGoggles = new AccessoryInfo("FrogSteampunkGoggles", "a_frog_steampunk-goggles", "steampunk goggles", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo FrogStraight = new AccessoryInfo("FrogStraight", "a_frog_straight", "straight", AccessoryType.Hair, CharacterType.Frog, 31);
	public static AccessoryInfo FrogSunglasses = new AccessoryInfo("FrogSunglasses", "a_frog_sunglasses", "sunglasses", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo FrogTopHat = new AccessoryInfo("FrogTopHat", "a_frog_top-hat", "top hat", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogTrafficCone = new AccessoryInfo("FrogTrafficCone", "a_frog_traffic-cone", "traffic cone", AccessoryType.Hat, CharacterType.Frog, 19);
	public static AccessoryInfo FrogVanDyke = new AccessoryInfo("FrogVanDyke", "a_frog_van-dyke", "van dyke", AccessoryType.Beard, CharacterType.Frog, 37);
	public static AccessoryInfo FrogVape = new AccessoryInfo("FrogVape", "a_frog_vape", "vape", AccessoryType.Mouth, CharacterType.Frog, 25);
	public static AccessoryInfo FrogVisor = new AccessoryInfo("FrogVisor", "a_frog_visor", "visor", AccessoryType.Eyewear, CharacterType.Frog, 13);
	public static AccessoryInfo HumanAfro = new AccessoryInfo("HumanAfro", "a_human_afro", "afro", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanBangs = new AccessoryInfo("HumanBangs", "a_human_bangs", "bangs", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanBaseballCap = new AccessoryInfo("HumanBaseballCap", "a_human_baseball-cap", "baseball cap", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanBeanie = new AccessoryInfo("HumanBeanie", "a_human_beanie", "beanie", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanBowlerHat = new AccessoryInfo("HumanBowlerHat", "a_human_bowler-hat", "bowler hat", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanBrokenGlasses = new AccessoryInfo("HumanBrokenGlasses", "a_human_broken-glasses", "broken glasses", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo HumanBun = new AccessoryInfo("HumanBun", "a_human_bun", "bun", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanBuzzcut = new AccessoryInfo("HumanBuzzcut", "a_human_buzzcut", "buzzcut", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanCigarette = new AccessoryInfo("HumanCigarette", "a_human_cigarette", "cigarette", AccessoryType.Mouth, CharacterType.Human, 27);
	public static AccessoryInfo HumanCircleBeard = new AccessoryInfo("HumanCircleBeard", "a_human_circle_beard", "circle beard", AccessoryType.Beard, CharacterType.Human, 39);
	public static AccessoryInfo HumanConstructionHelmet = new AccessoryInfo("HumanConstructionHelmet", "a_human_construction-helmet", "construction helmet", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanCowboyHat = new AccessoryInfo("HumanCowboyHat", "a_human_cowboy-hat", "cowboy hat", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanCrown = new AccessoryInfo("HumanCrown", "a_human_crown", "crown", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanCurly = new AccessoryInfo("HumanCurly", "a_human_curly", "curly", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanCurtain = new AccessoryInfo("HumanCurtain", "a_human_curtain", "curtain", AccessoryType.Beard, CharacterType.Human, 39);
	public static AccessoryInfo HumanCyborg = new AccessoryInfo("HumanCyborg", "a_human_cyborg", "cyborg", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo HumanDiamondStuds = new AccessoryInfo("HumanDiamondStuds", "a_human_diamond_studs", "diamond studs", AccessoryType.Piercing, CharacterType.Human, 45);
	public static AccessoryInfo HumanEyepatch = new AccessoryInfo("HumanEyepatch", "a_human_eyepatch", "eyepatch", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo HumanEyepatchSkull = new AccessoryInfo("HumanEyepatchSkull", "a_human_eyepatch_skull", "eyepatch skull", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo HumanFishHat = new AccessoryInfo("HumanFishHat", "a_human_fish-hat", "fish hat", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanGoatee = new AccessoryInfo("HumanGoatee", "a_human_goatee", "goatee", AccessoryType.Beard, CharacterType.Human, 39);
	public static AccessoryInfo HumanGogglesVr = new AccessoryInfo("HumanGogglesVr", "a_human_goggles_vr", "goggles vr", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo HumanHamburger = new AccessoryInfo("HumanHamburger", "a_human_hamburger", "hamburger", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanHandlebar = new AccessoryInfo("HumanHandlebar", "a_human_handlebar", "handlebar", AccessoryType.Beard, CharacterType.Human, 39);
	public static AccessoryInfo HumanHelmetViking = new AccessoryInfo("HumanHelmetViking", "a_human_helmet-viking", "helmet viking", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanHighFlatTop = new AccessoryInfo("HumanHighFlatTop", "a_human_high_flat_top", "high flat top", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanHoopEarring = new AccessoryInfo("HumanHoopEarring", "a_human_hoop_earring", "hoop earring", AccessoryType.Piercing, CharacterType.Human, 45);
	public static AccessoryInfo HumanHugeBeard = new AccessoryInfo("HumanHugeBeard", "a_human_huge_beard", "huge beard", AccessoryType.Beard, CharacterType.Human, 39);
	public static AccessoryInfo HumanHugeMustache = new AccessoryInfo("HumanHugeMustache", "a_human_huge_mustache", "huge mustache", AccessoryType.Beard, CharacterType.Human, 39);
	public static AccessoryInfo HumanKidPropellerHat = new AccessoryInfo("HumanKidPropellerHat", "a_human_kid-propeller-hat", "kid propeller hat", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanMask = new AccessoryInfo("HumanMask", "a_human_mask", "mask", AccessoryType.Mouth, CharacterType.Human, 27);
	public static AccessoryInfo HumanMohavk = new AccessoryInfo("HumanMohavk", "a_human_mohavk", "mohavk", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanMonacle = new AccessoryInfo("HumanMonacle", "a_human_monacle", "monacle", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo HumanMustache = new AccessoryInfo("HumanMustache", "a_human_mustache", "mustache", AccessoryType.Beard, CharacterType.Human, 39);
	public static AccessoryInfo HumanMutton = new AccessoryInfo("HumanMutton", "a_human_mutton", "mutton", AccessoryType.Beard, CharacterType.Human, 39);
	public static AccessoryInfo HumanNarrowGlasses = new AccessoryInfo("HumanNarrowGlasses", "a_human_narrow-glasses", "narrow glasses", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo HumanNoseRing = new AccessoryInfo("HumanNoseRing", "a_human_nose_ring", "nose ring", AccessoryType.Piercing, CharacterType.Human, 45);
	public static AccessoryInfo HumanPacifier = new AccessoryInfo("HumanPacifier", "a_human_pacifier", "pacifier", AccessoryType.Mouth, CharacterType.Human, 27);
	public static AccessoryInfo HumanPartyGlasses = new AccessoryInfo("HumanPartyGlasses", "a_human_party-glasses", "party glasses", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo HumanPartyHat = new AccessoryInfo("HumanPartyHat", "a_human_party-hat", "party hat", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanPartyHorn = new AccessoryInfo("HumanPartyHorn", "a_human_party_horn", "party horn", AccessoryType.Mouth, CharacterType.Human, 27);
	public static AccessoryInfo HumanPigtails = new AccessoryInfo("HumanPigtails", "a_human_pigtails", "pigtails", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanPipe = new AccessoryInfo("HumanPipe", "a_human_pipe", "pipe", AccessoryType.Mouth, CharacterType.Human, 27);
	public static AccessoryInfo HumanPirateHat = new AccessoryInfo("HumanPirateHat", "a_human_pirate-hat", "pirate hat", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanPoliceHat = new AccessoryInfo("HumanPoliceHat", "a_human_police-hat", "police hat", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanPonytail = new AccessoryInfo("HumanPonytail", "a_human_ponytail", "ponytail", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanPulledBack = new AccessoryInfo("HumanPulledBack", "a_human_pulled_back", "pulled back", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanRectangularGlasses = new AccessoryInfo("HumanRectangularGlasses", "a_human_rectangular-glasses", "rectangular glasses", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo HumanRoundGlasses = new AccessoryInfo("HumanRoundGlasses", "a_human_round-glasses", "round glasses", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo HumanSantaHat = new AccessoryInfo("HumanSantaHat", "a_human_santa-hat", "santa hat", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanShaggy = new AccessoryInfo("HumanShaggy", "a_human_shaggy", "shaggy", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanSimple = new AccessoryInfo("HumanSimple", "a_human_simple", "simple", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanSombrero = new AccessoryInfo("HumanSombrero", "a_human_sombrero", "sombrero", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanSpiky = new AccessoryInfo("HumanSpiky", "a_human_spiky", "spiky", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanSteampunkGoggles = new AccessoryInfo("HumanSteampunkGoggles", "a_human_steampunk-goggles", "steampunk goggles", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo HumanStraight = new AccessoryInfo("HumanStraight", "a_human_straight", "straight", AccessoryType.Hair, CharacterType.Human, 33);
	public static AccessoryInfo HumanSunglasses = new AccessoryInfo("HumanSunglasses", "a_human_sunglasses", "sunglasses", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo HumanTopHat = new AccessoryInfo("HumanTopHat", "a_human_top-hat", "top hat", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanTrafficCone = new AccessoryInfo("HumanTrafficCone", "a_human_traffic-cone", "traffic cone", AccessoryType.Hat, CharacterType.Human, 21);
	public static AccessoryInfo HumanVanDyke = new AccessoryInfo("HumanVanDyke", "a_human_van-dyke", "van dyke", AccessoryType.Beard, CharacterType.Human, 39);
	public static AccessoryInfo HumanVape = new AccessoryInfo("HumanVape", "a_human_vape", "vape", AccessoryType.Mouth, CharacterType.Human, 27);
	public static AccessoryInfo HumanVisor = new AccessoryInfo("HumanVisor", "a_human_visor", "visor", AccessoryType.Eyewear, CharacterType.Human, 15);
	public static AccessoryInfo ShareBanana = new AccessoryInfo("ShareBanana", "a_share_banana", "banana", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareBaseballBat = new AccessoryInfo("ShareBaseballBat", "a_share_baseball-bat", "baseball bat", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareBasic = new AccessoryInfo("ShareBasic", "a_share_basic", "basic", AccessoryType.Belt, CharacterType.Share, 60);
	public static AccessoryInfo ShareBasicShoes = new AccessoryInfo("ShareBasicShoes", "a_share_basic_shoes", "basic shoes", AccessoryType.Footwear, CharacterType.Share, 53);
	public static AccessoryInfo ShareBeer = new AccessoryInfo("ShareBeer", "a_share_beer", "beer", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareBomb = new AccessoryInfo("ShareBomb", "a_share_bomb", "bomb", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareBowtie = new AccessoryInfo("ShareBowtie", "a_share_bowtie", "bowtie", AccessoryType.Neckwear, CharacterType.Share, 56);
	public static AccessoryInfo ShareBoxers = new AccessoryInfo("ShareBoxers", "a_share_boxers", "boxers", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo ShareBoxersDotty = new AccessoryInfo("ShareBoxersDotty", "a_share_boxers_dotty", "boxers dotty", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo ShareBoxersStriped = new AccessoryInfo("ShareBoxersStriped", "a_share_boxers_striped", "boxers striped", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo ShareBoxingGloves = new AccessoryInfo("ShareBoxingGloves", "a_share_boxing-gloves", "boxing gloves", AccessoryType.Hands, CharacterType.Share, 54);
	public static AccessoryInfo ShareBra = new AccessoryInfo("ShareBra", "a_share_bra", "bra", AccessoryType.Top, CharacterType.Share, 58);
	public static AccessoryInfo ShareBriefs = new AccessoryInfo("ShareBriefs", "a_share_briefs", "briefs", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo ShareBurger = new AccessoryInfo("ShareBurger", "a_share_burger", "burger", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareCactus = new AccessoryInfo("ShareCactus", "a_share_cactus", "cactus", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareCarrot = new AccessoryInfo("ShareCarrot", "a_share_carrot", "carrot", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareCartoonGloves = new AccessoryInfo("ShareCartoonGloves", "a_share_cartoon-gloves", "cartoon gloves", AccessoryType.Hands, CharacterType.Share, 54);
	public static AccessoryInfo ShareCrabClaws = new AccessoryInfo("ShareCrabClaws", "a_share_crab-claws", "crab claws", AccessoryType.Hands, CharacterType.Share, 54);
	public static AccessoryInfo ShareCropTop = new AccessoryInfo("ShareCropTop", "a_share_crop-top", "crop top", AccessoryType.Top, CharacterType.Share, 58);
	public static AccessoryInfo ShareDinnerJacketOpen = new AccessoryInfo("ShareDinnerJacketOpen", "a_share_dinner-jacket_open", "dinner jacket open", AccessoryType.Outerwear, CharacterType.Share, 55);
	public static AccessoryInfo ShareDino = new AccessoryInfo("ShareDino", "a_share_dino", "dino", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareDollarSign = new AccessoryInfo("ShareDollarSign", "a_share_dollar-sign", "dollar sign", AccessoryType.Neckwear, CharacterType.Share, 56);
	public static AccessoryInfo ShareDuck = new AccessoryInfo("ShareDuck", "a_share_duck", "duck", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareDuckFaceShoes = new AccessoryInfo("ShareDuckFaceShoes", "a_share_duck-face_shoes", "duck face shoes", AccessoryType.Footwear, CharacterType.Share, 53);
	public static AccessoryInfo ShareEyeball = new AccessoryInfo("ShareEyeball", "a_share_eyeball", "eyeball", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareGloves = new AccessoryInfo("ShareGloves", "a_share_gloves", "gloves", AccessoryType.Hands, CharacterType.Share, 54);
	public static AccessoryInfo ShareGString = new AccessoryInfo("ShareGString", "a_share_g-string", "g string", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo ShareHeart = new AccessoryInfo("ShareHeart", "a_share_heart", "heart", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareHoodie = new AccessoryInfo("ShareHoodie", "a_share_hoodie", "hoodie", AccessoryType.Outerwear, CharacterType.Share, 55);
	public static AccessoryInfo ShareIceCream = new AccessoryInfo("ShareIceCream", "a_share_ice-cream", "ice cream", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareJacketClosed = new AccessoryInfo("ShareJacketClosed", "a_share_jacket_closed", "jacket closed", AccessoryType.Outerwear, CharacterType.Share, 55);
	public static AccessoryInfo ShareJacketElbowpatchesOpen = new AccessoryInfo("ShareJacketElbowpatchesOpen", "a_share_jacket_elbowpatches_open", "jacket elbowpatches open", AccessoryType.Outerwear, CharacterType.Share, 55);
	public static AccessoryInfo ShareJacketOpen = new AccessoryInfo("ShareJacketOpen", "a_share_jacket_open", "jacket open", AccessoryType.Outerwear, CharacterType.Share, 55);
	public static AccessoryInfo ShareJeans = new AccessoryInfo("ShareJeans", "a_share_jeans", "jeans", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo ShareJeansKneepads = new AccessoryInfo("ShareJeansKneepads", "a_share_jeans_kneepads", "jeans kneepads", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo ShareJesterShoes = new AccessoryInfo("ShareJesterShoes", "a_share_jester_shoes", "jester shoes", AccessoryType.Footwear, CharacterType.Share, 53);
	public static AccessoryInfo ShareJorts = new AccessoryInfo("ShareJorts", "a_share_jorts", "jorts", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo ShareJortsFrayed = new AccessoryInfo("ShareJortsFrayed", "a_share_jorts_frayed", "jorts frayed", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo ShareLeftItemArrow = new AccessoryInfo("ShareLeftItemArrow", "a_share_left-item_arrow", "left item arrow", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemAxe = new AccessoryInfo("ShareLeftItemAxe", "a_share_left-item_axe", "left item axe", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemBalloon = new AccessoryInfo("ShareLeftItemBalloon", "a_share_left-item_balloon", "left item balloon", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemBanana = new AccessoryInfo("ShareLeftItemBanana", "a_share_left-item_banana", "left item banana", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemBeer = new AccessoryInfo("ShareLeftItemBeer", "a_share_left-item_beer", "left item beer", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemBigSword = new AccessoryInfo("ShareLeftItemBigSword", "a_share_left-item_big-sword", "left item big sword", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemBoombox = new AccessoryInfo("ShareLeftItemBoombox", "a_share_left-item_boombox", "left item boombox", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemBoosterSword = new AccessoryInfo("ShareLeftItemBoosterSword", "a_share_left-item_booster-sword", "left item booster sword", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemBottle = new AccessoryInfo("ShareLeftItemBottle", "a_share_left-item_bottle", "left item bottle", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemBow = new AccessoryInfo("ShareLeftItemBow", "a_share_left-item_bow", "left item bow", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemBowlingBall = new AccessoryInfo("ShareLeftItemBowlingBall", "a_share_left-item_bowling-ball", "left item bowling ball", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemBriefcase = new AccessoryInfo("ShareLeftItemBriefcase", "a_share_left-item_briefcase", "left item briefcase", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemChainsaw = new AccessoryInfo("ShareLeftItemChainsaw", "a_share_left-item_chainsaw", "left item chainsaw", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemController = new AccessoryInfo("ShareLeftItemController", "a_share_left-item_controller", "left item controller", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemCrowbar = new AccessoryInfo("ShareLeftItemCrowbar", "a_share_left-item_crowbar", "left item crowbar", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemDynamite = new AccessoryInfo("ShareLeftItemDynamite", "a_share_left-item_dynamite", "left item dynamite", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemElectricGuitar = new AccessoryInfo("ShareLeftItemElectricGuitar", "a_share_left-item_electric-guitar", "left item electric guitar", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemEnergySword = new AccessoryInfo("ShareLeftItemEnergySword", "a_share_left-item_energy-sword", "left item energy sword", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemEyeWand = new AccessoryInfo("ShareLeftItemEyeWand", "a_share_left-item_eye-wand", "left item eye wand", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemFish = new AccessoryInfo("ShareLeftItemFish", "a_share_left-item_fish", "left item fish", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemFlail = new AccessoryInfo("ShareLeftItemFlail", "a_share_left-item_flail", "left item flail", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemFootscooter = new AccessoryInfo("ShareLeftItemFootscooter", "a_share_left-item_footscooter", "left item footscooter", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemFryingPanEggsBacon = new AccessoryInfo("ShareLeftItemFryingPanEggsBacon", "a_share_left-item_frying-pan-eggs-bacon", "left item frying pan eggs bacon", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemGoldring = new AccessoryInfo("ShareLeftItemGoldring", "a_share_left-item_goldring", "left item goldring", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemGun = new AccessoryInfo("ShareLeftItemGun", "a_share_left-item_gun", "left item gun", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemKey = new AccessoryInfo("ShareLeftItemKey", "a_share_left-item_key", "left item key", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemKeyboard = new AccessoryInfo("ShareLeftItemKeyboard", "a_share_left-item_keyboard", "left item keyboard", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemLaptop = new AccessoryInfo("ShareLeftItemLaptop", "a_share_left-item_laptop", "left item laptop", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemLeatherbag = new AccessoryInfo("ShareLeftItemLeatherbag", "a_share_left-item_leatherbag", "left item leatherbag", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemLifesaverRing = new AccessoryInfo("ShareLeftItemLifesaverRing", "a_share_left-item_lifesaver-ring", "left item lifesaver ring", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemLongboard = new AccessoryInfo("ShareLeftItemLongboard", "a_share_left-item_longboard", "left item longboard", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemMagnet = new AccessoryInfo("ShareLeftItemMagnet", "a_share_left-item_magnet", "left item magnet", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemMoneyBag = new AccessoryInfo("ShareLeftItemMoneyBag", "a_share_left-item_money-bag", "left item money bag", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemNunchucks = new AccessoryInfo("ShareLeftItemNunchucks", "a_share_left-item_nunchucks", "left item nunchucks", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemPoop = new AccessoryInfo("ShareLeftItemPoop", "a_share_left-item_poop", "left item poop", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemPotion = new AccessoryInfo("ShareLeftItemPotion", "a_share_left-item_potion", "left item potion", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemRope = new AccessoryInfo("ShareLeftItemRope", "a_share_left-item_rope", "left item rope", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemRubiksCube = new AccessoryInfo("ShareLeftItemRubiksCube", "a_share_left-item_rubiks-cube", "left item rubiks cube", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemSai = new AccessoryInfo("ShareLeftItemSai", "a_share_left-item_sai", "left item sai", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemScooter = new AccessoryInfo("ShareLeftItemScooter", "a_share_left-item_scooter", "left item scooter", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemScythe = new AccessoryInfo("ShareLeftItemScythe", "a_share_left-item_scythe", "left item scythe", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemSherbetIcecream = new AccessoryInfo("ShareLeftItemSherbetIcecream", "a_share_left-item_sherbet-icecream", "left item sherbet icecream", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemSkateboard = new AccessoryInfo("ShareLeftItemSkateboard", "a_share_left-item_skateboard", "left item skateboard", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemSkull = new AccessoryInfo("ShareLeftItemSkull", "a_share_left-item_skull", "left item skull", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemSoccerball = new AccessoryInfo("ShareLeftItemSoccerball", "a_share_left-item_soccerball", "left item soccerball", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemSpade = new AccessoryInfo("ShareLeftItemSpade", "a_share_left-item_spade", "left item spade", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemSpring = new AccessoryInfo("ShareLeftItemSpring", "a_share_left-item_spring", "left item spring", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemStick = new AccessoryInfo("ShareLeftItemStick", "a_share_left-item_stick", "left item stick", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemSword = new AccessoryInfo("ShareLeftItemSword", "a_share_left-item_sword", "left item sword", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemToiletPaper = new AccessoryInfo("ShareLeftItemToiletPaper", "a_share_left-item_toilet-paper", "left item toilet paper", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemTrumpet = new AccessoryInfo("ShareLeftItemTrumpet", "a_share_left-item_trumpet", "left item trumpet", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemUmbrella = new AccessoryInfo("ShareLeftItemUmbrella", "a_share_left-item_umbrella", "left item umbrella", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemVanillaIcecream = new AccessoryInfo("ShareLeftItemVanillaIcecream", "a_share_left-item_vanilla-icecream", "left item vanilla icecream", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemVhsTape = new AccessoryInfo("ShareLeftItemVhsTape", "a_share_left-item_vhs-tape", "left item vhs tape", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemWand = new AccessoryInfo("ShareLeftItemWand", "a_share_left-item_wand", "left item wand", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLeftItemYoyo = new AccessoryInfo("ShareLeftItemYoyo", "a_share_left-item_yoyo", "left item yoyo", AccessoryType.LeftItem, CharacterType.Share, 11);
	public static AccessoryInfo ShareLongsleeve = new AccessoryInfo("ShareLongsleeve", "a_share_longsleeve", "longsleeve", AccessoryType.Top, CharacterType.Share, 58);
	public static AccessoryInfo ShareLongsleeveButtonup = new AccessoryInfo("ShareLongsleeveButtonup", "a_share_longsleeve-buttonup", "longsleeve buttonup", AccessoryType.Top, CharacterType.Share, 58);
	public static AccessoryInfo ShareLongsleeveCollared = new AccessoryInfo("ShareLongsleeveCollared", "a_share_longsleeve-collared", "longsleeve collared", AccessoryType.Top, CharacterType.Share, 58);
	public static AccessoryInfo ShareMittens = new AccessoryInfo("ShareMittens", "a_share_mittens", "mittens", AccessoryType.Hands, CharacterType.Share, 54);
	public static AccessoryInfo ShareMoon = new AccessoryInfo("ShareMoon", "a_share_moon", "moon", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareMummy = new AccessoryInfo("ShareMummy", "a_share_mummy", "mummy", AccessoryType.Footwear, CharacterType.Share, 53);
	public static AccessoryInfo ShareMummyPants = new AccessoryInfo("ShareMummyPants", "a_share_mummy-pants", "mummy pants", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo ShareMummyTop = new AccessoryInfo("ShareMummyTop", "a_share_mummy-top", "mummy top", AccessoryType.Outerwear, CharacterType.Share, 55);
	public static AccessoryInfo ShareMummyWhole = new AccessoryInfo("ShareMummyWhole", "a_share_mummy-whole", "mummy whole", AccessoryType.Outerwear, CharacterType.Share, 55);
	public static AccessoryInfo ShareNecklaceGold = new AccessoryInfo("ShareNecklaceGold", "a_share_necklace_gold", "necklace gold", AccessoryType.Neckwear, CharacterType.Share, 56);
	public static AccessoryInfo ShareNumber15 = new AccessoryInfo("ShareNumber15", "a_share_number-15", "number 15", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareNumberOne = new AccessoryInfo("ShareNumberOne", "a_share_number-one", "number one", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareOveralls = new AccessoryInfo("ShareOveralls", "a_share_overalls", "overalls", AccessoryType.Outerwear, CharacterType.Share, 55);
	public static AccessoryInfo SharePants = new AccessoryInfo("SharePants", "a_share_pants", "pants", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo SharePow = new AccessoryInfo("SharePow", "a_share_pow", "pow", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo SharePrisonersRobe = new AccessoryInfo("SharePrisonersRobe", "a_share_prisoners_robe", "prisoners robe", AccessoryType.Outerwear, CharacterType.Share, 55);
	public static AccessoryInfo ShareRightItemArrow = new AccessoryInfo("ShareRightItemArrow", "a_share_right-item_arrow", "right item arrow", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemAxe = new AccessoryInfo("ShareRightItemAxe", "a_share_right-item_axe", "right item axe", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemBalloon = new AccessoryInfo("ShareRightItemBalloon", "a_share_right-item_balloon", "right item balloon", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemBeer = new AccessoryInfo("ShareRightItemBeer", "a_share_right-item_beer", "right item beer", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemBottle = new AccessoryInfo("ShareRightItemBottle", "a_share_right-item_bottle", "right item bottle", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemBread = new AccessoryInfo("ShareRightItemBread", "a_share_right-item_bread", "right item bread", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemBriefcase = new AccessoryInfo("ShareRightItemBriefcase", "a_share_right-item_briefcase", "right item briefcase", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemCheeseburger = new AccessoryInfo("ShareRightItemCheeseburger", "a_share_right-item_cheeseburger", "right item cheeseburger", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemController = new AccessoryInfo("ShareRightItemController", "a_share_right-item_controller", "right item controller", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemCrowbar = new AccessoryInfo("ShareRightItemCrowbar", "a_share_right-item_crowbar", "right item crowbar", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemCutlass = new AccessoryInfo("ShareRightItemCutlass", "a_share_right-item_cutlass", "right item cutlass", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemDiamond = new AccessoryInfo("ShareRightItemDiamond", "a_share_right-item_diamond", "right item diamond", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemDynamite = new AccessoryInfo("ShareRightItemDynamite", "a_share_right-item_dynamite", "right item dynamite", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemEnergySword = new AccessoryInfo("ShareRightItemEnergySword", "a_share_right-item_energy-sword", "right item energy sword", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemFryingPan = new AccessoryInfo("ShareRightItemFryingPan", "a_share_right-item_frying-pan", "right item frying pan", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemFryingPanEggsBacon = new AccessoryInfo("ShareRightItemFryingPanEggsBacon", "a_share_right-item_frying-pan-eggs-bacon", "right item frying pan eggs bacon", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemGun = new AccessoryInfo("ShareRightItemGun", "a_share_right-item_gun", "right item gun", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemHammer = new AccessoryInfo("ShareRightItemHammer", "a_share_right-item_hammer", "right item hammer", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemKeyboard = new AccessoryInfo("ShareRightItemKeyboard", "a_share_right-item_keyboard", "right item keyboard", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemLaptop = new AccessoryInfo("ShareRightItemLaptop", "a_share_right-item_laptop", "right item laptop", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemLifesaverRing = new AccessoryInfo("ShareRightItemLifesaverRing", "a_share_right-item_lifesaver-ring", "right item lifesaver ring", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemPencil = new AccessoryInfo("ShareRightItemPencil", "a_share_right-item_pencil", "right item pencil", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemPoop = new AccessoryInfo("ShareRightItemPoop", "a_share_right-item_poop", "right item poop", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemPotion = new AccessoryInfo("ShareRightItemPotion", "a_share_right-item_potion", "right item potion", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemPresent = new AccessoryInfo("ShareRightItemPresent", "a_share_right-item_present", "right item present", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemRubiksCube = new AccessoryInfo("ShareRightItemRubiksCube", "a_share_right-item_rubiks-cube", "right item rubiks cube", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemSai = new AccessoryInfo("ShareRightItemSai", "a_share_right-item_sai", "right item sai", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemSawWand = new AccessoryInfo("ShareRightItemSawWand", "a_share_right-item_saw_wand", "right item saw wand", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemScythe = new AccessoryInfo("ShareRightItemScythe", "a_share_right-item_scythe", "right item scythe", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemSherbetIcecream = new AccessoryInfo("ShareRightItemSherbetIcecream", "a_share_right-item_sherbet-icecream", "right item sherbet icecream", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemSickle = new AccessoryInfo("ShareRightItemSickle", "a_share_right-item_sickle", "right item sickle", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemSnake = new AccessoryInfo("ShareRightItemSnake", "a_share_right-item_snake", "right item snake", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemSpikeball = new AccessoryInfo("ShareRightItemSpikeball", "a_share_right-item_spikeball", "right item spikeball", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemSword = new AccessoryInfo("ShareRightItemSword", "a_share_right-item_sword", "right item sword", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemUmbrella = new AccessoryInfo("ShareRightItemUmbrella", "a_share_right-item_umbrella", "right item umbrella", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemVanillaIcecream = new AccessoryInfo("ShareRightItemVanillaIcecream", "a_share_right-item_vanilla-icecream", "right item vanilla icecream", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRightItemWand = new AccessoryInfo("ShareRightItemWand", "a_share_right-item_wand", "right item wand", AccessoryType.RightItem, CharacterType.Share, 12);
	public static AccessoryInfo ShareRockOn = new AccessoryInfo("ShareRockOn", "a_share_rock-on", "rock on", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareRunningShoes = new AccessoryInfo("ShareRunningShoes", "a_share_running_shoes", "running shoes", AccessoryType.Footwear, CharacterType.Share, 53);
	public static AccessoryInfo ShareSkirt = new AccessoryInfo("ShareSkirt", "a_share_skirt", "skirt", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo ShareSkull = new AccessoryInfo("ShareSkull", "a_share_skull", "skull", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareSmile = new AccessoryInfo("ShareSmile", "a_share_smile", "smile", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareSpaceBoots = new AccessoryInfo("ShareSpaceBoots", "a_share_space_boots", "space boots", AccessoryType.Footwear, CharacterType.Share, 53);
	public static AccessoryInfo ShareStriped = new AccessoryInfo("ShareStriped", "a_share_striped", "striped", AccessoryType.Belt, CharacterType.Share, 60);
	public static AccessoryInfo ShareStudded = new AccessoryInfo("ShareStudded", "a_share_studded", "studded", AccessoryType.Belt, CharacterType.Share, 60);
	public static AccessoryInfo ShareSun = new AccessoryInfo("ShareSun", "a_share_sun", "sun", AccessoryType.Print, CharacterType.Share, 57);
	public static AccessoryInfo ShareSweatband = new AccessoryInfo("ShareSweatband", "a_share_sweatband", "sweatband", AccessoryType.Wrist, CharacterType.Share, 59);
	public static AccessoryInfo ShareTankTop = new AccessoryInfo("ShareTankTop", "a_share_tank-top", "tank top", AccessoryType.Top, CharacterType.Share, 58);
	public static AccessoryInfo ShareTie = new AccessoryInfo("ShareTie", "a_share_tie", "tie", AccessoryType.Neckwear, CharacterType.Share, 56);
	public static AccessoryInfo ShareTshirt = new AccessoryInfo("ShareTshirt", "a_share_tshirt", "tshirt", AccessoryType.Top, CharacterType.Share, 58);
	public static AccessoryInfo ShareTshirtCollared = new AccessoryInfo("ShareTshirtCollared", "a_share_tshirt-collared", "tshirt collared", AccessoryType.Top, CharacterType.Share, 58);
	public static AccessoryInfo ShareVerticalStripePants = new AccessoryInfo("ShareVerticalStripePants", "a_share_vertical_stripe_pants", "vertical stripe pants", AccessoryType.Bottom, CharacterType.Share, 61);
	public static AccessoryInfo ShareVestTorn = new AccessoryInfo("ShareVestTorn", "a_share_vest-torn", "vest torn", AccessoryType.Top, CharacterType.Share, 58);
	public static AccessoryInfo ShareWaistcoat = new AccessoryInfo("ShareWaistcoat", "a_share_waistcoat", "waistcoat", AccessoryType.Outerwear, CharacterType.Share, 55);
	public static AccessoryInfo ShareWatch = new AccessoryInfo("ShareWatch", "a_share_watch", "watch", AccessoryType.Wrist, CharacterType.Share, 59);
	public static AccessoryInfo ShareWingedShoes = new AccessoryInfo("ShareWingedShoes", "a_share_winged_shoes", "winged shoes", AccessoryType.Footwear, CharacterType.Share, 53);
	public static AccessoryInfo ShareWorkBoots = new AccessoryInfo("ShareWorkBoots", "a_share_work_boots", "work boots", AccessoryType.Footwear, CharacterType.Share, 53);
	public static AccessoryInfo ShareZebraPants = new AccessoryInfo("ShareZebraPants", "a_share_zebra-pants", "zebra pants", AccessoryType.Bottom, CharacterType.Share, 61);

	public static Dictionary<string, AccessoryInfo> accessories = new Dictionary<string, AccessoryInfo>() {
		{ "a_alien_afro", AlienAfro },
		{ "a_alien_bangs", AlienBangs },
		{ "a_alien_baseball-cap", AlienBaseballCap },
		{ "a_alien_beanie", AlienBeanie },
		{ "a_alien_bowler-hat", AlienBowlerHat },
		{ "a_alien_broken-glasses", AlienBrokenGlasses },
		{ "a_alien_bun", AlienBun },
		{ "a_alien_buzzcut", AlienBuzzcut },
		{ "a_alien_cigarette", AlienCigarette },
		{ "a_alien_circle_beard", AlienCircleBeard },
		{ "a_alien_construction-helmet", AlienConstructionHelmet },
		{ "a_alien_cowboy-hat", AlienCowboyHat },
		{ "a_alien_crown", AlienCrown },
		{ "a_alien_curly", AlienCurly },
		{ "a_alien_curtain", AlienCurtain },
		{ "a_alien_cyborg", AlienCyborg },
		{ "a_alien_diamond_studs", AlienDiamondStuds },
		{ "a_alien_eyepatch", AlienEyepatch },
		{ "a_alien_eyepatch_skull", AlienEyepatchSkull },
		{ "a_alien_fish-hat", AlienFishHat },
		{ "a_alien_goatee", AlienGoatee },
		{ "a_alien_goggles_vr", AlienGogglesVr },
		{ "a_alien_hamburger", AlienHamburger },
		{ "a_alien_handlebar", AlienHandlebar },
		{ "a_alien_helmet-viking", AlienHelmetViking },
		{ "a_alien_high_flat_top", AlienHighFlatTop },
		{ "a_alien_hoop_earring", AlienHoopEarring },
		{ "a_alien_huge_beard", AlienHugeBeard },
		{ "a_alien_huge_mustache", AlienHugeMustache },
		{ "a_alien_kid-propeller-hat", AlienKidPropellerHat },
		{ "a_alien_mask", AlienMask },
		{ "a_alien_mohavk", AlienMohavk },
		{ "a_alien_monacle", AlienMonacle },
		{ "a_alien_mustache", AlienMustache },
		{ "a_alien_mutton", AlienMutton },
		{ "a_alien_narrow-glasses", AlienNarrowGlasses },
		{ "a_alien_nose_ring", AlienNoseRing },
		{ "a_alien_pacifier", AlienPacifier },
		{ "a_alien_party-glasses", AlienPartyGlasses },
		{ "a_alien_party-hat", AlienPartyHat },
		{ "a_alien_party_horn", AlienPartyHorn },
		{ "a_alien_pigtails", AlienPigtails },
		{ "a_alien_pipe", AlienPipe },
		{ "a_alien_pirate-hat", AlienPirateHat },
		{ "a_alien_police-hat", AlienPoliceHat },
		{ "a_alien_ponytail", AlienPonytail },
		{ "a_alien_pulled_back", AlienPulledBack },
		{ "a_alien_rectangular-glasses", AlienRectangularGlasses },
		{ "a_alien_round-glasses", AlienRoundGlasses },
		{ "a_alien_santa-hat", AlienSantaHat },
		{ "a_alien_shaggy", AlienShaggy },
		{ "a_alien_simple", AlienSimple },
		{ "a_alien_sombrero", AlienSombrero },
		{ "a_alien_spiky", AlienSpiky },
		{ "a_alien_steampunk-goggles", AlienSteampunkGoggles },
		{ "a_alien_straight", AlienStraight },
		{ "a_alien_sunglasses", AlienSunglasses },
		{ "a_alien_top-hat", AlienTopHat },
		{ "a_alien_traffic-cone", AlienTrafficCone },
		{ "a_alien_van-dyke", AlienVanDyke },
		{ "a_alien_vape", AlienVape },
		{ "a_alien_visor", AlienVisor },
		{ "a_ape_afro", ApeAfro },
		{ "a_ape_bangs", ApeBangs },
		{ "a_ape_baseball-cap", ApeBaseballCap },
		{ "a_ape_beanie", ApeBeanie },
		{ "a_ape_bowler-hat", ApeBowlerHat },
		{ "a_ape_broken-glasses", ApeBrokenGlasses },
		{ "a_ape_bun", ApeBun },
		{ "a_ape_buzzcut", ApeBuzzcut },
		{ "a_ape_cigarette", ApeCigarette },
		{ "a_ape_circle_beard", ApeCircleBeard },
		{ "a_ape_construction-helmet", ApeConstructionHelmet },
		{ "a_ape_cowboy-hat", ApeCowboyHat },
		{ "a_ape_crown", ApeCrown },
		{ "a_ape_curly", ApeCurly },
		{ "a_ape_curtain", ApeCurtain },
		{ "a_ape_cyborg", ApeCyborg },
		{ "a_ape_diamond_studs", ApeDiamondStuds },
		{ "a_ape_eyepatch", ApeEyepatch },
		{ "a_ape_eyepatch_skull", ApeEyepatchSkull },
		{ "a_ape_fish-hat", ApeFishHat },
		{ "a_ape_goatee", ApeGoatee },
		{ "a_ape_goggles_vr", ApeGogglesVr },
		{ "a_ape_hamburger", ApeHamburger },
		{ "a_ape_handlebar", ApeHandlebar },
		{ "a_ape_helmet-viking", ApeHelmetViking },
		{ "a_ape_high_flat_top", ApeHighFlatTop },
		{ "a_ape_hoop_earring", ApeHoopEarring },
		{ "a_ape_huge_beard", ApeHugeBeard },
		{ "a_ape_huge_mustache", ApeHugeMustache },
		{ "a_ape_kid-propeller-hat", ApeKidPropellerHat },
		{ "a_ape_mask", ApeMask },
		{ "a_ape_mohavk", ApeMohavk },
		{ "a_ape_monacle", ApeMonacle },
		{ "a_ape_mustache", ApeMustache },
		{ "a_ape_mutton", ApeMutton },
		{ "a_ape_narrow-glasses", ApeNarrowGlasses },
		{ "a_ape_nose_ring", ApeNoseRing },
		{ "a_ape_pacifier", ApePacifier },
		{ "a_ape_party-glasses", ApePartyGlasses },
		{ "a_ape_party-hat", ApePartyHat },
		{ "a_ape_party_horn", ApePartyHorn },
		{ "a_ape_pigtails", ApePigtails },
		{ "a_ape_pipe", ApePipe },
		{ "a_ape_pirate-hat", ApePirateHat },
		{ "a_ape_police-hat", ApePoliceHat },
		{ "a_ape_ponytail", ApePonytail },
		{ "a_ape_pulled_back", ApePulledBack },
		{ "a_ape_rectangular-glasses", ApeRectangularGlasses },
		{ "a_ape_round-glasses", ApeRoundGlasses },
		{ "a_ape_santa-hat", ApeSantaHat },
		{ "a_ape_shaggy", ApeShaggy },
		{ "a_ape_simple", ApeSimple },
		{ "a_ape_sombrero", ApeSombrero },
		{ "a_ape_spiky", ApeSpiky },
		{ "a_ape_steampunk-goggles", ApeSteampunkGoggles },
		{ "a_ape_straight", ApeStraight },
		{ "a_ape_sunglasses", ApeSunglasses },
		{ "a_ape_top-hat", ApeTopHat },
		{ "a_ape_traffic-cone", ApeTrafficCone },
		{ "a_ape_van-dyke", ApeVanDyke },
		{ "a_ape_vape", ApeVape },
		{ "a_ape_visor", ApeVisor },
		{ "a_cat_afro", CatAfro },
		{ "a_cat_bangs", CatBangs },
		{ "a_cat_baseball-cap", CatBaseballCap },
		{ "a_cat_beanie", CatBeanie },
		{ "a_cat_bowler-hat", CatBowlerHat },
		{ "a_cat_broken-glasses", CatBrokenGlasses },
		{ "a_cat_bun", CatBun },
		{ "a_cat_buzzcut", CatBuzzcut },
		{ "a_cat_cigarette", CatCigarette },
		{ "a_cat_circle_beard", CatCircleBeard },
		{ "a_cat_construction-helmet", CatConstructionHelmet },
		{ "a_cat_cowboy-hat", CatCowboyHat },
		{ "a_cat_crown", CatCrown },
		{ "a_cat_curly", CatCurly },
		{ "a_cat_curtain", CatCurtain },
		{ "a_cat_cyborg", CatCyborg },
		{ "a_cat_diamond_studs", CatDiamondStuds },
		{ "a_cat_eyepatch", CatEyepatch },
		{ "a_cat_eyepatch_skull", CatEyepatchSkull },
		{ "a_cat_fish-hat", CatFishHat },
		{ "a_cat_goatee", CatGoatee },
		{ "a_cat_goggles_vr", CatGogglesVr },
		{ "a_cat_hamburger", CatHamburger },
		{ "a_cat_handlebar", CatHandlebar },
		{ "a_cat_helmet-viking", CatHelmetViking },
		{ "a_cat_high_flat_top", CatHighFlatTop },
		{ "a_cat_hoop_earring", CatHoopEarring },
		{ "a_cat_huge_beard", CatHugeBeard },
		{ "a_cat_huge_mustache", CatHugeMustache },
		{ "a_cat_kid-propeller-hat", CatKidPropellerHat },
		{ "a_cat_mask", CatMask },
		{ "a_cat_mohavk", CatMohavk },
		{ "a_cat_monacle", CatMonacle },
		{ "a_cat_mustache", CatMustache },
		{ "a_cat_mutton", CatMutton },
		{ "a_cat_narrow-glasses", CatNarrowGlasses },
		{ "a_cat_nose_ring", CatNoseRing },
		{ "a_cat_pacifier", CatPacifier },
		{ "a_cat_party-glasses", CatPartyGlasses },
		{ "a_cat_party-hat", CatPartyHat },
		{ "a_cat_party_horn", CatPartyHorn },
		{ "a_cat_pigtails", CatPigtails },
		{ "a_cat_pipe", CatPipe },
		{ "a_cat_pirate-hat", CatPirateHat },
		{ "a_cat_police-hat", CatPoliceHat },
		{ "a_cat_ponytail", CatPonytail },
		{ "a_cat_pulled_back", CatPulledBack },
		{ "a_cat_rectangular-glasses", CatRectangularGlasses },
		{ "a_cat_round-glasses", CatRoundGlasses },
		{ "a_cat_santa-hat", CatSantaHat },
		{ "a_cat_shaggy", CatShaggy },
		{ "a_cat_simple", CatSimple },
		{ "a_cat_sombrero", CatSombrero },
		{ "a_cat_spiky", CatSpiky },
		{ "a_cat_steampunk-goggles", CatSteampunkGoggles },
		{ "a_cat_straight", CatStraight },
		{ "a_cat_sunglasses", CatSunglasses },
		{ "a_cat_top-hat", CatTopHat },
		{ "a_cat_traffic-cone", CatTrafficCone },
		{ "a_cat_van-dyke", CatVanDyke },
		{ "a_cat_vape", CatVape },
		{ "a_cat_visor", CatVisor },
		{ "a_doge_afro", DogeAfro },
		{ "a_doge_bangs", DogeBangs },
		{ "a_doge_baseball-cap", DogeBaseballCap },
		{ "a_doge_beanie", DogeBeanie },
		{ "a_doge_bowler-hat", DogeBowlerHat },
		{ "a_doge_broken-glasses", DogeBrokenGlasses },
		{ "a_doge_bun", DogeBun },
		{ "a_doge_buzzcut", DogeBuzzcut },
		{ "a_doge_cigarette", DogeCigarette },
		{ "a_doge_circle_beard", DogeCircleBeard },
		{ "a_doge_construction-helmet", DogeConstructionHelmet },
		{ "a_doge_cowboy-hat", DogeCowboyHat },
		{ "a_doge_crown", DogeCrown },
		{ "a_doge_curly", DogeCurly },
		{ "a_doge_curtain", DogeCurtain },
		{ "a_doge_cyborg", DogeCyborg },
		{ "a_doge_diamond_studs", DogeDiamondStuds },
		{ "a_doge_eyepatch", DogeEyepatch },
		{ "a_doge_eyepatch_skull", DogeEyepatchSkull },
		{ "a_doge_fish-hat", DogeFishHat },
		{ "a_doge_goatee", DogeGoatee },
		{ "a_doge_goggles_vr", DogeGogglesVr },
		{ "a_doge_hamburger", DogeHamburger },
		{ "a_doge_handlebar", DogeHandlebar },
		{ "a_doge_helmet-viking", DogeHelmetViking },
		{ "a_doge_high_flat_top", DogeHighFlatTop },
		{ "a_doge_hoop_earring", DogeHoopEarring },
		{ "a_doge_huge_beard", DogeHugeBeard },
		{ "a_doge_huge_mustache", DogeHugeMustache },
		{ "a_doge_kid-propeller-hat", DogeKidPropellerHat },
		{ "a_doge_mask", DogeMask },
		{ "a_doge_mohavk", DogeMohavk },
		{ "a_doge_monacle", DogeMonacle },
		{ "a_doge_mustache", DogeMustache },
		{ "a_doge_mutton", DogeMutton },
		{ "a_doge_narrow-glasses", DogeNarrowGlasses },
		{ "a_doge_nose_ring", DogeNoseRing },
		{ "a_doge_pacifier", DogePacifier },
		{ "a_doge_party-glasses", DogePartyGlasses },
		{ "a_doge_party-hat", DogePartyHat },
		{ "a_doge_party_horn", DogePartyHorn },
		{ "a_doge_pigtails", DogePigtails },
		{ "a_doge_pipe", DogePipe },
		{ "a_doge_pirate-hat", DogePirateHat },
		{ "a_doge_police-hat", DogePoliceHat },
		{ "a_doge_ponytail", DogePonytail },
		{ "a_doge_pulled_back", DogePulledBack },
		{ "a_doge_rectangular-glasses", DogeRectangularGlasses },
		{ "a_doge_round-glasses", DogeRoundGlasses },
		{ "a_doge_santa-hat", DogeSantaHat },
		{ "a_doge_shaggy", DogeShaggy },
		{ "a_doge_simple", DogeSimple },
		{ "a_doge_sombrero", DogeSombrero },
		{ "a_doge_spiky", DogeSpiky },
		{ "a_doge_steampunk-goggles", DogeSteampunkGoggles },
		{ "a_doge_straight", DogeStraight },
		{ "a_doge_sunglasses", DogeSunglasses },
		{ "a_doge_top-hat", DogeTopHat },
		{ "a_doge_traffic-cone", DogeTrafficCone },
		{ "a_doge_van-dyke", DogeVanDyke },
		{ "a_doge_vape", DogeVape },
		{ "a_doge_visor", DogeVisor },
		{ "a_frog_afro", FrogAfro },
		{ "a_frog_bangs", FrogBangs },
		{ "a_frog_baseball-cap", FrogBaseballCap },
		{ "a_frog_beanie", FrogBeanie },
		{ "a_frog_bowler-hat", FrogBowlerHat },
		{ "a_frog_broken-glasses", FrogBrokenGlasses },
		{ "a_frog_bun", FrogBun },
		{ "a_frog_buzzcut", FrogBuzzcut },
		{ "a_frog_cigarette", FrogCigarette },
		{ "a_frog_circle_beard", FrogCircleBeard },
		{ "a_frog_construction-helmet", FrogConstructionHelmet },
		{ "a_frog_cowboy-hat", FrogCowboyHat },
		{ "a_frog_crown", FrogCrown },
		{ "a_frog_curly", FrogCurly },
		{ "a_frog_curtain", FrogCurtain },
		{ "a_frog_cyborg", FrogCyborg },
		{ "a_frog_diamond_studs", FrogDiamondStuds },
		{ "a_frog_eyepatch", FrogEyepatch },
		{ "a_frog_eyepatch_skull", FrogEyepatchSkull },
		{ "a_frog_fish-hat", FrogFishHat },
		{ "a_frog_goatee", FrogGoatee },
		{ "a_frog_goggles_vr", FrogGogglesVr },
		{ "a_frog_hamburger", FrogHamburger },
		{ "a_frog_handlebar", FrogHandlebar },
		{ "a_frog_helmet-viking", FrogHelmetViking },
		{ "a_frog_high_flat_top", FrogHighFlatTop },
		{ "a_frog_hoop_earring", FrogHoopEarring },
		{ "a_frog_huge_beard", FrogHugeBeard },
		{ "a_frog_huge_mustache", FrogHugeMustache },
		{ "a_frog_kid-propeller-hat", FrogKidPropellerHat },
		{ "a_frog_mask", FrogMask },
		{ "a_frog_mohavk", FrogMohavk },
		{ "a_frog_monacle", FrogMonacle },
		{ "a_frog_mustache", FrogMustache },
		{ "a_frog_mutton", FrogMutton },
		{ "a_frog_narrow-glasses", FrogNarrowGlasses },
		{ "a_frog_nose_ring", FrogNoseRing },
		{ "a_frog_pacifier", FrogPacifier },
		{ "a_frog_party-glasses", FrogPartyGlasses },
		{ "a_frog_party-hat", FrogPartyHat },
		{ "a_frog_party_horn", FrogPartyHorn },
		{ "a_frog_pigtails", FrogPigtails },
		{ "a_frog_pipe", FrogPipe },
		{ "a_frog_pirate-hat", FrogPirateHat },
		{ "a_frog_police-hat", FrogPoliceHat },
		{ "a_frog_ponytail", FrogPonytail },
		{ "a_frog_pulled_back", FrogPulledBack },
		{ "a_frog_rectangular-glasses", FrogRectangularGlasses },
		{ "a_frog_round-glasses", FrogRoundGlasses },
		{ "a_frog_santa-hat", FrogSantaHat },
		{ "a_frog_shaggy", FrogShaggy },
		{ "a_frog_simple", FrogSimple },
		{ "a_frog_sombrero", FrogSombrero },
		{ "a_frog_spiky", FrogSpiky },
		{ "a_frog_steampunk-goggles", FrogSteampunkGoggles },
		{ "a_frog_straight", FrogStraight },
		{ "a_frog_sunglasses", FrogSunglasses },
		{ "a_frog_top-hat", FrogTopHat },
		{ "a_frog_traffic-cone", FrogTrafficCone },
		{ "a_frog_van-dyke", FrogVanDyke },
		{ "a_frog_vape", FrogVape },
		{ "a_frog_visor", FrogVisor },
		{ "a_human_afro", HumanAfro },
		{ "a_human_bangs", HumanBangs },
		{ "a_human_baseball-cap", HumanBaseballCap },
		{ "a_human_beanie", HumanBeanie },
		{ "a_human_bowler-hat", HumanBowlerHat },
		{ "a_human_broken-glasses", HumanBrokenGlasses },
		{ "a_human_bun", HumanBun },
		{ "a_human_buzzcut", HumanBuzzcut },
		{ "a_human_cigarette", HumanCigarette },
		{ "a_human_circle_beard", HumanCircleBeard },
		{ "a_human_construction-helmet", HumanConstructionHelmet },
		{ "a_human_cowboy-hat", HumanCowboyHat },
		{ "a_human_crown", HumanCrown },
		{ "a_human_curly", HumanCurly },
		{ "a_human_curtain", HumanCurtain },
		{ "a_human_cyborg", HumanCyborg },
		{ "a_human_diamond_studs", HumanDiamondStuds },
		{ "a_human_eyepatch", HumanEyepatch },
		{ "a_human_eyepatch_skull", HumanEyepatchSkull },
		{ "a_human_fish-hat", HumanFishHat },
		{ "a_human_goatee", HumanGoatee },
		{ "a_human_goggles_vr", HumanGogglesVr },
		{ "a_human_hamburger", HumanHamburger },
		{ "a_human_handlebar", HumanHandlebar },
		{ "a_human_helmet-viking", HumanHelmetViking },
		{ "a_human_high_flat_top", HumanHighFlatTop },
		{ "a_human_hoop_earring", HumanHoopEarring },
		{ "a_human_huge_beard", HumanHugeBeard },
		{ "a_human_huge_mustache", HumanHugeMustache },
		{ "a_human_kid-propeller-hat", HumanKidPropellerHat },
		{ "a_human_mask", HumanMask },
		{ "a_human_mohavk", HumanMohavk },
		{ "a_human_monacle", HumanMonacle },
		{ "a_human_mustache", HumanMustache },
		{ "a_human_mutton", HumanMutton },
		{ "a_human_narrow-glasses", HumanNarrowGlasses },
		{ "a_human_nose_ring", HumanNoseRing },
		{ "a_human_pacifier", HumanPacifier },
		{ "a_human_party-glasses", HumanPartyGlasses },
		{ "a_human_party-hat", HumanPartyHat },
		{ "a_human_party_horn", HumanPartyHorn },
		{ "a_human_pigtails", HumanPigtails },
		{ "a_human_pipe", HumanPipe },
		{ "a_human_pirate-hat", HumanPirateHat },
		{ "a_human_police-hat", HumanPoliceHat },
		{ "a_human_ponytail", HumanPonytail },
		{ "a_human_pulled_back", HumanPulledBack },
		{ "a_human_rectangular-glasses", HumanRectangularGlasses },
		{ "a_human_round-glasses", HumanRoundGlasses },
		{ "a_human_santa-hat", HumanSantaHat },
		{ "a_human_shaggy", HumanShaggy },
		{ "a_human_simple", HumanSimple },
		{ "a_human_sombrero", HumanSombrero },
		{ "a_human_spiky", HumanSpiky },
		{ "a_human_steampunk-goggles", HumanSteampunkGoggles },
		{ "a_human_straight", HumanStraight },
		{ "a_human_sunglasses", HumanSunglasses },
		{ "a_human_top-hat", HumanTopHat },
		{ "a_human_traffic-cone", HumanTrafficCone },
		{ "a_human_van-dyke", HumanVanDyke },
		{ "a_human_vape", HumanVape },
		{ "a_human_visor", HumanVisor },
		{ "a_share_banana", ShareBanana },
		{ "a_share_baseball-bat", ShareBaseballBat },
		{ "a_share_basic", ShareBasic },
		{ "a_share_basic_shoes", ShareBasicShoes },
		{ "a_share_beer", ShareBeer },
		{ "a_share_bomb", ShareBomb },
		{ "a_share_bowtie", ShareBowtie },
		{ "a_share_boxers", ShareBoxers },
		{ "a_share_boxers_dotty", ShareBoxersDotty },
		{ "a_share_boxers_striped", ShareBoxersStriped },
		{ "a_share_boxing-gloves", ShareBoxingGloves },
		{ "a_share_bra", ShareBra },
		{ "a_share_briefs", ShareBriefs },
		{ "a_share_burger", ShareBurger },
		{ "a_share_cactus", ShareCactus },
		{ "a_share_carrot", ShareCarrot },
		{ "a_share_cartoon-gloves", ShareCartoonGloves },
		{ "a_share_crab-claws", ShareCrabClaws },
		{ "a_share_crop-top", ShareCropTop },
		{ "a_share_dinner-jacket_open", ShareDinnerJacketOpen },
		{ "a_share_dino", ShareDino },
		{ "a_share_dollar-sign", ShareDollarSign },
		{ "a_share_duck", ShareDuck },
		{ "a_share_duck-face_shoes", ShareDuckFaceShoes },
		{ "a_share_eyeball", ShareEyeball },
		{ "a_share_gloves", ShareGloves },
		{ "a_share_g-string", ShareGString },
		{ "a_share_heart", ShareHeart },
		{ "a_share_hoodie", ShareHoodie },
		{ "a_share_ice-cream", ShareIceCream },
		{ "a_share_jacket_closed", ShareJacketClosed },
		{ "a_share_jacket_elbowpatches_open", ShareJacketElbowpatchesOpen },
		{ "a_share_jacket_open", ShareJacketOpen },
		{ "a_share_jeans", ShareJeans },
		{ "a_share_jeans_kneepads", ShareJeansKneepads },
		{ "a_share_jester_shoes", ShareJesterShoes },
		{ "a_share_jorts", ShareJorts },
		{ "a_share_jorts_frayed", ShareJortsFrayed },
		{ "a_share_left-item_arrow", ShareLeftItemArrow },
		{ "a_share_left-item_axe", ShareLeftItemAxe },
		{ "a_share_left-item_balloon", ShareLeftItemBalloon },
		{ "a_share_left-item_banana", ShareLeftItemBanana },
		{ "a_share_left-item_beer", ShareLeftItemBeer },
		{ "a_share_left-item_big-sword", ShareLeftItemBigSword },
		{ "a_share_left-item_boombox", ShareLeftItemBoombox },
		{ "a_share_left-item_booster-sword", ShareLeftItemBoosterSword },
		{ "a_share_left-item_bottle", ShareLeftItemBottle },
		{ "a_share_left-item_bow", ShareLeftItemBow },
		{ "a_share_left-item_bowling-ball", ShareLeftItemBowlingBall },
		{ "a_share_left-item_briefcase", ShareLeftItemBriefcase },
		{ "a_share_left-item_chainsaw", ShareLeftItemChainsaw },
		{ "a_share_left-item_controller", ShareLeftItemController },
		{ "a_share_left-item_crowbar", ShareLeftItemCrowbar },
		{ "a_share_left-item_dynamite", ShareLeftItemDynamite },
		{ "a_share_left-item_electric-guitar", ShareLeftItemElectricGuitar },
		{ "a_share_left-item_energy-sword", ShareLeftItemEnergySword },
		{ "a_share_left-item_eye-wand", ShareLeftItemEyeWand },
		{ "a_share_left-item_fish", ShareLeftItemFish },
		{ "a_share_left-item_flail", ShareLeftItemFlail },
		{ "a_share_left-item_footscooter", ShareLeftItemFootscooter },
		{ "a_share_left-item_frying-pan-eggs-bacon", ShareLeftItemFryingPanEggsBacon },
		{ "a_share_left-item_goldring", ShareLeftItemGoldring },
		{ "a_share_left-item_gun", ShareLeftItemGun },
		{ "a_share_left-item_key", ShareLeftItemKey },
		{ "a_share_left-item_keyboard", ShareLeftItemKeyboard },
		{ "a_share_left-item_laptop", ShareLeftItemLaptop },
		{ "a_share_left-item_leatherbag", ShareLeftItemLeatherbag },
		{ "a_share_left-item_lifesaver-ring", ShareLeftItemLifesaverRing },
		{ "a_share_left-item_longboard", ShareLeftItemLongboard },
		{ "a_share_left-item_magnet", ShareLeftItemMagnet },
		{ "a_share_left-item_money-bag", ShareLeftItemMoneyBag },
		{ "a_share_left-item_nunchucks", ShareLeftItemNunchucks },
		{ "a_share_left-item_poop", ShareLeftItemPoop },
		{ "a_share_left-item_potion", ShareLeftItemPotion },
		{ "a_share_left-item_rope", ShareLeftItemRope },
		{ "a_share_left-item_rubiks-cube", ShareLeftItemRubiksCube },
		{ "a_share_left-item_sai", ShareLeftItemSai },
		{ "a_share_left-item_scooter", ShareLeftItemScooter },
		{ "a_share_left-item_scythe", ShareLeftItemScythe },
		{ "a_share_left-item_sherbet-icecream", ShareLeftItemSherbetIcecream },
		{ "a_share_left-item_skateboard", ShareLeftItemSkateboard },
		{ "a_share_left-item_skull", ShareLeftItemSkull },
		{ "a_share_left-item_soccerball", ShareLeftItemSoccerball },
		{ "a_share_left-item_spade", ShareLeftItemSpade },
		{ "a_share_left-item_spring", ShareLeftItemSpring },
		{ "a_share_left-item_stick", ShareLeftItemStick },
		{ "a_share_left-item_sword", ShareLeftItemSword },
		{ "a_share_left-item_toilet-paper", ShareLeftItemToiletPaper },
		{ "a_share_left-item_trumpet", ShareLeftItemTrumpet },
		{ "a_share_left-item_umbrella", ShareLeftItemUmbrella },
		{ "a_share_left-item_vanilla-icecream", ShareLeftItemVanillaIcecream },
		{ "a_share_left-item_vhs-tape", ShareLeftItemVhsTape },
		{ "a_share_left-item_wand", ShareLeftItemWand },
		{ "a_share_left-item_yoyo", ShareLeftItemYoyo },
		{ "a_share_longsleeve", ShareLongsleeve },
		{ "a_share_longsleeve-buttonup", ShareLongsleeveButtonup },
		{ "a_share_longsleeve-collared", ShareLongsleeveCollared },
		{ "a_share_mittens", ShareMittens },
		{ "a_share_moon", ShareMoon },
		{ "a_share_mummy", ShareMummy },
		{ "a_share_mummy-pants", ShareMummyPants },
		{ "a_share_mummy-top", ShareMummyTop },
		{ "a_share_mummy-whole", ShareMummyWhole },
		{ "a_share_necklace_gold", ShareNecklaceGold },
		{ "a_share_number-15", ShareNumber15 },
		{ "a_share_number-one", ShareNumberOne },
		{ "a_share_overalls", ShareOveralls },
		{ "a_share_pants", SharePants },
		{ "a_share_pow", SharePow },
		{ "a_share_prisoners_robe", SharePrisonersRobe },
		{ "a_share_right-item_arrow", ShareRightItemArrow },
		{ "a_share_right-item_axe", ShareRightItemAxe },
		{ "a_share_right-item_balloon", ShareRightItemBalloon },
		{ "a_share_right-item_beer", ShareRightItemBeer },
		{ "a_share_right-item_bottle", ShareRightItemBottle },
		{ "a_share_right-item_bread", ShareRightItemBread },
		{ "a_share_right-item_briefcase", ShareRightItemBriefcase },
		{ "a_share_right-item_cheeseburger", ShareRightItemCheeseburger },
		{ "a_share_right-item_controller", ShareRightItemController },
		{ "a_share_right-item_crowbar", ShareRightItemCrowbar },
		{ "a_share_right-item_cutlass", ShareRightItemCutlass },
		{ "a_share_right-item_diamond", ShareRightItemDiamond },
		{ "a_share_right-item_dynamite", ShareRightItemDynamite },
		{ "a_share_right-item_energy-sword", ShareRightItemEnergySword },
		{ "a_share_right-item_frying-pan", ShareRightItemFryingPan },
		{ "a_share_right-item_frying-pan-eggs-bacon", ShareRightItemFryingPanEggsBacon },
		{ "a_share_right-item_gun", ShareRightItemGun },
		{ "a_share_right-item_hammer", ShareRightItemHammer },
		{ "a_share_right-item_keyboard", ShareRightItemKeyboard },
		{ "a_share_right-item_laptop", ShareRightItemLaptop },
		{ "a_share_right-item_lifesaver-ring", ShareRightItemLifesaverRing },
		{ "a_share_right-item_pencil", ShareRightItemPencil },
		{ "a_share_right-item_poop", ShareRightItemPoop },
		{ "a_share_right-item_potion", ShareRightItemPotion },
		{ "a_share_right-item_present", ShareRightItemPresent },
		{ "a_share_right-item_rubiks-cube", ShareRightItemRubiksCube },
		{ "a_share_right-item_sai", ShareRightItemSai },
		{ "a_share_right-item_saw_wand", ShareRightItemSawWand },
		{ "a_share_right-item_scythe", ShareRightItemScythe },
		{ "a_share_right-item_sherbet-icecream", ShareRightItemSherbetIcecream },
		{ "a_share_right-item_sickle", ShareRightItemSickle },
		{ "a_share_right-item_snake", ShareRightItemSnake },
		{ "a_share_right-item_spikeball", ShareRightItemSpikeball },
		{ "a_share_right-item_sword", ShareRightItemSword },
		{ "a_share_right-item_umbrella", ShareRightItemUmbrella },
		{ "a_share_right-item_vanilla-icecream", ShareRightItemVanillaIcecream },
		{ "a_share_right-item_wand", ShareRightItemWand },
		{ "a_share_rock-on", ShareRockOn },
		{ "a_share_running_shoes", ShareRunningShoes },
		{ "a_share_skirt", ShareSkirt },
		{ "a_share_skull", ShareSkull },
		{ "a_share_smile", ShareSmile },
		{ "a_share_space_boots", ShareSpaceBoots },
		{ "a_share_striped", ShareStriped },
		{ "a_share_studded", ShareStudded },
		{ "a_share_sun", ShareSun },
		{ "a_share_sweatband", ShareSweatband },
		{ "a_share_tank-top", ShareTankTop },
		{ "a_share_tie", ShareTie },
		{ "a_share_tshirt", ShareTshirt },
		{ "a_share_tshirt-collared", ShareTshirtCollared },
		{ "a_share_vertical_stripe_pants", ShareVerticalStripePants },
		{ "a_share_vest-torn", ShareVestTorn },
		{ "a_share_waistcoat", ShareWaistcoat },
		{ "a_share_watch", ShareWatch },
		{ "a_share_winged_shoes", ShareWingedShoes },
		{ "a_share_work_boots", ShareWorkBoots },
		{ "a_share_zebra-pants", ShareZebraPants },
	};

	public static AccessoryType GetAccessoryType(int layer)
	{
		var accessory = accessories.Values.FirstOrDefault(v => v.layer == layer);
		return accessory != null ? accessory.accessoryType : AccessoryType.None;
	}

	public static AccessoryInfo GetAccessoryInfo(string name)
	{
		if (accessories.ContainsKey(name))
		{
			return accessories[name];
		}
		return null;
	}

	public static AccessoryInfo[] GetAccessoryInfos(CharacterType characterType)
	{
		return accessories.Values.Where(v => v.characterType == characterType || v.characterType == CharacterType.Share).ToArray();
	}

	public static AccessoryInfo[] GetAccessoryInfos(CharacterType characterType, AccessoryType accessoryType)
	{
		return accessories.Values.Where(v => v.accessoryType == accessoryType && (v.characterType == characterType || v.characterType == CharacterType.Share)).ToArray();
	}
}

[System.Serializable]
public class AccessoryInfo
{
	public string name;
	public string displayName;
	public string layerName;
	public AccessoryType accessoryType;
	public CharacterType characterType;
	public int layer;

	public AccessoryInfo(string name, string layerName, string displayName, AccessoryType accessoryType, CharacterType characterType, int layer)
	{
		this.name = name;
		this.layerName = layerName;
		this.displayName = displayName;
		this.accessoryType = accessoryType;
		this.characterType = characterType;
		this.layer = layer;
	}

	public override string ToString()
	{
		return displayName;
	}
}

public enum AccessoryType
{
	None,
	Beard,
	Belt,
	Bottom,
	Eyewear,
	Footwear,
	Hair,
	Hands,
	Hat,
	LeftItem,
	Mouth,
	Neckwear,
	Outerwear,
	Piercing,
	Print,
	RightItem,
	Top,
	Wrist,
}
