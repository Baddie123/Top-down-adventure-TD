using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    private Transform focus;
    
    [SerializeField, Range(1f, 20f)]
    private float distance = 5f;

    public float focusRadius = 1f;

    private Vector3 focusPoint, previousFocusPoint;

    [SerializeField, Range(0f, 1f)] 
    private float focusCentering = 0.5f;
    
    [SerializeField, Range(1f, 360f)] 
    private float rotationSpeed = 90f;
    
    Vector2 orbitAngles = new Vector2(45f, 0f);
    
    private Vector2 rotInput = Vector2.zero;

    [SerializeField, Range(-89f, 89f)] 
    private float minVerticalAngle = -30f, maxVerticalAngle = 60f;

    private float lastManualRotationTime;
    private Quaternion lookRotation;

    [SerializeField, Range(0f, 90f)]
    private float alignSmoothRange = 45f;

    public Vector3 lookDirectionScale;
    
    [SerializeField, Min(0f)] private float alignDelay = 5f;
    void Awake()
    {
        focusPoint = focus.position;
        transform.localRotation = Quaternion.Euler(orbitAngles);

    }

    void LateUpdate()
    {
        
        UpdateFocus();
        
        if (ManualRotation() || AutomaticRotation())
        {
            ConstrainAngles();
            lookRotation = Quaternion.Euler(orbitAngles);
        }
        else
        {
            lookRotation = transform.localRotation;
        }
        Vector3 lookDirection = lookRotation * lookDirectionScale;

        Vector3 lookPosition = focusPoint - lookDirection * distance;
        transform.SetPositionAndRotation(lookPosition, lookRotation);

    }

    void UpdateFocus()
    {
        previousFocusPoint = focusPoint;
        Vector3 targetPoint = focus.position;
        float distance = Vector3.Distance(targetPoint, focusPoint);

        float t = 1f;
        if (distance > 0.1 && focusCentering > 0f)
        {
            t = Mathf.Pow(1 - focusCentering, Time.unscaledDeltaTime);
        }
        t = Mathf.Pow(1 - focusCentering, Time.unscaledDeltaTime);
        
        if (focusRadius > 0f)
        {
            if (distance > focusRadius)
            {
                t = Mathf.Min(t, focusRadius / distance);
            }
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);

        }
        else
        {
            focusPoint = targetPoint;
        }
    }
    
    bool ManualRotation () {
        if (Input.GetKey(KeyCode.E))
        {
            rotInput.y += .5f * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rotInput.y -= .5f * Time.unscaledDeltaTime;
        }
        const float e = 0.001f;
        if (rotInput.x < -e || rotInput.x > e || rotInput.y < -e || rotInput.y > e) {
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * rotInput;
            lastManualRotationTime = Time.unscaledTime;
            return true;
        }

        return false;
    }
    
    bool AutomaticRotation () {
        if (Time.unscaledTime - lastManualRotationTime < alignDelay) {
            return false;
        }
        
        Vector2 movement = new Vector2(
            focusPoint.x - previousFocusPoint.x,
            focusPoint.z - previousFocusPoint.z
            );

        float movementDeltaSqr = movement.sqrMagnitude;

        if (movementDeltaSqr < 0.00001f)
        {
            return false;
        }
        
        
        
        float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr));
        
        float rotationChange =
            rotationSpeed * Mathf.Min(Time.unscaledDeltaTime, movementDeltaSqr);
        
        float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(orbitAngles.y, headingAngle));
        if (deltaAbs < alignSmoothRange)
        {
            rotationChange *= deltaAbs / alignSmoothRange;
        }
        else if (180f - deltaAbs < alignSmoothRange) {
            rotationChange *= (180f - deltaAbs) / alignSmoothRange;
        }
        
        orbitAngles.y = Mathf.MoveTowardsAngle(orbitAngles.y, headingAngle, rotationChange);
        return true;
        
    }

    void ConstrainAngles()
    {
        orbitAngles.x = Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);
        if (orbitAngles.y < 0f)
        {
            orbitAngles.y += 360f;
        }
        if (orbitAngles.y >= 360f)
        {
            orbitAngles.y -= 360f;
        }
    }

    private void OnValidate()
    {
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
    }
    
    static float GetAngle (Vector2 direction) {
        float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
        return direction.x < 0f ? 360f - angle : angle;
    }
}
