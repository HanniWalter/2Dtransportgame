using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameobjectSelecter : MonoBehaviour
{
    MainInputController mainInputController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mainInputController = GetComponent<MainInputController>();
        var playerInputController = mainInputController.playerInputController;
        playerInputController.Mouse.RightButton.performed += _ => RightMouseClick();
        playerInputController.Mouse.LeftButton.performed += _ => LeftMouseClick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LeftMouseClick()
    {
        if(mainInputController.objectundercourser != null){
           
           Debug.Log(mainInputController.objectundercourser.getInterface() + "    " + mainInputController.objectundercourser.name);
        }
    }

    private void RightMouseClick()
    {
        //Debug.Log("Right");
    }
}
