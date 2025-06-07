using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ReelList
{
    public List<Symbols> symbols = new List<Symbols>();
}
public class SlotMachineManager : MonoBehaviour
{
    public List<ReelManager> Reels = new List<ReelManager>();
    public List<ReelList> ReelList = new List<ReelList>();
    public Button spinButton;
    public Vector2 spinDurationRange;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Spin all the reels in the slot machine
    public void Spin()
    {
        float spinDuration = UnityEngine.Random.Range(spinDurationRange.x, spinDurationRange.y);
        for (int i = 0;i < Reels.Count;)
        {
            float reelSpinDuration = spinDuration + (i * 0.3f);
            Reels[i].spinDuration = reelSpinDuration;            
            Reels[i].slotMachineManager = this;
            Reels[i].reelIndex = i;
            Reels[i].Spin();
            Reels[i].ResetSymbolColors();
            Reels[i].isSpinning = true;
            Reels[i].isStopped = false;

            i++;
        }
        // Clear the symbols in each reel list to prepare for the new spin
        foreach (ReelList item in ReelList)
        {
            item.symbols.Clear();
        }

        LeanTween.delayedCall(0.7f,()=> AudioManager.Instance.PlayAudio("loop"));

        spinButton.interactable = false;
        Debug.Log("Slot Machine Spin Started");
    }

    // Optional method to match and highlight matching rows in the slot machine
    public void MatchRows()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++) {
                if (j < 4 &&
                    ReelList[j].symbols[i].currentPos == ReelList[j + 1].symbols[i].currentPos &&
                    ReelList[j].symbols[i].slotSymbol == ReelList[j + 1].symbols[i].slotSymbol 
                    )
                {
                    ReelList[j].symbols[i].MatchedEffect(true);
                    ReelList[j + 1].symbols[i].MatchedEffect(true);
                }
            }
        }
        spinButton.interactable = true;
    }

}
