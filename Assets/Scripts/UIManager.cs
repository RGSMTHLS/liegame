using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnPlayerListChanged += Instance_OnPlayerListChanged;
    }

    private void Instance_OnPlayerListChanged(object sender, EventArgs e)
    {

    }
}