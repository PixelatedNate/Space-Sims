using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 localPageLocation;
    private Vector3 localPage1Location;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;

    [SerializeField]
    public Pages Page1, Page2;

    public int numPages = 2;
    public int page = 1;

    [Serializable]
    public class Pages
    {
        [SerializeField]
        public Transform page;
        [SerializeField]
        public Image PageIcon;
    }

    public void setFirstPage()
    {
        transform.localPosition = localPage1Location;
        localPageLocation = transform.localPosition;
        page = 1;
        Page1.PageIcon.color = Color.white;
        Page2.PageIcon.color = Color.gray;
    }

    float PageWidth { get { return Page2.page.localPosition.x - Page1.page.localPosition.x; } }

    public void OnDrag(PointerEventData eventData)
    {
        float difference = eventData.pressPosition.x - eventData.position.x;
        float xpos = localPageLocation.x - difference;
        if (page == 2)
        {
            xpos = Mathf.Clamp(xpos, localPageLocation.x - PageWidth, Mathf.Infinity);
        }
        else if (page == 1)
        { 
            xpos = Mathf.Clamp(xpos,Mathf.NegativeInfinity,localPageLocation.x + PageWidth);
        }
        transform.localPosition = new Vector3(xpos, localPageLocation.y, localPageLocation.z);
        TouchControls.EnableCameramovemntAndSelection(false);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float percentage = (eventData.pressPosition.x - eventData.position.x) / PageWidth;
        Debug.Log(percentage);
        TouchControls.EnableCameramovemntAndSelection(true);
        if(Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = localPageLocation;
            if(percentage < 0 && page != 1)
            {
                page--;
                newLocation += new Vector3(PageWidth, 0, 0);
                Page1.PageIcon.color = Color.white;
                Page2.PageIcon.color = Color.gray;
            }
            else if(percentage > 0 && page != 2)
            {
                page++;
                newLocation -= new Vector3(PageWidth, 0, 0);
                Page1.PageIcon.color = Color.gray;
                Page2.PageIcon.color = Color.white;
            }
            StartCoroutine(SmoothMove(transform.localPosition, newLocation, easing));
            localPageLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.localPosition, localPageLocation, easing));
        }
    }


    IEnumerator SmoothMove(Vector3 startPosition, Vector3 endPos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.localPosition = Vector3.Lerp(startPosition, endPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        localPage1Location = transform.localPosition;
        localPageLocation = transform.localPosition;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
