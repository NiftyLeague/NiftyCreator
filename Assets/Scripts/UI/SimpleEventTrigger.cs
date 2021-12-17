using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SimpleEventTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public UnityEvent onPressed;
	public UnityEvent onReleased;


	private bool isPressed = false;


	public void OnPointerDown(PointerEventData eventData)
	{
		if (!isPressed)
		{
			isPressed = true;
			onPressed.Invoke();
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (isPressed)
		{
			isPressed = false;
			onReleased.Invoke();
		}

	}
}
