using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player3Script : MonoBehaviour
{
   public int _score = 0; //oyuncunun skoru
   private Rigidbody _rb;
   private bool _isGrounded; // karakterin yerde olup olmadığını kontrol etmek için kullanılan değişken
   int _levelIndex; // sahne indexi
   int _levelMinScore; // leveli geçmek için gerekli minimum skor
   [SerializeField] private float _jumpPower = 5f; // karakterin zıplama gücü
   [SerializeField] TMP_Text _sText; // ekranda gözükecek skor texti
   [SerializeField] float _moveSpeed; // karakterin hareket hızı
   [SerializeField] private AudioSource _audiosource; // ses kaynağı için değişken
   [SerializeField] private AudioClip _truesound; // yeşil topun sesini tutacak değişken
   [SerializeField] private AudioClip _wrongsound; // kırmızı topun sesini tutacak değişken


   private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _levelIndex = SceneManager.GetActiveScene().buildIndex; // sahne indexini alır
        SetMinScore(); // level indexine göre leveli geçmek için gerekli minimum skoru ayarlar
    }

    private void Update() // her frame de çalışacak olan update fonksiyonu (fps'e bağlı)
    {
        if (Input.GetKeyDown(KeyCode.Space)) // space tuşuna basıldığında jump fonksiyonunu çağırır.
        {
            if (_isGrounded)
            {
                Jump();
            }
        }
    }

    private void FixedUpdate() // fizik tabanlı işlemler için kullanılan update fonksiyonu
    {
        Movement();
        
    }
    private void Movement() // hareket inputlarına göre yön belirleyip karakterin rigidbody'sine kuvvet uygular. , yukarı vektörüne dokunulmaz.
    {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        Vector3 _direction = new Vector3(_horizontal, 0f, _vertical);
        _rb.AddForce(_direction.normalized * _moveSpeed, ForceMode.Force);
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse); // karakterin zıplama gücü kadar yukarıya doğru anlık kuvvet uygular.
    }


    private void UpdateScoreUI() // ui içindeki score textin metnini oyuncunun skorunu gösterecek şekilde ayarlar.
    {
        _sText.text = $"Score: {_score} / {_levelMinScore}"; // ekranda gözükecek skor texti
    }
    
    private int SetMinScore() // integer değeri döndüren fonksiyon
    {
        switch (_levelIndex) // sahne indexine göre levelin geçmek için gerekli minimum skorunu döndürür.
        {
            case 1:
                _levelMinScore = 30;
                break;
            case 2:
                _levelMinScore = 50;
                break;
            case 3:
                _levelMinScore = 70;
                break;
            default:
                print("No more levels!"); // Eğer mevcut olmayan bir index girilirse konsola "No more levels!" yazdırır.
                break;
        }
        return _levelMinScore; // fonksiyonun sonunda levelin geçmek için gerekli minimum skorunu döndürür.
    }
   private void OnTriggerEnter(Collider other) // karakter bir collider ile etkileşime girdiğinde diğer objenin tagini kontrol ederek istediğimiz obje olup olmadığını kontrol ediyoruz.
   { 
    if (other.CompareTag("greenball")) // greenball tagına sahipse oyuncunun skorunu 10 arttır, konsola yazdır, ve topu yok et
        {
            _score += 10; // oyuncunun skorunu 10 arttırır.
            print("Greenball, " + _score);
            UpdateScoreUI(); // skor arttığında ui içindeki score textini günceller.
            _audiosource.PlayOneShot(_truesound); // yeşil topun sesini çalar.
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("redball")) // redball tagına sahipse oyuncunun skorunu 5 azalt, konsola yazdır, ve topu yok et
        {
            _score -= 5; // oyuncunun skorunu 5 azaltır.
            print("Redball, " + _score);
            UpdateScoreUI(); // skor azaldığında ui içindeki score textini günceller.
            _audiosource.PlayOneShot(_wrongsound); // kırmızı topun sesini çalar.
            Destroy(other.gameObject);
        }
   }
   
   private void OnCollisionEnter(Collision collision) // karakter bir collider ile çarpıştığında diğer objenin tagini kontrol ederek istediğimiz obje olup olmadığını kontrol ediyoruz.
   {
        if (collision.gameObject.CompareTag("ground")) // çarpıştığı obje ground tagına sahipse _isGrounded değişkenini true yapar.
        {
            _isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("finishbox"))
        {
            if (_score >= _levelMinScore) // finishbox tagına sahipse ve skor sahnenin minimum skoruna eşit veya daha fazla ise konsola "You Win!" yazdırır.
            {
                print("You Win!");
                _audiosource.PlayOneShot(_truesound); // seviyeyi geçince truesound çalar.
                _levelIndex++; // level indexini 1 arttırır.
                if (_levelIndex > 3) // level indexi 3'ten büyükse konsola "No more levels!" yazdırır.
                {
                    print("No more levels!");
                }
                else
                {
                SetMinScore(); // level indexine göre levelin geçmek için gerekli minimum skorunu günceller.
                SceneManager.LoadScene(_levelIndex); // level indexine göre sahneyi yükler.
                }
            }
            else // skor sahnenin minimum skorundan az ise konsola "You Lose!" yazdırır.
            {
                print("You Lose!");
                _audiosource.PlayOneShot(_wrongsound); // seviye tekrarında wrongsound çalar.
                SceneManager.LoadScene(_levelIndex); // sahneyi yeniden yükler.
            }
        }
        else if (collision.gameObject.CompareTag("resetbox")) // resetbox tagına sahipse leveli resetler.
        {
            SceneManager.LoadScene(_levelIndex); // sahneyi yeniden yükler.
        }
   }

   private void OnCollisionStay(Collision collision) // karakter bir collider ile çarpışmayı sürdürdüğünde diğer objenin tagini kontrol ederek istediğimiz obje olup olmadığını kontrol ediyoruz.
   {
        if (collision.gameObject.CompareTag("ground")) // çarpışmayı sürdürdüğü obje ground tagına sahipse _isGrounded değişkenini true yapar.
        {
            _isGrounded = true;
        }
   }

   private void OnCollisionExit(Collision collision) // karakter bir collider ile çarpışmayı bıraktığında diğer objenin tagini kontrol ederek istediğimiz obje olup olmadığını kontrol ediyoruz.
   {
        if (collision.gameObject.CompareTag("ground")) // çarpışmayı bıraktığı obje ground tagına sahipse _isGrounded değişkenini false yapar.
        {
            _isGrounded = false;
        }
   }
}
