using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testButton : MonoBehaviour
{
    Button m_Btn;
    private void Start()
    {
        m_Btn = this.transform.GetComponent<Button>();
        m_Btn.onClick.AddListener(Btn_Click);

    }

    void Btn_Click()
    {

        Debug.Log("버튼 클릭");

    }
}
