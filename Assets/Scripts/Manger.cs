using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manger : MonoBehaviour
{
    public GameObject Cars;
    public bool Genrate;
    [SerializeField] private float time;

    public  int carActive = 0;
    public int MaxActiveCar;

    private bool handel;
    private void Start()
    {
        handel = true;
        Genrate = true;
    }
    private void Update()
    {
       

        if(MaxActiveCar <= carActive)
        {
            Genrate = false;
        }
        if(Genrate && handel)
        {
            InvokeRepeating("activeCars", 0f, time);
            handel = false;
        }
    }

       

    private void activeCars()
    {
        if(!Genrate)
        {
            CancelInvoke("activeCars");
            handel = true;
            return;
        }
        int n = Cars.transform.childCount;
        for (int i = 0; i < n; i ++)
        {
            if(Cars.transform.GetChild(i).gameObject.activeInHierarchy == false)
            {
                Cars.transform.GetChild(i).gameObject.SetActive(true);
                carActive += 1;
                break;
            }
            else if(i == n -1)
            {
                print("No more car");
                Genrate = false;
            }
        }
    }
}
