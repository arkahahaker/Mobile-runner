using DG.Tweening;
using UnityEngine;

public class GameStarter : MonoBehaviour {

	[SerializeField] private PlayerController _controller;

	private void Start() {
		transform.DOScale(Vector3.one * 0.8f, 100);
	}
	
	private void Update() {
		if (InputManager.horizontalMove != 0) {
			_controller.enabled = true;
			transform.DOComplete();
			gameObject.SetActive(false);
		}
	}

}