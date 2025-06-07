using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReelManager : MonoBehaviour
{
    [HideInInspector] public int reelIndex; 
    public SlotMachineManager slotMachineManager; 
    public List<SlotSymbols> slotItemsData = new List<SlotSymbols>(); 
    public List<Symbols> slotItems = new List<Symbols>(); 
    [SerializeField] private float[] itemArrayPosition;
    
    [Header("Spin Control"),Space(5)]
    public bool isSpinning = false; 
    public bool isStopped = true;
    public float spinSpeed = 5f; 

    [HideInInspector]public float spinDuration = 2f; 
    private void Start()
    {
        PopulateSlotItem();
    }

    private float currentSpeed = 0f; 

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
                float step = (currentSpeed * 1000) * Time.deltaTime; 
                Vector3 targetPos = new Vector3(slotItems[i].transform.localPosition.x, slotItems[i].nextPos, slotItems[i].transform.localPosition.z);
                slotItems[i].transform.localPosition = Vector3.MoveTowards(slotItems[i].transform.localPosition, targetPos, step);
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

                    isStopped = true; 

                    StoppingProcess();
                    return;
                }
                // If the item has reached its next position, update its current position and get the next position
                slotItems[i].currentPos = (int)slotItems[i].transform.localPosition.y;
                GetNextPos(slotItems[i].currentPos, slotItems[i]);
            }
        }
    }



    public void StoppingProcess()
    {
        foreach (Symbols item in slotItems)
        {
            if (item.nextPos == 140 || item.nextPos == 405 || item.nextPos == 670)
            {
  
                if (!slotMachineManager.ReelList[reelIndex].symbols.Contains(item)) slotMachineManager.ReelList[reelIndex].symbols.Add(item); 
            }
        }

        // Sort the symbols in the ReelList based on their current position
        slotMachineManager.ReelList[reelIndex].symbols.Sort((a, b) => a.currentPos.CompareTo(b.currentPos));


    
        if (reelIndex != 4) AudioManager.Instance.PlayAudio("stop");

        // Check if all reels have stopped spinning
        if (reelIndex == slotMachineManager.Reels.Count - 1 )
        {
            Debug.Log("Slot Machine Spin Stopped");
            LeanTween.delayedCall(.1f, () => slotMachineManager.MatchRows());
            return;
        }
    }

    public void PopulateSlotItem()
    {
        
        foreach (Symbols item in slotItems)
        {
            item.slotSymbol = slotItemsData[Random.Range(0, slotItemsData.Count)]; 
            item.ShowSpecialNumber(); 
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
                // After the initial bounce, start spinning in the forward direction
                LeanTween.value(gameObject, currentSpeed, spinSpeed, 1f)
                    .setEaseInCubic()
                    .setOnUpdate((float value) => currentSpeed = value);
            });

        // Now refresh the symbols with new random items
        LeanTween.delayedCall(gameObject, spinDuration / 2, () =>
        {
            foreach (Symbols item in slotItems)
            {
                item.slotSymbol = slotItemsData[Random.Range(0, slotItemsData.Count - 1)]; 
                item.ShowSpecialNumber();             
            }
        });

        // After the spin duration, stop the spinning
        LeanTween.delayedCall(gameObject, spinDuration, () =>
        {
            isSpinning = false;
        });
    }


    public void ResetSymbolColors()
    {
        foreach (Symbols item in slotItems)
        {
            item.MatchedEffect(false);
        }
    }

    public void GetNextPos(int currentPos, Symbols sItem)
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
