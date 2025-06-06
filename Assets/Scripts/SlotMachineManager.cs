using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ReelList
{
    public List<Item> symbols = new List<Item>();
}
public class SlotMachineManager : MonoBehaviour
{
    public List<ReelManager> Reels = new List<ReelManager>();
    public List<bool> reelsStopped = new List<bool>();
    public List<ReelList> ReelList = new List<ReelList>();
    public Vector2 spinDurationRange;

    private void Awake()
    {
       
    }



    public void Spin()
    {
        float spinDuration = UnityEngine.Random.Range(spinDurationRange.x, spinDurationRange.y);
        for (int i = 0;i < Reels.Count;)
        {
            float reelSpinDuration = spinDuration + (i * 0.3f);
            //Debug.Log($"Spinning Reel {i} for {reelSpinDuration} seconds");
            Reels[i].spinDuration = reelSpinDuration;            
            Reels[i].slotMachineManager = this;
            Reels[i].reelIndex = i;
            Reels[i].Spin();
            Reels[i].ResetSymbolColors();
            Reels[i].isSpinning = true;
            Reels[i].isStopped = false;

            reelsStopped[i] = false;
            i++;
        }

        foreach (ReelList item in ReelList)
        {
            item.symbols.Clear();
        }
    }

    public void MatchRows()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++) {
                if (j < 4 &&
                    ReelList[j].symbols[i].currentPos == ReelList[j + 1].symbols[i].currentPos &&
                    ReelList[j].symbols[i].slotItem == ReelList[j + 1].symbols[i].slotItem 
                    )
                {
                    ReelList[j].symbols[i].MatchedEffect(true);
                    ReelList[j + 1].symbols[i].MatchedEffect(true);
                }
            }
        }
    }

}
