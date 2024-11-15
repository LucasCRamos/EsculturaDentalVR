using UnityEngine;
using TMPro;

public class ChangeColors : MonoBehaviour
{
    public GameObject[] fragments = new GameObject[5];

    public GameObject[] arrows = new GameObject[5];

    public GameObject canvasSegundaEtapa;

    public bool areAllFragmentsWithinMargin;

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

    private const float margin = 0.01f;

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
        areAllFragmentsWithinMargin = AllFragmentsWithinMargin();

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
        if (fragments == null)
            return true;

        Vector3 referencePosition = fragments[0]?.transform.position ?? Vector3.zero;

        foreach (var fragment in fragments)
        {
            if (fragment != null)
            {
                Vector3 position = fragment.transform.position;

                if (Mathf.Abs(position.x - referencePosition.x) > margin ||
                    Mathf.Abs(position.y - referencePosition.y) > margin ||
                    Mathf.Abs(position.z - referencePosition.z) > margin)
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
        canvasSegundaEtapa.SetActive(wereAllFragmentsWithinMargin);
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
