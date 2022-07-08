using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {

	public static float horizontalMove;

	private Action _getInput;

	private Vector3 _previousTouchPosition;
	private Vector3 _currentTouchPosition;

	private void Start() {
		_getInput = MobileInput;
	}

	private void KeyboardInput() {
		horizontalMove = Input.GetAxisRaw("Horizontal");
	}
	
	public void MoveHorizontal(InputAction.CallbackContext context) {
		horizontalMove = Math.Clamp((context.ReadValue<float>()/Screen.width-0.5f)*4f, -1, 1);
	}

	private void MobileInput() {
		if (Input.touchCount != 1) return;
		_currentTouchPosition = Input.GetTouch(0).position;
		horizontalMove = _currentTouchPosition.x - _previousTouchPosition.x;
	}

	private void Update() {
		_getInput();
	}

}