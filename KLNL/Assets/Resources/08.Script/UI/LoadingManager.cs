using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class LoadingManager : MonoBehaviour
{

    [SerializeField]
    private GameObject chapterName;
    [SerializeField]
    private GameObject stageNum;
    [SerializeField]
    private GameObject stageName;
    [SerializeField]
    private GameObject pressKey;

    private Text chapterNameText;
    private Text stageNumText;
    private Text stageNameText;

    private bool waitCheck = false;




    string xmlFileName = "StageName";

    void Awake()
    {
        
        chapterNameText = chapterName.GetComponent<Text>();
        stageNumText = stageNum.GetComponent<Text>();
        stageNameText = stageName.GetComponent<Text>();

        //chapterNameText.text = "Chapter" + GameDataManager.gameData.chapter;
        stageNumText.text = GameDataManager.gameData.chapter + " - " + GameDataManager.gameData.stage;
        Debug.Log("waitCheck : " + waitCheck);
        LoadXML(xmlFileName);

    }

    // Update is called once per frame
    void Update()
    {
       
        if (waitCheck == false)
        {
            StartCoroutine(Wait(1.5f));
        }
        if (waitCheck == true && Input.anyKeyDown) 
        {
            Time.timeScale = 1;
            SceneChangeManager.Instance.CloseLoading();
        }
    }
    private void LoadXML(string _fileName)
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("11.XML/" + _fileName);
        XmlDocument xmlDoc = new XmlDocument();
        Debug.Log(txtAsset.text);
        xmlDoc.LoadXml(txtAsset.text);

        // 하나씩 가져오기 테스트 예제.
        //XmlNodeList cost_Table = xmlDoc.GetElementsByTagName("cost");
        //foreach (XmlNode cost in cost_Table)
        //{
        //    Debug.Log("[one by one] cost : " + cost.InnerText);
        //}

        // 전체 아이템 가져오기 예제.
        //XmlNodeList all_nodes = xmlDoc.SelectNodes("dataroot/Script");
        
        
        //foreach (XmlNode node in all_nodes)
        //{
        //    // 수량이 많으면 반복문 사용.
        //    Debug.Log("[at once] id :" + node.SelectSingleNode("id").InnerText);
        //    Debug.Log("[at once] name : " + node.SelectSingleNode("name").InnerText);
 
        //}

        XmlNodeList all_nodes = xmlDoc.SelectNodes("dataroot/Script");

        foreach (XmlNode node in all_nodes) {
            if (node.SelectSingleNode("id").InnerText == GameDataManager.gameData.chapter.ToString()) {
                chapterNameText.text = node.SelectSingleNode("name").InnerText;
            }
            if (node.SelectSingleNode("id").InnerText == (
                GameDataManager.gameData.chapter * 100 + GameDataManager.gameData.stage).ToString()) { 
                stageNameText.text= node.SelectSingleNode("name").InnerText;
            }
        }
    }

    IEnumerator Wait(float waitTime)
    {
        Debug.Log("Loading Update");
        Debug.Log("waitCheck : " + waitCheck);
        yield return new WaitForSeconds(waitTime);
        Time.timeScale = 0;
        waitCheck = true;
        Debug.Log("waitCheck : " + waitCheck);
        pressKey.SetActive(true);
    }
}
