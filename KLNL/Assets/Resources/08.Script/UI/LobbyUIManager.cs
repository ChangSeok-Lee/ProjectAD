using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//
public class LobbyUIManager : MonoBehaviourUI
{

    string Scenename;
    private Image gameStartBtn;
    private Image optionBtn;
    private Image creditBtn;
    private Image exitBtn;

    private GameObject OptionUI;

    public static LobbyUIManager instance = null;

    private short pointer = 0;

    public enum LobbyPointer : short
    {
        startBtn = 0,
        optionBtn = 1,
        creditBtn = 2,
        exitBtn = 3

    }
    //private SceneChangeManager _instance;

    public static LobbyUIManager Instance
    {
        get { return instance; }
        set { }
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            DestroyImmediate(this);

        init();
    }

    private void Update()
    {
        if(OptionUI.activeSelf == false) { MovePointer(); }
        SelectBtn();
    }

    /// <summary>
    /// 
    /// </summary>
    public void init()
    {
        Scenename = SceneManager.GetActiveScene().name;
        Debug.Log(Scenename);
        OptionUI = GameObject.Find("UI").transform.Find("OptionUI").gameObject;
        OptionUI.AddComponent<OptionUIManager>();
        gameStartBtn = GameObject.Find("MainUI").transform.Find("GameStartButton").GetComponent<Image>();
        optionBtn = GameObject.Find("MainUI").transform.Find("OptionButton").GetComponent<Image>();
        creditBtn = GameObject.Find("MainUI").transform.Find("MakerButton").GetComponent<Image>();
        exitBtn = GameObject.Find("MainUI").transform.Find("ExitButton").GetComponent<Image>();
    }

    /// <summary>
    /// gameStart버튼 이벤트 -> 세이브파일이 있는 Scene으로 이동한다
    /// </summary>
    public void GameStart()
    {
        Debug.Log("method call");
        SceneChangeManager.Instance.SceneChange("LoadFile");
    }
    

    private void MovePointer()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            --pointer;
            if (pointer < 0) { pointer = 0; }
            CheckPointer();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ++pointer;
            if (pointer > 3) { pointer = 3; }
            CheckPointer();
        }
    }

    private void CheckPointer()
    {
        switch (pointer)
        {
            case (short)LobbyPointer.startBtn:
                gameStartBtn.sprite = Resources.Load("04.Image/UIButton/StartButton_Selected_1", typeof(Sprite)) as Sprite;
                optionBtn.sprite = Resources.Load("04.Image/UIButton/SettingButton_1", typeof(Sprite)) as Sprite;
                creditBtn.sprite = Resources.Load("04.Image/UIButton/CreditButton_1", typeof(Sprite)) as Sprite;
                exitBtn.sprite = Resources.Load("04.Image/UIButton/ExitButton_1", typeof(Sprite)) as Sprite;
                break;
            case (short)LobbyPointer.optionBtn:
                gameStartBtn.sprite = Resources.Load("04.Image/UIButton/StartButton_1", typeof(Sprite)) as Sprite;
                optionBtn.sprite = Resources.Load("04.Image/UIButton/SettingButton_Selected_1", typeof(Sprite)) as Sprite;
                creditBtn.sprite = Resources.Load("04.Image/UIButton/CreditButton_1", typeof(Sprite)) as Sprite;
                exitBtn.sprite = Resources.Load("04.Image/UIButton/ExitButton_1", typeof(Sprite)) as Sprite;
                break;
            case (short)LobbyPointer.creditBtn:
                gameStartBtn.sprite = Resources.Load("04.Image/UIButton/StartButton_1", typeof(Sprite)) as Sprite;
                optionBtn.sprite = Resources.Load("04.Image/UIButton/SettingButton_1", typeof(Sprite)) as Sprite;
                creditBtn.sprite = Resources.Load("04.Image/UIButton/CreditButton_Selected_1", typeof(Sprite)) as Sprite;
                exitBtn.sprite = Resources.Load("04.Image/UIButton/ExitButton_1", typeof(Sprite)) as Sprite;
                break;
            case (short)LobbyPointer.exitBtn:
                gameStartBtn.sprite = Resources.Load("04.Image/UIButton/StartButton_1", typeof(Sprite)) as Sprite;
                optionBtn.sprite = Resources.Load("04.Image/UIButton/SettingButton_1", typeof(Sprite)) as Sprite;
                creditBtn.sprite = Resources.Load("04.Image/UIButton/CreditButton_1", typeof(Sprite)) as Sprite;
                exitBtn.sprite = Resources.Load("04.Image/UIButton/ExitButton_Selected_1", typeof(Sprite)) as Sprite;
                break;
        }
    }

    private void SelectBtn()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (pointer)
            {
                case (short)LobbyPointer.startBtn:
                    GameStart();
                    break;
                case (short)LobbyPointer.optionBtn:
                    SetUI(OptionUI);
                    break;
                case (short)LobbyPointer.creditBtn:
                    SceneChangeManager.Instance.SceneChange("Credit");
                    break;
                case (short)LobbyPointer.exitBtn:
                    Application.Quit();
                    break;

            }
        }
    }

}
