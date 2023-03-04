using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class User : MonoBehaviour
{
    public int numberOfUsers;


    private Vector3 velocity = Vector3.zero;
    public float Speed = 0.1f;
    public Vector2 goalLocation;

    private WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

    private void Start()
    {
        goalLocation = transform.position;
        // Add ourself to main node
        //FindObjectOfType<NodeManagement>().AddUser(gameObject.GetComponent<User>());
        //StartCoroutine("TurnOff");
    }

    //private IEnumerator TurnOff()
    //{
    //    yield return new WaitForSeconds(5);
    //    TryGetComponent<LineRenderer>(out var lineRenderer);
    //    lineRenderer.enabled = false;
    //}
    public void MoveToTarget(Vector3 targetPosition)
    {
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Speed);
        goalLocation = targetPosition;
        StartCoroutine(MovingToTarget(targetPosition));
    }

    //private void Update()
    //{
    //    GetComponent<Rigidbody2D>().velocity = goalLocation;
    //    if (goalLocation )
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var x = Random.Range(-0.3f, 0.3f);
        var y = Random.Range(-0.3f, 0.3f);
        transform.position = transform.position + new Vector3(x, y, 0);
    }

    private IEnumerator MovingToTarget(Vector3 targetPosition)
    {
        float initialDistance = Vector3.Distance(transform.position, targetPosition);
        TryGetComponent<Rigidbody2D>(out var rb);
        
        // if float = 1, mouseclick is down
        while (initialDistance > 1.5f)
        {
            if (rb != null)
            {
                //Debug.Log(initialDistance);
                initialDistance = Vector3.Distance(transform.position, targetPosition);
                Vector3 direction = targetPosition - transform.position;
                rb.velocity = direction * Speed;
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
