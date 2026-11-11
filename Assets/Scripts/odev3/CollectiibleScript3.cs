using UnityEngine;

public class CollectiibleScript : MonoBehaviour
{
    Renderer _renderer; // topların rendererını tutacağımız değişken, materiallerine erişmek ve değiştirmek için.
    int _collectibleint;

    private void SetRandomBall()
    {
        Debug.Log("Integer: " + _collectibleint); // Konsolda integer değerini kontrol etmek için.
        if (_collectibleint == 0)
        {
            _renderer.material = ColorManager3.Instance.GetMaterialEmission(EmissionColor.GreenEmission); // ColorManager3 sınıfındaki GetMaterialEmission fonksiyonunu çağırarak yeşil topun materialini alıyoruz.
            gameObject.tag = "greenball";
        }
        else if (_collectibleint == 1)
        {
            _renderer.material = ColorManager3.Instance.GetMaterialEmission(EmissionColor.RedEmission);  // ColorManager3 sınıfındaki GetMaterialEmission fonksiyonunu çağırarak kırmızı topun materialini alıyoruz.
            gameObject.tag = "redball";
        }
    }
    void Start()
    {
            _renderer = GetComponent<Renderer>();
            _collectibleint = Random.Range(0,4); // 0 ve 1 yeşil ve kırmızı top için kullanılır. 2 ve 3 yeşil top çıkma ihtimalini arttırmak için.
            if (_collectibleint > 1) // Yeşil top çıkma ihtimalini arttırmak için 1'den büyükse 0 olarak ayarla.
            {
                _collectibleint = 0;
            }
            SetRandomBall(); // integerını yani rengini belirldikten sonra rengi uygula.
    }


}
