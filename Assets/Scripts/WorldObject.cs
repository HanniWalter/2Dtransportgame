using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    public WorldData worldData;

    public void Awake()
    {
        worldData = FindObjectOfType<WorldData>();
        gameObject.transform.parent = worldData.gameObject.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
