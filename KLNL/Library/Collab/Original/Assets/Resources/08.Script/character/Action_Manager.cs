using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//충돌을 하면 action_manager로 충돌체를 보냄

//sub플레이어는 s_trigger로  -> 범위안으로 들어가니까

//main플레이어는 m_trigger, m_collider -> 하향점프 범위, 바닥 체크



//1. 서브 플레이어가 어디에있는지 체크

//2. 액션이 가능한 상태인지 확인
// action = true ; -> 다른 액션을 하지 않는중
//점프
// 땅에 있다 ->점프가능한 상태; ground =true;

//하향점프
//하향점프가 가능한 곳에 있다 downjump = true;, ground = true;
//(하향점프 구현내용)


public class Action_Manager : MonoBehaviour
{
    SoundManager sound_Manager;


    [SerializeField]
    Animator M_animator;

    [SerializeField]
    private Prefab_Change prefab_change;
    [SerializeField]
    Character_State MainPlayer;
    [SerializeField]
    Rigidbody2D MainRigid;
    [SerializeField]
    GameObject M_collider;//메인플레이어와 부딪히는 물체 이름

    [SerializeField]
    GameObject M_Trigger;//메인플레이어를 감지하는 범위 이름

    [SerializeField]
    public GameObject Btn_Restart;//재시작 버튼



    public bool M_ground;//땅에 붙어있는지

    public bool M_action;//점프 하향점프등 발판행동을 할수있는 상태인지

    public bool M_downjump;//하향점프 할수 있는구간인지



    [SerializeField]
    Character_State SubPlayer; //서브플레이어의 상태를 표현하는 클래스

    [SerializeField]
    GameObject S_Trigger;//서브플레이어를 감지하는 범위 이름

    [SerializeField]
    Vector3 M_velocity;//서브플레이어를 감지하는 범위 이름
    [SerializeField]
    float JumpPower = 20f;

    Vector2 oldposition;// 처음 슬라임의 위치 r키를 눌렀을 때 이동하는 곳

    [SerializeField]
    public Sprite NULLsprite;
    [SerializeField]
    public Sprite IdleSprite;

    [SerializeField]
    GameObject eatFlat;

    Animator eatFlatAnim;

    public bool jump;
    public bool downjump;
    public bool die;
    public bool falling;
    public bool restart = false;

    enum EATSTATE { IDLE, CANEAT, CANSHOT }

    EATSTATE eatState = EATSTATE.IDLE;
    public bool canEat = false;
    public bool doEat = false;

    public GameObject shot;
    public Transform shotSpawn;

    
    public GameObject[] Switch;
    public GameObject[] ReSpawnObj;
    List<Vector2> SawInitPosition;
    [HideInInspector]
    public List<GameObject> trashCan;
    void Start()
    {
        init();
    }


    private void Update()
    {

        M_velocity = MainPlayer.gameObject.GetComponent<Rigidbody2D>().velocity;

        VelocityCheck();
        Debug.Log("doEat : " + doEat);
        Debug.Log("canEat : " + canEat);

    }

    private void FixedUpdate()
    {

        Check_State();

        Action();

        Debug.Log("eatState : " + eatState);

    }



    public void init()
    {

        prefab_change = GameObject.Find("Prefab_Manager").GetComponent<Prefab_Change>();
        MainPlayer = transform.Find("Player/MainPlayer").gameObject.GetComponent<Character_State>();
        MainRigid = MainPlayer.gameObject.GetComponent<Rigidbody2D>();
        SubPlayer = transform.Find("Player/SubPlayer").gameObject.GetComponent<Character_State>();
        sound_Manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        M_animator = transform.Find("Player/MainPlayer/Main_Slime").GetComponentInChildren<Animator>();
        Btn_Restart = transform.Find("Restart").gameObject;
        Btn_Restart.SetActive(false);
        doEat = false;
        canEat = false;
        if (eatFlat) {
            eatFlatAnim = eatFlat.GetComponent<Animator>();
        }
        
        
        //foreach (GameObject g in Saw) 
        //{
        //    SawInitPosition.Add(g.gameObject.GetComponent<Saw_move>().Saw.localPosition);
        //}


    }



