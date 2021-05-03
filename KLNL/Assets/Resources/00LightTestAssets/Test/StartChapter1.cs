using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChapter1 : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "MainPlayer") {
			Debug.Log("Intro Scene Change");
			
			SceneChangeManager.Instance.SceneChange("01");

		}
	}
}
