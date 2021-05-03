using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Sound_Manager : MonoBehaviour
{

    public AudioSource BGM;
    public AudioSource Playeraction;
  
    //각 사운드 출력하는 조건들
    public bool Jump;
    public bool downjump;
    public bool die;
    public bool clear;


    //사운드 클립들
    AudioClip C_downjump;
    AudioClip C_jump;
    AudioClip C_die;
    AudioClip C_clear;
    AudioClip Bgm;




    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        Check_State();
    }



    public void init()
    {
        //gameObject.AddComponent<AudioSource>();
        transform.Find("BGM").gameObject.AddComponent<AudioSource>();
        BGM = transform.Find("BGM").gameObject.GetComponent<AudioSource>();
        Playeraction = transform.Find("Player/MainPlayer").gameObject.AddComponent<AudioSource>();

        Bgm = Resources.Load("09.Sounds/BGM") as AudioClip;
        C_downjump = Resources.Load("09.Sounds/jump") as AudioClip;
        C_jump = Resources.Load("09.Sounds/jump") as AudioClip;
        C_die = Resources.Load("09.Sounds/DIE_LOUD") as AudioClip;
        C_clear = Resources.Load("09.Sounds/Stage_Clear") as AudioClip;
        BGM.clip = Bgm;
        BGM.loop = true;
        BGM.Play();


    }


    /// <summary>
    /// 사운드 출력함수
    /// </summary>
    void Check_State()
    {
        if(Jump)
        {
            Playeraction.clip = C_jump;
            Playeraction.Play();
            Jump = false;
        }
        if(downjump)
        {
            Playeraction.clip = C_downjump;
            Playeraction.Play();
            downjump = false;
        }
        if (die)
        {
            Playeraction.clip = C_die;
            Playeraction.Play();
            die = false;
        }
        if (clear)
        {
            Playeraction.clip = C_clear;
            Playeraction.Play();
            clear = false;
        }
    }
}
