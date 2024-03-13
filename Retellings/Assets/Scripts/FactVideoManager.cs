using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class FactVideoManager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    #region Variables
    [Header("VideoManager Variables")]
    [SerializeField] private VideoPlayer vp;
    [SerializeField] private AudioSource _as;
    [SerializeField] private Slider AudioSlider;
    [SerializeField] private GameObject videoGameObject;
    [SerializeField] private UnityEvent onFactVideoEnded;

    Slider VideoSlider;
    bool isSlide = false;
    bool isEnded { set; get; } = false;
    #endregion

    #region Main
    void Start()
    {
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
            onFactVideoEnded.Invoke();
        }
        if (VideoSlider.value < 0.999 && isEnded == true)
        {
            isEnded = false;
        }
    }

    public void OnPointerDown(PointerEventData args)
    {
        vp.Pause();
        isSlide = true;
    }

    public void OnPointerUp(PointerEventData args)
    {
        float frame = (float)VideoSlider.value * (float)vp.frameCount;
        vp.frame = (long)frame;
        isSlide = false;
        vp.Play();
    }

    public void Volume()
    {
        _as.volume = AudioSlider.value;
    }
    #endregion
}
