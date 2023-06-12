using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    [SerializeField] private int healingHp;
    [SerializeField] private TextMeshProUGUI countText;
    private Slider hpPotion;
    private bool healCool;
    private WaitForSeconds healCoolDel = new WaitForSeconds(1f);
    int count;

    private void Awake()
    {
        hpPotion = GetComponent<Slider>();
        countText.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        if(healCool) StartCoroutine(HealCool());
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) && !healCool)
        {
            healCool = true;
            count = (int)hpPotion.maxValue;
            countText.gameObject.SetActive(true);
            StartCoroutine(HealCool());
            PlayerController.Instance.Heal(healingHp);
        }
    }


    IEnumerator HealCool()
    {
        for (; count > 0; count--)
        {
            countText.text = $"{count}";
            hpPotion.value = count;
            yield return healCoolDel;
        }
        hpPotion.value = 0;
        countText.gameObject.SetActive(false);
        healCool = false;
    }
}
