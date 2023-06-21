using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trafficlights : MonoBehaviour
{

    public Intersection TargetIntersection;

    public int RedLightTime;
    public int GreenLightTime;

    private void Awake()
    {
       if(Buttons.buttons1.lightTime == null)
        {
            RedLightTime = GreenLightTime = 10;
        }
       int.TryParse(Buttons.buttons1.lightTime,out RedLightTime);
       int.TryParse(Buttons.buttons1.lightTime, out GreenLightTime);
       if(RedLightTime == 0 )
        {
            RedLightTime = GreenLightTime = 10;
        }

    }

    private void Start()
    {
        StartCoroutine(TrafficLightCounter());
    }

    private IEnumerator TrafficLightCounter()
    {
        while(true)
        {
            TargetIntersection.SetCarLane(Direction.NS, true);
            TargetIntersection.SetCarLane(Direction.WE, false);
            TargetIntersection.enablelight(TargetIntersection.RightLaneObjects);
            TargetIntersection.enablelight(TargetIntersection.LeftLaneRedObjects);
            TargetIntersection.disablelight(TargetIntersection.RightLaneRedObjects);
            TargetIntersection.disablelight(TargetIntersection.LeftLaneObjects);
            lightStartS(GreenLightTime, Color.red);
            lightStartW(GreenLightTime, Color.green);

            /////////////////////
            yield return new WaitForSeconds(RedLightTime);

            TargetIntersection.SetCarLane(Direction.NS, false);
            TargetIntersection.SetCarLane(Direction.WE, true);
            lightStartS(GreenLightTime, Color.green);
            lightStartW(GreenLightTime, Color.red);

            TargetIntersection.disablelight(TargetIntersection.RightLaneObjects);
            TargetIntersection.disablelight(TargetIntersection.LeftLaneRedObjects);
            TargetIntersection.enablelight(TargetIntersection.RightLaneRedObjects);
            TargetIntersection.enablelight(TargetIntersection.LeftLaneObjects);

            ///////
            yield return new WaitForSeconds(RedLightTime);
        }
    }

    private void lightStartW(int time, Color color)
    {
        StartCoroutine(CountDownW(time, color));
    }
    private void lightStartS(int time, Color color)
    {
        StartCoroutine(CountDownS(time, color));
    }

    IEnumerator CountDownW(int time, Color color)
    {
        var redCounter = RedLightTime;
        var GreenCounter = GreenLightTime;
        while(redCounter >= 0 || GreenCounter>=0)
        {
            var WEcounter =  GreenCounter;
            
            if(WEcounter >= 0)
            {
                TargetIntersection.SetWELaneLightNumber(WEcounter,color);
            }
            GreenCounter--;
            redCounter--;
            yield return new WaitForSeconds(1f);
        }
        yield return null;

    }
    IEnumerator CountDownS(int time, Color color)
    {
        var redCounter = RedLightTime;
        var GreenCounter = GreenLightTime;
        while (redCounter >= 0 || GreenCounter >= 0)
        {
            var WEcounter = GreenCounter;

            if (WEcounter >= 0)
            {
                TargetIntersection.SetNSLaneLightNumber(WEcounter, color);
            }
            GreenCounter--;
            redCounter--;
            yield return new WaitForSeconds(1f);
        }
        yield return null;

    }
}
