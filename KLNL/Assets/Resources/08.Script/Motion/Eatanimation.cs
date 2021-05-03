using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatanimation : MonoBehaviour
{

    Animator _animator2;
    // Start is called before the first frame update
    void Start()
    {
        _animator2 = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_animator2.GetCurrentAnimatorStateInfo(0).IsName("FINISH"))
        {
            Destroy(this.gameObject);
        }
        else
            Debug.Log("뱉을 수 있음");
    }
}
