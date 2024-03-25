using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class VideoManager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    #region Variables
    [Header("VideoManager Variables")]
    [SerializeField] private VideoPlayer vp;
    [SerializeField] private AudioSource _as;
    [SerializeField] private AnimManager am;
    [SerializeField] private UnityEvent _onEnd;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] sprite;  

    private int counter;
    private float _videoProgressOnPointerDown;
    private IdleStateManager ism;
    Slider VideoSlider;
    bool isSlide = false;
    bool isEnded { set; get; } = false;
    bool isStopped;
    #endregion

    #region Main
    void Start()
    {
        ism = GameObject.Find("IdleStateManager").GetComponent<IdleStateManager>();
        VideoSlider = GetComponent<Slider>();
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

    public void OnPointerDown(PointerEventData args)
    {
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
            counter++;
            _image.sprite = sprite[counter % 2];
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
            isSlide = false;
            vp.Play();
        }
    }
    #endregion
}
