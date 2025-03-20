using UnityEngine;

public class CrosshairTargetController : MonoBehaviour
{

    Camera mainCamera;
    Ray ray;
    RaycastHit hitInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;        
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = mainCamera.transform.position;
        ray.direction = mainCamera.transform.forward;

        if (Physics.Raycast(ray, out hitInfo))
        {
            transform.position = hitInfo.point;
        }
        else
        {
            transform.position = ray.origin + ray.direction * 1000.0f;
        }
    }
}
