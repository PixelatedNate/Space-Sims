using UnityEngine;

public class ClothSelectionMenuUIView : MonoBehaviour
{
    [SerializeField]
    GameObject ClothingItemPrefab;

    private const string clothesMalePath = "Artwork/Clothes/Male";
    private const string clothesFemalePath = "Artwork/Clothes/Female";

    [SerializeField]
    private Transform _ScrolPanal;

    public void PopulateList(PersonInfo person)
    {
        foreach (Transform child in _ScrolPanal)
        {
            Destroy(child.gameObject);
        }
        Sprite[] cloths;
        if (person.Gender == Gender.Male)
        {
            cloths = GetAllClothsFromPath(clothesMalePath);
        }
        else
        {
            cloths = GetAllClothsFromPath(clothesFemalePath);
        }

        foreach (Sprite cloth in cloths)
        {
            var ClothItemUI = GameObject.Instantiate(ClothingItemPrefab, _ScrolPanal);
            ClothItemUI.GetComponent<ClothCosmeticSelectionUI>().Setup(person, cloth);
        }
    }

    private Sprite[] GetAllClothsFromPath(string path)
    {
        return Resources.LoadAll<Sprite>(path);
    }

}
