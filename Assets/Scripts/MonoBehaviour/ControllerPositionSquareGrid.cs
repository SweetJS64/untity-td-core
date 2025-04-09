using UnityEngine;

public class ControllerPositionSquareGrid : MonoBehaviour
{
    void OnMouseExit()
    {
        transform.position = new Vector3(100, 100, 0);
    }
}