using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuotaSlip : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] Camera cam;
    [SerializeField] Text quota;
    [SerializeField] Canvas canvas;
    BoxCollider2D collide;
    Rigidbody2D rb;
    public int quotaValue;
    bool selected = false;
    bool zoom = false;
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collide = GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
        cam = Camera.main;
        rb.velocity = Vector2.up * -force;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
        quotaValue = Random.Range(50, gameManager.taxableTotal - 1 );
        quota.text = quotaValue.ToString();
        gameManager.quota = quotaValue;
    }
    private void Update()
    {
        if (selected == true && gameManager.pause == false)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 50;
            canvas.sortingOrder = 51;
            transform.position = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y);
            transform.rotation = Quaternion.identity;
            gameManager.holdingQuota = true;
        }
        else
        {
            gameManager.holdingQuota = false;
        }
        if (zoom == true && gameManager.pause == false)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 50;
            canvas.sortingOrder = 51;
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
        if(selected == false && zoom == false)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 3;
            canvas.sortingOrder = 4;
        }
        if(Input.GetKeyDown(KeyCode.Q) && selected == false && gameManager.pause == false)
        {
            transform.position = new Vector2(0, 0);
        }
    }
    private void OnMouseOver()
    {
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

}
