using Controller;
using UnityEngine;

namespace View.Game
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField] private ViewController _view;
        [SerializeField] private GameObject _panelBlocker;
        [SerializeField] private GameObject _panelResultUI;

        [SerializeField] private MenuGameBase _gameOver;
       
        public void OnClick_SetPath(int id)
        {
            _view.SetPath(id);
        }

        public void OnClick_Result()
        {
            _panelBlocker.SetActive(true);
            _view.Result();
        }

        public void GameOver(bool isWin)
        {
            _panelBlocker.SetActive(false);
            _panelResultUI.SetActive(false);
            
            ((MenuGameOver) _gameOver).SetTitle(isWin);
            _gameOver.UsePanel();
        }
    }
}