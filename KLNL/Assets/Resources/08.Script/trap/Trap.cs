using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Action_Manager AM;
    private void Start()
    {
        AM = transform.root.Find("Manager").GetComponent<Action_Manager>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag + "충돌");
        if (collision != null)
        {
            if (collision.gameObject.tag == "MainPlayer")
            {
                Debug.Log("사망");
                AM.die = true;
                AM.Die();

            }
        }
    }

}
