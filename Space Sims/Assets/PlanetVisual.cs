using UnityEngine;

public class PlanetVisual : MonoBehaviour, IInteractables
{
    [SerializeField]
    PlanetData planetData;

    [SerializeField]
    Transform ship;

    //[SerializeField]
    private Material _orignalMaterial;


    private PlanetContainer PlanetContainer { get; set; }

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
        UIManager.Instance.OpenPlanetView(PlanetContainer);
        _orignalMaterial = GetComponent<SpriteRenderer>().material;
        GetComponent<SpriteRenderer>().material = MaterialFinder.GetOutlineResourceMaterial();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlanetContainer = new PlanetContainer(planetData, transform.position);
        transform.GetComponent<SpriteRenderer>().sprite = planetData.PlanetSprite;
        if (planetData.IsStartPlanet)
        {
            if (NavigationManager.CurrentPlanet == null && !NavigationManager.InNavigation)
            {
                NavigationManager.CurrentPlanet = PlanetContainer;

            }
        }
        if (NavigationManager.CurrentPlanet.PlanetPosition == PlanetContainer.PlanetPosition)
        {
            ship.position = PlanetContainer.PlanetPosition;
        }
    }
}
