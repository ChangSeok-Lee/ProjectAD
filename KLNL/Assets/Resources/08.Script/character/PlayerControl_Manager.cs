using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl_Manager : MonoBehaviour
{
    public GameObject Btn_restart;
    public static PlayerControl_Manager Instance;
    private UnityAction MainplayerAction;
    private UnityAction SubplayerAction;
    private UnityAction JumpAction;

    [SerializeField]
    private Character_State MainPlayer;
    [SerializeField]
    private Character_State SubPlayer;
    [SerializeField]
    private float Speed = 6f;
    [SerializeField]
    private GameObject Jump;

    [SerializeField]
    private Vector2 Main_position;
    private Vector2 Sub_position;
    [SerializeField]
    private Animator animator;
    Action_Manager AM;
    public bool moveFlag;
    public bool right = true;
    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        ////    DontDestroyOnLoad(gameObject);
        //}
        //else
        //    DestroyImmediate(this);
        moveFlag = true;
        init();
        Debug.Log("PlayerControl_manager");
    }


    private void Update()
    {
            Move();
        
    }


    public void init()
    {

        MainPlayer = transform.Find("Player/MainPlayer").gameObject.GetComponent<Character_State>();
        SubPlayer = transform.Find("Player/SubPlayer").gameObject.GetComponent<Character_State>();
        Main_position = MainPlayer.gameObject.GetComponent<Transform>().position;
        Sub_position = SubPlayer.gameObject.GetComponent<Transform>().position;
        animator = transform.Find("Player/MainPlayer/Main_Slime").gameObject.GetComponent<Animator>();
        Btn_restart = transform.Find("Restart").gameObject;
        AM = this.gameObject.GetComponent<Action_Manager>();
    }



    public void Move()
    {

        Main_Player_Action();
        
        Sub_Player_Action();
    }


    /// <summary>
    /// 움직임 조절 및 방향설정하는 함수
    /// </summary>
    public void Main_Player_Action()
    {

        if (moveFlag == true) {
            //Debug.Log(Input.GetAxis("Main"));

            MainPlayer.transform.position += Vector3.left * Input.GetAxis("Main") * Time.deltaTime * Speed;

            //Debug.Log("Mainplayer     \n" + MainPlayer.position);
            if (Input.GetKey("a"))
            {

                right = false;
                MainPlayer.transform.localScale = new Vector2(-1 * Mathf.Abs(MainPlayer.gameObject.transform.localScale.x), 1 * MainPlayer.gameObject.transform.localScale.y);

            }
            else if (Input.GetKey("d"))
            {
                right = true;
                // MainPlayer.transform.position += Vector3.right * Speed * Time.deltaTime;
                MainPlayer.transform.localScale = new Vector2(1 * Mathf.Abs(MainPlayer.gameObject.transform.localScale.x), 1 * MainPlayer.gameObject.transform.localScale.y);
            }
        }
        
        if (Input.GetKey("r"))
        {
            //MainPlayer.gameObject.transform.position = position;


            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Invisible"))
            //{
            //    animator.SetTrigger("Restart");
            //    animator.ResetTrigger("DIE");
            //}
            Restart();
        }
    }



    public void Sub_Player_Action()
    {
        // Debug.Log(Input.GetAxis("Sub"));

        SubPlayer.transform.position += Vector3.left * Input.GetAxis("Sub") * Time.deltaTime * Speed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {

            //SubPlayer.transform.position += Vector3.left * Speed * Time.deltaTime;
            SubPlayer.transform.localScale = new Vector3(-1 * Mathf.Abs(SubPlayer.gameObject.transform.localScale.x), 1 * SubPlayer.gameObject.transform.localScale.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //SubPlayer.transform.position += Vector3.right * Speed * Time.deltaTime;
            SubPlayer.transform.localScale = new Vector2(1 * Mathf.Abs(SubPlayer.gameObject.transform.localScale.x), 1 * SubPlayer.gameObject.transform.localScale.y);
        }
    }

    public void Restart() 
    {
        //MainPlayer.gameObject.transform.position = Main_position;
        //SubPlayer.gameObject.transform.position = Sub_position;


        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Invisible"))
        //{
        //    animator.SetTrigger("Restart");
        //    animator.ResetTrigger("DIE");
        //    animator.ResetTrigger("Jump");
        //    animator.ResetTrigger("Landing");
        //    animator.ResetTrigger("Ground");
        //    transform.root.Find("Manager").GetComponent<Action_Manager>().enabled = true;
        //    Btn_restart.SetActive(false);
        //}
        //AM.ResetSwitch();
        //AM.EmptyTrash();
        //AM.AllReSpawn();
        GameObject.Find("Prefab_Manager").GetComponent<Prefab_Change>().PrefabReload();
    }
}