    /// <summary>
    /// 하향 점프할때 콜라이더를 켜는 데 사용됨 invoke와 사용
    /// </summary>
    void delay()
    {
        Physics2D.IgnoreLayerCollision
                   (LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("DownJumpArea"), false);
    }






    /// <summary>
    /// 하향점프
    /// 레이어 간 무시 적용 , 아래로 힘 가하기, 0.5f딜레이 후 레이어 간 무시 해제
    /// </summary>
    public void DownJump()
    {

        if (M_downjump) {
            M_downjump = false;
            MainRigid.AddForce(Vector2.down * 100f);
            Debug.Log("트리거 설정");

            Physics2D.IgnoreLayerCollision
                 (LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("DownJumpArea"), true);
            downjump = true;

            sound_Manager.downjump = true;

            Invoke("delay", 0.2f);

        }

    }


    /// <summary>
    /// 점프 
    /// </summary>
    public void Jump()
    {
        if (M_ground)
        {
            Debug.Log("점프");
            M_animator.SetTrigger("Jump");
            Effect_Ani(2);
            MainRigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            jump = true;
            Physics2D.IgnoreLayerCollision
               (LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("DownJumpArea"), true);
            sound_Manager.Jump = true;


        }
    }



    /// <summary>
    /// 죽음 
    /// 죽음모션 후 
    /// </summary>
    public void Die()
    {
        Debug.Log("죽음체크");
        if (die)
        {

            Debug.Log("죽음");
            sound_Manager.die = true;
            //tartCoroutine(CheckAnimationState());


            //Btn_Restart.SetActive(true);


            Restart();


            M_animator.SetTrigger("DIE");
            transform.root.Find("Manager").GetComponent<Action_Manager>().enabled = false;


            if (!die)
                MainPlayer.gameObject.SetActive(false);


            die = false;


        }
    }

    /// <summary>
    /// 스테이지를 재시작한다.
    /// </summary>
    void Restart()
    {

        Debug.Log("재시작");
        Btn_Restart.transform.position = M_animator.transform.position + Vector3.up * 3;
        if (!Btn_Restart.activeSelf)
            Btn_Restart.SetActive(true);



        //Switch의 스프라이트를 초기의 것으로 변경한다.
        
    }

    public void ResetSwitch() 
    {
        foreach (GameObject g in Switch) 
        {
            g.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("04.Image/Trap/switch_off");
        }
    }


    /// <summary>
    /// 발판액션을 수행하는 함수
    /// </summary>
    public void Action()
    {
        if (M_action)//서브캐릭터가 액션발판 안으로 들어갈 때
        {


            if (S_Trigger)//액션이 가능한 상태인지 인식
            {
                MainPlayer.Action = false;

                if (S_Trigger.gameObject.tag == "Jump_Button")
                {
                    // Debug.Log("점프수행");
                    Jump();
                }
                else if (S_Trigger.gameObject.tag == "DownJump_Button")
                {
                    Debug.Log("하향점프");
                    DownJump();
                }
                else if (S_Trigger.gameObject.tag == "Eat")
                {
                    Debug.Log("먹고 뱉기");

                    if (eatState == EATSTATE.IDLE && canEat == true)
                    {
                        eatState = EATSTATE.CANEAT;

                    }

                    //먹고 뱉기 발판을 사용하기 위해서 
                    //- 발판 태그를 eat, 먹을 오브젝트 태그를 EatableObject로
                    //- manager-Action_Manager 컴포넌트의 flat과 shot, shotSpan에 발판, 발사할 오브젝트를 추가
                    //- 
                    if (eatState == EATSTATE.CANEAT)//상태가 먹을 수 있는 상태일 때 - >  먹는다
                    {
                        Debug.Log("먹기");
                        //EatObject(eatableObj.gameObject);//오브젝트 먹기(슬라임 쪽으로 와서 사라짐),
                        doEat = true;
                        EAT_Ani();//슬라임 스프라이트 변경,(몸에 원 돌고 있는 스프라이트)
                        eatState = EATSTATE.CANSHOT;//뱉을 수 있는 상태로 변경,
                        eatFlatAnim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("04.Image/underBlock/shootingTile-Sheet_0");
                        //발판 스프라이트 변경(발사 가능 상태 스프라이트)
                    }
                    else if (eatState == EATSTATE.CANSHOT)//상태가 뱉을 수 있는 상태일 때 -> 발사한다.
                    {
                        Debug.Log("뱉기");
                        //오브젝트 발사,
                        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                        StopEatAni();//슬라임 스프라이트 변경,(기본 슬라임 스프라이트)
                        eatState = EATSTATE.IDLE;//기본 상태로 변경,
                        eatFlatAnim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("04.Image/underBlock/eatingTile-Sheet_0");
                        //발판 스프라이트 변경(기본 상태 스프라이트)
                    }

                }

            }
        }

       


    }

