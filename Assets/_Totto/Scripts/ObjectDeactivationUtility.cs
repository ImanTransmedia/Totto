using UnityEngine;
using System.Collections.Generic;

public static class ObjectDeactivationUtility
{
    public static void DeactivateObjectsInParts(
        List<GameObject> objectsToDeactivate,
        int totalParts,
        int hitsPerPart,
        int currentHit)
    {
        if (objectsToDeactivate == null || objectsToDeactivate.Count == 0 || totalParts <= 0 || hitsPerPart <= 0)
        {
            return;
        }

        // Calcular en qué parte nos encontramos basado en los golpes y la cantidad de golpes por parte
        int currentPart = currentHit / hitsPerPart;

        // Si ya hemos superado la cantidad de partes definidas, desactivar todos los objetos restantes
        if (currentPart > totalParts)
        {
            currentPart = totalParts;
        }

        // Calcular cuántos objetos desactivar para la parte actual
        int objectsToDeactivateCount = Mathf.CeilToInt((float)objectsToDeactivate.Count * currentPart / totalParts);

        // Desactivar los objetos correspondientes a la parte actual
        for (int i = 0; i < objectsToDeactivateCount; i++)
        {
            if (objectsToDeactivate.Count > 0)
            {
                GameObject obj = objectsToDeactivate[0];
                obj.SetActive(false);
                objectsToDeactivate.RemoveAt(0);
            }
            else
            {
                break; // Sale del bucle si ya no hay más objetos en la lista
            }
        }
    }
}
