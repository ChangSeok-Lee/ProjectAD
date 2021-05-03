using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootObject : MonoBehaviour
{
	public float speed;
	PlayerControl_Manager PM;
	Action_Manager AM;
	private void Start()
	{
		PM = GameObject.Find("Manager").GetComponent<PlayerControl_Manager>();
		AM = GameObject.Find("Manager").GetComponent<Action_Manager>();
		AM.trashCan.Add(this.gameObject);
		if (PM.right)
		GetComponent<Rigidbody2D>().velocity = transform.right * speed;
		else
			GetComponent<Rigidbody2D>().velocity = transform.right *(-speed);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log("Shot Trigger" + collision.gameObject.tag);
		if (collision.gameObject.tag == "Boundary") {
			AM.ReSpawn(0);
			Debug.Log("Respawn3");
			Destroy(this.gameObject);
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		//Debug.Log("Shot Collision" + collision.gameObject.tag);
		if (collision.gameObject.tag == "Switch")
		{
			//this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
			collision.gameObject.GetComponent<ChangeSwitchAnim>().StartAnim();
			//if (collision.gameObject.name == "jewel_switch_1_down")
			//{
			//	Debug.Log("2번 발판 on");
			//	AM.ActiveUnder(2);
			//}
			//else if (collision.gameObject.name == "jewel_switch_2_jump")
			//{
			//	Debug.Log("1번 발판 on");
			//	AM.ActiveUnder(1);
			//}
		}
	}

}
