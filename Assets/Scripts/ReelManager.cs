using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ReelManager : MonoBehaviour
{
    public List<SlotItem> slotItemsData = new List<SlotItem>(); // List to hold the slot item data
    public List<Item> slotItems = new List<Item>(); // Array to hold the reel GameObjects
    [SerializeField] private float[] itemArrayPosition;
    public bool isSpinning = false; // Flag to check if the reel is currently spinning
    public float spinDuration = 2f; // Duration for which the reel spins
    public float spinSpeed = 5f; // Speed of the spinning

    private void Start()
    {
        PopulateSlotItem();
    }

    private float currentSpeed = 0f; // Current speed of the reel

    public void Update()
    {
        if (isSpinning)
        {
            for (int i = 0; i < slotItems.Count; i++)
            {
                // Rotate each slot item around its own axis
                slotItems[i].transform.Translate(Vector3.down * Time.deltaTime * currentSpeed); // Move the reel downwards
                if (slotItems[i].transform.localPosition.y <= -125)
                {
                    slotItems[i].transform.localPosition = new Vector3(slotItems[i].transform.localPosition.x, 2525, slotItems[i].transform.localPosition.z); // Reset position to the top
                    slotItems[i].slotItem = slotItemsData[Random.Range(0, slotItemsData.Count - 1)];
                    slotItems[i].ShowSpecialNumber(); // Show the special number if applicable
                }
            }
        }
    }



    public void StoppingProcess()
    {
/*        int closestIndex = 0; // Index of the closest item to the stopping range
        int itemPos = 140;
        
        for (int i = 0; i < slotItems.Count; i++)
        {
            if (slotItems[i].transform.localPosition.y > 140 && slotItems[i].transform.localPosition.y < 405)
            {
                closestIndex = i; // Store the index of the item to be stopped                
                Debug.Log("Stopping Item at Index: " + closestIndex);
                break;
            }
        }
        LeanTween.moveLocalY(slotItems[closestIndex].gameObject, itemPos, 0.2f).setEaseInCubic(); // Move the item to its final position
        for (int i = closestIndex - 1; i >= 0; i--)
        {
            itemPos += 265;            
            LeanTween.moveLocalY(slotItems[i].gameObject, itemPos, 1.5f).setEaseInCubic(); // Move the item to its final position
            Debug.Log($"Item Positions {itemPos}");            
        }
        for (int i = closestIndex + 1; i < slotItems.Count; i++)
        {
            itemPos += 265;
            slotItems[i].transform.localPosition = new Vector3(slotItems[i].transform.localPosition.x, itemPos, slotItems[i].transform.localPosition.z);
   
        }

        Debug.Log("No item found in the stopping range.");*/

    }

    public void PopulateSlotItem()
    {
        foreach (Item item in slotItems)
        {
            item.slotItem = slotItemsData[Random.Range(0, slotItemsData.Count - 1)]; // Assign a random item from the slotItemsData list
            item.ShowSpecialNumber(); // Show the special number if applicable
        }
    }
    


    public void Spin()
    {
        LeanTween.value(gameObject, 0, spinSpeed, 1f).setEaseInCubic().setOnUpdate((float value) =>
        {
            currentSpeed = value; // Update the current speed
        });

        isSpinning = true; // Set the spinning flag to true
                           
        LeanTween.delayedCall(gameObject, spinDuration, () =>
        {
            StoppingProcess();
            isSpinning = false; // Set the spinning flag to false
            currentSpeed = 0f; // Reset the current speed
            //Debug.Log("Stopping Process Called");
        });
    }
}
