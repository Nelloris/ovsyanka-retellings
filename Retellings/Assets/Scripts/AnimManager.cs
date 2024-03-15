using System.Collections;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    #region Variables
    [Header("AnimationManager Variables")]
    [SerializeField] private GameObject previewPanel;
    private Animator previewButtonAnimator;
    private Animator bookIconAnimator;

    [SerializeField] private GameObject infoPanel;
    private Animator mainLabelAnimator;
    private Animator yearsTextAnimator;
    private Animator mainTextAnimator;
    private Animator infoButtonAnimator;
    private Animator factsIconsAnimator;
    private Animator factsTextAnimator;

    [SerializeField] private GameObject videoPanel;
    private Animator videoSliderAnimatorVideoPanel;
    private Animator homeButtonAnimatorVideoPanel;
    private Animator audioButtonAnimatorVideoPanel;
    private Animator videoAnimator;

    [SerializeField] private GameObject factsPanel;
    private Animator factsIndicatorsAnimator;
    private Animator[] factsAnimator;
    private Animator videoSliderAnimatorFactsPanel;
    private Animator homeButtonAnimatorFactsPanel;
    private Animator audioButtonAnimatorFactsPanel;
    private Animator videoFactAnimator;
    private Animator videoFactButtonAnimator;
    private Animator closeVideoFactButtonAnimator;

    private Animator[] previewPanelAnimators;
    private Animator[] infoPanelAnimators;
    private Animator[] videoPanelAnimators;
    private Animator[] factsPanelAnimators;
    private int CurrentScene = 0;
    private IdleStateManager ism;
    #endregion

    #region Main
    #region MainMethods
    private void Start()
    {
        ism = GameObject.Find("IdleStateManager").GetComponent<IdleStateManager>();

        previewButtonAnimator = GameObject.Find("previewButton").GetComponent<Animator>();
        bookIconAnimator = GameObject.Find("bookIcon").GetComponent<Animator>();

        infoPanel.SetActive(true);
        mainLabelAnimator = GameObject.Find("mainLabel").GetComponent<Animator>();
        yearsTextAnimator = GameObject.Find("yearsText").GetComponent<Animator>();
        mainTextAnimator = GameObject.Find("mainText").GetComponent<Animator>();
        infoButtonAnimator = GameObject.Find("infoButton").GetComponent<Animator>();
        factsIconsAnimator = GameObject.Find("factsIcons").GetComponent<Animator>();
        factsTextAnimator = GameObject.Find("factsText").GetComponent<Animator>();
        infoPanel.SetActive(false);

        videoPanel.SetActive(true);
        videoSliderAnimatorVideoPanel = GameObject.Find("videoSliderVideoPanel").GetComponent<Animator>();
        homeButtonAnimatorVideoPanel = GameObject.Find("homeButtonVideoPanel").GetComponent<Animator>();
        audioButtonAnimatorVideoPanel = GameObject.Find("audioButtonVideoPanel").GetComponent<Animator>();
        videoAnimator = GameObject.Find("VideoPanelPlayer").GetComponent<Animator>();
        videoPanel.SetActive(false);

        factsPanel.SetActive(true);
        factsIndicatorsAnimator = GameObject.Find("factsIndicators").GetComponent<Animator>();
        factsAnimator = GameObject.Find("Content").GetComponentsInChildren<Animator>();
        videoSliderAnimatorFactsPanel = GameObject.Find("videoSliderFactsPanel").GetComponent<Animator>();
        homeButtonAnimatorFactsPanel = GameObject.Find("homeButtonFactsPanel").GetComponent<Animator>();
        audioButtonAnimatorFactsPanel = GameObject.Find("audioButtonFactsPanel").GetComponent<Animator>();
        closeVideoFactButtonAnimator = GameObject.Find("closeVideoFactButton").GetComponent<Animator>();
        videoFactButtonAnimator = GameObject.Find("videoFactButton").GetComponent<Animator>();
        videoFactAnimator = GameObject.Find("VideoFactPlayer").GetComponent<Animator>();
        videoFactAnimator.gameObject.SetActive(false);
        factsPanel.SetActive(false);

        previewPanelAnimators = new Animator[] { 
            bookIconAnimator, 
            previewButtonAnimator };

        infoPanelAnimators = new Animator[] { 
            mainLabelAnimator, 
            yearsTextAnimator, 
            mainTextAnimator, 
            infoButtonAnimator, 
            factsIconsAnimator, 
            factsTextAnimator };

        videoPanelAnimators = new Animator[] { 
            videoSliderAnimatorVideoPanel, 
            homeButtonAnimatorVideoPanel, 
            audioButtonAnimatorVideoPanel, 
            videoAnimator };

        factsPanelAnimators = new Animator[] { 
            factsIndicatorsAnimator, 
            homeButtonAnimatorFactsPanel, 
            audioButtonAnimatorFactsPanel,
            videoFactButtonAnimator};
    }
    #endregion

    #region PanelSwitching
    public void ToTheInfo()
    {
        ism.UpdateIdleState();
        CurrentScene = 1;
        
        //Hiding previous UI elements and panel
        foreach (Animator an in previewPanelAnimators)
        {
            AnimationStateSwitch(an);
        }

        //Showing next UI elements and panel
        infoPanel.SetActive(true);
        foreach (Animator an in infoPanelAnimators)
        {
            AnimationStateSwitch(an);
        }
    }
    public void ToTheVideo()
    {
        ism.UpdateIdleState();
        CurrentScene = 2;

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

    }
    public void ToTheFacts()
    {
        ism.UpdateIdleState();
        if (CurrentScene == 1)
        {
            //Hiding previous UI elements and panel
            foreach (Animator an in infoPanelAnimators)
            {
                AnimationStateSwitch(an);
            }
            StartCoroutine(PanelSwitchDelayed(infoPanel, 0.5f));

            //Showing next UI elements and panel
            factsPanel.SetActive(true);
            AnimationStateSwitch(videoFactButtonAnimator);
            foreach (Animator an in factsPanelAnimators)
            {
                AnimationStateSwitch(an);
            }
            for (int i = 0; i < factsAnimator.Length; i++)
            {
                var icon = factsAnimator[i];
                AnimationStateSwitch(icon);
            }
        }
        if (CurrentScene == 2)
        {
            foreach (Animator an in videoPanelAnimators)
            {
                AnimationStateSwitch(an);
            }
            StartCoroutine(PanelSwitchDelayed(videoPanel, 0.2f));

            //Showing next UI elements and panel
            factsPanel.SetActive(true);
            AnimationStateSwitch(videoFactButtonAnimator);
            foreach (Animator an in factsPanelAnimators)
            {
                AnimationStateSwitch(an);
            }
            for (int i = 0; i < factsAnimator.Length; i++)
            {
                var icon = factsAnimator[i];
                AnimationStateSwitch(icon);
            }
        }
        CurrentScene = 3;
    }
    public void ToThePreview()
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
            previewPanel.SetActive(true);
            foreach (Animator an in previewPanelAnimators)
            {
                AnimationStateSwitch(an);
            }
        }
        if (CurrentScene == 3)
        {
            //Hiding previous UI elements and panel
            foreach (Animator an in factsPanelAnimators)
            {
                AnimationStateSwitch(an);
            }
            AnimationStateSwitch(videoFactButtonAnimator);
            for (int i = 0; i < factsAnimator.Length; i++)
            {
                var icon = factsAnimator[i];
                AnimationStateSwitch(icon);
            }
            StartCoroutine(PanelSwitchDelayed(factsPanel, 0.5f));

            //Showing next UI elements and panel
            previewPanel.SetActive(true);
            foreach (Animator an in previewPanelAnimators)
            {
                AnimationStateSwitch(an);
            }
        }
        CurrentScene = 0;
    }
    #endregion

    #region VideoFact
    public void VideoFactShow(GameObject videoFactVideo)
    {
        ism.UpdateIdleState();
        videoFactVideo.SetActive(true);
        AnimationStateSwitch(videoFactAnimator);
        AnimationStateSwitch(videoSliderAnimatorFactsPanel);
        AnimationStateSwitch(closeVideoFactButtonAnimator);
    }
    
    public void VideoFactHide(GameObject videoFactVideo)
    {
        if (videoFactAnimator.GetBool("Is") == true)
        {
            ism.UpdateIdleState();
            StartCoroutine(PanelSwitchDelayed(videoFactVideo, 0.3f));
            AnimationStateSwitch(videoFactAnimator);
            AnimationStateSwitch(videoSliderAnimatorFactsPanel);
            AnimationStateSwitch(closeVideoFactButtonAnimator);
        }
    }

    #endregion

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
    #endregion
    #endregion
}
