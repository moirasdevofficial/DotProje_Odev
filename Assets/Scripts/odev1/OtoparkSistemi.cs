using UnityEngine;

public class Traffic_System : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int ParkSuresi = 6;
        Debug.Log("Ortalama kalınan otopark süresi: " + ParkSuresi);

        switch(ParkSuresi)
        {
            case 1:
            Debug.Log("Otopark ücreti 120tl");
            break;

            case 2:
            Debug.Log("Otopark ücreti 200tl");
            break;

            case 3:
            Debug.Log("Otopark ücreti 300tl");
            break;
            
            case 4:
            Debug.Log("Otopark ücreti 400tl");
            break;

            default:
            Debug.Log("Otopark ücreti 550tl");
            break;
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
