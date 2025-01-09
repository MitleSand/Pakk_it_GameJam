using UnityEngine;

public class P_CursorController : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorClicked;

    private CursorControls controls;
    private Camera mainCamera;

    private bool isCursorVisible = true; // Track cursor visibility state

    private void Awake()
    {
        controls = new CursorControls();
        ChangeCursor(cursor);
        mainCamera = Camera.main;
        SetCursorVisibility(true);
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
        if (Physics.Raycast(ray, out RaycastHit hit, 1.5f))
        {
            if (hit.collider != null)
            {
                IClicked click = hit.collider.gameObject.GetComponent<IClicked>();
                if (click != null) click.onClickAction();
                Debug.Log("3D Hit: " + hit.collider.tag);
            }
        }
    }

    private void ChangeCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void SetCursorVisibility(bool visible)
    {
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void ToggleCursorVisibility()
    {
        isCursorVisible = !isCursorVisible;
        SetCursorVisibility(isCursorVisible);
    }
}
