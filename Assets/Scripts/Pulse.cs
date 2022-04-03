using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pulse : MonoBehaviour
{
    bool enlarge;
    private void FixedUpdate()
    {
        if(transform.localScale.x == 1)
        {
            if(transform.localScale.x < 2)
            {
                transform.localScale += new Vector3(0.01f, 0.01f, 0);
            }
 
        }
        if(transform.localScale.x == 2)
        {
            if (transform.localScale.x > 1)
            {
                transform.localScale -= new Vector3(0.01f, 0.01f, 0);
            }
        }

    }
}
