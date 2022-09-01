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
    [SerializeField]
    PersonInfo personInfo;


    [SerializeField]
    GameObject TempSelected;

    [SerializeField]
    bool IsBeingHeld = false;

    // Start is called before the first frame update
    void Start()
    {
        TimeTickSystem.OnTick += OnTick;
        personInfo = new PersonInfo();
        personInfo.Randomize();
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
