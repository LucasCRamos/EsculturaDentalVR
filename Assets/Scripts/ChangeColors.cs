using UnityEngine;
using TMPro;

public class ChangeColors : MonoBehaviour
{
    public GameObject[] fragments = new GameObject[5];
    public TextMeshProUGUI[] fragmentNames = new TextMeshProUGUI[5];

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
            ChangeFragmentColors();
            wereAllFragmentsWithinMargin = areAllFragmentsWithinMargin;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeNameColors();
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
