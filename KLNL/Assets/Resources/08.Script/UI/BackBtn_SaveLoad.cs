using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackBtn_SaveLoad : MonoBehaviourUI
{
    Button btn;

    private void Start()
    {
        btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ReturnToMain);
    }

    public void ReturnToMain()
    {
        Destroy(GameObject.Find("SaveLoadManager").gameObject);
        SceneChangeManager.Instance.SceneChange("Lobby");
    }
}
