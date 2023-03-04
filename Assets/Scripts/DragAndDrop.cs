using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField]
    private InputAction MouseClick;
    [SerializeField]
    private float MouseDragPhysicsSpeed = 10f;
    [SerializeField]
    private float MouseDragSpeed = 10f;

    private Camera MainCamera;
    private Vector2 velocity = Vector2.zero;
    private WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();   

    private void Awake()
    {
        MainCamera = Camera.main;
    }

    private void OnEnable()
    {
        MouseClick.Enable();
        MouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        MouseClick.performed -= MousePressed;
        MouseClick.Disable();
    }

    //private void MousePressed(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Mouse Down");
    //    Ray ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
    //    //RaycastHit hit;
    //    //if (Physics.Raycast(ray, out hit))
    //    //{
    //    //    if (hit.collider != null)
    //    //    {
    //    //        StartCoroutine(DragUpdate(hit.collider.gameObject));
    //    //    }
    //    //}

    //    RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
    //    Debug.Log(hit2D.collider.gameObject);
    //    if (hit2D.collider != null)
    //    {
    //        StartCoroutine(DragUpdate(hit2D.collider.gameObject));
    //    }
    //}

    //private IEnumerator DragUpdate(GameObject clickedObject)
    //{
    //    float initialDistance = Vector2.Distance(clickedObject.transform.position, MainCamera.transform.position);
    //    clickedObject.TryGetComponent<Rigidbody2D>(out var rb);

    //    // if float = 0, mouseclick is down
    //    while (MouseClick.ReadValue<float>() != 0)
    //    {
    //        Ray ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
    //        if (rb != null)
    //        {
    //            Vector2 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
    //            rb.velocity = direction * MouseDragPhysicsSpeed;
    //            yield return WaitForFixedUpdate;
    //        }
    //        else
    //        {
    //            clickedObject.transform.position = Vector2.SmoothDamp(clickedObject.transform.position,
    //                    ray.GetPoint(initialDistance), ref velocity, MouseDragSpeed);
    //            yield return null;
    //        }
    //    }
    //}

    private void MousePressed(InputAction.CallbackContext context)
    {
        Debug.Log("Mouse Down");
        Collider2D col = Physics2D.OverlapPoint(Mouse.current.position.ReadValue());
        Debug.Log(Mouse.current.position.ReadValue());
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    if (hit.collider != null)
        //    {
        //        StartCoroutine(DragUpdate(hit.collider.gameObject));
        //    }
        //}


        if (col != null)
        {
            Debug.Log(col.gameObject);
            StartCoroutine(DragUpdate(col.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        float initialDistance = Vector2.Distance(clickedObject.transform.position, MainCamera.transform.position);
        clickedObject.TryGetComponent<Rigidbody2D>(out var rb);

        // if float = 1, mouseclick is down
        while (MouseClick.ReadValue<float>() != 0)
        {
            if (rb != null)
            {
                Vector2 clickedObjPosition = clickedObject.transform.position;
                Vector2 direction = Mouse.current.position.ReadValue() - clickedObjPosition;
                rb.velocity = direction * MouseDragPhysicsSpeed;
                yield return WaitForFixedUpdate;
            }
            else
            {
                //clickedObject.transform.position = Vector2.SmoothDamp(clickedObject.transform.position,
                //        ray.GetPoint(initialDistance), ref velocity, MouseDragSpeed);
                yield return null;
            }
        }
    }
}
