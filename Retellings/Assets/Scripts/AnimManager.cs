using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AnimManager : MonoBehaviour
{
    #region Variables
    [Header("previewPanel panel animators")]
    [SerializeField] private GameObject previewPanel;
    [Space][SerializeField] private Animator bookIconAnimator;
    [SerializeField] private Animator previewButtonAnimator;
    
    [Header("infoPanel panel animators")]
    [SerializeField] private GameObject infoPanel;
    [Space][SerializeField] private Animator mainLabelAnimator;
    [SerializeField] private Animator yearsAnimator;
    [SerializeField] private Animator textAnimator;
    [SerializeField] private Animator infoButtonAnimator;
    [SerializeField] private Animator factsIconAnimator;
    [SerializeField] private Animator factsTextAnimator;

    [Header("videoPanel Panel animators")]
    [SerializeField] private GameObject videoPanel;
    [Space][SerializeField] private Animator videoSliderAnimatorVideoPanel;
    [SerializeField] private Animator audioSliderAnimatorVideoPanel;
    [SerializeField] private Animator homeButtonAnimatorVideoPanel;
    [SerializeField] private Animator audioButtonAnimatorVideoPanel;

    [Header("Panel panel animators")]
    [SerializeField] private GameObject factsPanel;
    [Space][SerializeField] private Animator factsButtons;
    [SerializeField] private Animator[] factsIcons;
    [Space] [SerializeField] private Animator audioSliderAnimatorFactsPanel;
    [SerializeField] private Animator videoSliderAnimatorFactsPanel;
    [SerializeField] private Animator homeButtonAnimatorFactsPanel;
    [SerializeField] private Animator audioButtonAnimatorFactsPanel;
    [SerializeField] private GameObject videoFactsButton;


    private int CurrentScene = 0;
    #endregion

    #region Main
    #region PanelSwitching
    public void ToTheInfo()
    {
        CurrentScene = 1;
        
        //Hiding previous UI elements and panel
        StartCoroutine(AnimationStateSwitch(bookIconAnimator));
        StartCoroutine(AnimationStateSwitch (previewButtonAnimator));

        //Showing next UI elements and panel
        infoPanel.SetActive(true);
        StartCoroutine(AnimationStateSwitch(mainLabelAnimator));
        StartCoroutine(AnimationStateSwitch(yearsAnimator));
        StartCoroutine(AnimationStateSwitch(textAnimator));
        StartCoroutine(AnimationStateSwitch(infoButtonAnimator));
        StartCoroutine(AnimationStateSwitch(factsIconAnimator));
        StartCoroutine(AnimationStateSwitch(factsTextAnimator));
    }
    public void ToTheVideo()
    {
        CurrentScene = 2;
        
        //Hiding previous UI elements and panel
        StartCoroutine(AnimationStateSwitch(mainLabelAnimator));
        StartCoroutine(AnimationStateSwitch(yearsAnimator));
        StartCoroutine(AnimationStateSwitch(textAnimator));
        StartCoroutine(AnimationStateSwitch(infoButtonAnimator));
        StartCoroutine(AnimationStateSwitch(factsIconAnimator));
        StartCoroutine(AnimationStateSwitch(factsTextAnimator));
        StartCoroutine(PanelSwitchDelayed(infoPanel, 0.5f));

        //Showing next UI elements and panel
        videoPanel.SetActive(true);
        StartCoroutine(AnimationStateSwitch(homeButtonAnimatorVideoPanel));
        StartCoroutine(AnimationStateSwitch(videoSliderAnimatorVideoPanel));
        StartCoroutine(AnimationStateSwitch(audioButtonAnimatorVideoPanel));
        
    }
    public void ToTheFacts()
    {
        if (CurrentScene == 1)
        {
            
            //Hiding previous UI elements and panel
            StartCoroutine(AnimationStateSwitch(mainLabelAnimator));
            StartCoroutine(AnimationStateSwitch(yearsAnimator));
            StartCoroutine(AnimationStateSwitch(textAnimator));
            StartCoroutine(AnimationStateSwitch(infoButtonAnimator));
            StartCoroutine(AnimationStateSwitch(factsIconAnimator));
            StartCoroutine(AnimationStateSwitch(factsTextAnimator));
            StartCoroutine(PanelSwitchDelayed(infoPanel, 0.5f));

            //Showing next UI elements and panel
            factsPanel.SetActive(true);
            StartCoroutine(AnimationStateSwitch(factsButtons));
            for (int i = 0; i < factsIcons.Length; i++)
            {
                var icon = factsIcons[i];
                StartCoroutine(AnimationStateSwitch(icon));
            }
            StartCoroutine(AnimationStateSwitch(audioButtonAnimatorFactsPanel));
            StartCoroutine(AnimationStateSwitch(homeButtonAnimatorFactsPanel));
        }
        if (CurrentScene == 2)
        {
            //Hiding previous UI elements and panel
            StartCoroutine(AnimationStateSwitch(homeButtonAnimatorVideoPanel));
            StartCoroutine(AnimationStateSwitch(videoSliderAnimatorVideoPanel));
            StartCoroutine(AnimationStateSwitch(audioButtonAnimatorVideoPanel));
            StartCoroutine(PanelSwitchDelayed(videoPanel, 0.2f));

            //Showing next UI elements and panel
            factsPanel.SetActive(true);
            StartCoroutine(AnimationStateSwitch(factsButtons));
            for (int i = 0; i < factsIcons.Length; i++)
            {
                var icon = factsIcons[i];
                StartCoroutine(AnimationStateSwitch(icon));
            }
            StartCoroutine(AnimationStateSwitch(audioButtonAnimatorFactsPanel));
            StartCoroutine(AnimationStateSwitch(homeButtonAnimatorFactsPanel));
        }
        CurrentScene = 3;
    }
    public void ToThePreview()
    {
        if (CurrentScene == 2)
        {
            //Hiding previous UI elements and panel
            StartCoroutine(AnimationStateSwitch(homeButtonAnimatorVideoPanel));
            StartCoroutine(AnimationStateSwitch(audioButtonAnimatorVideoPanel));
            StartCoroutine(PanelSwitchDelayed(videoPanel, 0.5f));

            //Showing next UI elements and panel
            previewPanel.SetActive(true);
            StartCoroutine(AnimationStateSwitch(bookIconAnimator));
            StartCoroutine(AnimationStateSwitch(previewButtonAnimator));
        }
        if (CurrentScene == 3)
        {
            //Hiding previous UI elements and panel
            VideoFactHide();
            StartCoroutine(AnimationStateSwitch(factsButtons));
            for (int i = 0; i < factsIcons.Length; i++)
            {
                var icon = factsIcons[i];
                StartCoroutine(AnimationStateSwitch(icon));
            }
            StartCoroutine(AnimationStateSwitch(audioButtonAnimatorFactsPanel));
            StartCoroutine(AnimationStateSwitch(homeButtonAnimatorFactsPanel));
            StartCoroutine(PanelSwitchDelayed(factsPanel, 0.5f));

            //Showing next UI elements and panel
            previewPanel.SetActive(true);
            StartCoroutine(AnimationStateSwitch(bookIconAnimator));
            StartCoroutine(AnimationStateSwitch(previewButtonAnimator));
        }
        CurrentScene = 0;
    }
    #endregion

    #region VideoFact
    public void VideoFactShow()
    {
        videoFactsButton.SetActive(true);
        StartCoroutine(AnimationStateSwitch(videoSliderAnimatorFactsPanel));
    }
    
    public void VideoFactHide()
    {
        videoFactsButton.SetActive(false);
        StartCoroutine(AnimationStateSwitch(videoSliderAnimatorFactsPanel));
    }

    #endregion

    #region Other
    public void VolumeButton(Animator an)
    {
        StartCoroutine(AnimationStateSwitch(an));
    }

    IEnumerator AnimationStateSwitch(Animator an)
    {
        if (an.GetBool("Is") == false) { an.SetBool("Is", true); } else { an.SetBool("Is", false); }
        yield return null;
    }

    IEnumerator PanelSwitchDelayed(GameObject go, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        if (go.activeSelf == true) { go.SetActive(false); } else { go.SetActive(true); }
    }
    #endregion
    #endregion
}
