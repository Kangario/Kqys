using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] private float parallaxIntensity = 0.1f; // Intensity of the parallax effect
    [SerializeField] private Camera mainCamera;

    private Vector3 _startPosition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        _startPosition = transform.position;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position from screen space to world space
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

        // Calculate the offset based on mouse position and parallax intensity
        Vector3 offset = (mouseWorldPosition - mainCamera.transform.position) * parallaxIntensity;

        // Apply the offset to the background position
        transform.position = new Vector3(_startPosition.x + offset.x, _startPosition.y + offset.y, _startPosition.z);
    }
}
