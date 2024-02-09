using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coverDetect1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("cover")) FindAnyObjectByType<Audios>().sfxButtonPlay("cover");
    }
}
