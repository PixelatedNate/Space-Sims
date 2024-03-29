using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchControls : MonoBehaviour
{

    static Vector3 TouchStartPos;
    [SerializeField]
    private float minZoom = 1;
    [SerializeField]
    private float MaxZoom = 8;
    [SerializeField]
    private float zoomSpeed = 2;

    private static bool NewCameraMovemntEnabled { get; set; } = true;

    private bool CameraMovemntEnabled { get; set; } = true;

    private Button _button;   // prevent trigering of UI buttons when panning or performing other touch related actions

    [SerializeField]
    private float SHORT_CLICK_END = 0.5f;
    [SerializeField]
    private float HOLD_TIME_START = 0.5f;


    [SerializeField]
    private float ClickDuration = 0;

    [SerializeField]
    private float PANDEADZONE = 0.25f;

    bool Paning = false;   // maybe can be enum with state.
    bool Zooming = false;

    private float BorderPixelsSize = 100f; // can be changed to a percentage.
    private float EdgePanSpeed = 5.0f;

    private GameObject FollowObj;


    private static IInteractables SelectedObject = null;
    bool InteractableIsIsHeld = false;

    private int UILayer { get; set; }

    [SerializeField]
    private LayerMask ActiveLayer;


    [SerializeField]
    private Vector2 maxPanDist;

    static public void DeceletAll()
    {
        try
        {
            SelectedObject.OnDeselect();
        }
        catch { }
        SelectedObject = null;
    }


    void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
        ClickDuration = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (!CameraMovemntEnabled)
        {
            CameraMovemntEnabled = NewCameraMovemntEnabled;
            Zooming = false;
            return;
        }
        if (Input.GetMouseButtonUp(0) && !Zooming)
        {
            if (_button != null)
            {
                _button.interactable = true;
                _button = null;
            }
            if (!Paning)
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
                else
                {
                    if (!IsPointerOverUIElement())
                    {
                        IInteractables currentSelectedObject = SelectedObject;
                        if (SelectedObject != null)
                        {
                            DeceletAll();
                            SelectedObject = null;
                        }
                        var selected = GetInteractableUnderMouse(ActiveLayer);
                        if (selected != null)
                        {
                            if (ClickDuration < SHORT_CLICK_END)
                            {
                                if (selected == currentSelectedObject)
                                {
                                    UIManager.Instance.DeselectAll();
                                }
                                else
                                {
                                    SelectedObject = selected;
                                    selected.OnSelect();
                                }
                            }
                        }
                        if (SelectedObject == null)
                        {
                            UIManager.Instance.DeselectAll();
                        }
                    }
                }
            }
            ClickDuration = 0;
            Paning = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            TouchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                _button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
                _button.interactable = false;
            }
            TouchZoom();
        }
        else if (Input.GetMouseButton(0))
        {
            if (Zooming)
            {
                Zooming = false;
                TouchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            ClickDuration += Time.deltaTime;
            if (InteractableIsIsHeld)
            {
                PanAtEdge();
                return;
            }
            Vector3 dir = TouchStartPos - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!Paning && Mathf.Abs(dir.magnitude) > PANDEADZONE && EventSystem.current.currentSelectedGameObject == null)
            {
                Paning = true;
                CameraManager.Instance.ClearFollow();
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    _button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
                    _button.interactable = false;
                }
            }
            if (Paning)
            {
                float xvalue = Mathf.Clamp(Camera.main.transform.position.x + dir.x, maxPanDist.x * -1, maxPanDist.x);
                float yvalue = Mathf.Clamp(Camera.main.transform.position.y + dir.y, maxPanDist.y * -1, maxPanDist.y);

                Camera.main.transform.position = new Vector3(xvalue, yvalue, Camera.main.transform.position.z);
            }
            else if (ClickDuration > HOLD_TIME_START) // this is not optermized so will run the check multipale times.
            {
                var selectableHold = GetInteractableUnderMouse(ActiveLayer);
                if (selectableHold != null)
                {
                    InteractableIsIsHeld = selectableHold.OnHold();
                    if (InteractableIsIsHeld)
                    {
                        if (SelectedObject != null)
                        {
                            SelectedObject.OnDeselect();
                        }
                        SelectedObject = selectableHold;
                    }
                }
            }
        }
        else if (Zooming && Input.touchCount == 0)
        {
            Zooming = false;
        }

        //if in editor can zoom with scrollwheel
#if UNITY_EDITOR

        Zoom(Input.GetAxis("Mouse ScrollWheel"));

#endif
        CameraMovemntEnabled = NewCameraMovemntEnabled;
    }


    private void TouchZoom()
    {
        Zooming = true;
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

    public static IInteractables GetInteractableUnderMouse(LayerMask? activeLayerMask = null)
    {
        RaycastHit2D hit;
        if (activeLayerMask != null)
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, (int)activeLayerMask);
        }
        else
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        }
        IInteractables selected = null;
        if (hit.collider != null)
        {
            if (activeLayerMask == null || (1 << hit.collider.gameObject.layer & activeLayerMask) != 0)
            {
                MonoBehaviour scrpit = hit.collider.gameObject.GetComponent<MonoBehaviour>();
                if (scrpit is IInteractables)
                {
                    return selected = (IInteractables)((System.Object)scrpit);
                }
            }
        }
        return null;
    }

    public static AbstractRoom GetRoomUnderMouse()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                MonoBehaviour scrpit = hit.collider.gameObject.GetComponent<MonoBehaviour>();
                if (scrpit is AbstractRoom)
                {
                    return (AbstractRoom)scrpit;
                }
            }
        }
        return null;
    }



    public void Zoom(float increment)
    {
        increment = increment * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoom, MaxZoom);
    }



    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
            {
                return true;
            }
        }
        return false;
    }

    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

    public static void EnableCameramovemntAndSelection(bool value)
    {
        NewCameraMovemntEnabled = value;
        TouchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static void RecenterCamera()
    {
        Camera.main.orthographicSize = 10;
        Camera.main.transform.position = new Vector3(5, 8.5f, -10);
    }


}
