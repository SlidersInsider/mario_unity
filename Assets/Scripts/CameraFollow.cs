using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform leftBounds;
    public Transform rightBounds;
    public Transform bottomBounds;
    public Transform topBounds;

    public float smoothDampTime = 0.15f;
    private Vector3 smoothDampVelocity = Vector3.zero;

    private float camWidth, camHeight, levelMinX, levelMaxX, levelMinY, levelMaxY;

    void Start()
    {
        camHeight = Camera.main.orthographicSize * 2;
        camWidth = camHeight * Camera.main.aspect;

        float leftBoundsWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float rightBoundsWidth = rightBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;

        levelMinX = leftBounds.position.x + leftBoundsWidth + (camWidth / 2);
        levelMaxX = rightBounds.position.x - rightBoundsWidth - (camWidth / 2);

        float bottomBoundsWidth = bottomBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;
        float topBoundsWidth = topBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;

        levelMinY = bottomBounds.position.y + bottomBoundsWidth + (camWidth / 2);
        levelMaxY = topBounds.position.y - topBoundsWidth - (camWidth / 2);
    }

    void Update()
    {
        if (target) 
        {
            float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, target.position.x));
            float x = Mathf.SmoothDamp(transform.position.x, targetX, ref smoothDampVelocity.x, smoothDampTime);

            float targetY = Mathf.Max(levelMinY, Mathf.Min(levelMaxY, target.position.y));
            float y = Mathf.SmoothDamp(transform.position.y, targetY, ref smoothDampVelocity.y, smoothDampTime);

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
