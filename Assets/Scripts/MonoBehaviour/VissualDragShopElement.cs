using UnityEngine;

public class VissualDragShopElement : MonoBehaviour
{
    private void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.position = Vector3.Lerp(transform.position, ray.origin, Time.deltaTime * 10);
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }
    }
}