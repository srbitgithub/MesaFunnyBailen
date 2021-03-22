using UnityEngine.UI;
using UnityEngine;

public class FurnitureAutoConfig : MonoBehaviour
{
    string findBodies = "body";
    string findLegs = "leg";

    [Header("FURNITURE PARTS")]
    public Transform[] bodies;
    public Transform[] legs;

    public Transform furnitureParent;

    public GameObject furniture;

    private void Awake()
    {
        furnitureParent = GetComponent<Transform>();

        int childNumber = furnitureParent.childCount;

        bodies = new Transform[childNumber];
        bodies = findChilds(findBodies, furnitureParent, childNumber);

        legs = new Transform[5];
        legs = findChilds(findLegs, furnitureParent, 5);
    }
    private void Start()
    {
        for (int i = 1; i < legs.Length; i++)
        {
            Transform item;
            item = legs[i];
            if (item != null)
            {
                item.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    Transform[] findChilds(string findChildContains, Transform Parent, int childsNumber)
    {
        Transform children;
        int TotalChild = Parent.childCount;
        Transform[] objectsToReturn = new Transform[childsNumber];
        int objectsToReturnPosition = 0;
        for (int i = 0; i < TotalChild; i++)
        {
            children = Parent.GetChild(i);
            if (children.name.Contains(findChildContains))
            {
                objectsToReturn[objectsToReturnPosition] = children;
                objectsToReturnPosition++;
            }
        }
        return objectsToReturn;
    }
}
