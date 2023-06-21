using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Buttons : MonoBehaviour
{
    public static Buttons buttons1;

    public string lightTime;
    public TMP_InputField inputField;

    private void Awake()
    {
        if(buttons1==null)
        {
            buttons1 = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
            
    }
    public void play()
    {
        SceneManager.LoadScene(1);
    }
    public void quit()
    {
        Application.Quit();
    }

    public void setTime()
    {
        lightTime = inputField.text;
    }
  
}
