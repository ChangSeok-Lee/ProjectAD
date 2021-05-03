using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditEnding : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) {
            
                
            
            
            SceneChangeManager.Instance.SceneChange("Lobby");
        }
    }
}
