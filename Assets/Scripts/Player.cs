using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
   private const float SPEED=2f;
   private const int COUNT_BOARD_IN_PICK_UP=3;
   private const float TIME_BETWEEN_USE_BOARDS=0.13f;

   [SerializeField] private GameObject _boardPref;
   [SerializeField] private Transform _boardParent;
   [SerializeField] private Transform _legPos;
   [SerializeField] private UIController _uiController;
   [SerializeField] private GameManager _gameManager;

   private Rigidbody _rigidbody;
   private readonly List<Board> _boards=new List<Board>();
   private Vector3 _startPosition;
   private Vector3 _endPosition;

   public bool activeFinger;
   public float changedHeight;
   private void Start()
   {
      _rigidbody = GetComponent<Rigidbody>();
   }

   public void ChangeStatusControl(bool status)
   {
      activeFinger = status;
      _rigidbody.useGravity = !status;
      if (activeFinger)
      {
         _startPosition = transform.position;
         StartCoroutine(FlyOnBoard());
      }
      else
      {
         changedHeight = 0;
      }
   }
   private IEnumerator FlyOnBoard()
   {
      while (activeFinger)
      {
         if(HaveBoard())
            UseBoard();
         yield return new WaitForSeconds(TIME_BETWEEN_USE_BOARDS);
      }
   }
   public void SetPositionToMove(float bias)
   {
      if(activeFinger)
         changedHeight = bias;
   }
   private void FixedUpdate()
   {
      Vector3 playerPosition = transform.position;
      _endPosition = new Vector3(playerPosition.x+1, _startPosition.y+changedHeight, playerPosition.z);
      transform.position= Vector3.MoveTowards(playerPosition, _endPosition, 0.2f);
   }
   private void UseBoard()
   {
      Board lastBoard = _boards[_boards.Count - 1];
      _boards.Remove(lastBoard);
      Transform boardTr = lastBoard.transform;
      boardTr.parent = null;
      boardTr.position = _legPos.position;
      lastBoard.spawnFromPlayer = true;
      _uiController.ChangeCountBoards(_boards.Count);
   }

   private bool HaveBoard()
   {
      if (_boards.Count > 0)
      {
         return true;
      }
      ChangeStatusControl(false);
      return false;
   }
   private void PickUpBoard()
   {
      for(int i = 0; i < COUNT_BOARD_IN_PICK_UP; i++)
      {
         Vector3 nextPosition;
         nextPosition = _boards.Count == 0 ? _boardParent.position : _boards[_boards.Count - 1].nextPosition.position;
         GameObject currentBoard = Instantiate(_boardPref, nextPosition,Quaternion.identity,_boardParent);
         _boards.Add(currentBoard.GetComponent<Board>());
      }
      _uiController.ChangeCountBoards(_boards.Count);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.TryGetComponent(out Board board))
      {
         if (board.spawnFromPlayer == true)
            return;

         Destroy(board.gameObject);
         PickUpBoard();
      }
      else if (other.gameObject.TryGetComponent(out Barrier barrier))
      {
         _gameManager.LoseLevel();
         Time.timeScale = 0f;
      }
      else if (other.gameObject.TryGetComponent(out Finish finishinish))
      {
         _gameManager.Win();
         Time.timeScale = 0f;
      }
   }

}
