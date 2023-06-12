using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    Image fadeImage;

    void Awake()
    {
        fadeImage = GetComponent<Image>();
    }
    
    public void FadeOutC()
    {
        fadeImage.color = Color.black;
        StartCoroutine(FadeOut());
    }
    
    public void FadeInC()
    {
        fadeImage.color = Color.black;
        StartCoroutine(FadeIn());
    }

    public void Black()
    {
        fadeImage.color = Color.black;
    }


    IEnumerator FadeOut()
    {
        Color con = fadeImage.color;
        for(float i = 1; i > 0; i -= 0.01f)
        {
            con.a = i;
            fadeImage.color = con;
            yield return null;
        }
    }
    
    IEnumerator FadeIn()
    {
        Color con = fadeImage.color;
        for(float i = 0; i < 1; i += 0.01f)
        {
            con.a = i;
            fadeImage.color = con;
            yield return null;
        }
    }
    
    public void WFadeOutC()
    {
        fadeImage.color = Color.white;
        StartCoroutine(WFadeOut());
    }
    
    public void WFadeInC()
    {
        fadeImage.color = Color.white;
        StartCoroutine(WFadeIn());
    }


    IEnumerator WFadeOut()
    {
        Color con = fadeImage.color;
        for(float i = 1; i > 0; i -= 0.01f)
        {
            con.a = i;
            fadeImage.color = con;
            yield return null;
        }
    }
    
    IEnumerator WFadeIn()
    {
        Color con = fadeImage.color;
        for(float i = 0; i < 1; i += 0.01f)
        {
            con.a = i;
            fadeImage.color = con;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
