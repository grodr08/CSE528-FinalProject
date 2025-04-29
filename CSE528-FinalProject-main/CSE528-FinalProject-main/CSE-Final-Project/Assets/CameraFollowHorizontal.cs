using UnityEngine;

public class CameraFixY : MonoBehaviour
{
    public float fixedY = 0f;

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = fixedY;
        transform.position = pos;
    }
}

