using UnityEngine;

public class DisableColliders : MonoBehaviour
{
    public Collider2D[] colliders;

    public void DisableCollider()
    {
        foreach(Collider2D col in colliders)
        {
            col.enabled = false;
        }
    }

    public void EnableCollider()
    {
        foreach (Collider2D col in colliders)
        {
            col.enabled = true;
        }
    }
}
