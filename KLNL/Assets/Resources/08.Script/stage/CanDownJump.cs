using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDownJump : MonoBehaviour
{
    public Action_Manager AM;
    private void Start()
    {
        AM = transform.root.Find("Manager").GetComponent<Action_Manager>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision != null)
        {
            if (collision.gameObject.tag == "MainPlayer")
            {
                AM.M_ground = true;
                AM.M_action = true;
                AM.M_downjump = true;
                AM.LandingDust();
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision != null)
        {
            if (collision.gameObject.tag == "MainPlayer")
            {
                AM.M_ground = true;
                AM.M_action = true;

                AM.M_downjump = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision != null)
        {
            if (collision.gameObject.tag == "MainPlayer")
            {
                AM.M_ground = false;
                AM.M_action = false;

                AM.M_downjump = false;
            }
        }
    }
}
