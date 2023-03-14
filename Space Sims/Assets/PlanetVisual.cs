using System;
using UnityEngine;

public class PlanetVisual : MonoBehaviour, IInteractables
{
    [SerializeField]
    PlanetData planetData;

    [SerializeField]
    Transform ship;

    public static event Action<PlanetData> OnPlaentSelected;


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
        _orignalMaterial = GetComponent<SpriteRenderer>().material;
        if (NavigationManager.ReachedFirstPlanet || planetData.IsStartPlanet)
        {
            OnPlaentSelected?.Invoke(planetData);
            UIManager.Instance.OpenPlanetView(PlanetContainer);
            _orignalMaterial = GetComponent<SpriteRenderer>().material;
            GetComponent<SpriteRenderer>().material = MaterialFinder.GetOutlineResourceMaterial();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (NavigationManager.PlanetList.ContainsKey(planetData))
        {
            PlanetContainer = NavigationManager.PlanetList[planetData];
            transform.GetComponent<SpriteRenderer>().sprite = planetData.PlanetSprite;
        }
        else
        {
            PlanetContainer = new PlanetContainer(planetData, transform.position);
            NavigationManager.PlanetList.Add(planetData, PlanetContainer);
            transform.GetComponent<SpriteRenderer>().sprite = planetData.PlanetSprite;
        }

        if (NavigationManager.CurrentPlanet != null)
        {
            if (NavigationManager.CurrentPlanet.PlanetPosition == PlanetContainer.PlanetPosition)
            {
                ship.position = PlanetContainer.PlanetPosition;
            }
        }

    }
}
