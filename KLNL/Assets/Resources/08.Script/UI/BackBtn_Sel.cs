using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackBtn_Sel : MonoBehaviourUI
{

    Button btn;

	private void Start()
	{
		btn = this.gameObject.GetComponent<Button>();
		btn.onClick.AddListener(ReturnToSaveSlot);
	}
	/// <summary>
	/// 세이브 슬롯 재선택
	/// </summary>
	public void ReturnToSaveSlot()
    {
        SceneChangeManager.Instance.SceneChange("LoadFile");
    }
}
