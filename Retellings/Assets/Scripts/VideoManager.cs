using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    #region Variables
    [Header("VideoManager Variables")]
    [SerializeField] private VideoPlayer vp;
    [SerializeField] private AudioSource _as;
    [SerializeField] private Slider AudioSlider;
    [SerializeField] private AnimManager am;

    private IdleStateManager ism;
    Slider VideoSlider;
    bool isSlide = false;
    bool isEnded { set; get; } = false;
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
            am.ToTheFacts();
        }
        if (VideoSlider.value < 0.999 && isEnded == true)
        {
            isEnded = false;
        }
    }

    public void PlayButton()
    {
        ism.UpdateIdleState();
        if (vp.isPlaying)
        {
            vp.Pause();
            isSlide = true;
        }
        else
        {
            isSlide = false;
            vp.Play();
        }
    }

    public void OnPointerDown(PointerEventData args)
    {
        ism.UpdateIdleState();
        vp.Pause();
        isSlide = true;
    }

    public void OnPointerUp(PointerEventData args)
    {
        ism.UpdateIdleState();
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
