using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour {
	
	[SerializeField] private Player _player;

	private Transform _buttonT;
	
	private void Awake() {
		_buttonT = transform.GetChild(0);
	}

	private void OnEnable() {
		_player.OnDeath += ShowButton;
		_buttonT.DOShakeScale(1000, Vector3.one * 0.05f, 2,0f, false);
	}

	private void ShowButton() {
		_buttonT.gameObject.SetActive(true);
	}

	public void OnClick() {
		SceneManager.LoadScene(0);
	}

	private void OnDisable() {
		_player.OnDeath -= ShowButton;
		_buttonT.DOKill();
	}

}