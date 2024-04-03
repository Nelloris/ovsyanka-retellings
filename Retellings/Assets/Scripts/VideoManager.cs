using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    #region Variables

    #region Serializable Fields
    [Header("VideoManager Variables")]
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AnimManager _animationManager;
    [SerializeField] private UnityEvent _onVideoEnded;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private TextMeshProUGUI _currentTimeText;
    #endregion

    #region Private Fields
    private int _counter;
    private float _videoProgressOnPointerDown;
    private IdleStateManager _idleStateManager;
    private Slider _videoSlider;
    private bool _isSlide = false;
    private bool _isEnded { set; get; } = false;
    private bool _isStopped;
    #endregion

    #endregion

    #region Main

    #region Main Methods
    void Start()
    {
        _idleStateManager = GameObject.Find("IdleStateManager").GetComponent<IdleStateManager>();
        _videoSlider = GetComponent<Slider>();
        _image.sprite = _sprites[0];
        _videoSlider.value = 0;
        _videoPlayer.frame = 0;
    }
    void FixedUpdate()
    {
        if (!_isSlide && _videoPlayer.isPlaying)
        {
            _videoSlider.value = (float)_videoPlayer.frame / (float)_videoPlayer.frameCount;
        }
        if (_videoSlider.value >= 0.999 && _isEnded == false && _isSlide == false)
        {
            _isEnded = true;
            _onVideoEnded.Invoke();
        }
        if (_videoSlider.value < 0.999 && _isEnded == true)
        {
            _isEnded = false;
        }
    }
    #endregion

    #region Input Methods
    public void OnPointerDown(PointerEventData args)
    {
        _counter++;
        _image.sprite = _sprites[_counter % 2];
        _idleStateManager.UpdateIdleState();
        _videoPlayer.Pause();
        _isSlide = true;
        _videoProgressOnPointerDown = _videoSlider.value;
    }
    public void OnPointerUp(PointerEventData args)
    {
        _idleStateManager.UpdateIdleState();
        float frame = (float)_videoSlider.value * (float)_videoPlayer.frameCount;
        _videoPlayer.frame = (long)frame;
        if (Mathf.Abs(_videoProgressOnPointerDown - _videoSlider.value) < 0.1f)
        {
            if (_isStopped == false)
            {
                _videoPlayer.Pause();
                _isSlide = true;
                _isStopped = true;
            }
            else
            {
                _isStopped = false;
                _isSlide = false;
                _videoPlayer.Play();
            }
        }
        else
        {
            if (_image.sprite != _sprites[_counter % 1])
            {
                _counter++;
                _image.sprite = _sprites[_counter % 2];
            }
            _isStopped = false;
            _isSlide = false;
            _videoPlayer.Play();
        }
    }
    #endregion

    #region Other Methods
    public void PlayButton()
    {
        _counter++;
        _image.sprite = _sprites[_counter % 2];
        _idleStateManager.UpdateIdleState();
        if (_videoPlayer.isPlaying)
        {
            _videoPlayer.Pause();
            _isSlide = true;
            _isStopped = true;
        }
        else
        {
            _isStopped = false;
            _isSlide = false;
            _videoPlayer.Play();
        }
    }
    public void Reset()
    {
        _videoPlayer.Play();
        _image.sprite = _sprites[0];
        _videoSlider.value = 0;
        _videoPlayer.frame = 0;
    }
    public void UpdateTextUI()
    {
        string minutes = Mathf.Floor((int)_videoPlayer.time / 60).ToString("00");
        string seconds = ((int)_videoPlayer.time % 60).ToString("00");

        _currentTimeText.text = minutes + ":" + seconds;
    }
    #endregion

    #endregion
}
