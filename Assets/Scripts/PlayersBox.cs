using System;

public class PlayersBox : WallInteractable {
	
	private const float TIME_TO_DESTROY = 2f;

	public event Action OnWallCollided;

	private bool _destroyed;

	private void Start() {
		_destroyed = false;
	}

	public override void TouchWall() {
		if (_destroyed) return;
		_destroyed = true;
		OnWallCollided?.Invoke();
		transform.SetParent(null);
		Invoke(nameof(DestroyBox), TIME_TO_DESTROY);
	}

	private void DestroyBox() {
		Destroy(gameObject);
	}

}