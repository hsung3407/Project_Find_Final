using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WarnigImg : MonoBehaviour
{
    private WaitForSeconds fadeTime = new WaitForSeconds(0.01f);
    private WaitForSeconds fadeDelay = new WaitForSeconds(0.2f);

    [SerializeField] private SpriteRenderer img;

    private void Awake()
    {  
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        StartCoroutine(FadeEffect());
    }

    IEnumerator FadeEffect()
    {
        float i;
        Color color = img.color;
        for(i = 0; i < 0.4; i += 0.01f)
        {
            color.a = i;
            img.color = color;
            yield return fadeTime;
        }
        yield return fadeDelay;
        for(; i >= 0; i -= 0.01f)
        {
            color.a = i;
            img.color = color;
            yield return fadeTime;
        }
        gameObject.SetActive(false);
    }
}
