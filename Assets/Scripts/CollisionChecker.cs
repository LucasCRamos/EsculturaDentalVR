using System.Collections;
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

    void Start()
    {
        StartCoroutine(CheckCollisionsCoroutine());
    }

    IEnumerator CheckCollisionsCoroutine()
    {
        while (true)
        {
            allCollidingFirstState = AllPairsCollidingFirstState();
            allCollidingSecondState = AllPairsCollidingSecondState();

            yield return new WaitForSeconds(0.5f);
        }
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

                    if (!targetObjectCollider.bounds.Intersects(fittingObjectCollider.bounds))
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

                    if (!targetObjectCollider.bounds.Intersects(fittingObjectCollider.bounds))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}
