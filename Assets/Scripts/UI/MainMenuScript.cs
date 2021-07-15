using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    private Button newGameButton;
    private Button loadButton;
    private Button exitButton;
    private Button continueButton;

    private void OnEnable(){
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        newGameButton = rootVisualElement.Q<Button>("newGameButton");
        loadButton = rootVisualElement.Q<Button>("loadButton");
        exitButton = rootVisualElement.Q<Button>("exitButton");
        continueButton = rootVisualElement.Q<Button>("continueButton");

        continueButton.RegisterCallback<ClickEvent>(ev => continueAction()); 
    }

    private void continueAction(){
        gameObject.SetActive(false);
    }

    private void Debug_(){
        Debug.Log("hover");
    }
}
