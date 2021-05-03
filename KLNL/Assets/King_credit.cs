using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King_credit : MonoBehaviour
{
    public float distance;
    public float speed;
    public List<Credit_Slime> credit_Slimes;
    int count = 0;
    public bool name;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 1.7f;
        distance = 2;
        StartCoroutine(temp());
    }

    // Update is called once per frame
    void Update()
    {




           transform.position += Vector3.right * speed * Time.deltaTime;
            Vector3 pos = transform.position;

            for (int i = 0; i < count; i++)
            {
                credit_Slimes[i].king = true;
                credit_Slimes[i].King = pos + new Vector3(-(i ) * distance, -0.5f, 0);

            }
        
      
     

    }



   




    IEnumerator temp()
    {

        while(count< 4)
        {

            Debug.Log("코루틴");
            yield return new WaitForSeconds(3.0f);
            count++;
        }


        while(this.transform.position.x<30)
        {
            yield return new WaitForSeconds(3.0f);
        }

        for( int i = 0; i < credit_Slimes.Count;i++)
        {
            Destroy(credit_Slimes[i].gameObject);
        }
        Destroy(this.gameObject);

    }




}
