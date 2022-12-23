using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavigationRocket : MonoBehaviour
{

    [SerializeField]
    float OrbitSpeed;

    // Start is called before the first frame update
    void Start()
    {      
    }

    // Update is called once per frame
    void Update()
    {
        if (NavigationManager.CurrentPlanet != null)
        {
            transform.position = NavigationManager.CurrentPlanet.transform.position;
            transform.Rotate(new Vector3(0, 0, OrbitSpeed * Time.deltaTime));
        }
        else if(NavigationManager.InNavigation)
        {
            transform.position = NavigationManager.GetPositionRelativeToJourny();
        }
    }
}
