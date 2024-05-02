using UnityEngine;
using System.Collections;

public class NineMay : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject videoPanel;

    private Animator[] infoPanelAnimators;
    private Animator[] videoPanelAnimators;

    private Animator mainLabelAnimator;
    private Animator yearsTextAnimator;
    private Animator infoButtonAnimator;

    private Animator videoSliderAnimatorVideoPanel;
    private Animator homeButtonAnimatorVideoPanel;
    private Animator audioButtonAnimatorVideoPanel;
    private Animator currentTimeTextAnimatorVideoPanel;
    private Animator videoAnimator;

    private GameObject eventSystem;
    private int CurrentScene = 0;
    private IdleStateManager ism;
    #endregion

    private void Start()
    {
        eventSystem = GameObject.Find("EventSystem");
        ism = GameObject.Find("IdleStateManager").GetComponent<IdleStateManager>();

        infoPanel.SetActive(true);
        mainLabelAnimator = GameObject.Find("mainLabel").GetComponent<Animator>();
        yearsTextAnimator = GameObject.Find("yearsText").GetComponent<Animator>();
        infoButtonAnimator = GameObject.Find("infoButton").GetComponent<Animator>();

        videoPanel.SetActive(true);
        videoSliderAnimatorVideoPanel = GameObject.Find("videoSliderVideoPanel").GetComponent<Animator>();
        homeButtonAnimatorVideoPanel = GameObject.Find("homeButtonVideoPanel").GetComponent<Animator>();
        audioButtonAnimatorVideoPanel = GameObject.Find("audioButtonVideoPanel").GetComponent<Animator>();
        currentTimeTextAnimatorVideoPanel = GameObject.Find("currentTimeTextVideoPanel").GetComponent<Animator>();
        videoAnimator = GameObject.Find("VideoPanelPlayer").GetComponent<Animator>();
        videoPanel.SetActive(false);


        infoPanelAnimators = new Animator[] {
            mainLabelAnimator,
            yearsTextAnimator,
            infoButtonAnimator};

        videoPanelAnimators = new Animator[] {
            videoSliderAnimatorVideoPanel,
            homeButtonAnimatorVideoPanel,
            audioButtonAnimatorVideoPanel,
            currentTimeTextAnimatorVideoPanel,
            videoAnimator };
        foreach (Animator an in infoPanelAnimators)
        {
            an.SetBool("Is", true);
        }
    }

    public void ToTheInfo()
    {
        ism.UpdateIdleState();
        if (CurrentScene == 2)
        {
            //Hiding previous UI elements and panel
            foreach (Animator an in videoPanelAnimators)
            {
                AnimationStateSwitch(an);
            }
            StartCoroutine(PanelSwitchDelayed(videoPanel, 0.5f));

            //Showing next UI elements and panel
            infoPanel.SetActive(true);
            foreach (Animator an in infoPanelAnimators)
            {
                AnimationStateSwitch(an);
            }
        }
        CurrentScene = 1;
    }
    public void ToTheVideo()
    {
        ism.UpdateIdleState();

        //Hiding previous UI elements and panel
        foreach (Animator an in infoPanelAnimators)
        {
            AnimationStateSwitch(an);
        }
        StartCoroutine(PanelSwitchDelayed(infoPanel, 0.5f));

        //Showing next UI elements and panel
        videoPanel.SetActive(true);
        foreach (Animator an in videoPanelAnimators)
        {
            AnimationStateSwitch(an);
        }
        CurrentScene = 2;

    }

    #region Other
    public void AnimationStateSwitch(Animator an)
    {
        if (an.GetBool("Is") == false) { an.SetBool("Is", true); } else { an.SetBool("Is", false); }
    }
    IEnumerator PanelSwitchDelayed(GameObject go, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        if (go.activeSelf == true) { go.SetActive(false); } else { go.SetActive(true); }
    }
    public void EventSystemOnInvoke()
    {
        Invoke("EventSystemOn", 0.5f);
    }
    void EventSystemOn()
    {
        eventSystem.SetActive(true);
    }
    #endregion
}
