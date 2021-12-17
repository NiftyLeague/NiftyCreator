using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Collections.Generic;

public class XUtils
{
	public const long KB = 1024;
	public const long MB = KB * KB;

	public const int MIN = 60;
	public const int HOUR = 60 * MIN;
	public const int DAY = 24 * HOUR;
	public const int WEEK = 7 * DAY;

	public static long Timestamp()
	{
		var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
		return (long)timeSpan.TotalSeconds;
	}

	public static double TimestampMilliseconds()
	{
		var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
		return (long)timeSpan.TotalMilliseconds;
	}

	public static string ColorToHex(Color color)
	{
		return string.Format("#{0}{1}{2}{3}",
			((int)(color.r * 255)).ToString("X2"),
			((int)(color.g * 255)).ToString("X2"),
			((int)(color.b * 255)).ToString("X2"),
			((int)(color.a * 255)).ToString("X2")
			);
	}

	public static string SerializeObject(object o)
	{
		if (!o.GetType().IsSerializable)
		{
			return null;
		}

		using (MemoryStream stream = new MemoryStream())
		{
			new BinaryFormatter().Serialize(stream, o);
			return Convert.ToBase64String(Compress(stream.ToArray()));
		}
	}


	public static byte[] SerializeObjectToBytes(object o, bool compress)
	{
		using (MemoryStream stream = new MemoryStream())
		{
			new BinaryFormatter().Serialize(stream, o);
			return compress ? Compress(stream.ToArray()) : stream.ToArray();
		}
	}

	public static T DeserializeObjectFromBytes<T>(byte[] bytes, bool isCompressed)
	{
		try
		{
			if (isCompressed)
			{
				bytes = Decompress(bytes);
			}

			using (MemoryStream stream = new MemoryStream(bytes))
			{
				return (T)(new BinaryFormatter().Deserialize(stream));
			}
		}
		catch
		{
			Debug.LogError("Failed to deserialize " + typeof(T).FullName);
		}


		return default(T);
	}

	public static T DeserializeObject<T>(string str) where T : new()
	{
		if (!string.IsNullOrEmpty(str))
		{
			try
			{
				byte[] bytes = Decompress(Convert.FromBase64String(str));

				using (MemoryStream stream = new MemoryStream(bytes))
				{
					return (T)(new BinaryFormatter().Deserialize(stream));
				}
			}
			catch
			{
				Debug.LogError("Failed to deserialize " + typeof(T).FullName);
			}
		}

		return new T();
	}


	public static byte[] DeserializeToBytes(string str)
	{
		if (!string.IsNullOrEmpty(str))
		{
			try
			{
				return Decompress(Convert.FromBase64String(str));
			}
			catch
			{
				Debug.LogError("Failed to deserialize bytes");
			}
		}

		return null;
	}

	public static string DeserializeToString(string str)
	{
		if (!string.IsNullOrEmpty(str))
		{
			try
			{
				var bytes = Decompress(Convert.FromBase64String(str));
				return System.Text.Encoding.ASCII.GetString(bytes);
			}
			catch
			{
				Debug.LogError("Failed to deserialize to string");
			}
		}

		return "";
	}


	static byte[] Compress(string text)
	{
		return Compress(System.Text.Encoding.ASCII.GetBytes(text));
	}

	static byte[] Compress(byte[] data)
	{
		return SevenZip.Compression.LZMA.SevenZipHelper.Compress(data);
	}

	static byte[] Decompress(byte[] data)
	{
		return SevenZip.Compression.LZMA.SevenZipHelper.Decompress(data);
	}
	public static void CopyTo(Stream input, Stream output)
	{
		byte[] buffer = new byte[64 * KB];
		int bytesRead;
		while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
		{
			output.Write(buffer, 0, bytesRead);
		}
	}

	public static bool IsNaN(Quaternion q)
	{
		return float.IsNaN(q.x) || float.IsNaN(q.y) || float.IsNaN(q.z) || float.IsNaN(q.w);
	}

	public static bool IsZero(Quaternion q)
	{
		return q.x == 0f && q.y == 0f && q.z == 0f && q.w == 0f;
	}


	public static long GetInt64HashCode(string text)
	{
		long hashCode = 0;
		if (!string.IsNullOrEmpty(text))
		{
			byte[] byteContents = System.Text.Encoding.ASCII.GetBytes(text);
			System.Security.Cryptography.SHA256 hash =
			new System.Security.Cryptography.SHA256CryptoServiceProvider();
			byte[] hashText = hash.ComputeHash(byteContents);
			//32Byte hashText separate
			//hashCodeStart = 0~7  8Byte
			//hashCodeMedium = 8~23  8Byte
			//hashCodeEnd = 24~31  8Byte
			//and Fold
			long hashCodeStart = BitConverter.ToInt64(hashText, 0);
			long hashCodeMedium = BitConverter.ToInt64(hashText, 8);
			long hashCodeEnd = BitConverter.ToInt64(hashText, 24);
			hashCode = hashCodeStart ^ hashCodeMedium ^ hashCodeEnd;
		}
		return (hashCode);
	}

