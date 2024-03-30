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
    [SerializeField] private VideoPlayer vp;
    [SerializeField] private AudioSource _as;
    [SerializeField] private AnimManager am;
    [SerializeField] private UnityEvent _onEnd;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private TextMeshProUGUI _currentTimeText;
    #endregion

    #region Private Fields
    private int counter;
    private float _videoProgressOnPointerDown;
    private IdleStateManager ism;
    private Slider VideoSlider;
    private bool isSlide = false;
    private bool isEnded { set; get; } = false;
    private bool isStopped;
    #endregion

    #endregion

    #region Main

    #region Main Methods
    void Start()
    {
        ism = GameObject.Find("IdleStateManager").GetComponent<IdleStateManager>();
        VideoSlider = GetComponent<Slider>();
        _image.sprite = sprite[0];
        VideoSlider.value = 0;
        vp.frame = 0;
    }
    void FixedUpdate()
    {
        if (!isSlide && vp.isPlaying)
        {
            VideoSlider.value = (float)vp.frame / (float)vp.frameCount;
        }
        if (VideoSlider.value >= 0.999 && isEnded == false && isSlide == false)
        {
            isEnded = true;
            _onEnd.Invoke();
        }
        if (VideoSlider.value < 0.999 && isEnded == true)
        {
            isEnded = false;
        }
    }
    #endregion

    #region Input Methods
    public void OnPointerDown(PointerEventData args)
    {
        counter++;
        _image.sprite = sprite[counter % 2];
        ism.UpdateIdleState();
        vp.Pause();
        isSlide = true;
        _videoProgressOnPointerDown = VideoSlider.value;
    }
    public void OnPointerUp(PointerEventData args)
    {
        ism.UpdateIdleState();
        float frame = (float)VideoSlider.value * (float)vp.frameCount;
        vp.frame = (long)frame;
        if (Mathf.Abs(_videoProgressOnPointerDown - VideoSlider.value) < 0.1f)
        {
            if (isStopped == false)
            {
                vp.Pause();
                isSlide = true;
                isStopped = true;
            }
            else
            {
                isStopped = false;
                isSlide = false;
                vp.Play();
            }
        }
        else
        {
            if (_image.sprite != sprite[counter % 1])
            {
                counter++;
                _image.sprite = sprite[counter % 2];
            }
            isStopped = false;
            isSlide = false;
            vp.Play();
        }
    }
    #endregion

    #region Other Methods
    public void PlayButton()
    {
        counter++;
        _image.sprite = sprite[counter % 2];
        ism.UpdateIdleState();
        if (vp.isPlaying)
        {
            vp.Pause();
            isSlide = true;
            isStopped = true;
        }
        else
        {
            isStopped = false;
            isSlide = false;
            vp.Play();
        }
    }
    public void Reset()
    {
        vp.Play();
        _image.sprite = sprite[0];
        VideoSlider.value = 0;
        vp.frame = 0;
    }
    public void UpdateTextUI()
    {
        string minutes = Mathf.Floor((int)vp.time / 60).ToString("00");
        string seconds = ((int)vp.time % 60).ToString("00");

        _currentTimeText.text = minutes + ":" + seconds;
    }
    #endregion

    #endregion
}
