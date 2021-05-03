using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab_Change : MonoBehaviour
{
    public GameObject[] gameObjects;
    public bool clear;
    public bool All_clear;
    public int index;

    private void Awake()
    {
        SceneChangeManager.Instance.AddScene("Loading");
    }
    private void Start()
    {
        if(GameDataManager.gameData != null)
            index = GameDataManager.gameData.stage - 1;
        GameObject.Instantiate(gameObjects[index], Vector3.zero, Quaternion.identity);
        Debug.Log(gameObjects[index].name);
    }


    private void Update()
    {
        //if(clear)
        //{

        //        if (gameObjects.Length > index+1)
        //        {
        //            Destroy(GameObject.Find(gameObjects[index].name + "(Clone)"));
        //            index++;
        //            GameObject.Instantiate(gameObjects[index], Vector3.zero, Quaternion.identity);
        //            //init();
        //            Debug.Log(gameObjects[index].name);
        //        }
        //        else
        //        {
        //            index = 0;
        //            All_clear = true;
        //        }
           

        //    clear = false;
           
        //}


        //if(All_clear)
        //{
        //    //스테이지를 전부 클리어하면 할 곳
        //}
    }

    public void PrefabChange() {
        if (gameObjects.Length > index + 1)
        {
            Destroy(GameObject.Find(gameObjects[index].name + "(Clone)"));
            index++;
            GameObject.Instantiate(gameObjects[index], Vector3.zero, Quaternion.identity);
            //init();
            Debug.Log(gameObjects[index].name);
        }
    }

    public void PrefabReload() {
        Destroy(GameObject.Find(gameObjects[index].name + "(Clone)"));
        GameObject.Instantiate(gameObjects[index], Vector3.zero, Quaternion.identity);
    }
    void init()
    {

        Action_Manager action = GameObject.Find("Action_Manager").GetComponent<Action_Manager>();
        Sound_Manager sound_Manager = GameObject.Find("Sound_Manager").GetComponent<Sound_Manager>();
        PlayerControl_Manager playerControl = GameObject.Find("PlayerControl").GetComponent<PlayerControl_Manager>();


        if(action)
        {
            Debug.Log("action 잡음");
        }  if(sound_Manager)
        {
            Debug.Log("sound_Manager 잡음");
        }  if(playerControl)
        {
            Debug.Log("playerControl 잡음");
        }


        //action.init();
        //sound_Manager.init();
        //playerControl.init();

        
    }
}
