using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Transactions;
using JetBrains.Annotations;

//세이브 로드 창에서 Awake된다.
public class SaveLoadManager : MonoBehaviour
{
    private static SaveLoadManager instance;
    public static SaveLoadManager Instance { get { return instance; } set { } }
    public bool[] SaveFileIndex= new bool[3];
    SaveObject[] saveFileList = new SaveObject[3];

    //각 챕터별 스테이지 갯수
    public int[] stage = new int[] { 6, 6, 7, 7, 8, 8, 9 };

    private void Update()
    {

    }
    void Awake() 
    {
        DontDestroyOnLoad(this);
        if (instance == null) { 
            instance = this; 
        } else 
        {
            DestroyImmediate(this);
        }
        
        init();
    }

    private void init()
    {
        if(!Directory.Exists(Application.dataPath+"/SaveFile"))
            Directory.CreateDirectory(Application.dataPath + "/SaveFile");
        for (int i = 0; i < 3; i++)
        {
            SaveFileIndex[i] = (CheckSaveFile(i + 1) ? true : false);
            Debug.Log(SaveFileIndex[i]);
            if (SaveFileIndex[i])
            {
                saveFileList[i] = Load(i + 1);
            }
        }
        GameObject.Find("Canvas/Save_Slots_UI").AddComponent<SaveLoadUiManager>();
    }

    /// <summary>
    /// 게임내에서 데이터를 자동으로 저장하는 함수
    /// </summary>
    public void Save(int slotIndex)
    {
        //각 변수에 해당하는 데이터를 받아와야함
        SaveObject saveObject = GameDataManager.gameData;

        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.dataPath + "/SaveFile/save" + slotIndex + ".txt", json);

        Debug.Log("saved!" + slotIndex);
    }

    /// <summary>
    /// save파일을 읽어 데이터를 로드하는 함수
    /// </summary>
    public SaveObject Load(int fileNum)
    {
        SaveObject saveObject = new SaveObject(fileNum);
        if (CheckSaveFile(fileNum))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/SaveFile/save" + fileNum+".txt");
            Debug.Log("Loaded: " + saveString);

            saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            //각 데이터를 필요한 곳에 뿌려주는 기능이 추가되어야 한다
        }
        else
        {
            Debug.Log("NO save");
            return null;
        }

        return saveObject;
    }

    /// <summary>
    /// 세이브 파일이 존재하는지 확인하는 함수
    /// </summary>
    private bool CheckSaveFile(int fileNum)
    {

        if (File.Exists(Application.dataPath + "/SaveFile/save" + fileNum + ".txt"))
            return true;
        else
            return false;
    }

    public void DeleteFile(int FileNum) 
    {
        Debug.Log("Delete method call");
        if (File.Exists(Application.dataPath+"/SaveFile/save" + FileNum + ".txt"))
        {
            File.Delete(Application.dataPath + "/SaveFile/save" + FileNum + ".txt");
            Debug.Log("Delete success");
        }
       
    }
    /// <summary>
    /// cleared 데이터 변경 -> 현재 스테이지를 다음 스테이지로 변경 -> 로딩화면 띄우기  or 다음 챕터 씬으로 전환
    /// </summary>
    public void ClearEvent()
    {
        if (GameDataManager.gameData.chapter == 4 && GameDataManager.gameData.stage == 7) 
        {
            SceneChangeManager.Instance.SceneChange("Lobby");
            return;
        }

        //이제 진행해야할 스테이지로 변경
        GameDataManager.gameData.stage++;

        //클리어되었다고 저장된 챕터보다 현재 클리어한 챕터가 더 크면 챕터가 바뀐 첫 스테이지를 클리어
        if (GameDataManager.gameData.clearedChapter < GameDataManager.gameData.chapter) 
        {
            GameDataManager.gameData.clearedChapter = GameDataManager.gameData.chapter;
            GameDataManager.gameData.clearedStage = 1;
        }
        //클리어되었다고 저장된 챕터와 현재 클리어한 챕터가 같고 클리어된 스테이지보다 현재 클리어한 스테이지가 더 크면 업데이트
        else if ((GameDataManager.gameData.clearedChapter==GameDataManager.gameData.chapter) &&
            (GameDataManager.gameData.clearedStage < GameDataManager.gameData.stage))
            GameDataManager.gameData.clearedStage = GameDataManager.gameData.stage;
        
        //그 외에는 업데이트할 필요 없음


        
        //다음 챕터로 넘어가야하는 경우
        if (GameDataManager.gameData.stage > stage[GameDataManager.gameData.chapter-1])
        {
            GameDataManager.gameData.chapter++;
            GameDataManager.gameData.stage = 1;
            SceneChangeManager.Instance.SceneChange("0" + GameDataManager.gameData.chapter);
            
        }
        //챕터 변경x
        else    
        { 
            SceneChangeManager.Instance.AddScene("Loading"); 
        }

        Save(GameDataManager.gameData.slotNum);

          

    }
}   


