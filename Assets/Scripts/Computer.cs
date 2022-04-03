using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] Sprite closed;
    [SerializeField] Sprite opened;
    [SerializeField] AudioClip sfx;
    GameManager gameManager;
    Rigidbody2D rb;
    Collider2D collide;
    Camera cam;
    bool selected;
    bool zoom;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collide = GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
        cam = Camera.main;
        rb.velocity = Vector2.up * -force;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
        gameManager.propertyTax = gameObject;
    }
    private void Update()
    {
        if (selected == true && gameManager.pause == false)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 50;
            transform.position = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y);
            transform.rotation = Quaternion.identity;
            gameManager.holdingProperty = true;
        }
        else
        {
            gameManager.holdingProperty = false;
        }
        if (zoom == true && gameManager.pause == false)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 50;
            rb.velocity = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector2(2, 2);
            collide.isTrigger = true;
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            collide.isTrigger = false;
        }
        if (zoom == false && selected == false)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 7;
        }
        if (Input.GetKeyDown(KeyCode.P) && selected == false)
        {
            transform.position = new Vector2(0, 0);
        }
    }
    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().sprite = opened;

        if (Input.GetMouseButtonDown(0) && zoom == false && gameManager.pause == false)
        {
            selected = !selected;
            rb.velocity = new Vector3(0, 0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            zoom = false;
        }
        if (Input.GetMouseButtonDown(1) && selected == false && gameManager.pause == false)
        {
            Debug.Log("mouse2");
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            zoom = !zoom;
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector2(0, 0);
        }
    }
    private void OnMouseEnter()
    {
        GetComponent<AudioSource>().PlayOneShot(sfx);
    }
    private void OnMouseExit()
    {
        if (zoom == false && gameManager.pause == false)
        {
            GetComponent<SpriteRenderer>().sprite = closed;
        }

    }
    public void ReceiveInputProperty(string i)
    {
        gameManager.propertyValue = int.Parse(i);
    }
}
