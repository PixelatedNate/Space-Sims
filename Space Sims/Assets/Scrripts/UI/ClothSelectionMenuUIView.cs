using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClothSelectionMenuUIView : MonoBehaviour
{
    [SerializeField]
    GameObject ClothingItemPrefab;

    private ClothRarity raritySelected = ClothRarity.Basic;

    [SerializeField]
    private Transform _BasicScrolPanal, _CommonScrolPanal, _RareScrolPanal, _EpicScrolPanal;

    [SerializeField]
    private Image basicBtnImg, CommonBtnImg, RareBtnImg, EpicBtnImg;


    private Dictionary<ClothRarity, List<ClothCosmeticSelectionUI>> newCloths = new Dictionary<ClothRarity, List<ClothCosmeticSelectionUI>>();

    public void PopulateList(PersonInfo person)
    {
        newCloths.Clear();
        PopulateCosmeticScroll(_BasicScrolPanal, person, ClothRarity.Basic);
        PopulateCosmeticScroll(_CommonScrolPanal, person, ClothRarity.Common);
        PopulateCosmeticScroll(_RareScrolPanal, person, ClothRarity.Rare);
        PopulateCosmeticScroll(_EpicScrolPanal, person, ClothRarity.Epic);
        SetBtnColours();
        SetView(raritySelected);
    }

    public void SetBasicView()
    {
        ClearNewFromRarity(raritySelected);
        SetView(ClothRarity.Basic);
    }
    public void SetCommonView()
    {
        ClearNewFromRarity(raritySelected);
        SetView(ClothRarity.Common);
    }
    public void SetRareView()
    {
        ClearNewFromRarity(raritySelected);
        SetView(ClothRarity.Rare);
    }
    public void SetEpicView()
    {
        ClearNewFromRarity(raritySelected);
        SetView(ClothRarity.Epic);
    }


    public void SetView(ClothRarity rarity)
    {
        raritySelected = rarity;

        _BasicScrolPanal.parent.gameObject.SetActive(false);
        _CommonScrolPanal.parent.gameObject.SetActive(false);
        _RareScrolPanal.parent.gameObject.SetActive(false);
        _EpicScrolPanal.parent.gameObject.SetActive(false);

        if (raritySelected == ClothRarity.Basic)
        {
            _BasicScrolPanal.parent.gameObject.SetActive(true);
        }
        else if (raritySelected == ClothRarity.Common)
        {
            _CommonScrolPanal.parent.gameObject.SetActive(true);
        }

        else if (raritySelected == ClothRarity.Rare)
        {
            _RareScrolPanal.parent.gameObject.SetActive(true);
        }

        else if (raritySelected == ClothRarity.Epic)
        {
            _EpicScrolPanal.parent.gameObject.SetActive(true);
        }
        SetBtnColours();
    }

    public void PopulateCosmeticScroll(Transform scroll, PersonInfo person, ClothRarity rarity)
    {
        List<ClothCosmeticSelectionUI> newItems = new List<ClothCosmeticSelectionUI>();

        foreach (Transform child in scroll)
        {
            Destroy(child.gameObject);
        }
        Sprite[] cloths = ResourceHelper.PersonHelper.GetAllCloths(person, rarity);

        foreach (Sprite cloth in cloths)
        {
            var ClothItemUI = GameObject.Instantiate(ClothingItemPrefab, scroll);
            ClothItemUI.GetComponent<ClothCosmeticSelectionUI>().Setup(person, cloth);
            if(UnlocksManager.NewCoths.Contains(cloth.name))
            {
                newItems.Add(ClothItemUI.GetComponent<ClothCosmeticSelectionUI>());
            }
        }
        foreach (var go in newItems)
        {
            go.transform.SetAsFirstSibling();
        }

        newCloths.Add(rarity, newItems);

    }

    public void SetBtnColours()
    {
        basicBtnImg.color = Color.gray;
        CommonBtnImg.color = Color.gray;
        RareBtnImg.color = Color.gray;
        EpicBtnImg.color = Color.gray;

        basicBtnImg.transform.GetChild(1).gameObject.SetActive(false);
        CommonBtnImg.transform.GetChild(1).gameObject.SetActive(false);
        RareBtnImg.transform.GetChild(1).gameObject.SetActive(false);
        EpicBtnImg.transform.GetChild(1).gameObject.SetActive(false);

        if(newCloths[ClothRarity.Basic].Count != 0)
        {
            basicBtnImg.transform.GetChild(1).gameObject.SetActive(true);
        }
        if(newCloths[ClothRarity.Common].Count != 0)
        {
            CommonBtnImg.transform.GetChild(1).gameObject.SetActive(true);
        }
        if(newCloths[ClothRarity.Epic].Count != 0)
        {
            EpicBtnImg.transform.GetChild(1).gameObject.SetActive(true);
        }
        if(newCloths[ClothRarity.Rare].Count != 0)
        {
            RareBtnImg.transform.GetChild(1).gameObject.SetActive(true);
        }


        if (raritySelected == ClothRarity.Basic)
        {
            basicBtnImg.color = Color.white;
        }
        else if (raritySelected == ClothRarity.Common)
        {
            CommonBtnImg.color = Color.white;
        }
        else if (raritySelected == ClothRarity.Rare)
        {
            RareBtnImg.color = Color.white;
        }
        else if (raritySelected == ClothRarity.Epic)
        {
            EpicBtnImg.color = Color.white;
        }
    }


    private void ClearNewFromRarity(ClothRarity rarity)
    {
        foreach(var clothUI in newCloths[rarity])
        {
            clothUI.disableNewText();
            UnlocksManager.NewCoths.Remove(clothUI.Cloth.name);
        }
        newCloths[rarity].Clear();
    }

}

