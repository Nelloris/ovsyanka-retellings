using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GG.Infrastructure.Utils.Swipe;

public class ScrollSnap : MonoBehaviour
{
    #region Variables
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject scrollbar, imageContent;
    int btnNumber;
    [SerializeField] private SwipeListener swipeListener;
    [SerializeField] private int currentScrollPosition = 0;
    public float scroll_pos = 0;
    private IdleStateManager ism;
    float[] pos;
    private bool runIt = false;
    private float time;
    private Button takeTheBtn;
    #endregion

    #region Main
    public void ResetCurrentScrollPos()
    {
        currentScrollPosition = 0;
        scroll_pos = 0;
    }
    private void Start()
    {
        ism = GameObject.Find("IdleStateManager").GetComponent<IdleStateManager>();
    }
    void FixedUpdate()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);

        if (runIt)
        {
            Scroll(distance, pos, takeTheBtn);
            time += Time.deltaTime;

            if (time > 1f)
            {
                time = 0;
                runIt = false;
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            ism.UpdateIdleState();
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }


        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                imageContent.transform.GetChild(i).localScale = Vector2.Lerp(imageContent.transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f);
                imageContent.transform.GetChild(i).GetComponent<Image>().color = colors[1];
                transform.GetChild(i).GetComponent<Image>().color = colors[1];
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        imageContent.transform.GetChild(j).GetComponent<Image>().color = colors[0];
                        transform.GetChild(j).GetComponent<Image>().color = colors[0];
                        imageContent.transform.GetChild(j).localScale = Vector2.Lerp(imageContent.transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }
    private void Scroll(float distance, float[] pos, Button btn)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[btnNumber], Time.deltaTime);
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
                btnNumber = i;
                takeTheBtn = btn;
                time = 0;
                scroll_pos = (pos[btnNumber]);
                runIt = true;
            }
        }
    }
    private void OnSwipe(string swipe)
    {
        switch (swipe)
        {
            case "Left":
                if (currentScrollPosition < pos.Length - 1)
                {
                    currentScrollPosition += 1;
                    scroll_pos = (pos[currentScrollPosition]);
                }
                break;

            case "Right":
                if (currentScrollPosition > 0)
                {
                    currentScrollPosition -= 1;
                    scroll_pos = (pos[currentScrollPosition]);
                }
                break;
        }
        
    }
    private void OnEnable()
    {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }
    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }
    public void OnElementClicked(int targtPos)
    {
        ism.UpdateIdleState();
        currentScrollPosition = targtPos;
        scroll_pos = (pos[targtPos]);
    }
    #endregion
}