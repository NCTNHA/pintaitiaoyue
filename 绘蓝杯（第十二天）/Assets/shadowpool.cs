using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowpool : MonoBehaviour
{
    public static shadowpool instance;
    public GameObject shadow;
    public int sum=10;
    private Queue<GameObject> availableobjects = new Queue<GameObject>();
    void Awake()
    {
        instance = this;

        Fillpool();
    }
    public void Fillpool()
    {
        for(int i=0; i<sum;i++)
        {
            var newShadow=Instantiate(shadow);
            newShadow.transform.SetParent(transform);

            Returnpow(newShadow);
        }
    }
    public void Returnpow(GameObject gameObject)
    {
        gameObject.SetActive(false);
        availableobjects.Enqueue(gameObject);
    }
    public GameObject GetFrompool()
    {
        if (availableobjects.Count == 0)
        {
            Fillpool();
        }
        var outShadow = availableobjects.Dequeue();
        outShadow.SetActive(true);
        return outShadow;
    }
}
