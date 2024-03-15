using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IdleStateManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private int idleTime;
    [SerializeField] private Text text;

    private int timeRemain;
    private AnimManager am;
    #endregion

    #region Main
    void Start()
    {
        am = GameObject.Find("AnimManager").GetComponent<AnimManager>();
        UpdateIdleState();
        StartCoroutine(IdleStateClock());
    }
    public void UpdateIdleState()
    {
        timeRemain = idleTime;
    }
    IEnumerator IdleStateClock()
    {
        yield return new WaitForSeconds(1);
        timeRemain -= 1;
        text.text = timeRemain.ToString();
        if (timeRemain == 0)
        {
            am.ToThePreview();
        }
        StartCoroutine(IdleStateClock());
    }
    #endregion
}
