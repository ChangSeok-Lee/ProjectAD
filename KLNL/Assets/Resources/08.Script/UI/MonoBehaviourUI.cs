using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MonoBehaviourUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 버튼에 이벤트를 추가하는 함수
    /// </summary>
    /// <param name="button">이벤트를 붙힐 버튼</param>
    /// <param name="action">이벤트</param>
    //public void AddBtnEvent(Button button, UnityAction action)
    //{
    //    button.onClick.AddListener(action);
    //}

    public void AddBtnEvent(string path, UnityAction action) 
    {
        this.transform.Find(path).GetComponent<Button>().onClick.AddListener(action);
    }

    /// <summary>
    /// UI가 활성화 되어있으면 활성화 시키고 비활성화 되어있으면 활성화 시킨다.
    /// </summary>
    /// <param name="gameObject"></param>
    public void SetUI(GameObject gameObject) 
    {
        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
        else 
        {
            gameObject.SetActive(true);
        }
    }
}
