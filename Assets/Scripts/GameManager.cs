using UnityEngine;

[RequireComponent(typeof(LevelManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UIController _uiController;
    [SerializeField] private Transform _finish;

    private LevelManager _levelManager;
    private void Start()
    {
        _levelManager = GetComponent<LevelManager>();
        _uiController.SetLenghtLevel(Vector3.Distance(_player.transform.position,_finish.position));
    }
    private void FixedUpdate()
    {
        _uiController.ChangeLevelProgress(Vector3.Distance(SameToFinishPositionPlayer(),_finish.position));
    }

    private Vector3 SameToFinishPositionPlayer()
    {
        Vector3 position = _finish.position;
        return new Vector3(_player.transform.position.x, position.y, position.z);
    }

    public void OpenNextLevel()
    {
        Time.timeScale = 1f;
        _levelManager.Next();
    }
    public void RefreshLevel()
    {
        Time.timeScale = 1f;
       _levelManager.Refresh();
    }
    public void LoseLevel()
    {
     _uiController.OpenLosePanel();
    }
    public void Win()
    {
        _uiController.OpenWinPanel();
    }
}
