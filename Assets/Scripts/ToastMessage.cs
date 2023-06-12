using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToastMessage : MonoBehaviour
{
    private TextMeshProUGUI message;
    
    void Awake()
    {
        message = GetComponent<TextMeshProUGUI>();
    }
    void OnEnable()
    {
        
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        Color color = message.color;
        color.a = 1;
        message.color = color;
        yield return new WaitForSeconds(1.7f); 
        for(float i = 1; i > 0; i -= 0.01f)
        {
            color.a = i;
            message.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.SetActive(false);
    }
}
