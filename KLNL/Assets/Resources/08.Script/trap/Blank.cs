using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Blank : MonoBehaviour
{

    TilemapRenderer TilemapRenderer;
    TilemapCollider2D TilemapCollider2D;
    public float ontime=2;
    public float offtime=2;
    // Start is called before the first frame update
    void Start()
    {
        TilemapRenderer=gameObject.GetComponent<TilemapRenderer>();
        TilemapCollider2D=gameObject.GetComponent<TilemapCollider2D>();
        Object_On();
    }

   




    void Object_On()
    {
        TilemapRenderer.enabled = true;
        TilemapCollider2D.enabled = true;

        Invoke("Object_Off", ontime);

    }
    void Object_Off()
    {
        TilemapRenderer.enabled = false;
        TilemapCollider2D.enabled = false;

        Invoke("Object_On", offtime);
    }
}
