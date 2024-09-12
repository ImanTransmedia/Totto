using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class UtilityAngel : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ControlOutput suprimidoTrigger; // Salida de flujo personalizada para cuando se desactive una parte

    [DoNotSerialize]
    public ValueInput objectsToDeactivate;

    [DoNotSerialize]
    public ValueInput totalParts;

    [DoNotSerialize]
    public ValueInput hitsPerPart;

    [DoNotSerialize]
    public ValueInput currentHit;

    [DoNotSerialize]
    public ValueOutput suprimidoCountOutput; // Salida del contador de desactivaciones

    private int suprimidoCount = 0; // Contador de cuántas veces se ha ejecutado 'suprimido'

    protected override void Definition()
    {
        // Definir entradas y salidas del nodo
        inputTrigger = ControlInput("", OnTriggered);
        outputTrigger = ControlOutput("");
        suprimidoTrigger = ControlOutput("suprimido"); // Salida de flujo para 'suprimido'

        // Definir valores de entrada
        objectsToDeactivate = ValueInput<List<GameObject>>("objectsToDeactivate");
        totalParts = ValueInput<int>("totalParts", 1); // Por defecto al menos 1 parte
        hitsPerPart = ValueInput<int>("hitsPerPart", 1); // Por defecto al menos 1 golpe por parte
        currentHit = ValueInput<int>("currentHit", 0); // Golpes actuales

        // Definir valor de salida
        suprimidoCountOutput = ValueOutput<int>("suprimidoCount", (flow) => suprimidoCount);

        // Requerir que los valores de entrada estén disponibles antes de ejecutar
        Requirement(objectsToDeactivate, inputTrigger);
        Requirement(totalParts, inputTrigger);
        Requirement(hitsPerPart, inputTrigger);
        Requirement(currentHit, inputTrigger);

        // Asignar el valor de salida 'suprimidoCount' al 'suprimidoTrigger'
        //Assignment(suprimidoTrigger, suprimidoCountOutput);
    }

    // Método que se ejecuta cuando el nodo es activado
    private ControlOutput OnTriggered(Flow flow)
    {
        // Obtener los valores de entrada
        List<GameObject> objectList = flow.GetValue<List<GameObject>>(objectsToDeactivate);
        int totalPartsValue = flow.GetValue<int>(totalParts);
        int hitsPerPartValue = flow.GetValue<int>(hitsPerPart);
        int currentHitValue = flow.GetValue<int>(currentHit);

        // Calcular en qué parte nos encontramos basado en los golpes y la cantidad de golpes por parte
        int currentPart = currentHitValue / hitsPerPartValue;

        // Si ya hemos superado la cantidad de partes definidas, desactivar todos los objetos restantes
        if (currentPart > totalPartsValue)
        {
            currentPart = totalPartsValue;
        }

        // Calcular cuántos objetos desactivar para la parte actual
        int objectsToDeactivateCount = Mathf.CeilToInt((float)objectList.Count * currentPart / totalPartsValue);

        // Desactivar los objetos correspondientes a la parte actual
        for (int i = 0; i < objectsToDeactivateCount; i++)
        {
            if (objectList.Count > 0)
            {
                GameObject obj = objectList[0];
                obj.SetActive(false);
                objectList.RemoveAt(0);
            }
            else
            {
                break; // Sale del bucle si ya no hay más objetos en la lista
            }
        }

        // Si se completó una parte, incrementar el contador y retornar el flujo 'suprimido'
        if (currentHitValue % hitsPerPartValue == 0 && currentPart <= totalPartsValue)
        {
            suprimidoCount++; // Incrementar el contador de suprimido
            return suprimidoTrigger; // Lanza el flujo de salida 'suprimido'
        }

        // Retornar el flujo de salida estándar 'output'
        return outputTrigger;
    }
}