	public static int GetInt32HashCode(string text)
	{
		int hashCode = 0;
		if (!string.IsNullOrEmpty(text))
		{
			byte[] byteContents = System.Text.Encoding.ASCII.GetBytes(text);
			System.Security.Cryptography.SHA256 hash =
			new System.Security.Cryptography.SHA256CryptoServiceProvider();
			byte[] hashText = hash.ComputeHash(byteContents);
			hashCode = BitConverter.ToInt32(hashText, 0);

		}
		return (hashCode);
	}


	public static void SaveBytes(byte[] bytes, string path, bool compress)
	{
		if (compress)
		{
			File.WriteAllBytes(path, SimpleAES.Encrypt(Compress(bytes)));
		}
		else
		{
			File.WriteAllBytes(path, SimpleAES.Encrypt(bytes));
		}
	}

	public static byte[] LoadBytes(string path, bool decompress)
	{
		if (decompress)
		{
			return Decompress(SimpleAES.DecryptToBytes(File.ReadAllBytes(path)));
		}
		else
		{
			return SimpleAES.DecryptToBytes(File.ReadAllBytes(path));
		}
	}

	public static void SaveObject(object obj, string name)
	{
		try
		{
			byte[] bytes = SerializeObjectToBytes(obj, false);
			string path = Path.Combine(Application.persistentDataPath, name + ".obj");
			File.WriteAllBytes(path, SimpleAES.Encrypt(bytes));
		}
		catch (Exception e)
		{
			Debug.LogError("Failed to save object: " + e.Message);
		}
	}

	public static T LoadObject<T>(string name)
	{
		try
		{
			string path = Path.Combine(Application.persistentDataPath, name + ".obj");
			byte[] bytes = File.ReadAllBytes(path);
			bytes = SimpleAES.DecryptToBytes(bytes);
			return DeserializeObjectFromBytes<T>(bytes, false);
		}
		catch (Exception e)
		{
			Debug.LogError("Failed to load object: " + e.Message);
		}
		return default(T);
	}



	public static byte[] Encrypt(string str, bool compress)
	{
		byte[] bytes = System.Text.Encoding.ASCII.GetBytes(str);
		if (compress)
		{
			return SimpleAES.Encrypt(Compress(bytes));
		}
		else
		{
			return SimpleAES.Encrypt(bytes);
		}
	}

	public static string Decrypt(byte[] bytes, bool decompress)
	{
		if (decompress)
		{
			return System.Text.Encoding.ASCII.GetString(Decompress(SimpleAES.DecryptToBytes(bytes)));
		}
		else
		{
			return System.Text.Encoding.ASCII.GetString(SimpleAES.DecryptToBytes(bytes));
		}
	}

	public static long GetSimpleDeviceId()
	{
		string id = $"{SystemInfo.deviceModel},{SystemInfo.deviceName},{SystemInfo.deviceType},{SystemInfo.graphicsDeviceName}";
		return GetInt64HashCode(id);
	}

	public static string Scramble(string str)
	{
		char[] chars = str.ToCharArray();
		System.Random r = new System.Random(827 >> 2);
		for (int i = 0; i < chars.Length; i++)
		{
			int randomIndex = r.Next(0, chars.Length);
			char temp = chars[randomIndex];
			chars[randomIndex] = chars[i];
			chars[i] = temp;
		}
		return new string(chars);
	}

	public static string Unscramble(string str)
	{
		System.Random r = new System.Random(827 >> 2);
		char[] scramChars = str.ToCharArray();
		List<int> swaps = new List<int>();
		for (int i = 0; i < scramChars.Length; i++)
		{
			swaps.Add(r.Next(0, scramChars.Length));
		}
		for (int i = scramChars.Length - 1; i >= 0; i--)
		{
			char temp = scramChars[swaps[i]];
			scramChars[swaps[i]] = scramChars[i];
			scramChars[i] = temp;
		}
		return new string(scramChars);
	}
}


public class SimpleAES
{
	// Change these keys
	private static byte[] Key = { 12, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 4, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 31, 236, 53, 209 };

	// a hardcoded IV should not be used for production AES-CBC code
	// IVs should be unpredictable per ciphertext
	private static byte[] Vector = { 1, 64, 191, 12, 23, 3, 113, 119, 231, 121, 252, 75, 79, 32, 114, 233 };


	private static ICryptoTransform EncryptorTransform, DecryptorTransform;
	private static System.Text.UTF8Encoding UTFEncoder;

