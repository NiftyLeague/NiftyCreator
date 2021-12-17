using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Utils
{

	public static GameObject CreateGameObject(GameObject parent, string name)
	{
		return CreateGameObject(parent, name, Vector3.zero, Quaternion.identity);
	}

	public static GameObject CreateGameObject(GameObject parent, string name, Vector3 position, Quaternion rotation)
	{
		var go = new GameObject();
		if (parent)
		{
			go.transform.SetParent(parent.transform);
		}
		go.transform.localPosition = position;
		go.transform.localRotation = rotation;
		go.name = name;
		return go;
	}

	public static string UnderscoreToCamelCase(string name)
	{
		string[] array = name.Split('_', '-', ' ');
		for (int i = 0; i < array.Length; i++)
		{
			string s = array[i];
			string first = string.Empty;
			string rest = string.Empty;
			if (s.Length > 0)
			{
				first = Char.ToUpperInvariant(s[0]).ToString();
			}
			if (s.Length > 1)
			{
				rest = s.Substring(1).ToLowerInvariant();
			}
			array[i] = first + rest;
		}
		return string.Join("", array);
	}

	public static string ToFirstLetterUppercase(string value)
	{
		char[] array = value.ToCharArray();
		if (array.Length >= 1)
		{
			if (char.IsLower(array[0]))
			{
				array[0] = char.ToUpper(array[0]);
			}
		}
		for (int i = 1; i < array.Length; i++)
		{
			if (array[i - 1] == ' ')
			{
				if (char.IsLower(array[i]))
				{
					array[i] = char.ToUpper(array[i]);
				}
			}
		}
		return new string(array);
	}

	public static string GetMD5Hash(string input)
	{
		StringBuilder hash = new StringBuilder();
		MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
		byte[] bytes = md5provider.ComputeHash(Encoding.ASCII.GetBytes(input));
		for (int i = 0; i < bytes.Length; i++)
		{
			hash.Append(bytes[i].ToString("x2"));
		}
		return hash.ToString();
	}

	public static bool IsHexString(string value)
	{
		return Regex.IsMatch(value, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");
	}
}

