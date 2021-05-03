using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//불러오기 화면의 UI이벤트를 담당, 씬이 전환되면 SaveLoadManager를 통해 정보를 받아오고 ui를 만든다 
public class SaveLoadUiManager : MonoBehaviourUI
{
    private void Awake()
    {
        init();
    }
    private void init()
    {
        for(int i = 0; i < 3; i++) 
        {
            MakeUI(i);
        }
    }
  

    /// <summary>
    /// 스테이지 선택화면으로 넘어가는 함수(스테이지 선택화면으로 넘어간 후 GameDataManager에서 정보를 받아온다)
    /// </summary>
    private void StageSelectEvent(int slotNum)
    {
        //데모버전은 바로 1스테이지로 가도록 구현
        //저장데이터와 연결해서 사용하도록 수정 --- 저장데이터의 스테이지 기록을 이용하여 stage 선택 UI를 그릴수 있도록 구현
        GameDataManager.gameData = SaveLoadManager.Instance.Load(slotNum);
        if (!GameDataManager.gameData.PlayedIntro) {
            SceneChangeManager.Instance.SceneChange("Intro");
            GameDataManager.gameData.PlayedIntro = true;
            return;
        }
        SceneChangeManager.Instance.SceneChange("SelectStage");
    }
    private void StageSelectBox1() 
    { StageSelectEvent(1); }
    private void StageSelectBox2()
    { StageSelectEvent(2); }
    private void StageSelectBox3()
    { StageSelectEvent(3); }


    /// <summary>
    /// 해당 저장 슬롯을 비우는 함수 해당 세이브 파일을 삭제한다
    /// </summary>
    /// <param name="num">슬롯 번호</param>
    private void DeleteSlot(int slotNum)
    {
        SaveLoadManager.Instance.DeleteFile(slotNum);
        SaveLoadManager.Instance.SaveFileIndex[slotNum - 1] = false;
        MakeUI(slotNum - 1);
    }
    private void DeleteSaveFileBox1()
    {
        DeleteSlot(1);
    }
    private void DeleteSaveFileBox2()
    {
        DeleteSlot(2);
    }
    private void DeleteSaveFileBox3()
    {
        DeleteSlot(3);
    }

    /// <summary>
    /// 슬롯에 새로운 세이브파일 추가하고 UI를 새로 그려주는 함수, 새로운 세이브파일을 만든다
    /// </summary>
    /// <param name="slotNum">슬롯 번호</param>
    private void MakeNewSaveFile(int slotNum)
    {
        GameDataManager.gameData = new SaveObject(slotNum);
        SaveLoadManager.Instance.Save(slotNum);
        SaveLoadManager.Instance.SaveFileIndex[slotNum-1] = true;
        MakeUI(slotNum-1);
    }

    private void MakeSaveFileBox1() 
    {
        MakeNewSaveFile(1);
    }
    private void MakeSaveFileBox2()
    {
        MakeNewSaveFile(2);
    }
    private void MakeSaveFileBox3()
    {
        MakeNewSaveFile(3);
    }


    private void MakeUI(int slotNum)
    {
        // 활성화슬롯 on
        if (SaveLoadManager.Instance.SaveFileIndex[slotNum])
        { 
            transform.Find("slot" + (slotNum + 1) + "/Save_Slot" + (slotNum + 1)).gameObject.SetActive(true);
            transform.Find("slot" + (slotNum + 1) + "/Save_Slot_Empty" + (slotNum + 1)).gameObject.SetActive(false);
            AddActivateSlotEvent(slotNum);
        }
        //empty slot 활성화
        else
        { 
            transform.Find("slot" + (slotNum + 1) + "/Save_Slot_Empty" + (slotNum + 1)).gameObject.SetActive(true);
            transform.Find("slot" + (slotNum + 1) + "/Save_Slot" + (slotNum + 1)).gameObject.SetActive(false);
            AddEmptySlotEvent(slotNum);
        }
    }


    /// <summary>
    /// 활성화 된 슬롯의 각 버튼들을 활성화(삭제버튼, 스타트버튼)
    /// </summary>
    private void AddActivateSlotEvent(int slotNum) 
    {
        switch (slotNum)
        {
            case 0:
                AddBtnEvent($"slot{slotNum + 1}/Save_Slot{slotNum + 1}/StartButton", StageSelectBox1);
                AddBtnEvent($"slot{slotNum + 1}/Save_Slot{slotNum + 1}/DeleteButton", DeleteSaveFileBox1);
                break;
            case 1:
                AddBtnEvent($"slot{slotNum + 1}/Save_Slot{slotNum + 1}/StartButton", StageSelectBox2);
                AddBtnEvent($"slot{slotNum + 1}/Save_Slot{slotNum + 1}/DeleteButton", DeleteSaveFileBox2);
                break;
            case 2:
                AddBtnEvent($"slot{slotNum + 1}/Save_Slot{slotNum + 1}/StartButton", StageSelectBox3);
                AddBtnEvent($"slot{slotNum + 1}/Save_Slot{slotNum + 1}/DeleteButton", DeleteSaveFileBox3);
                break;
        }
    }


    /// <summary>
    /// 비어있는 슬롯의 세이브파일 추가 버튼들을 활성화
    /// </summary>
    private void AddEmptySlotEvent(int slotNum)
    {
        switch (slotNum) 
        {
            case 0:
                AddBtnEvent($"slot{slotNum + 1}/Save_Slot_Empty{slotNum + 1}/Slot_Background", MakeSaveFileBox1);
                break;
            case 1:
                AddBtnEvent($"slot{slotNum + 1}/Save_Slot_Empty{slotNum + 1}/Slot_Background", MakeSaveFileBox2);
                break;
            case 2:
                AddBtnEvent($"slot{slotNum + 1}/Save_Slot_Empty{slotNum + 1}/Slot_Background", MakeSaveFileBox3);
                break;
        }
       
    }

   
}
