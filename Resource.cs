using UnityEngine;

public class Resource : MonoBehaviour
{
    public void Follow(Transform transform)
    {
        this.transform.SetParent(transform);
    }
}