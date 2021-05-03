using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMove : MonoBehaviour
{
    public float speed;
    public List<GameObject> name;
    public King_credit king;
    public GameObject logo;
    bool stop;


    RectTransform rect;
    void Start()
    {

        stop = true; ;
        speed = 200;
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            if (rect.anchoredPosition.y < 1700)
            {
                //Debug.Log(rect.anchoredPosition.y);
                rect.anchoredPosition += Vector2.up * speed * Time.deltaTime;
            }
            else
            {
                Invoke("temp1", 2f);

                stop = false;
                rect.anchoredPosition += Vector2.up * 500f;



            }
        }
    }



    void temp1()
    {
        logo.SetActive(true);

        Invoke("temp", 3f);
    }

    void temp()
    {
        logo.SetActive(false);
        for (int i = 0; i < name.Count; i++)
            name[i].SetActive(true);


       



    }






}
