using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _player = default;

    private void Awake()
    {
        Instantiate(_player,this.transform.position, Quaternion.identity);
    }
}
