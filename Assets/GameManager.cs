using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<User> users;
    public List<NodeManagement> nodes;
    public List<NodeManagement> activeNodes;


    // Start is called before the first frame update
    public void ManagerStart()
    {
        nodes = FindObjectsOfType<NodeManagement>().ToList();
        users = FindObjectsOfType<User>().ToList();
        foreach (var node in nodes)
        {
            if (node.Active)
            {
                activeNodes.Add(node);
            }
        }

        foreach (User user in users)
        {
            AssignNodeToUser(user);
        }

    }

    public void AssignNodeToUser(User user)
    {
        if (activeNodes.Count > 0)
        {
            var index = (int)Random.Range(0f, activeNodes.Count);
            activeNodes[index].AddUser(user);
        }
        else
        {
            Debug.LogError("No active nodes available");
        }
        
    }

    public void DeactivateNode(NodeManagement node)
    {
        activeNodes.Remove(node);
        foreach (User user in node.Users)
        {
            AssignNodeToUser(user);
        }
    }

    public void ActivateNode(NodeManagement node)
    {
        activeNodes.Add(node);
    }

    //public void AddNode(NodeManagement node)
    //{
    //    nodes.Add(node);
    //    ActivateNode(node);
    //}

    
}
