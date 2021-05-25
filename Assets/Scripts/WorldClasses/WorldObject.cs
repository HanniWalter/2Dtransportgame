using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldObject : MonoBehaviour
{
    public WorldData worldData;

    public void Awake()
    {
        worldData = FindObjectOfType<WorldData>();
        gameObject.transform.parent = worldData.gameObject.transform;
    }

    public Vector2 location{
        get{
            return Util.toV2(transform.position);
        }
        set{
            transform.position = Util.toV3(value);
        }
    }
    public float direction = 0f;

    public virtual string getInterface(){
        return this.GetType().ToString();
    }
}
