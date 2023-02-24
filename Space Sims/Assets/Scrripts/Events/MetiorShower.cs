using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetiorShower : MonoBehaviour
{
    private void OnEnable()
    {
        TouchControls.SetZoomActive(false);
        Camera.main.orthographicSize = 70;
        

    }
    






    // Update is called once per frame
    void Update()
    {
        
    }
}
