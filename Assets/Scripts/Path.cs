using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] Paths;
    [SerializeField] Transform[] Points;

    [SerializeField] private float speed;

    private int pointsindex=0;
    void Start()
    {
        int rand;
        rand = Random.Range(0, Paths.Length - 1);
        Points = Paths[rand].GetComponentsInChildren<Transform>();
        transform.position = Points[pointsindex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(pointsindex <= Points.Length -1)
        {
            transform.position = Vector3.MoveTowards(transform.position, Points[pointsindex].transform.position, speed * Time.deltaTime);
            
            if(transform.position == Points[pointsindex].transform.position) 
            {
                pointsindex += 1;
                // pointsindex = pointsindex + 1
            }
            if(pointsindex == Points.Length)
            {
                pointsindex = 0;
                // deactive, a new path,
            }
        }
    }
}
