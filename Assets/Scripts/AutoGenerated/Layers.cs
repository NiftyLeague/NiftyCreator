// This class is auto-generated do not modify
namespace NiftyLeague
{
	public static class Layers
	{
		public const int Default = 0;
		public const int TransparentFX = 1;
		public const int IgnoreRaycast = 2;
		public const int Water = 4;
		public const int UI = 5;
		public const int Ground = 8;
		public const int Character = 9;
		public const int OneWayPlatform = 10;
		public const int Tongue = 11;
		public const int Glow = 12;
		public const int Background = 13;
		public const int Fly = 14;


		public static int OnlyIncluding( params int[] layers )
		{
			int mask = 0;
			for( var i = 0; i < layers.Length; i++ )
				mask |= ( 1 << layers[i] );

			return mask;
		}


		public static int EverythingBut( params int[] layers )
		{
			return ~OnlyIncluding( layers );
		}
	}
}
