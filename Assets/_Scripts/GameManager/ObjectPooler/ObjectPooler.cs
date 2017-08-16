using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler current;

    public GameObject pooledObject;
    public int pooledAmount = 0;
    public bool poolWillGrow = true;

    List<GameObject> pooledObjects;

	// Use this for initialization
	void Start ()
    {
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
	}

    //public GameObject GetPooledObject(){}
}
