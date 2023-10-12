using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class menu : MonoBehaviour
{
    public static int startPoint;

    public void RandomStart()
    {
        startPoint = 0;
        SceneManager.LoadScene(1);
    }

    public void GPStart()
    {
        startPoint = 1;
        SceneManager.LoadScene(1);
    }

    public void CrossStart()
    {
        startPoint = 2;
        SceneManager.LoadScene(1);
    }
}
