
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LauncherUI : MonoBehaviour
{
	public Text statusText;
	public Text versionText;
	public RectTransform options;
	public RectTransform selection;
	public Text startText;
	public float selectionMovementSpeed;
	public float selectionMovementRange;
	public float selectionCharSize;
	public Image[] selectionFlashes;

	public Color textColor01;
	public Color textColor02;
	public float colorLerpSpeed;

	private bool statusFlashing = true;
	private bool fadingStatus = false;

	private Text currentSelection;
	private bool seletionConfirmed = false;

	InputState input = new InputState();


	public void Update()
	{
		InputReader.GetInput(input);
		if (statusFlashing && !fadingStatus)
		{
			statusText.enabled = Mathf.Sin(Time.time * colorLerpSpeed) > 0f;
		}

		if (fadingStatus && statusText.color.a > 0f)
		{
			Vector2 pos = statusText.rectTransform.anchoredPosition;
			pos.y = 5.5f;
			statusText.rectTransform.anchoredPosition = Vector2.Lerp(statusText.rectTransform.anchoredPosition, pos, Time.deltaTime * 3f);
			statusText.color = Color.Lerp(statusText.color, Color.clear, Time.deltaTime * 3f);
			if (statusText.color.a < 0.05f)
			{
				statusText.color = Color.clear;
			}
		}

		if (options.gameObject.activeInHierarchy && currentSelection && !seletionConfirmed)
		{
			Vector2 size = selection.sizeDelta;
			float sin = Mathf.Sin(Time.time * selectionMovementSpeed);
			size.x = currentSelection.text.Length * selectionCharSize + sin * selectionMovementRange;
			selection.sizeDelta = size;
			selection.anchoredPosition = new Vector2(0f, currentSelection.rectTransform.anchoredPosition.y);
			currentSelection.enabled = sin > 0f;
			if (!seletionConfirmed && input.start && !input.wasStart)
			{
				StartCoroutine(SelectOption(currentSelection));
			}
		}
	}


	private IEnumerator SelectOption(Text option)
	{
		seletionConfirmed = true;
		currentSelection.enabled = true;
		Color originalColor = selectionFlashes[0].color;
		currentSelection = option;
		float startTime = Time.time;
		while (Time.time - startTime < 1f)
		{
			selectionFlashes[0].color = Time.time % 0.12f > 0.06f ? Color.white : originalColor;
			selectionFlashes[1].color = Time.time % 0.12f > 0.06f ? Color.white : originalColor;
			yield return new WaitForEndOfFrame();
		}
		selectionFlashes[0].color = Color.clear;
		selectionFlashes[1].color = Color.clear;
		option.enabled = false;
		option.GetComponent<EventTrigger>().OnSubmit(new BaseEventData(EventSystem.current));
	}

	public void SetVersion(string version)
	{
		versionText.text = version;
	}

	public void SetStatus(string status)
	{
		statusText.text = status.ToUpper();
	}

	public void SetStatusColorFlash(bool enabled)
	{
		if (statusFlashing && enabled == false)
		{
			statusText.color = Color.white;
			statusText.enabled = true;
		}
		statusFlashing = enabled;
	}

	public void FadeStatus()
	{
		fadingStatus = true;
	}

	public void EnableOptions()
	{
		options.gameObject.SetActive(true);
		currentSelection = startText;
	}
}
