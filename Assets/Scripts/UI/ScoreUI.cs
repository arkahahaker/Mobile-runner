using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour {
    
    [SerializeField] private Transform _player;
    private TMP_Text _text;

    private void Start() {
        _text = GetComponent<TMP_Text>();
    }

    private void Update() {
        var distanceBeaten = (int)_player.transform.position.z;
        _text.text = distanceBeaten.ToString();
    }
    
}