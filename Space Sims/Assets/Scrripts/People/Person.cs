using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer HeadRender;
    [SerializeField]
    private SpriteRenderer BodyRender;
    [SerializeField]
    PersonInfo personInfo;

    // Start is called before the first frame update
    void Start()
    {

        personInfo = ScriptableObject.CreateInstance<PersonInfo>();
        personInfo.Randomize();
        BodyRender.sprite = personInfo.Body;
        HeadRender.sprite = personInfo.Head;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
