using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Symbols : MonoBehaviour
{
    public SlotSymbols slotSymbol; 
    public Image icon;
    public GameObject effectUI;
    public TextMeshProUGUI specialNumber;
    public Animator anim;

    [Space(10)]
    public int currentPos;
    public int nextPos;

    // Based on the slot symbol ScriptableObject, check if the symbol is a special symbol if yes then show the special number else hide it
    public void ShowSpecialNumber()
    {
        currentPos = (int)transform.localPosition.y;
        if (slotSymbol.ID == 10)
        {
            icon.sprite = slotSymbol.icon;
            specialNumber.gameObject.SetActive(true);
            specialNumber.text = Random.Range(150, 301).ToString();
            icon.transform.localScale = new Vector3(1.3f, 1.3f, 1f);
        }
        else
        {
            specialNumber.gameObject.SetActive(false);
            icon.sprite = slotSymbol.icon;
            icon.transform.localScale = Vector3.one;
        }
    }


    public void MatchedEffect(bool isTrue)
    {
        effectUI.SetActive(isTrue);
    }


    public void PlayAnim(string animString)
    {        
        if (nextPos == 140 || nextPos == 405 || nextPos == 670)
            anim.Play(animString);
    }
}
