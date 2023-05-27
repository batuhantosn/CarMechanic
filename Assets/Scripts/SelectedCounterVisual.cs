using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnselectedCounterChanged;
    }

    private void Player_OnselectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
        
    }

    private void Show()
    {
        foreach ( GameObject visualGameObj in visualGameObjArray)
        {
            visualGameObj.SetActive(true);
        }
        
    }
    private void Hide()
    {
        foreach (GameObject visualGameObj in visualGameObjArray)
        {
            visualGameObj.SetActive(false);
        }
    }
}
