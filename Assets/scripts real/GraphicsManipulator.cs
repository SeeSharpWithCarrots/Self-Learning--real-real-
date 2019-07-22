using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsManipulator : MonoBehaviour
{
    public Slider ShadowDist;

    // Shadow Distance
    void Update()
    {
        Debug.Log(ShadowDist.value);
        QualitySettings.shadowDistance = ShadowDist.value;
    }



    //TEXTURE QUALITY. Lower = Better - - -
    public void Ultra()
    {
        QualitySettings.masterTextureLimit = 0;
    }
    public void High()
    {
        QualitySettings.masterTextureLimit = 1;
    }
    public void Medium()
    {
        QualitySettings.masterTextureLimit = 2;
    }
    public void Low()
    {
        QualitySettings.masterTextureLimit = 3;
    }
    public void Lowest()
    {
        QualitySettings.masterTextureLimit = 4;
    }

    // ANISOTROPIC FILTERING - - -
    public void Aniso0() //disabled
    {
     
     QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
     Debug.Log("Aniso enabled");
    }
    public void Aniso1() //enabled
    {
    QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
    Debug.Log("Aniso disabled");
        
    }
    //SHADOW QUALITY - - - HARD - SOFT - NONE
    
    public void SHADOW_0() // Higher is better
    {
        QualitySettings.shadows = ShadowQuality.Disable;
    }
     public void SHADOW_1() 
    {
        QualitySettings.shadows = ShadowQuality.HardOnly;
    }
    public void SHADOW_2()
    {
        QualitySettings.shadows = ShadowQuality.All;
    }

    //SHADOW RESOLUTION
    public void ShadowResolutionVeryHigh()
    {
        QualitySettings.shadowCascades = 4;
    }

    public void ShadowResolutionHigh()
    {
        QualitySettings.shadowCascades = 2;
    }

    public void ShadowResolutioLow()
    {
        QualitySettings.shadowCascades = 0;
    }
}