	static SimpleAES()
	{
		//This is our encryption method
		RijndaelManaged rm = new RijndaelManaged();

		//Create an encryptor and a decryptor using our encryption method, key, and vector.
		EncryptorTransform = rm.CreateEncryptor(Key, Vector);
		DecryptorTransform = rm.CreateDecryptor(Key, Vector);

		//Used to translate bytes to text and vice versa
		UTFEncoder = new System.Text.UTF8Encoding();
	}

	/// -------------- Two Utility Methods (not used but may be useful) -----------
	/// Generates an encryption key.
	static public byte[] GenerateEncryptionKey()
	{
		//Generate a Key.
		RijndaelManaged rm = new RijndaelManaged();
		rm.GenerateKey();
		return rm.Key;
	}

	/// Generates a unique encryption vector
	static public byte[] GenerateEncryptionVector()
	{
		//Generate a Vector
		RijndaelManaged rm = new RijndaelManaged();
		rm.GenerateIV();
		return rm.IV;
	}


	/// ----------- The commonly used methods ------------------------------    
	/// Encrypt some text and return a string suitable for passing in a URL.
	static public string EncryptToString(string TextValue)
	{
		return ByteArrToString(Encrypt(TextValue));
	}

	/// Encrypt some text and return an encrypted byte array.
	static public byte[] Encrypt(string TextValue)
	{
		//Translates our text value into a byte array.
		Byte[] bytes = UTFEncoder.GetBytes(TextValue);
		return Encrypt(bytes);
	}

	static public byte[] Encrypt(byte[] bytes)
	{
		//Used to stream the data in and out of the CryptoStream.
		MemoryStream memoryStream = new MemoryStream();

		/*
		 * We will have to write the unencrypted bytes to the stream,
		 * then read the encrypted result back from the stream.
		 */
		#region Write the decrypted value to the encryption stream
		CryptoStream cs = new CryptoStream(memoryStream, EncryptorTransform, CryptoStreamMode.Write);
		cs.Write(bytes, 0, bytes.Length);
		cs.FlushFinalBlock();
		#endregion

		#region Read encrypted value back out of the stream
		memoryStream.Position = 0;
		byte[] encrypted = new byte[memoryStream.Length];
		memoryStream.Read(encrypted, 0, encrypted.Length);
		#endregion

		//Clean up.
		cs.Close();
		memoryStream.Close();

		return encrypted;
	}

	/// The other side: Decryption methods
	static public string DecryptString(string EncryptedString)
	{
		return Decrypt(StrToByteArray(EncryptedString));
	}

	/// Decryption when working with byte arrays.    
	static public string Decrypt(byte[] EncryptedValue)
	{
		return UTFEncoder.GetString(DecryptToBytes(EncryptedValue));
	}

	static public byte[] DecryptToBytes(byte[] EncryptedValue)
	{
		#region Write the encrypted value to the decryption stream
		MemoryStream encryptedStream = new MemoryStream();
		CryptoStream decryptStream = new CryptoStream(encryptedStream, DecryptorTransform, CryptoStreamMode.Write);
		decryptStream.Write(EncryptedValue, 0, EncryptedValue.Length);
		decryptStream.FlushFinalBlock();
		#endregion

		#region Read the decrypted value from the stream.
		encryptedStream.Position = 0;
		Byte[] decryptedBytes = new Byte[encryptedStream.Length];
		encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
		encryptedStream.Close();
		#endregion
		return decryptedBytes;
	}

	/// Convert a string to a byte array.  NOTE: Normally we'd create a Byte Array from a string using an ASCII encoding (like so).
	//      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
	//      return encoding.GetBytes(str);
	// However, this results in character values that cannot be passed in a URL.  So, instead, I just
	// lay out all of the byte values in a long string of numbers (three per - must pad numbers less than 100).
	static public byte[] StrToByteArray(string str)
	{
		if (str.Length == 0)
			throw new Exception("Invalid string value in StrToByteArray");

		byte val;
		byte[] byteArr = new byte[str.Length / 3];
		int i = 0;
		int j = 0;
		do
		{
			val = byte.Parse(str.Substring(i, 3));
			byteArr[j++] = val;
			i += 3;
		}
		while (i < str.Length);
		return byteArr;
	}

	// Same comment as above.  Normally the conversion would use an ASCII encoding in the other direction:
	//      System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
	//      return enc.GetString(byteArr);    
	static public string ByteArrToString(byte[] byteArr)
	{
		byte val;
		string tempStr = "";
		for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
		{
			val = byteArr[i];
			if (val < (byte)10)
				tempStr += "00" + val.ToString();
			else if (val < (byte)100)
				tempStr += "0" + val.ToString();
			else
				tempStr += val.ToString();
		}
		return tempStr;
	}
}
