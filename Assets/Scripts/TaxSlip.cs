using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaxSlip : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float force;
    [SerializeField] AudioClip soundClip;
    [SerializeField] GameObject iField1;
    [SerializeField] GameObject iField2;
    [SerializeField] InputField inpt1;
    [SerializeField] InputField inpt2;
    [SerializeField] GameObject fakeIncome;
    [SerializeField] GameObject fakeWriteoff;
    [SerializeField] GameManager gameManager;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject overdue;
    [SerializeField] GameObject submitted;
    BoxCollider2D collide;
    AudioSource audioSrc;
    Rigidbody2D rb;
    bool selected = false;
    bool zoom = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>();
        collide = GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
        inpt1 = iField1.GetComponent<InputField>();
        inpt2 = iField1.GetComponent<InputField>();
        cam = Camera.main;
        audioSrc.PlayOneShot(soundClip);
        rb.velocity = Vector2.up * -force;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
        gameManager.tax = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.income > 0 && zoom == false && gameManager.pause == false)
        {
            fakeIncome.SetActive(true);
        }
        else
        {
            fakeIncome.SetActive(false);
        }
        if (gameManager.writeoff > 0 && zoom == false && gameManager.pause == false)
        {
            fakeWriteoff.SetActive(true);
        }
        else
        {
            fakeWriteoff.SetActive(false);
        }
        if (selected == true && gameManager.pause == false)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 50;
            canvas.sortingOrder = 51;
            transform.position = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y);
            transform.rotation = Quaternion.identity;
            gameManager.holdingTax = true;
        }
        else
        {
            gameManager.holdingTax = false;
        }
        if(zoom == true && gameManager.pause == false)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 50;
            canvas.sortingOrder = 51;
            rb.velocity = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector2(2, 2);
            collide.isTrigger = true;
            iField1.SetActive(true);
            iField2.SetActive(true);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            collide.isTrigger = false;
            iField1.SetActive(false);
            iField2.SetActive(false);
        }
        if(zoom == false && selected == false)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 5;
            canvas.sortingOrder = 6;
        }
        if (Input.GetKeyDown(KeyCode.T) && selected == false)
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
    private void FixedUpdate()
    {
        if(rb.velocity.x > 0)
        {
            rb.velocity -= new Vector2(0.1f, 0);
        }
        if (rb.velocity.y > 0)
        {
            rb.velocity -= new Vector2(0, 0.1f);
        }
        if (rb.velocity.x < 0)
        {
            rb.velocity += new Vector2(0.1f, 0);
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += new Vector2(0, 0.1f);
        }
    }
    public void ReceiveInputIncome(string i)
    {
        gameManager.income = int.Parse(i);
    }
    public void ReceiveInputWriteoff(string i)
    {
        gameManager.writeoff = int.Parse(i);
    }
    public void Overdue()
    {
        gameManager.tax = null;
        Destroy(gameObject);
        Instantiate(overdue, transform.position, Quaternion.identity);
    }
    public void Submit()
    {
        gameManager.taxSubmitted = true;
        gameManager.tax = null;
        Destroy(gameObject);
        Instantiate(submitted, transform.position, Quaternion.identity);
    }
}
