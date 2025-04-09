using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//using NUnit.Framework;

public class ShowFPS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _meanFpsText;
    [SerializeField] private TextMeshProUGUI _minFpsText;
    [SerializeField] private TextMeshProUGUI _maxFpsText;
    [SerializeField] private TextMeshProUGUI _averageFpsText;
    [SerializeField] private TextMeshProUGUI _medianFpsText;
    [SerializeField] private TextMeshProUGUI _diffFpsText;

    private bool _startTest = false;
    
    private int _fpsCount;
    private float _poolingTime = 1f;
    private float _time;
    private float _fps;
    private List<float> _fpsArray;

    private int _stopFps = 0;
    private int _diffMinMaxFps;

    private int _lengthArray;
    private int _indexArray;


    private void Start()
    {
        _fpsArray = new List<float>();
    }

    private void Update()
    {
        if (_stopFps >= 30 || !_startTest) return;

        _time += Time.deltaTime;
        _fpsCount++;
        if (_time >= _poolingTime)
        {
            _stopFps++;
            UpdateFps();
            UpdateMinFps();
            UpdateMaxFps();
            UpdateAverageFps();
            UpdateMedianFps();
            UpdateDiffMinMaxFps();

            _time -= _poolingTime;
            _fpsCount = 0;
        }
    }

    public void StartOnButtonSpawn()
    {
        _startTest = true;
    }

    private void UpdateDiffMinMaxFps()
    {
        var diff = Mathf.RoundToInt(_fpsArray.Max()) - Mathf.RoundToInt(_fpsArray.Min());
        if (_diffMinMaxFps < diff)
        {
            _diffMinMaxFps = diff;
            _diffFpsText.text = "DIFF: " + _diffMinMaxFps;
        }
    }

    private void UpdateMedianFps()
    {
        var sortedPNumbers = _fpsArray.OrderBy(r => r).ToArray();
        var mid = sortedPNumbers.Length / 2;
        var median = sortedPNumbers.Length == 0
            ? 0
            : sortedPNumbers.Length % 2 != 0
                ? sortedPNumbers[mid]
                : (sortedPNumbers[mid] + sortedPNumbers[mid - 1]) / 2;
        _medianFpsText.text = "MED: " + Mathf.RoundToInt(median);
    }

    private void UpdateFps()
    {
        _fps = _fpsCount / _time;
        _fpsArray.Add(_fps);

        if (_fpsArray.Count > 10 && _stopFps <= 30)
        {
            _fpsArray.RemoveAt(0);
        }

        _meanFpsText.text = "FPS: " + Mathf.RoundToInt(_fps);
    }

    private void UpdateMinFps()
    {
        _minFpsText.text = "MIN: " + Mathf.RoundToInt(_fpsArray.Min());
    }

    private void UpdateMaxFps()
    {
        _maxFpsText.text = "MAX: " + Mathf.RoundToInt(_fpsArray.Max());
    }

    private void UpdateAverageFps()
    {
        _averageFpsText.text = "AVR: " + Mathf.RoundToInt(_fpsArray.Average());
    }
}