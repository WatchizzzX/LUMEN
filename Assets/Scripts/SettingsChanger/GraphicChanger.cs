using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GraphicChanger
{
    public enum GraphicsSettings
    {
        Low = 0,
        Medium = 1,
        High = 2
    }
    
    public static void SetQuality(GraphicsSettings graphicsSettings)
    {
        QualitySettings.SetQualityLevel((int)graphicsSettings);
    }
    
    public static void SetQuality(int graphicsSettings)
    {
        QualitySettings.SetQualityLevel(graphicsSettings);
    }
}
