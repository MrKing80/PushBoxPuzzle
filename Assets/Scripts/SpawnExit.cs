using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExit : MonoBehaviour
{
    [SerializeField] private GameObject[] _switches = default;
    [SerializeField] private GameObject _exitDoor = default;
    private bool[] _isClears = default;
    private bool _isPassedTheCheck = false;
    private float _waitSec = 0.5f;

    private void Awake()
    {
        _exitDoor.SetActive(false);

        _isClears = new bool[_switches.Length];

        for (int i = 0; i < _switches.Length; i++)
        {
            _isClears[i] = false;

            StartCoroutine(SwitchChecker());
        }
    }

    private void Update()
    {
        ClearChecker();

        if (_isPassedTheCheck)
        {
            _exitDoor.SetActive(true);
        }
    }

    private bool ClearChecker()
    {
        for (int i = 0; i < _isClears.Length; i++)
        {
            if (!_isClears[i])
            {
                return _isPassedTheCheck = false;
            }
        }

        return _isPassedTheCheck = true;
    }

    private IEnumerator SwitchChecker()
    {

        while (!_isPassedTheCheck)
        {
            int i = 0;

            while (i < _isClears.Length)
            {
                _isClears[i] = _switches[i].GetComponent<ClearJudge>().IsClear;

                yield return new WaitForSeconds(_waitSec);

                i++;
            }
        }
    }
}
