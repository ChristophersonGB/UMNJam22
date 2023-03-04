using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NodeManagement : MonoBehaviour
{
    [SerializeField]
    private InputAction MouseClick;

    public List<User> Users;
    public bool Active = true;

    public SpriteRenderer cancelled;

    //public delegate void NodeManagementDelegate(NodeManagement node);
    //public event NodeManagementDelegate NodeManagementEvent;

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

    private void Start()
    {
        //Users = new List<User>();
    }


    public void AddUser(User user)
    {
        // Add user to list
        Users.Add(user);
        // Create Line between Node and User
        CreateLine(user);
        // Calculate a space for the User to target as a location
        // CalculateSpot();
        user.MoveToTarget(transform.position);
    }

    public Vector2 CalculateSpot()
    {
        // Currently just returns location.
        // TODO: Create offset from center (radius + levelOffest, like valence electrons), Adjust Direction by Vector2(+x, -y) 0 +y etc etc, further divide by W where W = 1/4 NUM_OF_USERS_IN_CIRCLE
        Vector2 goalLocation = transform.position;
        return goalLocation;
    }

    public void SeverAllNodeConnections()
    {
        //Debug.Log("IT SHORE IS ACTIVE!");
        foreach (User user in Users)
        {
            //Debug.Log("FORREACH!");
            user.TryGetComponent<LineRenderer>(out var lineRenderer);
            lineRenderer.enabled = false;
        }
        FindObjectOfType<GameManager>().DeactivateNode(this);
        Active = false;
        cancelled.enabled = true;
        Users.Clear();
    }

    public void SetNodeActive()
    {
        FindObjectOfType<GameManager>().ActivateNode(this);
        Active = true;
        cancelled.enabled = false;
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null)
        {
            if (hit.collider.transform == transform)
            {
                if (Active)
                    SeverAllNodeConnections();
                else
                    SetNodeActive();
            }
        }
        //SeverAllNodeConnections();
        //Debug.Log("MOUSEDOWN!");
    }

    
    public void CreateLine(User user)
    {
        var gameObj = user.gameObject;
        gameObj.TryGetComponent<LineRenderer>(out var connectionLine);
        if (connectionLine == null) {
            connectionLine = gameObj.AddComponent<LineRenderer>();
        }
        var lineWidth = 0.1f;
        connectionLine.enabled = true;
        connectionLine.positionCount = 2;
        connectionLine.SetPositions(new Vector3[] { transform.position, gameObj.transform.position });
        connectionLine.startWidth = lineWidth;
        connectionLine.endWidth = lineWidth; 
    }

    private void UpdateLines()
    {
        foreach (User user in Users) {
            var line = user.GetComponent<LineRenderer>();
            line.SetPositions(new Vector3[] { transform.position, user.gameObject.transform.position });
        }
    }


    void FixedUpdate()
    {
        if (Active)
            UpdateLines();    
    }
}
