using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip gameOverMusic;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.onGameOver += GameOver;
        source = GetComponent<AudioSource>();
    }

    public void OnDestroy()
    {
        PlayerHealth.onGameOver -= GameOver;
    }

    void GameOver()
    {
        source.loop = false;
        source.clip = gameOverMusic;
        source.Play();
    }
}
