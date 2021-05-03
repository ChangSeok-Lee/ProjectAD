using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectanimation : MonoBehaviour
{

    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("FINISH"))
        {
            Destroy(this.gameObject);
        }
        else
            Debug.Log("아직 안끝남");
    }
}
