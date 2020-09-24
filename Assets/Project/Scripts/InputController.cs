using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
	[SerializeField] private RectTransform stickBaseTransform;
	[SerializeField] private RectTransform stickTransform;
	[SerializeField] private RectTransform initialTransform;

	private float stickBaseRadius;
	private Vector2 startPos;

	public event Action<Vector2> OnMoveStickEvent;

	private void OnEnable() {
		stickBaseRadius = stickBaseTransform.rect.width * .5f;
	}

	public void OnFingerDown(BaseEventData ev) {
		startPos = Input.mousePosition;
		stickBaseTransform.position = startPos;
	}

	public void OnFingerDrag(BaseEventData ev) {
		StickPosition(Input.mousePosition);
	}

	public void OnFingerUp(BaseEventData ev) {
		stickBaseTransform.position = initialTransform.position;
		stickTransform.localPosition = Vector2.zero;
		OnMoveStickEvent?.Invoke(Vector2.zero);
	}

	private void StickPosition(Vector2 pos) {

		Vector2 centerPosition = stickBaseTransform.position;
		float distance = Vector2.Distance(pos, centerPosition);

		if (distance > stickBaseRadius) {
			var direction = (pos - centerPosition);
			direction *= stickBaseRadius / distance;
			pos = centerPosition + direction;
		}

		stickTransform.position = pos;
		var moveVector = (pos - startPos) / stickBaseRadius;
		OnMoveStickEvent?.Invoke(moveVector);
	}
}