    /// <summary>
    /// 캐릭터의 상태를 설정하는 메소드
    /// </summary>
    public void Check_State()
    {
        if (M_collider)
        {
            if (M_collider.tag == "Ground")//땅으로 인식되는 부분
            {

                LandingDust();
            }
            else if (M_collider.name == "CanDownJump")//하향점프구간
            {
                LandingDust();
            }

            if (M_collider.tag == "Trap")//trap과 부딪힐때
            {

                die = true;
                //MainPlayer.enabled = false;
            }

            if (M_collider.tag == "Switch") 
            {
            //MoveSaw;  ----> Saw들을 가져와서 'someflag'를 ture로 모두 변경
            }

            //if(MainPlayer.Ground)
            //{
            //    Debug.Log("충돌 on");
            //   // Physics2D.IgnoreLayerCollision
            //   //(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("DownJumpArea"), false);
            //}




        }



        if (M_Trigger)//클리어 스테이지 체크용
        {
            if (M_Trigger.name == "Clear")
            {
                sound_Manager.clear = true;
                //Prefab_Change.clear = true;
                prefab_change.PrefabChange();
                //cleared 데이터 변경 -> 현재 스테이지를 다음 스테이지로 변경 -> 로딩화면 띄우기  or 다음 챕터 씬으로 전환
                SaveLoadManager.Instance.ClearEvent();
                Debug.Log("clear");
            }
            if (M_Trigger.tag == "Trap")// 나중에 쓸것
            {

                die = true;
                //MainPlayer.enabled = false;
            }
        }


        ///해당 애니메이션이면 트리거를 초기화 시킴
        ///트리거의 중복실행 오류 없애기
        if (M_animator.GetCurrentAnimatorStateInfo(0).IsName("Invisible"))
        {

            M_animator.ResetTrigger("DIE");

        }
        else if (M_animator.GetCurrentAnimatorStateInfo(0).IsName("slime"))
        {
            M_animator.ResetTrigger("Restart");
        }
    }




    /// <summary>
    /// 상승일땐 (==점프) 하향점프 가능 발판 충돌무시 
    /// 하강중일땐 하향점프 가능 발판 충돌가능
    /// </summary>
    public void VelocityCheck()
    {
        if (M_velocity.y < 0)
        {
            Debug.Log("하강");
            M_animator.SetTrigger("Landing");
            falling = true;

        }


        if (jump)
        {
            //Debug.Log("점프상승중 "+ M_velocity.y);
            if (M_velocity.y < 0)
            {
                // Debug.Log("하강");
                Physics2D.IgnoreLayerCollision
                    (LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("DownJumpArea"), false);
                jump = false;
            }


        }

    }


    /// <summary>
    /// 메인플레이어와 부딪히는 물체 이름 설정
    /// </summary>
    /// <param name="gameObject"></param>
    public void SetM_collider(GameObject gameObject)
    {
        M_collider = gameObject;
    }

    /// <summary>
    /// 메인플레이어를 감지하는 범위 이름 설정
    /// </summary>
    /// <param name="gameObject"></param>
    public void SetM_Trigger(GameObject gameObject)
    {
        M_Trigger = gameObject;
    }

    /// <summary>
    /// 서브플레이어를 감지하는 범위 이름
    /// </summary>
    /// <param name="gameObject"></param>
    public void SetS_Trigger(GameObject gameObject)
    {
        // Debug.Log("S_Trigger set " + gameObject);
        S_Trigger = gameObject;
    }


