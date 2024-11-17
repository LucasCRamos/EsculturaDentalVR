using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public List<GameObject> targetObjectsFirstState;
    public List<GameObject> fittingObjectFirstState;
    public List<GameObject> targetObjectsSecondState;
    public List<GameObject> fittingObjectSecondState;
    public bool allCollidingFirstState;
    public bool allCollidingSecondState;

    void Update()
    {

        allCollidingFirstState = AllPairsCollidingFirstState();
        allCollidingSecondState = AllPairsCollidingSecondState();
    }

    bool AllPairsCollidingFirstState()
    {
        foreach (GameObject targetObject in targetObjectsFirstState)
        {
            foreach (GameObject fittingObject in fittingObjectFirstState)
            {
                if (targetObject.tag == fittingObject.tag)
                {
                    Collider targetObjectCollider = targetObject.GetComponent<Collider>();
                    Collider fittingObjectCollider = fittingObject.GetComponent<Collider>();

                    if (
                        !targetObjectCollider.bounds.Intersects(fittingObjectCollider.bounds))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    bool AllPairsCollidingSecondState()
    {
        foreach (GameObject targetObject in targetObjectsSecondState)
        {
            foreach (GameObject fittingObject in fittingObjectSecondState)
            {
                if (targetObject.tag == fittingObject.tag)
                {
                    Collider targetObjectCollider = targetObject.GetComponent<Collider>();
                    Collider fittingObjectCollider = fittingObject.GetComponent<Collider>();

                    if (
                        !targetObjectCollider.bounds.Intersects(fittingObjectCollider.bounds))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

}
