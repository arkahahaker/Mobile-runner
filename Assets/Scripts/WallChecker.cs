using UnityEngine;

public class WallChecker : MonoBehaviour {

	private const int WALL_LAYER = 8;
	
	[SerializeField] private WallInteractable _interactable;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == WALL_LAYER) {
			_interactable.TouchWall();
			Destroy(this);
		}
	}

}