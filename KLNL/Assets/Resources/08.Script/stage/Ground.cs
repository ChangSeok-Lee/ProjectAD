using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
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

         
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "MainPlayer")
            {
                AM.M_ground = false;

                AM.M_action = false;
            }
        }
    }

}
