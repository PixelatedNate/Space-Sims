using UnityEngine;

public class Planet : MonoBehaviour, IInteractables
{

    [SerializeField]
    string Name;

    [SerializeField]
    Quest[] _quests;

    [SerializeField]
    private bool IsStartPlanet;

    [SerializeField]
    private Sprite _background;
    public Sprite Background { get { return _background; } }

    public Sprite PlanetSprite { get; private set; }
    public Quest[] Quests { get { return _quests; } }


    private Material _orignalMaterial;

    public void OnDeselect()
    {
        GetComponent<SpriteRenderer>().material = _orignalMaterial;
    }

    public bool OnHold()
    {
        throw new System.NotImplementedException();
    }

    public void OnHoldRelease()
    {
        throw new System.NotImplementedException();
    }

    public void OnSelect()
    {
        UIManager.Instance.OpenPlanetView(this);
        _orignalMaterial = GetComponent<SpriteRenderer>().material;
        GetComponent<SpriteRenderer>().material = MaterialFinder.GetOutlineResourceMaterial();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (IsStartPlanet)
        {
            if (NavigationManager.CurrentPlanet == null && !NavigationManager.InNavigation)
            {
                NavigationManager.CurrentPlanet = this;
            }
        }
        PlanetSprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
