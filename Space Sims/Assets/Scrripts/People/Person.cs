using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour, IInteractables
{
    [SerializeField]
    private SpriteRenderer HeadRender;
    [SerializeField]
    private SpriteRenderer BodyRender;
    PersonInfo personInfo = null;


    [SerializeField]
    GameObject TempSelected;

    [SerializeField]
    bool IsBeingHeld = false;

    // Start is called before the first frame update
    void Start()
    {
        TimeTickSystem.OnTick += OnTick;

        //testing
        if (personInfo == null)
        {
            PersonInfo person = new PersonInfo();
            person.Randomize();
            AssginPerson(person);
            ReRenderPerson();
            GlobalStats.Instance.PlayersPeople.Add(personInfo);
        }
    }

    public void AssginPerson(PersonInfo person)
    {
        if(personInfo != null)
        {
    //       throw new Exception("Trying to assgin a person who allready has a personholder");
        }
        gameObject.name = person.Name;
        personInfo = person;
        person.PersonMonoBehaviour = this;
        ReRenderPerson();
    }


    private void OnDestroy()
    {
        if(IsBeingHeld)
        {
            throw new Exception("Trying to destroy a person whilstBeingheld");
        }
        personInfo.PersonMonoBehaviour = null;
    }

    private void ReRenderPerson()
    {
        BodyRender.sprite = personInfo.Body;
        HeadRender.sprite = personInfo.Head;
    }


    private void OnTick (object source, EventArgs e)
    {
        GlobalStats.Instance.PlayerResources -= personInfo.Upkeep;
     //   Debug.Log("PlayerTick");
    }



    // Update is called once per frame

    private void LateUpdate()
    {
        if(IsBeingHeld)
        {
           Vector3 mousePointOnWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           mousePointOnWorld.z = 0;
           transform.position = mousePointOnWorld;
        }
    }

#region InteractableInterace
    public void OnSelect()
    {
        TempSelected.SetActive(true);
        Debug.Log("Select: " + gameObject.name);
        UIManager.Instance.DisplaySelected(personInfo);
    }

    public bool OnHold()
    {
        Debug.Log("OnHold" + gameObject.name);
        
        if(IsBeingHeld)
        {
            throw new Exception("Cannot start a hold on someone who is allready being held");
        }
        IsBeingHeld = true;


        return true;
    }

    public void OnHoldRelease()
    {
        Debug.Log("OnHoldRelease" + gameObject.name);
        IsBeingHeld = false;
    }

    public void OnDeselect()
    {
        TempSelected.SetActive(false);
        Debug.Log("Deselect: " + gameObject.name);
    }
    #endregion
}
