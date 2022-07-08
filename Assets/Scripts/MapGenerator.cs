using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour {

	[Header("Put presets here")]
	[SerializeField] private List<GameObject> _readyToUsePresets;

	[Space(10)]
	
	[Header("Generating settings")]
	[SerializeField] private int _startGenerateCount;
	[SerializeField] private float _presetLength;
	[SerializeField] private float _deactivatePresetTime;
	
	[Space(10)]

	[SerializeField] private float _newPresetsY;
	[SerializeField] private float _newPresetsFlyDuration;

	private LinkedList<GameObject> _currentlyUsingPresets;
	private float _distanceToBuild;

	private void Start() {
		_distanceToBuild = 0;
		_currentlyUsingPresets = new LinkedList<GameObject>();
		GeneratePresets();

		GameObject preset1 = _readyToUsePresets[0];
		_readyToUsePresets.Remove(preset1);
		_currentlyUsingPresets.AddFirst(preset1);
		
		BuildStartPresets();
	}

	private void OnEnable() {
		WallPassObserver.OnWallPassed += LastPresetUsed;
	}

	private void GeneratePresets() {
		_readyToUsePresets = _readyToUsePresets.Select(preset => Instantiate(preset)).ToList();
		foreach (var preset in _readyToUsePresets) preset.SetActive(false);
	}

	private void BuildStartPresets() {
		for (int i = 0; i < _startGenerateCount; i++) BuildPreset();
	}

	private GameObject ChooseRandomPreset() {
		return _readyToUsePresets[Random.Range(0, _readyToUsePresets.Count)];
	}
	
	private Transform BuildPreset() {
		var preset = ChooseRandomPreset();
		_readyToUsePresets.Remove(preset);
		_currentlyUsingPresets.AddLast(preset);
		preset.SetActive(true);
		var presetT = preset.transform;
		RefreshPreset(presetT);
		presetT.position = new Vector3(0, 0, _distanceToBuild);
		_distanceToBuild += _presetLength;
		return presetT;
	}

	private void BuildPresetWithAnimation() {
		Transform presetT = BuildPreset();
		presetT.Translate(0, -_newPresetsY, 0);
		presetT.DOMoveY(0, _newPresetsFlyDuration);
	}

	private void RefreshPreset(Transform t) {
		for (int i = 0; i < t.childCount; i++)
			t.GetChild(i).gameObject.SetActive(true);
	}

	private void LastPresetUsed() {
		BuildPresetWithAnimation();
		Invoke(nameof(DeactivatePreset), _deactivatePresetTime);
	}

	private void DeactivatePreset() {
		GameObject lastPreset = _currentlyUsingPresets.First();
		_currentlyUsingPresets.RemoveFirst();
		_readyToUsePresets.Add(lastPreset);
		lastPreset.SetActive(false);
	}

	private void OnDisable() {
		WallPassObserver.OnWallPassed -= LastPresetUsed;
	}


}