using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        // add code here to handle when a color is selected
        MainManager.Instance.unitColour = color;
    }
    
    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;

        // pre-select the saved colour from the Main Manager when menu is launched
        ColorPicker.SelectColor(MainManager.Instance.unitColour);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        // save user's last selected colour when the application exits
        MainManager.Instance.SaveColour();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveColourClicked()
    {
        MainManager.Instance.SaveColour();
    }

    public void LoadColourClicked()
    {
        MainManager.Instance.LoadColour();
        ColorPicker.SelectColor(MainManager.Instance.unitColour);
    }
}
