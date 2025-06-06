using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public SlotItem slotItem; // The item that this GameObject represents
    public Image icon;
    public GameObject effectUI;
    public TextMeshProUGUI specialNumber;

    [Space(10)]
    public int currentPos;
    public int nextPos;
    public Animator anim;
    private void Start()
    {
        ShowSpecialNumber();
    }
    public void ShowSpecialNumber()
    {
        currentPos = (int)transform.localPosition.y;
        if (slotItem.ID == 10)
        {
            icon.gameObject.SetActive(false);
            specialNumber.gameObject.SetActive(true);
            specialNumber.text = Random.Range(150, 301).ToString();
        }
        else
        {
            icon.gameObject.SetActive(true);
            specialNumber.gameObject.SetActive(false);
            icon.sprite = slotItem.icon;
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
