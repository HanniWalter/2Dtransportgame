using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public TabButton selectedTab;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);


        if (button.def)
        {
            selectedTab = button;
        }

        ResetTabs();
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (button != selectedTab)
        {
            button.background.color = ColorClass.getHighlighted();
        }
        //ResetTabs();
    }

    /*internal void Close()
    {
        gameObject.SetActive(false);
    }*/

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        ResetTabs();
        if (button != null)
        {
            button.background.color = ColorClass.getSelected();

        }
        selectedTab = button;
        ResetTabs();
    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {   
            if (selectedTab != button)
            {
                button.background.color = ColorClass.getUnselected();
                if (button.toshow != null)
                {
                    button.toshow.SetActive(false);
                }
            } 
        }
        if (selectedTab != null)
        {
            selectedTab.background.color = ColorClass.getSelected();
            if (selectedTab.toshow != null)
            {
                selectedTab.toshow.SetActive(true);
            }
        }
    }
}
