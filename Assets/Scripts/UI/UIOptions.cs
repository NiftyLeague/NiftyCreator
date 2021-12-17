using UnityEngine;
using UnityEngine.UI;

public class UIOptions : MonoBehaviour
{
	public SimpleEventTrigger right;
	public SimpleEventTrigger left;
	public Text text;
	public Text soldout;
	public Image reset;
	public float soldoutFadeSpeed;
	public bool loop = true;
	public Color enabledColor;
	public Color disableColor;
	public Color navEnabledColor;
	public Color navDisableColor;
	public Vector2 bottonY;
	public AudioSource clickSfx;
	internal bool IsSoldOut { get { return GetCount() == 0 || (GetCount() == 1 && !options.HasEmpty); } }

	private OptionEnumarator options;
	private Vector3 leftPosition, rightPosition;
	private RectTransform leftRect, rightRect;
	private Text leftText, rightText;

	private bool leftDown, rightDown;
	private Color soldoutOriginalColor;
	private float soldoutDisplayTime;

	private void Awake()
	{
		rightRect = right.GetComponent<RectTransform>();
		leftRect = left.GetComponent<RectTransform>();
		rightText = right.GetComponent<Text>();
		leftText = left.GetComponent<Text>();
		rightPosition = rightRect.localPosition;
		leftPosition = leftRect.localPosition;
		leftDown = false;
		rightDown = false;
		soldout.enabled = false;
		soldoutOriginalColor = soldout.color;
		reset.enabled = false;
	}

	private void FixedUpdate()
	{
		if (soldout.enabled && Time.time - soldoutDisplayTime > 0.5f)
		{
			soldout.color = Color.Lerp(soldout.color, Color.clear, Time.deltaTime * soldoutFadeSpeed);
			if (soldout.color.a < 0.05f)
			{
				soldout.enabled = false;
			}
		}
	}

	public void Initialize(OptionEnumarator options)
	{
		this.options = options;
		UpdateOption();
	}

	public int GetCount()
	{
		return options != null ? (options.Count - (options.HasEmpty ? 1 : 0)) : 0;
	}

	public object GetCurrent()
	{
		return options != null ? options.Current.Value : null;
	}

	public object GetId()
	{
		return options.Id;
	}

	public string SetIndex(int index)
	{
		options.SetIndex(index);
		UpdateOption();
		return options.Current.Key;
	}

	public string Next()
	{
		if (!options.HasNext())
		{
			options.SetFirst();
		}
		else
		{
			options.Next();
		}
		UpdateOption();
		return options.Current.Key;
	}

	public string Prev()
	{
		if (!options.HasPrev())
		{
			options.SetLast();
		}
		else
		{
			options.Prev();
		}
		UpdateOption();
		return options.Current.Key;
	}

	public string NextGroup()
	{
		options.NextGroup();
		UpdateOption();
		return options.Current.Key;
	}

	public string Reselect()
	{
		options.Reselect();
		UpdateOption();
		return options.Current.Key;
	}

	private void UpdateOption()
	{
		string name = options.ToString().ToLower();
		string displayName = TraitInfo.displayNameMap.ContainsKey(name) ? TraitInfo.displayNameMap[name] : name;
		text.text = displayName.ToUpper().Replace("LEFT ITEM ", "").Replace("RIGHT ITEM ", "");

		if (!loop)
		{
			leftText.color = options.HasPrev() ? navEnabledColor : navDisableColor;
			rightText.color = options.HasNext() ? navEnabledColor : navDisableColor;
		}
		text.color = IsSoldOut ? disableColor : enabledColor;
		soldout.enabled = false;
		reset.enabled = options.HasEmpty && options.SelectionIndex >= 0;
	}

	private void DisplayNoOptionsAvailable()
	{
		soldout.enabled = true;
		soldout.color = soldoutOriginalColor;
		soldoutDisplayTime = Time.time;
	}

	public void UI_OnRightDown()
	{
		if (options.HasNext())
		{
			right.GetComponent<Shadow>().enabled = false;
			rightPosition.y = bottonY.y;
			rightRect.localPosition = rightPosition;
			Next();
			clickSfx.Play();
			rightDown = true;
		}
		else if (IsSoldOut)
		{
			DisplayNoOptionsAvailable();
		}
	}

	public void UI_OnRightUp()
	{
		if (rightDown)
		{
			right.GetComponent<Shadow>().enabled = true;
			rightPosition.y = bottonY.x;
			rightRect.localPosition = rightPosition;
			rightDown = false;
		}
	}

	public void UI_OnLeftDown()
	{
		if (options.HasPrev())
		{
			left.GetComponent<Shadow>().enabled = false;
			leftPosition.y = bottonY.y;
			leftRect.localPosition = leftPosition;
			Prev();
			clickSfx.Play();
			leftDown = true;
		}
		else if (IsSoldOut)
		{
			DisplayNoOptionsAvailable();
		}
	}

	public void UI_OnLeftUp()
	{
		if (leftDown)
		{
			left.GetComponent<Shadow>().enabled = true;
			leftPosition.y = bottonY.x;
			leftRect.localPosition = leftPosition;
			leftDown = false;
		}
	}

	public void UI_OnTextPressed()
	{
		if (!IsSoldOut)
		{
			NextGroup();
		}
		else
		{
			DisplayNoOptionsAvailable();
		}
		clickSfx.Play();
	}

	public void UI_OnResetPressed()
	{
		if (GetCount() != 0)
		{
			SetIndex(0);
		}
		clickSfx.Play();
	}
}
