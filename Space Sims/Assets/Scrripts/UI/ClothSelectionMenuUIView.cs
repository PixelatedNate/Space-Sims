using UnityEngine;
using UnityEngine.UI;

public class ClothSelectionMenuUIView : MonoBehaviour
{
    [SerializeField]
    GameObject ClothingItemPrefab;


    private ClothRarity raritySelected = ClothRarity.Basic;

    private const string clothesMalePath = "Artwork/Clothes/Male";
    private const string clothesFemalePath = "Artwork/Clothes/Female";

    [SerializeField]
    private Transform _BasicScrolPanal, _CommonScrolPanal, _RareScrolPanal, _EpicScrolPanal;

    [SerializeField]
    private Image basicBtnImg, CommonBtnImg, RareBtnImg, EpicBtnImg;



    public void PopulateList(PersonInfo person)
    {
        PopulateCosmeticScroll(_BasicScrolPanal, person, ClothRarity.Basic);
        PopulateCosmeticScroll(_CommonScrolPanal, person, ClothRarity.Common);
        PopulateCosmeticScroll(_RareScrolPanal, person, ClothRarity.Rare);
        PopulateCosmeticScroll(_EpicScrolPanal, person, ClothRarity.Epic);
        SetBtnColours();
        SetView(raritySelected);
    }

    public void SetBasicView()
    {
        SetView(ClothRarity.Basic);
    }
    public void SetCommonView()
    {
        SetView(ClothRarity.Common);
    }
    public void SetRareView()
    {
        SetView(ClothRarity.Rare);
    }
    public void SetEpicView()
    {
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
        foreach (Transform child in scroll)
        {
            Destroy(child.gameObject);
        }
        Sprite[] cloths = ResourceHelper.PersonHelper.GetAllCloths(person, rarity);

        foreach (Sprite cloth in cloths)
        {
            var ClothItemUI = GameObject.Instantiate(ClothingItemPrefab, scroll);
            ClothItemUI.GetComponent<ClothCosmeticSelectionUI>().Setup(person, cloth);
        }

    }

    public void SetBtnColours()
    {
        basicBtnImg.color = Color.gray;
        CommonBtnImg.color = Color.gray;
        RareBtnImg.color = Color.gray;
        EpicBtnImg.color = Color.gray;
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


    private Sprite[] GetAllClothsFromPath(string path)
    {
        return Resources.LoadAll<Sprite>(path);
    }

}
