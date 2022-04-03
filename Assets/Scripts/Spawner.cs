using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    public bool spawn = false;
    // Update is called once per frame
    void Update()
    {
        if(spawn == true)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            spawn = false;
            if(gameObject.tag == "Arrest")
            {
                Destroy(gameObject);
            }
        }
    }
}
