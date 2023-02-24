using UnityEngine;
using UnityEngine.UI;

public class ClothCosmeticSelectionUI : MonoBehaviour
{

    public Sprite Cloth { get; private set; }

    private PersonInfo PersonInfo { get; set; }

    [SerializeField]
    private Image _clothImageUI;
    [SerializeField]
    private GameObject newText;

    public void Setup(PersonInfo personInfo, Sprite cloth)
    {
        if (UnlocksManager.UnlockedCoths.Contains(cloth.name))
        {
            transform.SetAsFirstSibling();
            if(UnlocksManager.NewCoths.Contains(cloth.name))
            {
                newText.SetActive(true);
            }
        }
        else
        {
            _clothImageUI.color = Color.black;
        }

        _clothImageUI.sprite = cloth;
        Cloth = cloth;
        PersonInfo = personInfo;
    }

    public void SetPersonCloths()
    {
        if (UnlocksManager.UnlockedCoths.Contains(Cloth.name))
        {
            PersonInfo.SetCloths(Cloth);
            UIManager.Instance.OpenPersonView(PersonInfo);
        }
        else
        {
            AlertOverLastTouch.Instance.PlayAlertOverLastTouch("Locked", Color.red);
            SoundManager.Instance.PlaySound(SoundManager.Sound.Error);
        }
    }

    public void disableNewText()
    {
        newText.SetActive(false);
    }
}
