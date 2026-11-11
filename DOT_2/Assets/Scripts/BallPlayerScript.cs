using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallPlayerScript : MonoBehaviour
{
    public GameColor _playerMatColor;
    private Renderer _renderer;
    private GateScript _currentGateScript;
    public GateWayScript _gateWayScript;
    public int _playerscore;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float moveSpeed = 5f;
    private float speedmultiplier = 1f;
    [SerializeField] private float maxSpeedMultiplier = 2f;
    [SerializeField] private float her3SaniyedekiArtis = 0.1f;
    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private AudioClip _truesound;
    [SerializeField] private AudioClip _wrongsound;
    
    void Start()
    {
        _playerscore = 0;
        _renderer = GetComponent<Renderer>();
        Invoke("Colorset", 0.7f);
        StartCoroutine(HizArtirmaRoutine());
        Invoke("GenerateFirstGate", 0.5f);
    }
    void Update()
    {
        Movement();
    }
      private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 direction = new Vector3(horizontal, 0f, 1f);

        transform.Translate(direction * (moveSpeed * Time.deltaTime * speedmultiplier), Space.World);
    }
     private IEnumerator HizArtirmaRoutine()
    {
        // Oyun açık olduğu sürece (ve hız sınıra ulaşmadığı sürece) dönecek sonsuz döngü
        while (speedmultiplier < maxSpeedMultiplier)
        {
            // 3 saniye boyunca kodun bu satırında bekle
            yield return new WaitForSeconds(3f);

            // 3 saniye doldu! Hızı artır
            speedmultiplier += her3SaniyedekiArtis;
        }
    }

    private void Colorset()
    {
        _playerMatColor = _gateWayScript.GetTrueColorFromCurrentGate();
        _renderer.material = ColorManager.Instance.GetMaterial(_playerMatColor);
        var _block = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_block);
        Color _blockcolor = _renderer.sharedMaterial.color;
        _blockcolor.a = 1f;
        _block.SetColor("_BaseColor", _blockcolor);
        _renderer.SetPropertyBlock(_block);
    }

     private void UpdateScoreUI()
    {
        _scoreText.text = $"Score: {_playerscore}";
    }
    public void GenerateFirstGate()
    {
        _gateWayScript.GenerateNextGate();
    }

    private void FinishGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnTriggerEnter(Collider _collision)
    {
        if (_collision.gameObject.CompareTag("leftgate"))
        {   
            _currentGateScript = _collision.gameObject.GetComponentInParent<GateScript>();
            if (_currentGateScript._isCorrectGateLeft)
            {
                Debug.Log("Doğru Kapıdan Geçtin!");
                _playerscore++;
                UpdateScoreUI();
                _audiosource.PlayOneShot(_truesound);
                _gateWayScript._currentgateIndex++;
                _gateWayScript.GenerateNextGate();
                Invoke("Colorset", 0.2f);
            }
            else
            {
                _audiosource.PlayOneShot(_wrongsound);
                Invoke("FinishGame", 0.35f);
            }
        }
        else if (_collision.gameObject.CompareTag("rightgate"))
        {
            _currentGateScript = _collision.gameObject.GetComponentInParent<GateScript>();
            if (_currentGateScript._isCorrectGateLeft)
            {
                _audiosource.PlayOneShot(_wrongsound);
                Invoke("FinishGame", 0.35f);
            }
            else
            {
                Debug.Log("Doğru Kapıdan Geçtin!");
                _playerscore++;
                UpdateScoreUI();
                _audiosource.PlayOneShot(_truesound);
                _gateWayScript._currentgateIndex++;
                _gateWayScript.GenerateNextGate();
                Invoke("Colorset", 0.2f);
            }
            
        }
        else
        {
            Debug.Log("Kapı Dışında Bir Şeye Çarptın!");
        }
    }
}
