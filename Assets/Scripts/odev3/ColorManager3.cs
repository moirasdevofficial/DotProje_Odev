using UnityEngine;
public enum EmissionColor
{
RedEmission,
GreenEmission ,
}

public class ColorManager3 : MonoBehaviour
{
    public static ColorManager3 Instance;
    public Material[] materials;

    void Awake()
    {
       Instance = this;
    }
    public Material GetMaterialEmission(EmissionColor color)
    {
        return materials[(int)color];
    }
}
