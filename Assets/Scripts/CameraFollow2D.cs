using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] static Transform objectTransform;
    [SerializeField] static string objectTag = "";
    [SerializeField] static float movingSpeed = 0.1f;

    public static float MovingSpeed { get => movingSpeed; set => movingSpeed = value; }
    public static void SetTarget(string tag)
    {
        try
        {
            objectTransform = GameObject.FindGameObjectWithTag(objectTag).transform;
        }
        catch (System.NullReferenceException)
        {
            Debug.LogWarning("Object with tag " + objectTag + " not found.");
        }
    }
    void Start()
    {
        if (objectTransform == null)
        {
            if (objectTag == "")
            {
                objectTag = "Player";
            }

            objectTransform = GameObject.FindGameObjectWithTag(objectTag).transform;
        }
    }
    void Update()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        if (objectTransform)
        {
            //Debug.Log("x - " + objectTransform.position.x + " y - " + objectTransform.position.y + " z - " + objectTransform.position.z);

            Vector3 pos = Vector3.Lerp(transform.position, objectTransform.position, MovingSpeed * Time.deltaTime);
            transform.position = pos;
        }
    }
}
