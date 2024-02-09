using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScript : MonoBehaviour
{
    public AudioSource backgroundSound;
    private void Start()
    {
        backgroundSound.Play();
    }

    public void CallingGamePlayerScene()
    {
        SceneManager.LoadScene("Game Player");
    }
    public void ExitApp()
    {
        Application.Quit();
    }
}
