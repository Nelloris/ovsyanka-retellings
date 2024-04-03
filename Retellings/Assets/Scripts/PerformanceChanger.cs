using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceChanger : MonoBehaviour
{
    #region Variables
    [SerializeField] private int _targetFrameRate = 60;
    [SerializeField] private int _vSyncCount = 0;
    [SerializeField] private Text _textDebugFps;

    private Dictionary<int, string> _cachedNumberStrings = new();
    private int[] _frameRateSamples;
    private int _cacheNumbersAmount = 300;
    private int _averageFromAmount = 30;
    private int _averageCounter = 0;
    private int _currentAveraged;
    #endregion

    #region Main
    
    void Awake()
    {
        {
            for (int i = 0; i < _cacheNumbersAmount; i++)
            {
                _cachedNumberStrings[i] = i.ToString();
            }
            _frameRateSamples = new int[_averageFromAmount];
        }

        QualitySettings.SetQualityLevel(0);
        QualitySettings.vSyncCount = _vSyncCount;
        Application.targetFrameRate = _targetFrameRate;
    }

    void Update()
    {
        {
            var currentFrame = (int)Mathf.Round(1f / Time.smoothDeltaTime);
            _frameRateSamples[_averageCounter] = currentFrame;
        }

        {
            var average = 0f;

            foreach (var frameRate in _frameRateSamples)
            {
                average += frameRate;
            }

            _currentAveraged = (int)Mathf.Round(average / _averageFromAmount);
            _averageCounter = (_averageCounter + 1) % _averageFromAmount;
        }

        {
            _textDebugFps.text = _currentAveraged switch
            {
                var x when x >= 0 && x < _cacheNumbersAmount => _cachedNumberStrings[x],
                var x when x >= _cacheNumbersAmount => $"> {_cacheNumbersAmount}",
                var x when x < 0 => "< 0",
                _ => "?"
            };
        }
    }

    #endregion
}
