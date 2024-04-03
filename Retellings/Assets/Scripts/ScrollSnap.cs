using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GG.Infrastructure.Utils.Swipe;

public class ScrollSnap : MonoBehaviour
{
    #region Variables
    [SerializeField] private Color[] _colors;
    [SerializeField] private GameObject _scrollbar, _imageContent;
    [SerializeField] private SwipeListener _swipeListener;
    [SerializeField] private int _currentScrollPosition = 0;

    private int _buttonNumber;
    private float _scrollPos = 0;
    private IdleStateManager _idleStateManager;
    private float[] _panelsPositions;
    private bool _runIt = false;
    private float _time;
    private Button _takeTheButton;
    #endregion

    #region Main
    public void ResetCurrentScrollPos()
    {
        _currentScrollPosition = 0;
        _scrollPos = 0;
    }
    private void Start()
    {
        _idleStateManager = GameObject.Find("IdleStateManager").GetComponent<IdleStateManager>();
    }
    void FixedUpdate()
    {
        _panelsPositions = new float[transform.childCount];
        float distance = 1f / (_panelsPositions.Length - 1f);

        if (_runIt)
        {
            Scroll(distance, _panelsPositions, _takeTheButton);
            _time += Time.deltaTime;

            if (_time > 1f)
            {
                _time = 0;
                _runIt = false;
            }
        }

        for (int i = 0; i < _panelsPositions.Length; i++)
        {
            _panelsPositions[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            _idleStateManager.UpdateIdleState();
        }
        else
        {
            for (int i = 0; i < _panelsPositions.Length; i++)
            {
                if (_scrollPos < _panelsPositions[i] + (distance / 2) && _scrollPos > _panelsPositions[i] - (distance / 2))
                {
                    _scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(_scrollbar.GetComponent<Scrollbar>().value, _panelsPositions[i], 0.1f);
                }
            }
        }


        for (int i = 0; i < _panelsPositions.Length; i++)
        {
            if (_scrollPos < _panelsPositions[i] + (distance / 2) && _scrollPos > _panelsPositions[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                _imageContent.transform.GetChild(i).localScale = Vector2.Lerp(_imageContent.transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f);
                _imageContent.transform.GetChild(i).GetComponent<Image>().color = _colors[1];
                transform.GetChild(i).GetComponent<Image>().color = _colors[1];
                for (int j = 0; j < _panelsPositions.Length; j++)
                {
                    if (j != i)
                    {
                        _imageContent.transform.GetChild(j).GetComponent<Image>().color = _colors[0];
                        transform.GetChild(j).GetComponent<Image>().color = _colors[0];
                        _imageContent.transform.GetChild(j).localScale = Vector2.Lerp(_imageContent.transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }
    private void Scroll(float distance, float[] _panelsPositions, Button btn)
    {
        for (int i = 0; i < _panelsPositions.Length; i++)
        {
            if (_scrollPos < _panelsPositions[i] + (distance / 2) && _scrollPos > _panelsPositions[i] - (distance / 2))
            {
                _scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(_scrollbar.GetComponent<Scrollbar>().value, _panelsPositions[_buttonNumber], Time.deltaTime);
            }
        }

        for (int i = 0; i < btn.transform.parent.transform.childCount; i++)
        {
            btn.transform.name = ".";
        }

    }
    public void WhichBtnClicked(Button btn)
    {
        btn.transform.name = "clicked";
        for (int i = 0; i < btn.transform.parent.transform.childCount; i++)
        {
            if (btn.transform.parent.transform.GetChild(i).transform.name == "clicked")
            {
                _buttonNumber = i;
                _takeTheButton = btn;
                _time = 0;
                _scrollPos = (_panelsPositions[_buttonNumber]);
                _runIt = true;
            }
        }
    }
    private void OnSwipe(string swipe)
    {
        switch (swipe)
        {
            case "Left":
                if (_currentScrollPosition < _panelsPositions.Length - 1)
                {
                    _currentScrollPosition += 1;
                    _scrollPos = (_panelsPositions[_currentScrollPosition]);
                }
                break;

            case "Right":
                if (_currentScrollPosition > 0)
                {
                    _currentScrollPosition -= 1;
                    _scrollPos = (_panelsPositions[_currentScrollPosition]);
                }
                break;
        }
        
    }
    private void OnEnable()
    {
        _swipeListener.OnSwipe.AddListener(OnSwipe);
    }
    private void OnDisable()
    {
        _swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }
    public void OnElementClicked(int targtPos)
    {
        _idleStateManager.UpdateIdleState();
        _currentScrollPosition = targtPos;
        _scrollPos = (_panelsPositions[targtPos]);
    }
    #endregion
}