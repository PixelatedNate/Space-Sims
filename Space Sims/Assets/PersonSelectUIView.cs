using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonSelectUIView : MonoBehaviour
{
    PersonInfo SelectedPerson;

    [SerializeField]
    Image Head; 
    [SerializeField]
    Image Body;

    public void SetPerson(PersonInfo person)
    {
        SelectedPerson = person;
        UpdateHeadAndBody();
        UpdateText();
    }




    private void UpdateHeadAndBody()
    {
        Head.sprite = SelectedPerson.Head;
        Body.sprite = SelectedPerson.Body;
    }

    private void UpdateText()
    {
        Debug.Log("here");
    }

}
