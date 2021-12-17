using System;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryOptionEnumrator : OptionEnumarator
{
	public object Id { get; }

	public KeyValuePair<string, object> Current => new KeyValuePair<string, object>(options[currentIndex].Key, options[currentIndex].Value);

	public int Count { get; }

	public int SelectionIndex => currentIndex - (HasEmpty ? 1 : 0);

	public bool HasEmpty { get; }

	public bool HasNext()
	{
		return currentIndex < Count - 1;
	}

	public bool HasPrev()
	{
		return currentIndex > 0;
	}

	public KeyValuePair<string, object> Next()
	{
		currentIndex++;
		SelectionChanged();
		return Current;
	}

	public KeyValuePair<string, object> Prev()
	{
		currentIndex--;
		SelectionChanged();
		return Current;
	}

	public KeyValuePair<string, object> NextGroup()
	{
		int nextGroupIndex = groupIndices.Find(i => i > currentIndex);
		if (nextGroupIndex > 0)
		{
			currentIndex = nextGroupIndex;
			SelectionChanged();
		}
		else if (groupIndices.Count > 0)
		{
			currentIndex = groupIndices[0];
			SelectionChanged();
		}
		return Current;
	}

	public KeyValuePair<string, object> SetIndex(int index)
	{
		currentIndex = index;
		SelectionChanged();
		return Current;
	}

	public KeyValuePair<string, object> SetFirst()
	{
		return SetIndex(0);
	}

	public KeyValuePair<string, object> SetLast()
	{
		return SetIndex(options.Count - 1);
	}

	public KeyValuePair<string, object> Reselect()
	{
		return SetIndex(currentIndex);
	}


	private string emptyString;
	private List<KeyValuePair<string, AccessoryOption>> options;
	private Action<OptionEnumarator> callback;
	private int currentIndex = 0;
	private List<int> groupIndices;


	public AccessoryOptionEnumrator(object id, string[] keys, AnimationLayerVariation[] values, Action<OptionEnumarator> callback, AccessoryOption currentSelection, bool addEmpty, string emptyString = "----")
	{
		this.Id = id;
		this.callback = callback;
		this.emptyString = emptyString;
		this.HasEmpty = addEmpty;
		options = new List<KeyValuePair<string, AccessoryOption>>();
		groupIndices = new List<int>();

		if (addEmpty)
		{
			groupIndices.Add(Count);
			options.Add(new KeyValuePair<string, AccessoryOption>(null, new AccessoryOption(null, -1)));
			Count++;
		}

		for (int i = 0; i < keys.Length; i++)
		{
			AnimationLayerVariation alv = values[i];
			int traitAdded = 0;
			if (alv.colorVariations.Length > 1)
			{
				for (int j = 0; j < alv.colorVariations.Length; j++)
				{
					// Hacky but there's no time :s
					Trait t = CustomizationManager.GetTraitFromOption(this, new KeyValuePair<string, object>(keys[i], new AccessoryOption(alv, j)));
					if (!CustomizationManager.IsRemovedTrait(t))
					{
						AddOption(keys[i], new AccessoryOption(alv, j), currentSelection);
						traitAdded++;
					}
				}
			}
			else
			{
				Trait t = CustomizationManager.GetTraitFromOption(this, new KeyValuePair<string, object>(keys[i], new AccessoryOption(alv, -1)));
				if (!CustomizationManager.IsRemovedTrait(t))
				{
					AddOption(keys[i], new AccessoryOption(alv, -1), currentSelection);
					traitAdded++;
				}
			}
			if (traitAdded > 0)
			{
				groupIndices.Add(Count);
				Count += traitAdded;
			}
		}
		SelectionChanged();
	}

	private void AddOption(string key, AccessoryOption option, AccessoryOption currentSelection)
	{
		options.Add(new KeyValuePair<string, AccessoryOption>(key, option));
		if (currentSelection != null && currentSelection.animationLayerVariation == option.animationLayerVariation && currentSelection.colorVariationIndex == option.colorVariationIndex)
		{
			currentIndex = options.Count - 1;
		}
	}

	public override string ToString()
	{
		string value = Current.Value.ToString();
		return string.IsNullOrEmpty(value) ? emptyString : value;
	}

	private void SelectionChanged()
	{
		if (callback != null)
		{
			callback(this);
		}
	}

	public class AccessoryOption
	{
		public AnimationLayerVariation animationLayerVariation;
		public int colorVariationIndex;

		public AccessoryOption(AnimationLayerVariation animationLayerVariation, int colorVariationIndex)
		{
			this.animationLayerVariation = animationLayerVariation;
			this.colorVariationIndex = colorVariationIndex;
		}

		public override string ToString()
		{
			if (animationLayerVariation)
			{
				return colorVariationIndex >= 0 ? $"{animationLayerVariation} {colorVariationIndex + 1:00}" : animationLayerVariation.ToString();
			}
			return null;
		}
	}
}

