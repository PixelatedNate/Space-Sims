using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public GameObject[] uiPopup;
    public GameObject[] slideTab;
    public Button[] slideBtn;
    public Animator leftTabAnim;
    // Start is called before the first frame update
    void Start()
    {
        leftTabAnim = GameObject.Find("Left_Slide_Tab").GetComponent<Animator>();
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
        leftTabAnim.SetBool("Open", true);
    }
    public void LeftTabSlideIn()
    {
        leftTabAnim.SetBool("Open", false);
    }
}
