using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IdleStateManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private int _idleTime;
    [SerializeField] private Text _debugText;

    private int _timeRemain;
    private AnimManager _animationManager;
    #endregion

    #region Main
    void Start()
    {
        _animationManager = GameObject.Find("AnimManager").GetComponent<AnimManager>();
        UpdateIdleState();
        StartCoroutine(IdleStateClock());
    }
    public void UpdateIdleState()
    {
        _timeRemain = _idleTime;
    }
    IEnumerator IdleStateClock()
    {
        yield return new WaitForSeconds(1);
        _timeRemain -= 1;
        _debugText.text = _timeRemain.ToString();
        if (_timeRemain == 0)
        {
            _animationManager.ToThePreview();
        }
        StartCoroutine(IdleStateClock());
    }
    #endregion
}
