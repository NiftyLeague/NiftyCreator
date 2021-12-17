
using System;

public class NetworkCustomTypes
{
	private static bool initialized = false;

	public static void Initialize()
	{
		if (!initialized)
		{
#if PHOTON_UNITY_NETWORKING
			ExitGames.Client.Photon.Protocol.TryRegisterType(CustomTypePlayer.Type, (byte)CustomTypePlayer.Code, CustomTypePlayer.Serialize, CustomTypePlayer.Deserialize);
#endif
		}
	}
}


class CustomTypePlayer
{
	public static Type Type => typeof(Player);

	public static CustomTypeCodes Code => CustomTypeCodes.Player;

	public static object Deserialize(byte[] data)
	{
		throw new NotImplementedException();
	}

	public static byte[] Serialize(object obj)
	{
		throw new NotImplementedException();
	}
}


enum CustomTypeCodes
{
	Player = 1,
}
