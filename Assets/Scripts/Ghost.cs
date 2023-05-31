using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float appearanceDuration = 4;
    public float minWaitTime = 2;
    public float maxWaitTime = 6;
    public float minX = -130;
    public float maxX = 30;
    public float minY = 3.5f;
    public float maxY = 25;
    public float minZ = -80;
    public float maxZ = 130;
    Vector3 startPosition;
    float timer = 0;
    float waitTime;
    float timeLimit;
    bool on = false;
    AudioSource audioSource;
    public float fadeTime = 1;
    float maxFade = .8f;
    SpriteRenderer spriteRenderer;
    Color color;
    Vector3 direction;
    float movementSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        timeLimit = waitTime;
        Calibrate();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeLimit)
        {
            on = !on;
            if(on == true)
            {
                Appear();
            }
            else
            {
                Disappear();
            }
        }
        transform.position += direction * movementSpeed * Time.deltaTime;
    }
    void Appear()
    {
        timeLimit = appearanceDuration;
        audioSource.PlayDelayed(fadeTime);
        StartCoroutine(FadeIn());
        transform.position = startPosition;

    }
    void Disappear()
    {
        timeLimit = waitTime;
    }
    void Calibrate()
    {
        direction = new Vector3(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1)).normalized;
        startPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
    }
    IEnumerator FadeIn()
    {
        while (spriteRenderer.color.a <= maxFade)
        {
            color = spriteRenderer.color;
            color.a += Time.deltaTime / fadeTime * maxFade;
            spriteRenderer.color = color;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator FadeOut()
    {
        while (spriteRenderer.color.a >= 0)
        {
            color = spriteRenderer.color;
            color.a -= Time.deltaTime / fadeTime * maxFade;
            spriteRenderer.color = color;
            yield return new WaitForEndOfFrame();
        }
    }
}
