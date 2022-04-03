using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] int slot;
    GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnMouseOver()
    {
        Debug.Log("working");
        if(Input.GetMouseButtonDown(0))
        {
            switch (gameObject.tag)
            {
                case "Plus":
                    gameManager.values[slot - 1] += 100;
                    break;
                case "Minus":
                    if (gameManager.values[slot - 1] > 0)
                    {
                        gameManager.values[slot - 1] -= 100;
                    }
                    break;
            }
            Debug.Log("Pressed");
        }
    }
}
