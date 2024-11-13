using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public List<GameObject> arrows;
    public List<GameObject> texts;
    public bool allColliding;

    void Update()
    {

        allColliding = AllPairsColliding();
    }

    bool AllPairsColliding()
    {
        foreach (GameObject arrow in arrows)
        {
            foreach (GameObject text in texts)
            {
                if (arrow != null && text != null && arrow.tag == text.tag)
                {
                    Collider arrowCollider = arrow.GetComponent<Collider>();
                    Collider textCollider = text.GetComponent<Collider>();

                    if (arrowCollider == null || textCollider == null || 
                        !arrowCollider.bounds.Intersects(textCollider.bounds))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}
