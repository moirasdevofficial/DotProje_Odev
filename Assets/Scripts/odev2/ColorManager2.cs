using UnityEngine;
public enum GameColor
{
    Red,
    Blue,
    Green,
    Yellow,
    Pink,
    Orange,
    Gray,
    Brown
}

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;
    public Material[] materials;

    void Awake()
    {
       Instance = this;
    }
    public Material GetMaterial(GameColor color)
    {
        return materials[(int)color];
    }
}