public class SimpleOptionEnumrator : OptionEnumarator
{
	public object Id { get; }

	public KeyValuePair<string, object> Current => options[currentIndex];

	public int Count { get; }

	public int SelectionIndex => currentIndex - (HasEmpty ? 1 : 0);

	public bool HasEmpty { get; }

	public bool HasNext()
	{
		return currentIndex < Count - 1;
	}

	public bool HasPrev()
	{
		return currentIndex > 0;
	}

	public KeyValuePair<string, object> Next()
	{
		currentIndex++;
		SelectionChanged();
		return Current;
	}

	public KeyValuePair<string, object> Prev()
	{
		currentIndex--;
		SelectionChanged();
		return Current;
	}

	public KeyValuePair<string, object> NextGroup()
	{
		if (HasNext())
		{
			return Next();
		}
		else
		{
			return SetIndex(0);
		}
	}

	public KeyValuePair<string, object> SetIndex(int index)
	{
		currentIndex = index;
		SelectionChanged();
		return Current;
	}

	public KeyValuePair<string, object> SetFirst()
	{
		return SetIndex(0);
	}

	public KeyValuePair<string, object> SetLast()
	{
		return SetIndex(options.Count - 1);
	}

	public KeyValuePair<string, object> Reselect()
	{
		return SetIndex(currentIndex);
	}

	private string emptyString;
	private List<KeyValuePair<string, object>> options;
	private int currentIndex = 0;
	private Action<OptionEnumarator> callback;


	public SimpleOptionEnumrator(object id, string[] keys, object[] values, Action<OptionEnumarator> callback, object currentSelection, bool addEmpty, string emptyString = "----")
	{
		this.Id = id;
		this.callback = callback;
		this.emptyString = emptyString;
		this.HasEmpty = addEmpty;
		options = new List<KeyValuePair<string, object>>();

		if (addEmpty)
		{
			options.Add(new KeyValuePair<string, object>(null, emptyString));
		}

		int traitAdded = 0;
		for (int i = 0; i < keys.Length; i++)
		{
			var option = new KeyValuePair<string, object>(keys[i], values[i]);
			Trait t = CustomizationManager.GetTraitFromOption(this, option);
			if (!CustomizationManager.IsRemovedTrait(t))
			{
				options.Add(option);
				traitAdded++;

				if (values[i] == currentSelection)
				{
					currentIndex = options.Count - 1;
				}
			}
		}
		Count = traitAdded;
		SelectionChanged();
	}

	private void SelectionChanged()
	{
		if (callback != null)
		{
			callback(this);
		}
	}

	public override string ToString()
	{
		string value = Current.Key.ToString();
		return string.IsNullOrEmpty(value) ? emptyString : value;
	}
}


public interface OptionEnumarator
{
	public object Id { get; }
	public KeyValuePair<string, object> Current { get; }
	public int Count { get; }
	public int SelectionIndex { get; }
	public bool HasEmpty { get; }
	public KeyValuePair<string, object> Next();
	public KeyValuePair<string, object> Prev();
	public KeyValuePair<string, object> NextGroup();
	public KeyValuePair<string, object> SetIndex(int selection);
	public KeyValuePair<string, object> SetFirst();
	public KeyValuePair<string, object> SetLast();
	public KeyValuePair<string, object> Reselect();
	public bool HasNext();
	public bool HasPrev();
}
