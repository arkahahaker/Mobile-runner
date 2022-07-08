using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour {

    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeForce;

    private void OnEnable() {
        WallPassObserver.OnWallPassed += ShakeCamera;
    }

    private void ShakeCamera() {
        StartCoroutine(ShakeCameraRoutine());
    }

    private IEnumerator ShakeCameraRoutine() {
        float currentTime = 0f;
        Vector3 startPosition = transform.position;
        while (currentTime < _shakeDuration) {
            float shakeX = Random.Range(-_shakeForce, +_shakeForce);
            float shakeY = Random.Range(-_shakeForce, +_shakeForce);
            transform.Translate(shakeX, shakeY, 0);
            yield return null;
            currentTime += Time.deltaTime;
        }
        transform.position = new Vector3(startPosition.x, startPosition.y, transform.position.z);
    }

    private void OnDisable() {
        WallPassObserver.OnWallPassed -= ShakeCamera;
    }
}
