using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] Sprite closed;
    [SerializeField] Sprite opened;
    [SerializeField] AudioClip sfx;
    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().sprite = opened;
    }
    private void OnMouseEnter()
    {
        GetComponent<AudioSource>().PlayOneShot(sfx);
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = closed;
    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene(1);
    }
}
