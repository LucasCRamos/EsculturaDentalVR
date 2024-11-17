using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class MainScript : MonoBehaviour
{

    public GameObject[] objectsToShowFirstState;

    public GameObject[] objectsToHideFirstState;

    public GameObject canvasToGoToSecondState;

    public GameObject[] objectsToShowSecondState;

    public GameObject[] objectsToHideSecondState;

    public GameObject[] objectsToShowThirdState;

    public GameObject[] objectsToHideThirdState;
    public int gameState = 0;

    public int previousGameState = 0;

    private bool fragmentColorsChanged = false;

    private bool fragmentNamesColorsChanged = false;

    public GameObject[] fragments = new GameObject[5];
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

    public void ChangeGameState()
    {
        gameState = gameState + 1;
    }

    void Start()
    {
        collisionChecker = FindObjectOfType<CollisionChecker>();
    }

    void Update()
    {

        if (gameState != previousGameState)
        {

            switch (gameState)
            {
                case 0: // fase do menu
                    break;
                case 1: // fase do puzzle
                    foreach (GameObject obj in objectsToShowFirstState)
                    {
                        obj.SetActive(true);
                    }
                    foreach (GameObject obj in objectsToHideFirstState)
                    {
                        obj.SetActive(false);
                    }
                    break;
                case 2: // fase da associacao dos nomes
                    foreach (GameObject obj in objectsToShowSecondState)
                    {
                        obj.SetActive(true);
                    }
                    foreach (GameObject obj in objectsToHideSecondState)
                    {
                        obj.SetActive(false);
                    }
                    break;
                case 3: // fase da conclusao
                    ChangeNameColors();
                    foreach (GameObject obj in objectsToShowThirdState)
                    {
                        obj.SetActive(true);
                    }
                    foreach (GameObject obj in objectsToHideThirdState)
                    {
                        obj.SetActive(false);
                    }
                    break;
                default:
                    Debug.Log("Estado não reconhecido.");
                    break;
            }

            previousGameState = gameState;
        }

        if (collisionChecker.allCollidingFirstState && !fragmentColorsChanged)
        {
            ChangeFragmentColors();

            canvasToGoToSecondState.SetActive(true);

            fragmentColorsChanged = true;

        }

        if (collisionChecker.allCollidingSecondState && !fragmentNamesColorsChanged)
        {
            ChangeGameState();

            fragmentNamesColorsChanged = true;

            foreach (GameObject obj in objectsToHideThirdState)
            {
                var scriptType = Type.GetType("HandGrabInteractable");

                if (scriptType != null)
                {
                    var scriptToDisable = obj.GetComponent(scriptType) as MonoBehaviour;
                    if (scriptToDisable != null)
                    {
                        scriptToDisable.enabled = false;
                    }
                    else
                    {
                        Debug.LogWarning($"O script {scriptType.Name} não foi encontrado no objeto {obj.name}.");
                    }
                }
            }
        }

    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);   
    }

    public void QuitApplication()
    {
         Application.Quit();
    }

    void ChangeFragmentColors()
    {
        for (int i = 0; i < fragments.Length; i++)
        {
            Renderer renderer = fragments[i].GetComponent<Renderer>();
            renderer.material.color = colors[i];
        }

    }

    void ChangeNameColors()
    {
        for (int i = 0; i < fragmentNames.Length; i++)
        {
            fragmentNames[i].color = colors[i];
        }
    }
}
