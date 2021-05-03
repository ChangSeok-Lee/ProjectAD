using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveObject
{
    public int slotNum;
    public int chapter;
    public int stage;
    public int clearedChapter;
    public int clearedStage;
    public int currentSkin;
    public bool isInStage;
    public bool[] unLockedSkin;
    public bool PlayedIntro;

    public SaveObject(int slotNum)
    {
        this.slotNum = slotNum;
        this.chapter = 1;
        this.stage = 1;
        this.clearedChapter = 1;
        this.clearedStage = 1;
        this.currentSkin = 0;
        this.isInStage = false;
        this.unLockedSkin = new bool[5];
        this.PlayedIntro = false;
    }
}
