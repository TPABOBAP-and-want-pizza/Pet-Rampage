using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float[] zoomLevels = new float[] { 8f, 10f, 12f }; 
    public int currentZoomLevel = 0; 
    public float zoomSpeed = 0.1f; 

    private Camera cameraComponent;

    void Start()
    {
        cameraComponent = GetComponent<Camera>();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0.0f)
        {
            if (scroll < 0)
            {
                IncreaseZoom();
            }
            else if (scroll > 0)
            {
                DecreaseZoom();
            }
        }
    }

    void IncreaseZoom()
    {
        if (currentZoomLevel < zoomLevels.Length - 1)
        {
            currentZoomLevel++;
            SetZoomLevel(currentZoomLevel);
        }
    }

    void DecreaseZoom()
    {
        if (currentZoomLevel > 0)
        {
            currentZoomLevel--;
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
