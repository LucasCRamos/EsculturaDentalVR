using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugPositionsVR : MonoBehaviour
{
    [Header("Lista de Objetos para Monitorar")]
    public List<Transform> monitoredObjects;

    [Header("Componente de Texto para Exibir as Posições")]
    public TextMeshProUGUI debugText;

    private void Update()
    {
        if (debugText != null && monitoredObjects.Count > 0)
        {
            debugText.text = "Posições dos Objetos:\n";

            foreach (Transform obj in monitoredObjects)
            {
                if (obj != null)
                {
                    debugText.text += $"{obj.name}: {obj.position}, {obj.localPosition}\n";
                }
            }
        }
    }
}
