using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{

   public static BackgroundManager Instance;

    [SerializeField]
    private Image _backgroundImage;


    [SerializeField]
    private Sprite _inTransitBackground;

    [SerializeField]
    private Material TravalingBackgroundMaterial;
    [SerializeField]
    private Material RandomBackGroundNoise;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void setBackground(Sprite newBackground)
    {
        _backgroundImage.sprite = newBackground;
        _backgroundImage.material = RandomBackGroundNoise;
    }

    public void setBackgroundToInTransit()
    {
        _backgroundImage.sprite = _inTransitBackground;
        _backgroundImage.material = TravalingBackgroundMaterial;
    }

}
