using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public GameObject[] uiPopup;
    public GameObject[] slideTab;
    public Button[] slideBtn;
    public bool leftSliding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TopLeftButton()
    {
        uiPopup[0].SetActive(true);
    }
    public void TopRightButton()
    {
        uiPopup[1].SetActive(true);
    }
    public void MiddleLeftButton()
    {
        uiPopup[2].SetActive(true);
    }
    public void MiddleRightButton()
    {
        uiPopup[3].SetActive(true);
    }
    public void CloseTopLeft()
    {
        uiPopup[0].SetActive(false);
    }
    public void CloseTopRight()
    {
        uiPopup[1].SetActive(false);
    }
    public void CloseCenterLeft()
    {
        uiPopup[2].SetActive(false);
    }
    public void CloseCenterRight()
    {
        uiPopup[3].SetActive(false);
    }
    public void LeftTabSlideOut()
    {
        if (!leftSliding)
        {
            slideBtn[0].interactable = false;
            StartCoroutine(LeftTabSlidingOut());
        }
        else if (leftSliding)
        {
            slideBtn[0].interactable = false;
            StartCoroutine(LeftTabSlidingIn());
        }
    }
    public IEnumerator LeftTabSlidingOut()
    {
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x + 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        leftSliding = true;
        slideBtn[0].interactable = true;
    }
    public IEnumerator LeftTabSlidingIn()
    {
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        yield return new WaitForSeconds(.02f);
        slideTab[0].transform.position = new Vector3((slideTab[0].transform.position.x - 24.2f), slideTab[0].transform.position.y, slideTab[0].transform.position.z);
        leftSliding = false;
        slideBtn[0].interactable = true;
    }
}
