using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameOptionUIManager : MonoBehaviourUI
{
    bool init= false;
    //private void Awake()
    //{
    //    AddBtnEvent("OptionUI2/Image/ReturnToGame", ReturnToGame);
    //    AddBtnEvent("OptionUI2/Image/Restart", Restart);
    //    AddBtnEvent("OptionUI2/Image/ReturnToSelectStage", ReturnToSelectStage);
    //    AddBtnEvent("OptionUI2/Image/ReturnToMain", ReturnToMain);
    //    AddBtnEvent("OptionUI2/Image/Option", Option);
    //    AddBtnEvent("OptionUI2/Image/ExitGame", ExitGame);
    //}
    private void Start()
    {
        Debug.Log("IngameOptionUIManager");
        AddBtnEvent("OptionUI2/Image/ReturnToGame", ReturnToGame);
        AddBtnEvent("OptionUI2/Image/Restart", Restart);
        AddBtnEvent("OptionUI2/Image/ReturnToSelectStage", ReturnToSelectStage);
        AddBtnEvent("OptionUI2/Image/ReturnToMain", ReturnToMain);
        AddBtnEvent("OptionUI2/Image/Option", Option);
        AddBtnEvent("OptionUI2/Image/ExitGame", ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("key down esc");
            if ( Time.timeScale>= 0.9f &&Time.timeScale <= 1.1f)
            {
                Time.timeScale = 0.0f;
            }
            else if (Time.timeScale == 0f) {
                Time.timeScale = 1.0f;
            }
            SetUI(transform.Find("OptionUI2").gameObject);
           
        }
        
    }

    public void ReturnToGame() 
    {
        Time.timeScale = 1.0f;
        SetUI(transform.Find("OptionUI2").gameObject);
    }
    public void Restart() 
    {
        Time.timeScale = 1.0f;
        FindManager();
        SetUI(transform.Find("OptionUI2").gameObject);
    }
    private void FindManager() 
    {
        GameObject.Find("Manager").GetComponent<PlayerControl_Manager>().Restart();
    }


    public void ReturnToSelectStage() 
    {
        Time.timeScale = 1.0f;
        SceneChangeManager.Instance.SceneChange("SelectStage");
    
    }
    public void ReturnToMain() {
        Time.timeScale = 1.0f;
        Destroy(GameObject.Find("SaveLoadManager").gameObject);
        SceneChangeManager.Instance.SceneChange("Lobby");
    
    }
    public void Option() 
    {
        SetUI(GameObject.Find("UI").gameObject.transform.Find("OptionUI").gameObject);
    }
    public void ExitGame() {
        Application.Quit();
    }

}
