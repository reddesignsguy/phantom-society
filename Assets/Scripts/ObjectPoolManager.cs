using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    public static GameObject spawnObject(GameObject objectToSpawn, Vector3 spawnPos, Quaternion spawnRot)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.lookup == objectToSpawn.name);

        // Edge: Game object to be spawned does not have a pool, so create one for it
        if (pool == null)
        {
            pool = new PooledObjectInfo() { lookup = objectToSpawn.name};
            ObjectPools.Add(pool);
        }

        // Fetch any inactive object in the pool
        GameObject spawnableObj = null;
        spawnableObj = pool.InactiveObjects.FirstOrDefault();

        // No inactive obj found
        if (spawnableObj == null)
        {
            spawnableObj = Instantiate(objectToSpawn, spawnPos, spawnRot);
        } else // Inactive obj found, so reuse it
        {
            spawnableObj.transform.position = spawnPos;
            spawnableObj.transform.rotation = spawnRot;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void returnObjectToPool(GameObject obj)
    {
        // Edge: Cloned objects have the string "(Clone)" to their names
        string name = obj.name.Replace("(Clone)", string.Empty);

        PooledObjectInfo pool = ObjectPools.Find(p => p.lookup == name);

        if (pool == null)
        {
            Debug.Log("Trying to release an object that is not pooled");
        } else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }
}


public class PooledObjectInfo
{
    public string lookup;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
