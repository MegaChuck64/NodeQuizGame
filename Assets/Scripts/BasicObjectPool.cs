using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObjectPool : MonoBehaviour
{
    public GameObject prefab;

    private Stack<GameObject> inactiveInstances = new Stack<GameObject>();


    public GameObject GetObject()
    {
        GameObject spawned;
        if (inactiveInstances.Count > 0)
        {
            spawned = inactiveInstances.Pop();
        }
        else
        {
            spawned = Instantiate<GameObject>(prefab);

            PooledObject pooledObject = spawned.AddComponent<PooledObject>();
            pooledObject.pool = this;
        }

        spawned.SetActive(true);

        return spawned;
    }

    public void ReturnObject(GameObject toReturn)
    {
        PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

        if (pooledObject && pooledObject.pool == this)
        {
            toReturn.SetActive(false);
            inactiveInstances.Push(toReturn);
        }
        else
        {
            Destroy(toReturn);
        }
    }

}


public class PooledObject : MonoBehaviour
{
    public BasicObjectPool pool;

}
