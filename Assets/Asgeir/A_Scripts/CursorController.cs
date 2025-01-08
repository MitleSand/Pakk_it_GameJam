using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursor;

    public Texture2D cursorClicked;

    private CursorControls controls;

    private Camera mainCamera;

    public LayerMask packBoy;

    private void Awake()
    {
        
        controls = new CursorControls();
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    private void Start()
    {
        //bind input action events
        controls.Mouse.Click.started += _ => StartedClick();
        controls.Mouse.Click.performed += _ => EndedClick();
    }

    private void StartedClick()
    {
        ChangeCursor(cursorClicked);
    }

    private void EndedClick()
    {
        ChangeCursor(cursor);
        DetectObject();
    }

    private void DetectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                IClicked click = hit.collider.gameObject.GetComponent<IClicked>();
                if (click != null) click.onClickAction();
                Debug.Log("3D Hit: " + hit.collider.tag);
            }
        }

        // Used for if you want mulitple hits
        /*RaycastHit[] hits = Physics.RaycastAll(ray, 2);
        for (int i = 0; i < hits.Length; ++i)
        {
            if (hits[i].collider != null)
            {
                
                Debug.Log("3D Hit All: " + hits[i].collider.tag);
            }
        }*/

       /* RaycastHit[] hitsNonAlloc = new RaycastHit[1];
        int numberOfHits = Physics.RaycastNonAlloc(ray, hitsNonAlloc);
        for (int i = 0; i < numberOfHits; ++i)
        {
            if (hitsNonAlloc[i].collider != null)
            {
                
                Debug.Log("3D Hit All Non Alloc All: " + hitsNonAlloc[i].collider.tag);
            }
        }*/

    }


    private void ChangeCursor(Texture2D cursor)
    {
        //Can change where center of cursor is (default is top left)
        //Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2)

        //Set the cursor with no hotspot
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }


}
