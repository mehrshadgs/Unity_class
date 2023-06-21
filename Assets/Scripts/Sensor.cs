using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Sensor : MonoBehaviour
{
    public bool draw;

    public Transform Front;
    public float lenght;

    public CarEngine carEngine;

    public bool find;
    private void OnDrawGizmos()
    {
        if(draw)
        {
            Gizmos.DrawLine(Front.position, Front.position + Front.forward * lenght);
            
        }
    }
    private void FixedUpdate()
    {
        SFront();
    }
    private void SFront()
    {
        var dir = Front.forward;
        var start = Front.position;
        var ray = new Ray(start, dir);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, lenght))
        {
            if(hit.transform.tag == "BlockCar" || hit.transform.tag == "Car")
            {
                Debug.DrawLine(start, start + dir * lenght, Color.red);
                carEngine.isBrake = true;
                find = true;
            }
        }
        if(find)
        {
            if(!Physics.Raycast(ray, out hit, lenght))
            {
                Debug.DrawLine(start, start + dir * lenght, Color.green);
                carEngine.isBrake = false;
                find = false;
            }
        }

    }
}
