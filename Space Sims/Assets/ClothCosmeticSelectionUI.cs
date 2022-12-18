using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothCosmeticSelectionUI : MonoBehaviour
{

    private Sprite Cloth { get; set; }

    private PersonInfo PersonInfo { get; set; }

    [SerializeField]
    private Image _clothImageUI;

    public void Setup(PersonInfo personInfo, Sprite cloth)
    {
        _clothImageUI.sprite = cloth;
        Cloth = cloth;
        PersonInfo = personInfo;
    }

    public void SetPersonCloths()
    {
        PersonInfo.SetCloths(Cloth);
        UIManager.Instance.OpenPersonView(PersonInfo);
    }
}
