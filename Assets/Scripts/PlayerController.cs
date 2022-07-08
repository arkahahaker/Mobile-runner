using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header("Forward control")]
	[SerializeField] private float _forwardSpeed;
	[SerializeField] private float _accelerate;
	
	[Space(10)]
	
	[Header("Horizontal control")]
	[SerializeField] private float _horizontalSpeed;
	[SerializeField] private float _gameZoneBorder;

	private float _startSpeed;
	
	private void OnEnable() {
		WallPassObserver.OnWallPassed += SpeedUp;
	}

	private void Start() {
		_startSpeed = _forwardSpeed;
	}

	private void FixedUpdate() {
		transform.Translate(Vector3.forward*_forwardSpeed*Time.fixedDeltaTime);
		transform.position = transform.position-new Vector3(transform.position.x, 0,0)+Vector3.right*_gameZoneBorder*InputManager.horizontalMove;
		if (!(Math.Abs(transform.position.x) > _gameZoneBorder)) return;
		Vector3 currentPosition = transform.position;
		transform.position = new Vector3(Math.Sign(currentPosition.x)*_gameZoneBorder, currentPosition.y, currentPosition.z);
	}

	public float GetAccelerate() {
		return (float)Math.Round((double)_forwardSpeed / _startSpeed, 1);
	}
	
	private void SpeedUp() {
		_forwardSpeed += _accelerate;
	}

	private void OnDisable() {
		WallPassObserver.OnWallPassed -= SpeedUp;
	}

}