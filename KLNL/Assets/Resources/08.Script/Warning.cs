using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(GameObject.Find("SaveLoadManager").gameObject);
		SceneChangeManager.Instance.SceneChange("Credit");
	}
}
