using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangeManager:MonoBehaviour
{
    private GameObject startButton;
    private AudioSource audio;
    public static SceneChangeManager Instance 
    {
        get { return instance; }
        set { }
    }

    private static SceneChangeManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }

    public void SceneChange(string sceneName)
    {
        audio = GameObject.Find("SoundManager/BGM").GetComponent<AudioSource>();
        Debug.Log("scenChanged");
        SceneManager.LoadScene(sceneName);


        switch (sceneName) {
            case "Intro":
                audio.clip = Resources.Load("09.Sounds/BGM/bgm_00") as AudioClip;
                audio.Play();
                break;
            case "01":
                audio.clip = Resources.Load("09.Sounds/BGM/bgm_01") as AudioClip;
                audio.Play();
                break;
            case "02":
                audio.clip = Resources.Load("09.Sounds/BGM/bgm_02") as AudioClip;
                audio.Play();
                break;
            case "03":
                audio.clip = Resources.Load("09.Sounds/BGM/bgm_03") as AudioClip;
                audio.Play();
                break;
            case "04":
                audio.clip = Resources.Load("09.Sounds/BGM/bgm_04") as AudioClip;
                audio.Play();
                break;
        }
    }

    public void AddScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    public void CloseLoading() 
    {
        SceneManager.UnloadSceneAsync("Loading");
    }
}
