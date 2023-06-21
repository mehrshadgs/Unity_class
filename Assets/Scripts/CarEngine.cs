using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public GameObject paths;
    private Transform path;
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

   

    public WheelCollider frontDriverW, frontPassengerW, RearDriverW, RearPassengerW;
    public Transform frontDriverT, frontPassengerT, RearDriverT, RearPassengerT;

    public float motorForce = 50;
    public float maxSteerAngle = 30;

    public float brakeForce = 100f;

    public int maxSpeed;
    private float currentSpeed;

    private List<Transform> nodes;
    public int currentNode = 0;


    public bool isBrake;
    private int maxPath;

    public Manger manger;
    private int randomPath;
    private void OnEnable()
    {
        maxPath = paths.transform.childCount;

        randomPath = Random.Range(0, maxPath);

        path = paths.transform.GetChild(randomPath);
        
        Debug.Log("Active");
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
       

        MakeRoation();
     
        if(isFree())
        {
            transform.position = new Vector3(nodes[0].position.x, transform.position.y, nodes[0].position.z);
        }
        else
        {
            print("Full");
            gameObject.SetActive(false);
            if(manger.carActive != 0 )
            {
                manger.carActive = manger.carActive - 1;
            }

        }
    }
    private void Start()
    {

    }
    private bool isFree()
    {
        Collider[] isThereCar = Physics.OverlapBox(nodes[0].position, new Vector3(3f, 0f, 3f));
        if(isThereCar.Length == 0)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }
    private void MakeRoation()
    {
        if (path.gameObject.name.StartsWith("N") || path.gameObject.name.StartsWith("R"))
        {
      
            var rotaionVector = transform.rotation.eulerAngles;
            rotaionVector.y = 90;
            rotaionVector.x = 0;
            rotaionVector.z = 0;
            transform.localRotation = Quaternion.Euler(rotaionVector);
        }
        else
        {

            var rotaionVector = transform.rotation.eulerAngles;
            rotaionVector.y = 0;
            rotaionVector.x = 0;
            rotaionVector.z = 0;
            transform.localRotation = Quaternion.Euler(rotaionVector);
        }
    }
    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }
    public void Steer()
    {
        // m_steeringAngle = maxSteerAngle * m_horizontalInput;
        // frontDriverW.steerAngle = m_steeringAngle;
        // frontPassengerW.steerAngle = m_steeringAngle;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteerAngle = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        frontDriverW.steerAngle = newSteerAngle;
        frontPassengerW.steerAngle = newSteerAngle;
    }
    private void Drive()
    {
        //frontDriverW.motorTorque = m_verticalInput * motorForce;
        //frontPassengerW.motorTorque = m_verticalInput * motorForce;

        currentSpeed = 2 * Mathf.PI * frontDriverW.radius * frontDriverW.rpm * 60 / 1000;
        //print(currentSpeed);
        if(currentSpeed < maxSpeed)
        {
            frontDriverW.motorTorque =  motorForce;
            frontPassengerW.motorTorque =  motorForce;
        }
        else
        {
            frontDriverW.motorTorque = 0;
            frontPassengerW.motorTorque = 0;
        }
    }
    private void UpdateWheelpose(WheelCollider collider, Transform transform)
    {
        Vector3 pos = transform.position;
        Quaternion quat = transform.rotation;

        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;
        transform.rotation = quat;
    }
    private void UpdateWheelPoses()
    {
        UpdateWheelpose(frontDriverW, frontDriverT);
        UpdateWheelpose(frontPassengerW, frontPassengerT);
        UpdateWheelpose(RearDriverW, RearDriverT);
        UpdateWheelpose(RearPassengerW, RearPassengerT);
    }

    private void CheckWaypointDistance()
    {
       // print(Vector3.Distance(transform.position, nodes[currentNode].position));
        if(Vector3.Distance(transform.position, nodes[currentNode].position) <2f)
        {
            if(currentNode == nodes.Count -1)
            {
                transform.position = new Vector3(nodes[0].position.x, transform.position.y, nodes[0].position.z);
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
    private void Braking()
    {
        if (isBrake)
        {
            frontDriverW.brakeTorque = brakeForce;
            frontPassengerW.brakeTorque = brakeForce;
        }
        else
        {
            frontDriverW.brakeTorque = 0;
            frontPassengerW.brakeTorque = 0;
        }
    }
    private void FixedUpdate()
    {
        // GetInput();
      
        Steer();
        Drive();
        CheckWaypointDistance();
        Braking();
        UpdateWheelPoses();
    }
    //////////
    /// realtivevector 
    // 

}
