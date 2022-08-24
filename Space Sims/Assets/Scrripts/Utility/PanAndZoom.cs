using System;
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


    [SerializeField]
    private float SHORT_CLICK_END = 0.5f;
    [SerializeField]
    private float HOLD_TIME_START = 1f;


    [SerializeField]
    private float ClickDuration = 0;

    [SerializeField]
    private float PANDEADZONE = 0.25f;
    bool Paning;


    private float BorderPixelsSize = 100f; // can be changed to a percentage.
    private float EdgePanSpeed = 5.0f;



    IInteractables SelectedObject = null;
    bool InteractableIsIsHeld = false;


    // Start is called before the first frame update
    void Start()
    {
        ClickDuration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonUp(0))
        {
           if(!Paning)
            {
                if (InteractableIsIsHeld && SelectedObject != null)
                {
                    SelectedObject.OnHoldRelease();
                    SelectedObject = null;
                    InteractableIsIsHeld = false;
                    ClickDuration = 0;
                    return;
                }
                else if (InteractableIsIsHeld)
                {
                    throw new Exception("Interctable Cannot be held when it is not set");
                }
                else {
                    
                    if(SelectedObject != null)
                    {
                        SelectedObject.OnDeselect();
                        SelectedObject = null;
                    }
                    var selected = GetInteractableUnderMouse();
                    if(selected != null)
                    {                  
                          if (ClickDuration < SHORT_CLICK_END)
                          {
                              SelectedObject = selected;
                              selected.OnSelect();
                          }                       
                    }
                }
            }
            ClickDuration = 0;
            Paning = false;
        }
        if(Input.GetMouseButtonDown(0))
        {
            TouchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
         if(Input.touchCount == 2)
        {
            ClickDuration = 0;             
            Paning = false;
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
            ClickDuration += Time.deltaTime;
            if (InteractableIsIsHeld)
            {
                PanAtEdge();
                return;
            }
            Vector3 dir = TouchStartPos - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!Paning && Mathf.Abs(dir.magnitude) > PANDEADZONE)
            {
                Paning = true;
            }
            //ClickDuration += Time.deltaTime;
            if(Paning)
            {
                Camera.main.transform.position += dir;
            }
            else if(ClickDuration > HOLD_TIME_START) // this is not optermized so will run the check multipale times.
            {
                var selectableHold = GetInteractableUnderMouse();
                if (selectableHold != null)
                {
                    InteractableIsIsHeld = selectableHold.OnHold();
                    if (InteractableIsIsHeld)
                    {
                        if(SelectedObject != null)
                        {
                        SelectedObject.OnDeselect();
                        }
                        SelectedObject = selectableHold;
                    }
                }
            }
        }


         //if in editor can zoom with scrollwheel
#if UNITY_EDITOR

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
        
#endif

    }



    public void PanAtEdge()
    {

        Vector3 newCameraTransform = Camera.main.transform.position;
        if (Input.mousePosition.x > Screen.width - BorderPixelsSize)
        {
            newCameraTransform.x += EdgePanSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x < 0 + BorderPixelsSize)
        {
            newCameraTransform.x -= EdgePanSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y > Screen.height - BorderPixelsSize)
        {
            newCameraTransform.y += EdgePanSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y < 0 + BorderPixelsSize)
        {
            newCameraTransform.y -= EdgePanSpeed * Time.deltaTime;
        }

        Camera.main.transform.position = newCameraTransform;
    }

    public IInteractables GetInteractableUnderMouse()
    {
    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    IInteractables selected = null;
        if (hit.collider != null)
        {
             MonoBehaviour scrpit = hit.collider.gameObject.GetComponent<MonoBehaviour>();
            if (scrpit is IInteractables)
            {
                 return selected = (IInteractables)((System.Object)scrpit);
            }
        }
    return null;
    }

    public void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoom, MaxZoom);
    }
}
