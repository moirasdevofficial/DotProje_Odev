using TMPro;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public Renderer _leftbox;
    public Renderer _rightbox;
    public BallPlayerScript _player;
    public GameColor _colorleft;
    public GameColor _colorright;
    public GameColor _wrongcolor;
    public GameColor _truecolor;
    public bool _isCorrectGateLeft;
    public TextMesh _leftText;
    public TextMesh _rightText;
    private void Start()
    {
     setgray();
    }


    private void GenerateGate()
    {
        int correctSide = Random.Range(0, 2);

        _truecolor = _player._playerMatColor;
        _wrongcolor = (GameColor)Random.Range(0, 8);

        if (_wrongcolor == _truecolor)
        {
            _wrongcolor = (GameColor)(((int)_truecolor + 1) % 8);
        }

        if (correctSide == 0)
        {
            _colorleft = _truecolor;
            _colorright = _wrongcolor;
            _isCorrectGateLeft = true;
        }
        else
        {
            _colorleft = _wrongcolor;
            _colorright = _truecolor;
            _isCorrectGateLeft = false;
        }

        Material leftMaterial =
            ColorManager.Instance.GetMaterial(_colorleft);
        Material rightMaterial =
            ColorManager.Instance.GetMaterial(_colorright);

        _leftbox.sharedMaterial = leftMaterial;
        _rightbox.sharedMaterial = rightMaterial;
    }
    public void destroyself()
    {
        Invoke("destroywithdelay", 0.5f);
    }

    private void destroywithdelay()
    {
        Destroy(gameObject);
    }
    public void setgray()
    {
        Material leftMaterial =
        ColorManager.Instance.GetMaterial(GameColor.Gray);
        Material rightMaterial =
        ColorManager.Instance.GetMaterial(GameColor.Gray);

        _leftbox.sharedMaterial = leftMaterial;
        _rightbox.sharedMaterial = rightMaterial;
    }
    public void GenerateGate2()
    {
        _truecolor = (GameColor)Random.Range(0, 8);
        _wrongcolor = (GameColor)Random.Range(0, 8);
        if (_wrongcolor == _truecolor)
        {
            _wrongcolor = (GameColor)(((int)_truecolor + 1) % 8);
        }
        int correctSide = Random.Range(0, 2);
        if (correctSide == 0)
        {
            _colorleft = _truecolor;
            _colorright = _wrongcolor;
            _isCorrectGateLeft = true;
        }
        else
        {
            _colorleft = _wrongcolor;
            _colorright = _truecolor;
            _isCorrectGateLeft = false;
        }
        _leftText.text = _colorleft.ToString();
        _rightText.text = _colorright.ToString();

        Material leftMaterial =
        ColorManager.Instance.GetMaterial(_colorleft);
        Material rightMaterial =
        ColorManager.Instance.GetMaterial(_colorright);

        _leftbox.sharedMaterial = leftMaterial;
        _rightbox.sharedMaterial = rightMaterial;
    }

}