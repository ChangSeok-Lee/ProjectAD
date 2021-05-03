using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_State : MonoBehaviour
{

    public Action_Manager action_Manager;
    public bool Action;//액션이 가능한지 상태 표현
    public bool Ground;//땅에 닿아있는지 상태 표현
    public bool DownJump;//하향점프 가능한지 상태 표현

    //getset 설정부분
   


    private void Start()
    {
        action_Manager = transform.root.Find("Manager").gameObject.GetComponent<Action_Manager>();
    }


    //아래 것들은 각 충돌제들을 action_manager로 보내는 것
    
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision)
        {
            if (this.gameObject.tag == "MainPlayer")
            {
                action_Manager.SetM_Trigger(collision.gameObject);

            }

            if (this.gameObject.tag == "SubPlayer")
            {
                action_Manager.SetS_Trigger(collision.gameObject);

            }
        }
        if (collision.gameObject.tag == "Melting")
        {
            Debug.Log("녹기");
            action_Manager.SlimeMelt();
            //mainplayer 녹는 애니메이션 실행
            //콜라이더 끄기
        }

    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.tag == "SubPlayer")
        {
            action_Manager.SetS_Trigger(null);

        }
        if (this.gameObject.tag == "MainPlayer")
        {
            action_Manager.SetS_Trigger(null);

        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.tag == "MainPlayer")
        {
            Debug.Log("초기화");
            action_Manager.SetM_Trigger(null);


        }
        if (this.gameObject.tag == "SubPlayer")
        {
            action_Manager.SetS_Trigger(null);


        }
        if (collision.gameObject.tag == "Melting")
        {
            Debug.Log("녹기 끝");
            action_Manager.SlimeRecover();
            //mainplayer 녹는 애니메이션 끝내기
            //콜라이더 켜기
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision : " + collision.gameObject.tag);
        if (this.gameObject.tag == "MainPlayer")
        {
            action_Manager.SetM_collider(collision.gameObject);
            //if (collision.gameObject.tag == "EatableObject")
            //{
            //    action_Manager.Set_eatableObj(collision.gameObject);
            //}
        }
        

    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("collision stay : " + collision.gameObject.tag);
        if (this.gameObject.tag == "MainPlayer") 
        {
            if (collision.gameObject.tag == "EatableObject")
            {
                action_Manager.canEat = true;
                if (action_Manager.doEat == true) {
                    Debug.Log("실행 성공");
                    Destroy(collision.gameObject);
                    action_Manager.doEat = false;
                }
              
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {

        if (this.gameObject.tag == "MainPlayer")
        {
            action_Manager.SetM_collider(null);
            if (collision.gameObject.tag == "EatableObject")
            {
                action_Manager.canEat = false;
            }
        }
        

    }

    




}
