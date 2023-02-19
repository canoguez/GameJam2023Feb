using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxManager : MonoBehaviour
{
    ParallaxBackground parallaxBackground;

    [SerializeField]
    float balance = 0f;

    public AudioClip song1, song2;
    public RectTransform portalMask;
    public float portalScale = 2f;
    public float growSpeed = 0.5f;
    public float shrinkSpeed = 0.5f;


    private AudioSource source1,source2;

    public float transitionSpeed;

    private void Awake()
    {
        source1 = gameObject.AddComponent<AudioSource>();
        source1.clip = song1;
        source1.loop = true;
        source1.Play();
        source2 = gameObject.AddComponent<AudioSource>();
        source2.clip = song2;
        source2.loop = true;
        source2.Play();

        UpdateLevels();
        StartPortalOpen();
    }

    private void StartPortalOpen()
    {
        float portalMag = Random.Range(0.5f, 1f);
        float portalDelay = Random.Range(2f, 10f);

        StartCoroutine(OpenPortal(portalMag, portalDelay, 10f));
    }

    private void Update()
    {
        UpdateLevels();
    }

    IEnumerator OpenPortal(float magnitude, float delay = 0f, float portalOpenDuration = 0f)
    {
        yield return new WaitForSeconds(delay);

        portalMask.transform.position = DetermineRandomScreenPosition();
        portalMask.GetComponent<Image>().enabled = true;
        portalMask.transform.localScale = new Vector3(0,0,0);
        float curTime = 0f;

        while(curTime < 1f)
        {
            float curScale = Mathf.Lerp(0, magnitude, curTime);
            balance = curScale;

            curTime += Time.deltaTime * growSpeed;

            portalMask.transform.localScale = new Vector3(curScale, curScale, curScale);
            yield return null;
        }

        balance = magnitude;

        yield return new WaitForSeconds(portalOpenDuration);

        curTime = 0f;

        while (curTime < 1f)
        {
            float curScale = Mathf.Lerp(magnitude, 0, curTime);
            balance = curScale;

            curTime += Time.deltaTime * shrinkSpeed;

            portalMask.transform.localScale = new Vector3(curScale, curScale, curScale);
            yield return null;
        }

        portalMask.GetComponent<Image>().enabled = false;

        balance = 0;
        UpdateLevels();


        StartPortalOpen();
    }

    Vector2 DetermineRandomScreenPosition()
    {
        return new Vector2(Random.Range(0,1f), Random.Range(0,1f));
    }

    void UpdateLevels()
    {
        source1.volume = balance;
        source2.volume = 1-balance;
    }
}
