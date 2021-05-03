using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSwitchAnim : MonoBehaviour
{
	
	Animator onAnimator;

	[SerializeField]
	Action_Manager AM;
	[SerializeField]
	string Animname;
	[SerializeField]
	int ActiveUnder;
	private void Start()
	{
		AM= transform.root.Find("Manager").GetComponent<Action_Manager>();
		onAnimator = this.gameObject.GetComponent<Animator>();
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Switch Trigger" + collision.gameObject.tag);
		if (collision.gameObject.tag == "EatableObject")
		{
			Debug.Log("EatableObject----" + collision.gameObject.tag);
			AM.ActiveUnder(ActiveUnder);
		}
	}

	public void StartAnim()
	{
		onAnimator.runtimeAnimatorController = (RuntimeAnimatorController)
			RuntimeAnimatorController.Instantiate(Resources.Load("04.Image/Trap/"+ Animname,
			typeof(RuntimeAnimatorController)));
	}
}
