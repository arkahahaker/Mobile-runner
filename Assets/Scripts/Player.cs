using System;
using UnityEngine;

public class Player : WallInteractable {

	private static readonly int JumpState = Animator.StringToHash("Jump");

	public event Action OnDeath;

	[Header("New box prefab")] 
	[SerializeField] private PlayersBox _boxPrefab;
	
	[Header("The height of one box")]
	[SerializeField] private float _boxHight;
	
	[Header("Player")]
	[SerializeField] private GameObject _playerGO;

	[Header("Enable when game over")] 
	[SerializeField] private GameObject _skelet;

	[Space(10)]
	
	private Collider _collider;
	private Rigidbody _rb;
	private Animator _animator;
	private PlayerController _controller;
	
	[SerializeField] private int _boxesCount;
	public int BoxesCount {
		get => _boxesCount;
		set {
			if (_boxesCount < value) {
				AddBox();
				Jump();
			}
			else if (value == 0)
				GameOver();
			_boxesCount = value;
		}
	}

	private void Awake() {
		_boxesCount = 1;
		_controller = GetComponent<PlayerController>();
		_animator = _playerGO.GetComponent<Animator>();
		_collider = _playerGO.GetComponent<Collider>();
		_rb = _playerGO.GetComponent<Rigidbody>();
		GetComponentInChildren<PlayersBox>().OnWallCollided += () => { BoxesCount--; };
	}
	
	public override void TouchWall() {
		GameOver();
	}

	private void AddBox() {
		PlayersBox box = Instantiate(_boxPrefab, new Vector3(0, (BoxesCount + 0.5f) * _boxHight, 0), Quaternion.identity, transform);
		box.OnWallCollided += () => { BoxesCount--; };
		box.transform.localPosition = new Vector3(0, (BoxesCount+0.5f) * _boxHight, 0);
	}
	
	private void Jump() {
		_playerGO.transform.Translate(0, 1.1f, 0);
		_animator.SetTrigger(JumpState);	
	}

	private void GameOver() {
		OnDeath?.Invoke();
		_skelet.SetActive(true);
		Destroy(_animator);
		Destroy(_collider);
		Destroy(_rb);
		Destroy(_controller);
	}
	
}