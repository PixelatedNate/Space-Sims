using UnityEngine;

public class PlanetVisual : MonoBehaviour, IInteractables
{
    [SerializeField]
    Planet planet;
    [SerializeField]
    private Material _orignalMaterial;

    public Sprite PlanetSprite { get; private set; }

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
        UIManager.Instance.OpenPlanetView(planet);
        _orignalMaterial = GetComponent<SpriteRenderer>().material;
        GetComponent<SpriteRenderer>().material = MaterialFinder.GetOutlineResourceMaterial();
    }

    // Start is called before the first frame update
    void Start()
    {
        planet.PlanetPosition = transform.position;
        if (planet.IsStartPlanet)
        {
            if (NavigationManager.CurrentPlanet == null && !NavigationManager.InNavigation)
            {
                NavigationManager.CurrentPlanet = planet;
            }
        }
        planet.PlanetSprite = GetComponent<SpriteRenderer>().sprite;
    }



}
