using UnityEngine;

public class Wall : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			WallPassObserver.WallPassed();
		}
	}

}