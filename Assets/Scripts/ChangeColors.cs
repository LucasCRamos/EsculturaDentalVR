using UnityEngine;
using TMPro;

public class ChangeColors : MonoBehaviour
{
    public GameObject[] fragments = new GameObject[5];

    public GameObject[] arrows = new GameObject[5];

    public GameObject canvas;
    public TextMeshProUGUI[] fragmentNames = new TextMeshProUGUI[5];
    private CollisionChecker collisionChecker;

    private Color[] colors = new Color[5]
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        Color.magenta
    };

    private Color[] fragmentsOriginalColors;
    private Color[] fragmentNamesOriginalColors;

    private bool isFragmentOriginalColor = true;
    private bool isFragmentNameOriginalColor = true;
    private bool wereAllFragmentsWithinMargin = false;

    private const float margin = 0.1f;

    void Start()
    {
        collisionChecker = FindObjectOfType<CollisionChecker>();

        if (collisionChecker == null)
        {
            Debug.LogError("Nenhum CollisionChecker foi encontrado na cena!");
        }

        fragmentsOriginalColors = new Color[fragments.Length];
        fragmentNamesOriginalColors = new Color[fragmentNames.Length];

        for (int i = 0; i < fragments.Length; i++)
        {
            if (fragments[i] != null)
            {
                Renderer renderer = fragments[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    fragmentsOriginalColors[i] = renderer.material.color;
                }
            }
        }

        for (int i = 0; i < fragmentNames.Length; i++)
        {
            if (fragmentNames[i] != null)
            {
                fragmentNamesOriginalColors[i] = fragmentNames[i].color;
            }
        }
    }

    void Update()
    {
        bool areAllFragmentsWithinMargin = AllFragmentsWithinMargin();

        if (areAllFragmentsWithinMargin != wereAllFragmentsWithinMargin)
        {
            wereAllFragmentsWithinMargin = areAllFragmentsWithinMargin;
            ChangeFragmentColors();
            ActivateObjects();

            if (collisionChecker.allColliding)
            {
                ChangeNameColors();
            }
        }

    }

    bool AllFragmentsWithinMargin()
    {
        foreach (var fragment in fragments)
        {
            if (fragment != null)
            {
                Vector3 localPosition = fragment.transform.localPosition;

                if (Mathf.Abs(localPosition.x) > margin ||
                    Mathf.Abs(localPosition.y) > margin ||
                    Mathf.Abs(localPosition.z) > margin)
                {
                    return false;
                }
            }
        }

        return true;
    }

    void ChangeFragmentColors()
    {
        for (int i = 0; i < fragments.Length; i++)
        {
            if (fragments[i] != null)
            {
                Renderer renderer = fragments[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = isFragmentOriginalColor ? colors[i] : fragmentsOriginalColors[i];
                }
            }
        }

        isFragmentOriginalColor = !isFragmentOriginalColor;
    }

    void ActivateObjects()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (arrows[i] != null)
            {
                arrows[i].SetActive(wereAllFragmentsWithinMargin);
            }
            
        }

        canvas.SetActive(wereAllFragmentsWithinMargin);
    }

    void ChangeNameColors()
    {
        for (int i = 0; i < fragmentNames.Length; i++)
        {
            if (fragmentNames[i] != null)
            {
                fragmentNames[i].color = isFragmentNameOriginalColor ? colors[i] : fragmentNamesOriginalColors[i];
            }
        }

        isFragmentNameOriginalColor = !isFragmentNameOriginalColor;
    }
}
