using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] private Transform _playerTransform;

	private Vector3 _positionDifferenceToPlayer;
	
	private void Awake() {
		_positionDifferenceToPlayer = transform.position - _playerTransform.position;
	}

	private void Update() {
		transform.position = _playerTransform.position + _positionDifferenceToPlayer;
	}

}