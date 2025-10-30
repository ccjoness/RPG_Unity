using UnityEngine;

public class ObjectDistributor : MonoBehaviour
{
    public GameObject[] objectsToDistribute;
    public Vector3 startPosition = new Vector3(-5f, 0f, 0f);
    public Vector3 endPosition = new Vector3(5f, 0f, 0f);

    void Start()
    {
        DistributeObjectsEvenly();
    }

    void DistributeObjectsEvenly()
    {
        if (objectsToDistribute == null || objectsToDistribute.Length == 0)
        {
            Debug.LogWarning("No objects assigned for distribution.");
            return;
        }

        for (int i = 0; i < objectsToDistribute.Length; i++)
        {
            float t = (float)i / (objectsToDistribute.Length - 1); // Calculate interpolation factor
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, t);
            objectsToDistribute[i].transform.position = newPosition;
        }
    }
}