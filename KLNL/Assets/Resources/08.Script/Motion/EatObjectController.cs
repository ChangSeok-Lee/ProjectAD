using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatObjectController : MonoBehaviour
{
    public Action_Manager action_Manager;
    private void Start()
    {
        action_Manager = transform.root.Find("Manager").gameObject.GetComponent<Action_Manager>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (this.gameObject.tag == "MainPlayer")
        {
            action_Manager.SetM_collider(collision.gameObject);
            if (collision.gameObject.tag == "EatableObject")
            {
                //action_Manager.eatableObj = collision.gameObject;
            }
        }


    }

}
