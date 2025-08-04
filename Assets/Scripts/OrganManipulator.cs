using UnityEngine;

public class OrganManipulator : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;
    private Vector3 lastTouchPosition;
    private bool isDragging = false;

    void Update()
    {
        // Two-finger pinch to SCALE
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch0.position, touch1.position);
                initialScale = transform.localScale;
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                if (Mathf.Approximately(initialDistance, 0)) return;
                float factor = currentDistance / initialDistance;
                transform.localScale = initialScale * factor;
            }
        }

        // One-finger drag to ROTATE
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float rotationSpeed = 0.2f;
                float rotateAmount = touch.deltaPosition.x * rotationSpeed;
                transform.Rotate(Vector3.up, -rotateAmount, Space.World);
            }
        }
    }
}
