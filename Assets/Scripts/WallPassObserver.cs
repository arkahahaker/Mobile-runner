using System;

public static class WallPassObserver {
	
	public static event Action OnWallPassed;
	
	public static void WallPassed() {
		OnWallPassed?.Invoke();
	}

}