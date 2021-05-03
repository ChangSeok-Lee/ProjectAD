using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    GameObject SpawnObj;

	Action_Manager AM;
	private void Awake()
	{
		AM = GameObject.Find("Manager").GetComponent<Action_Manager>();
	}
	public void SpawnObject() {
        GameObject j = Instantiate(SpawnObj, this.transform.position, this.transform.rotation) as GameObject;
		AM.trashCan.Add(j);
    }

}
