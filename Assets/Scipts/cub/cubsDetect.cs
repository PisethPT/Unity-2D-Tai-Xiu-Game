using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubsDetect : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("cub2")) FindAnyObjectByType<Audios>().sfxButtonPlay("roulette");
        if (collision.gameObject.CompareTag("cub3")) FindAnyObjectByType<Audios>().sfxButtonPlay("roulette");

    }
}
