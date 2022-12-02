using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour, IInteractables
{

    [SerializeField]
    string Name;

    [SerializeField]
    Quest[] _quests;


    [SerializeField]
    private bool IsPlayerShipPresent;

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
        UIManager.Instance.DisplayPlanet(this);
        _orignalMaterial = GetComponent<SpriteRenderer>().material;
        GetComponent<SpriteRenderer>().material = MaterialFinder.GetOutlineResourceMaterial();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlanetSprite = GetComponent<SpriteRenderer>().sprite;
        if(IsPlayerShipPresent)
        {
            NavigationManager.CurrentPlanet = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
