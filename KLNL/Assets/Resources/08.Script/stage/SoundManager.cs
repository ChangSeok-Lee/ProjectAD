using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SoundManager : MonoBehaviour
{

    public AudioSource BGM;
    public AudioSource Playeraction;
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } private set { } }


    public bool Jump;
    public bool downjump;
    public bool die;
    public bool clear;


    AudioClip C_downjump;
    AudioClip C_jump;
    AudioClip C_die;
    AudioClip C_clear;
    AudioClip Bgm;


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






        Playeraction = transform.Find("ActionSound").gameObject.AddComponent<AudioSource>();

        C_downjump = Resources.Load("09.Sounds/jump") as AudioClip;
        Bgm = Resources.Load("09.Sounds/BGM/bgm_lobby") as AudioClip;

        C_jump = Resources.Load("09.Sounds/jump") as AudioClip;
        C_die = Resources.Load("09.Sounds/DIE_LOUD") as AudioClip;
        C_clear = Resources.Load("09.Sounds/Stage_Clear") as AudioClip;

        BGM.clip = Bgm;
        BGM.loop = true;
        BGM.Play();


    }



    void Check_State()
    {

        if (Jump)
        {
            Playeraction.clip = C_jump;
            Playeraction.Play();
            Jump = false;
        }
        if (downjump)
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