    /// <summary>
    /// 착지,점프할때 나오는 애니메이션
    /// </summary>
    /// <param name="str">1=land 2=jump</param>
    public void Effect_Ani(int num)//착지,점프할때 나오는 애니메이션
    {
        GameObject temp = new GameObject("test");
        temp.transform.position = MainPlayer.transform.position;

        Animator temp_Ani = temp.AddComponent<Animator>();

        switch (num)
        {
            case 1:
                {
                    temp.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("07.char/Landing_Dust");
                    temp.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("07.char/Land_Effect");
                    temp.GetComponent<SpriteRenderer>().sortingOrder = 10;

                    break;
                }
            case 2:
                {
                    temp.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("07.char/Jump_Dust");
                    temp.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("07.char/Jump_Effect");
                    temp.GetComponent<SpriteRenderer>().sortingOrder = 10;

                    break;
                }
            default:
                break;

        }
        temp.AddComponent<effectanimation>();
    }




    /// <summary>
    /// eat ani관련 애니메이션
    /// </summary>
    /// <param name="num"></param>
    public void EAT_Ani()//착지,점프할때 나오는 애니메이션
    {
        GameObject temp2 = new GameObject("eat");

        temp2.transform.parent = MainPlayer.transform;
        temp2.transform.localPosition = new Vector2(0, 0);



        temp2.AddComponent<Animator>();
        Animator tempAnimator = temp2.GetComponent<Animator>();
        temp2.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("07.char/Landing_Dust");
        tempAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("07.char/ball-Sheet_0");
        temp2.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    public void LandingDust()
    {
        if (M_collider.tag == "Ground" || M_collider.name == "CanDownJump")//땅으로 인식되는 부분
        {

            Debug.Log("Ground");


            if (falling && M_velocity.y >= 0)//착지
            {
                Effect_Ani(1);

                M_animator.SetTrigger("Ground");
                M_animator.ResetTrigger("Landing");
                falling = false;
            }
        }
    }

    void StopEatAni()
    {

        Destroy(GameObject.Find("eat").gameObject);
        Debug.Log("destroy eat object");

    }
    /// <summary>
    /// 메인 플레이어가 obj를 먹는다
    /// </summary>
    /// <param name="obj"></param>
    void EatObject(GameObject obj)
    {
        obj.transform.position = MainPlayer.transform.position;
        Destroy(obj);
    }

    public void SlimeMelt()
    {
        if (M_ground&&!M_downjump) {
            this.transform.GetComponent<PlayerControl_Manager>().moveFlag = false;
            MainPlayer.GetComponent<PolygonCollider2D>().isTrigger = true;
            MainPlayer.GetComponent<Rigidbody2D>().gravityScale = 0f;
            M_animator.SetTrigger("Melting");//녹는 anim 
        }


     }

    public void SlimeRecover() 
    {
        MainPlayer.GetComponent<PolygonCollider2D>().isTrigger = false;
        MainPlayer.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        M_animator.SetTrigger("Recover");//복구 anim
        this.transform.GetComponent<PlayerControl_Manager>().moveFlag = true;

    }


    //public bool CheckDirection() {
    //    if (this.gameObject.GetComponent<PlayerControl_Manager>().right)
    //    {
    //        return true;
    //    }
    //    else
    //        return false;
    //}
    public void ActiveUnder(int underNum) {
        GameObject Under = GameObject.Find("Under").gameObject;
        switch (underNum) {
            case 1:
                Under.transform.Find("Jump").gameObject.SetActive(true);
                break;
            case 2:
                Under.transform.Find("DownJump").gameObject.SetActive(true);
                break;
        }
    }

    public void EmptyTrash() {
        foreach (GameObject g in trashCan) {
            Destroy(g.gameObject);
        }
        trashCan.Clear();
    }

    public void AllReSpawn() 
    {
        foreach (GameObject g in ReSpawnObj) {
            g.gameObject.GetComponent<Spawn>().SpawnObject();
        }
    }

    public void ReSpawn(int num) {
        ReSpawnObj[num].gameObject.GetComponent<Spawn>().SpawnObject();
        Debug.Log("Respawn1");
    }
	private void OnDestroy()
	{
        EmptyTrash();
    }
}

