using Unity.VisualScripting;
using UnityEngine;

public class GateWayScript : MonoBehaviour
{
    public GateScript _gate1;
    public GateScript _gate2;
    public GateScript _gate3;
    public Transform _nextGateSpawnPoint;
    public int _currentgateIndex = 1;
    private GameObject _spawned;
    [SerializeField] private GameObject _gateprefab;
    [SerializeField] private BallPlayerScript _playerScript;


    public void GenerateNextGate()
    {
        if (_currentgateIndex == 0)
        {
            _spawned = Instantiate(_gateprefab, _nextGateSpawnPoint.position, Quaternion.identity);
            Debug.Log("Generating Gate 1");
            _gate1.GenerateGate2();
        }
        else if (_currentgateIndex == 1)
        {
            _gate1.destroyself();
            _gate2.GenerateGate2();
        }
        else if (_currentgateIndex == 2)
        {
            _gate2.destroyself();
            _gate3.GenerateGate2();
        }
        else if (_currentgateIndex == 3)
        {
            _gate3.destroyself();
            GateWayScript _newgatewayscript = _spawned.GetComponent<GateWayScript>();
            _newgatewayscript.setreferences(_playerScript);
            Destroy(gameObject, 10f);
            _newgatewayscript._playerScript.GenerateFirstGate();
        }
    }

    public void setreferences(BallPlayerScript _Player)
    {
        _playerScript = _Player;
        _playerScript._gateWayScript = this;
    }

    public GameColor GetTrueColorFromCurrentGate()
    {
        if (_currentgateIndex == 0)
        {  
            return _gate1._truecolor;
        }
        else if (_currentgateIndex == 1)
        {
            return _gate2._truecolor;
        }
        else if (_currentgateIndex == 2)
        {
            return _gate3._truecolor;
        }
        return GameColor.Red; 
    }
}
