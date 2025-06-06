using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReelManager : MonoBehaviour
{
    [HideInInspector] public int reelIndex; // Index of the reel
    public SlotMachineManager slotMachineManager; // Reference to the SlotMachineManager
    public List<SlotItem> slotItemsData = new List<SlotItem>(); // List to hold the slot item data
    public List<Item> slotItems = new List<Item>(); // Array to hold the reel GameObjects
    [SerializeField] private float[] itemArrayPosition;   

    public bool isSpinning = false; // Flag to check if the reel is currently spinning
    public bool isStopped = true;
    public float spinSpeed = 5f; // Speed of the spinning

    [HideInInspector]public float spinDuration = 2f; // Duration for which the reel spins
    private void Start()
    {
        PopulateSlotItem();
    }

    private float currentSpeed = 0f; // Current speed of the reel

    public void Update()
    {
        SpinLogic();
    }

    public void SpinLogic()
    {
        for (int i = 0; i < slotItems.Count; i++)
        {
            if (slotItems[i].transform.localPosition.y != slotItems[i].nextPos)
            {
                // Move the item towards the next position based on the current speed
                float step = (currentSpeed * 1000) * Time.deltaTime; // Calculate the step size based on the current speed
                slotItems[i].transform.localPosition = Vector3.MoveTowards(slotItems[i].transform.localPosition, new Vector3(slotItems[i].transform.localPosition.x, slotItems[i].nextPos, slotItems[i].transform.localPosition.z), step);
            }
            else
            {
                if (!isSpinning && !isStopped)
                {
                    currentSpeed = 0;
                    for (int j = 0; j < slotItems.Count; j++)
                    {    
                        slotItems[j].PlayAnim("Bounce");
                    }

                    slotMachineManager.reelsStopped[reelIndex] = true;
                    isStopped = true;
                    StoppingProcess();
                    return;
                }

                slotItems[i].currentPos = (int)slotItems[i].transform.localPosition.y;
                GetNextPos(slotItems[i].currentPos, slotItems[i]);
            }
        }
    }



    public void StoppingProcess()
    {
        foreach (Item item in slotItems)
        {
            if (item.nextPos == 140 || item.nextPos == 405 || item.nextPos == 670)
            {
                if(!slotMachineManager.ReelList[reelIndex].symbols.Contains(item)) slotMachineManager.ReelList[reelIndex].symbols.Add(item); // Add the reel to the ReelList
            }
        }
        if(reelIndex == slotMachineManager.Reels.Count - 1 )
        {
            Debug.Log($"{this.name} => All symbols Stopped");
            LeanTween.delayedCall(.1f, () => slotMachineManager.MatchRows());
            return;
        }
    }

    public void PopulateSlotItem()
    {
        foreach (Item item in slotItems)
        {
            item.slotItem = slotItemsData[Random.Range(0, slotItemsData.Count - 1)]; // Assign a random item from the slotItemsData list
            item.ShowSpecialNumber(); // Show the special number if applicable
            GetNextPos(item.currentPos, item);
        }
    }




    public void Spin()
    {
        if (isSpinning) return;        
        LeanTween.value(gameObject, 0, -spinSpeed / 8, 0.01f)
            .setEaseInSine()
            .setOnUpdate((float value) => currentSpeed = value)
            .setOnComplete(() =>
            {
                LeanTween.value(gameObject, currentSpeed, spinSpeed, 1f)
                    .setEaseInCubic()
                    .setOnUpdate((float value) => currentSpeed = value);
            });
        LeanTween.delayedCall(gameObject, spinDuration / 2, () =>
        {
            foreach (Item item in slotItems)
            {
                item.slotItem = slotItemsData[Random.Range(0, slotItemsData.Count - 1)]; // Assign a random item from the slotItemsData list
                item.ShowSpecialNumber(); // Show the special number if applicable                
            }
        });
        //Debug.Log("Spin Duration: " + spinDuration);
        LeanTween.delayedCall(gameObject, spinDuration, () =>
        {
            isSpinning = false;
        });
    }

    public void ResetSymbolColors()
    {
        foreach (Item item in slotItems)
        {
            item.MatchedEffect(false);
        }
    }

    public void GetNextPos(int currentPos, Item sItem)
    {
        foreach (int item in itemArrayPosition)
        {
            if(item == currentPos)
            {
                int index = itemArrayPosition.ToList().IndexOf(item);
                if(index == 0)
                {
                    sItem.transform.localPosition = new Vector3(sItem.transform.localPosition.x, itemArrayPosition[itemArrayPosition.Length - 1], sItem.transform.localPosition.z);
                    sItem.currentPos = (int)itemArrayPosition[itemArrayPosition.Length - 1];
                    sItem.nextPos = (int)itemArrayPosition[itemArrayPosition.Length - 2];
                }
                else if (index > 0)
                {
                    sItem.nextPos = (int)itemArrayPosition[index - 1];
                }               
            }
        }
    }
}
