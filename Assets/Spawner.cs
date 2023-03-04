using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnSettings spawnSettings;

    private Camera MainCamera;

    public GameManager gameManager;

    public GameObject UserPrefab;
    public GameObject NodePrefab;

    public int numOfUsers = 20;
    public int numOfNodes = 5;

    // Start is called before the first frame update
    IEnumerator StartManager(GameManager gM)
    {
        yield return new WaitForSeconds(2f);
        gM.ManagerStart();
    }

    void Start()
    {
        numOfNodes = spawnSettings.NumNodes;
        numOfUsers = spawnSettings.NumUsers;
        Spawn();
        //call managerStart
        StartCoroutine(StartManager(gameManager));
    }

    void Spawn()
    {
        //Camera.main.pixelWidth
        StartCoroutine(SpawnNodes());
        StartCoroutine(SpawnUsers());
    }

    IEnumerator SpawnNodes()
    {
        for (int i = 0; i < numOfNodes; i++)
        {
            Instantiate(NodePrefab);
            yield return null;
        }
        
    }

    IEnumerator SpawnUsers()
    {
        for(int i = 0; i < numOfUsers; i++)
        {
            Instantiate(UserPrefab);
            yield return null;
        }
    }
}
