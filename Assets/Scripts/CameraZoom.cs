using UnityEngine;

public class CameraZoom : MonoBehaviour
{
<<<<<<< HEAD
    public float[] zoomLevels = new float[] { 8f, 10f, 12f }; 
    public int currentZoomLevel = 0; 
    public float zoomSpeed = 0.1f; 
=======
    public float[] zoomLevels = new float[] { 8f, 10f, 12f };
    public int currentZoomLevel = 0;
    public float zoomSpeed = 0.1f;
>>>>>>> dev

    private Camera cameraComponent;

    void Start()
    {
        cameraComponent = GetComponent<Camera>();
    }

<<<<<<< HEAD
    //void Update()
    //{
    //    float scroll = Input.GetAxis("Mouse ScrollWheel");

    //    if (scroll != 0.0f)
    //    {
    //        if (scroll < 0)
    //        {
    //            IncreaseZoom();
    //        }
    //        else if (scroll > 0)
    //        {
    //            DecreaseZoom();
    //        }
    //    }
    //}

    void IncreaseZoom()
    {
        if (currentZoomLevel < zoomLevels.Length - 1)
        {
            currentZoomLevel++;
=======
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Equals))
        {
            IncreaseZoom();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            DecreaseZoom();
        }
    }

    void IncreaseZoom()
    {

        if (currentZoomLevel > 0)
        {
            currentZoomLevel--;
>>>>>>> dev
            SetZoomLevel(currentZoomLevel);
        }
    }

    void DecreaseZoom()
    {
<<<<<<< HEAD
        if (currentZoomLevel > 0)
        {
            currentZoomLevel--;
=======
        if (currentZoomLevel < zoomLevels.Length - 1)
        {
            currentZoomLevel++;
>>>>>>> dev
            SetZoomLevel(currentZoomLevel);
        }
    }

    void SetZoomLevel(int level)
    {
        float targetZoom = zoomLevels[level];
        StartCoroutine(ChangeZoom(targetZoom));
    }

    System.Collections.IEnumerator ChangeZoom(float targetZoom)
    {
        float initialZoom = cameraComponent.orthographicSize;
        float timePassed = 0f;

        while (timePassed < zoomSpeed)
        {
            timePassed += Time.deltaTime;
            cameraComponent.orthographicSize = Mathf.Lerp(initialZoom, targetZoom, timePassed / zoomSpeed);
            yield return null;
        }

        cameraComponent.orthographicSize = targetZoom;
    }
}
