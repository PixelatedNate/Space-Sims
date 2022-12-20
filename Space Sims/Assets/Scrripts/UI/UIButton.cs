using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public GameObject[] uiPopup;
    public GameObject[] slideTab;
    public Button[] slideBtn;
    private Animator leftTabAnim { get; set; }
    private Animator rightTabAnim { get; set; }
    private Animator alertTabAnim { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        leftTabAnim = GameObject.Find("Left_Slide_Tab").GetComponent<Animator>();
        rightTabAnim = GameObject.Find("Right_Slide_Tab").GetComponent<Animator>();
        alertTabAnim = GameObject.Find("Alert_Slide_Tab").GetComponent<Animator>();
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
        SoundManager.Instance.PlaySound(SoundManager.Sound.PanalOpen);
        leftTabAnim.SetBool("Open", true);
    }
    public void LeftTabSlideIn()
    {
        if (leftTabAnim.GetBool("Open"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.PanalOpen);
            leftTabAnim.SetBool("Open", false);
        }
    }
    public void RightTabSlideOut()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.PanalOpen);
        rightTabAnim.SetBool("Open", true);
    }
    public void RightTabSlideIn()
    {
        if (rightTabAnim.GetBool("Open"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.PanalOpen);
            rightTabAnim.SetBool("Open", false);
        }
    }

    public void AlertTabSlideOut()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.PanalOpen);
        alertTabAnim.SetBool("Open", true);
    }
    public void AlertTabSlideIn()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.PanalOpen);
        alertTabAnim.SetBool("Open", false);
    }

}
