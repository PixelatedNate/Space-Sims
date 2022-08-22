using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanAndZoom : MonoBehaviour
{

    Vector3 TouchStartPos;
    [SerializeField]
    private float minZoom = 1;
    [SerializeField]
    private float MaxZoom = 8;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetMouseButtonDown(0))
        {
            TouchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
         if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitue = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitue = (touchZero.position - touchOne.position).magnitude;

            float diff = currentMagnitue - prevMagnitue;

            Zoom(diff * 0.01f);
        }
         else if(Input.GetMouseButton(0))
        {
            Vector3 dir = TouchStartPos - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += dir;
        }


         //if in editor can zoom with scrollwheel
#if UNITY_EDITOR

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
        
#endif

    }



    public void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoom, MaxZoom);
    }
}
