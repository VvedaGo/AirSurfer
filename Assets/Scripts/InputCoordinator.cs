using UnityEngine;
using UnityEngine.EventSystems;

public class InputCoordinator : MonoBehaviour ,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    private const float SENSIVITY=0.05f;

    [SerializeField] private Player _player;

    private Vector2 _startPosition;
    private float _bias;

    public void OnPointerDown(PointerEventData eventData)
    {
        _startPosition = eventData.position;
        _player.ChangeStatusControl(true);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _player.ChangeStatusControl(false);
    }
    public void OnDrag(PointerEventData eventData)
    {
        _bias = eventData.position.y-_startPosition.y;
        _bias *= SENSIVITY;
        _player.SetPositionToMove(_bias);
    }
}
