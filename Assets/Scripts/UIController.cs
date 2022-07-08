using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountBoard;
    [SerializeField] private Slider _levelProgress;
    [SerializeField] private GameObject _loseView;
    [SerializeField] private GameObject _winView;

    public void ChangeCountBoards(int count)
    {
        _amountBoard.text = count.ToString();
    }
    public void SetLenghtLevel(float amoung)
    {
        _levelProgress.maxValue = amoung;
    }
    public void ChangeLevelProgress(float distanse)
    {
        _levelProgress.value =_levelProgress.maxValue - distanse;
    }
    public void OpenLosePanel()
    {
        _loseView.SetActive(true);
    }
    public void OpenWinPanel()
    {
        _winView.SetActive(true);
    }
}
