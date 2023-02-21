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
        if (!UnlocksManager.UnlockedCoths.Contains(Cloth.name))
        {
            _clothImageUI.color = Color.black;
        }
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
}
