using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overdue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(transform.localScale.x < 2)
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
