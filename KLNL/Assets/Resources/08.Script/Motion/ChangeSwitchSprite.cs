using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSwitchSprite : MonoBehaviour
{
	[SerializeField]
	Action_Manager AM;
	[SerializeField]
	Sprite on;
	public GameObject[] Saw;
	private void Start()
	{
		//AM = transform.root.Find("Manager").GetComponent<Action_Manager>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{

		Debug.Log("Switch Trigger" + collision.gameObject.tag);
		if (collision.gameObject.tag == "MainPlayer")
		{
			ChangeSprite();
			//AM.SawAction(true);
			SawAction(true);
		}

	}

	public void ChangeSprite()
	{
		//this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("04.Image/Trap/switch_on");
		this.GetComponent<SpriteRenderer>().sprite = on;
	}


	public void SawAction(bool StartMove)
	{



		foreach (GameObject g in Saw)
		{
			Saw_move s;
			Blank b;
			if (g.gameObject.GetComponent<Saw_move>())
			{
				s = g.gameObject.GetComponent<Saw_move>();
				s.On = true;
			}
			else if(g.gameObject.GetComponent<Blank>())
			{
				b = g.gameObject.GetComponent<Blank>();
				g.SetActive(true);
				b.enabled = true;
			}
			
		
		}
	}
}