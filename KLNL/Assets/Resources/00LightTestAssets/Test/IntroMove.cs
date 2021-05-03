using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMove : MonoBehaviour
{
	[SerializeField]
	private GameObject mainPlayer;
	[SerializeField]
	private GameObject Shadow;
	[SerializeField]
	private GameObject ArrowUI;
	Transform content;
	GameObject SubPlayer;

	private presentation p;
	private float Speed = 6f;
	bool moveflag;

	bool IsScroll;
	public GameObject[] turnOffObject;

	
	private void Awake()
	{
		
		IsScroll = true;
		moveflag = true;
		SubPlayer = this.gameObject;
		p = mainPlayer.gameObject.GetComponent<presentation>();
		content = Shadow.GetComponent<Transform>();

	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "FallingBtn") {
			
			pt();
		}
	}

	private void Update()
	{
		if(moveflag)
			move();
	}

	void move() 
	{
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

	void pt()
	{
		moveflag = false;
		TurnOff();
		StartCoroutine(Scroll());
		StartCoroutine(WaitTime(1.5f));
		ArrowUI.gameObject.SetActive(false);
		//빛이 내려온다
		
	}



	void TurnOff() {
		foreach (GameObject g in turnOffObject) {
			g.gameObject.SetActive(false);
		}
	}


	IEnumerator Scroll()
	{
		while (IsScroll)
		{
			content.localPosition = Vector2.Lerp(content.localPosition, new Vector2(0, -10), Time.deltaTime * 1);
			if (Vector2.Distance(content.localPosition, new Vector2(0, -10)) < 0.05f)
			{
				IsScroll = false;
			}
			yield return null;
		}
	}

	IEnumerator WaitTime(float time) {

		yield return new WaitForSeconds(time);
		p.falling = true;

	}
}
