using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMove : MonoBehaviour
{
    [SerializeField]
    GameObject mainPlayer;

    float x;
    float y;
    Transform t;
    // Start is called before the first frame update
    void Start()
    {
        t = mainPlayer.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

		x = t.position.x;
        y = this.gameObject.GetComponent<Transform>().position.y;
        this.transform.position = new Vector2(x, y);
    }
}
