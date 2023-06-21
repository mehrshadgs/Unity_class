using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Direction
{
    NS, WE
}
public class Intersection : MonoBehaviour
{

    public Text[] RightLaneTexts;
    public Text[] LeftLaneTexts;

    public GameObject[] RightLaneObjects;
    public GameObject[] RightLaneRedObjects;
    public GameObject[] LeftLaneObjects;
    public GameObject[] LeftLaneRedObjects;


    public GameObject[] carLaneNS;
    public GameObject[] carLaneWE;

    public string TrafficBlockTag;
    
    public void SetCarLane(Direction direction,bool active)
    {
        switch(direction)
        {
            case Direction.NS:
                ListActivation(carLaneNS, active);
                break;
            case Direction.WE:
                ListActivation(carLaneWE, active);
                break;
        }
    }
    private  void ListActivation(GameObject[] list, bool active)
    {
        for(int i = 0; i < list.Length; i++)
        {
            var toMove = list[i].CompareTag(TrafficBlockTag);
            if(toMove)
            {
                list[i].transform.localPosition = active ? Vector3.zero : Vector3.up * 1000;
            }
            else
            {
                list[i].SetActive(active);
            }
        }
       
    }
    public void enablelight(GameObject[] lights)
    {
        for(int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(true);
        }
    }
    public void disablelight(GameObject[] lights)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
    }

    public void SetWELaneLightNumber(int number, Color color)
    {
        foreach(var text in RightLaneTexts)
        {
            text.color = color;
            number = number >= 0 ? number : 0;
            if(number < 10)
            {
                text.text = "0" + number;
            }
            else
            {
                text.text = number.ToString();
            }
        }
    }
    public void SetNSLaneLightNumber(int number,Color color)
    {
        foreach (var text in LeftLaneTexts)
        {
            text.color = color;
            number = number >= 0 ? number : 0;
            if (number < 10)
            {
                text.text = "0" + number;
            }
            else
            {
                text.text = number.ToString();
            }
        }
    }


}
