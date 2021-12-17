using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR && false
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
#endif //#if UNITY_EDITOR

public static class TransformExtensions
{
	public static Transform FindInChildren(this Transform self, string name)
	{
		Queue<Transform> queue = new Queue<Transform>();
		queue.Enqueue(self);
		while (queue.Count > 0)
		{
			Transform c = queue.Dequeue();
			if (c.name == name)
			{
				return c;
			}
			foreach (Transform t in c)
			{
				queue.Enqueue(t);
			}
		}
		return null;
	}
}

public static class ColorExtentions
{
	public static bool SameColorAs(this Color self, Color color)
	{
		return self.r == color.r && self.g == color.g && self.b == color.b && self.a == color.a;
	}

	public static bool SameColorAs(this Color32 self, Color32 color)
	{
		return self.r == color.r && self.g == color.g && self.b == color.b && self.a == color.a;
	}
}

#if UNITY_EDITOR && false
public static class AddressableExtensions
{
	public static void RemoveAddressableAssetLabel(this Object source, string label)
	{
		if (source == null || !AssetDatabase.Contains(source))
			return;

		var entry = source.GetAddressableAssetEntry();
		if (entry != null && entry.labels.Contains(label))
		{
			entry.labels.Remove(label);

			AddressableAssetSettingsDefaultObject.Settings.SetDirty(AddressableAssetSettings.ModificationEvent.LabelRemoved, entry, true);
		}
	}

	public static void AddAddressableAssetLabel(this Object source, string label)
	{
		if (source == null || !AssetDatabase.Contains(source))
			return;

		var entry = source.GetAddressableAssetEntry();
		if (entry != null && !entry.labels.Contains(label))
		{
			entry.labels.Add(label);

			AddressableAssetSettingsDefaultObject.Settings.SetDirty(AddressableAssetSettings.ModificationEvent.LabelAdded, entry, true);
		}
	}

	public static void SetAddressableAssetAddress(this Object source, string address)
	{
		if (source == null || !AssetDatabase.Contains(source))
			return;

		var entry = source.GetAddressableAssetEntry();
		if (entry != null)
		{
			entry.address = address;
		}
	}

	public static void SetAddressableAssetGroup(this Object source, string groupName)
	{
		if (source == null || !AssetDatabase.Contains(source))
			return;

		var group = !AddressableHelper.GroupExists(groupName) ? AddressableHelper.CreateGroup(groupName) : AddressableHelper.GetGroup(groupName);
		source.SetAddressableAssetGroup(group);
	}

	public static void SetAddressableAssetGroup(this Object source, AddressableAssetGroup group)
	{
		if (source == null || !AssetDatabase.Contains(source))
			return;

		var entry = source.GetAddressableAssetEntry();
		if (entry != null && !source.IsInAddressableAssetGroup(group.Name))
		{
			entry.parentGroup = group;
		}
	}

	public static HashSet<string> GetAddressableAssetLabels(this Object source)
	{
		if (source == null || !AssetDatabase.Contains(source))
			return null;

		var entry = source.GetAddressableAssetEntry();
		return entry?.labels;
	}

	public static string GetAddressableAssetPath(this Object source)
	{
		if (source == null || !AssetDatabase.Contains(source))
			return string.Empty;

		var entry = source.GetAddressableAssetEntry();
		return entry != null ? entry.address : string.Empty;
	}

	public static bool IsInAddressableAssetGroup(this Object source, string groupName)
	{
		if (source == null || !AssetDatabase.Contains(source))
			return false;

		var group = source.GetCurrentAddressableAssetGroup();
		return group != null && group.Name == groupName;
	}

	public static AddressableAssetGroup GetCurrentAddressableAssetGroup(this Object source)
	{
		if (source == null || !AssetDatabase.Contains(source))
			return null;

		var entry = source.GetAddressableAssetEntry();
		return entry?.parentGroup;
	}

	public static AddressableAssetEntry GetAddressableAssetEntry(this Object source)
	{
		if (source == null || !AssetDatabase.Contains(source))
			return null;

		var addressableSettings = AddressableAssetSettingsDefaultObject.Settings;
		var sourcePath = AssetDatabase.GetAssetPath(source);
		var sourceGuid = AssetDatabase.AssetPathToGUID(sourcePath);

		return addressableSettings.FindAssetEntry(sourceGuid);
	}
}
#endif //#if UNITY_EDITOR
