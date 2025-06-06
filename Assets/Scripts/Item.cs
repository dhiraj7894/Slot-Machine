using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public SlotItem slotItem; // The item that this GameObject represents
    public Image icon;
    public TextMeshProUGUI specialNumber;
    private void Start()
    {
        ShowSpecialNumber();
    }
    public void ShowSpecialNumber()
    {
        if (slotItem.ID == 5)
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
}
