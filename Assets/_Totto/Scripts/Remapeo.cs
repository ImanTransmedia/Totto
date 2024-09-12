using UnityEngine;

public static class Remapeo

{
    public static float Map(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        float mappedValue = (value - inputMin) * (outputMax - outputMin) / (inputMax - inputMin) + outputMin;
        if (mappedValue < 0)
        {
            mappedValue = 0;
        }
        return mappedValue;
    }
}
