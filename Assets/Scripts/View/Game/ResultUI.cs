using Controller;
using UnityEngine;

namespace View.Game
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField] private ViewController _view;
        [SerializeField] private GameObject _panelResultUI;

        [SerializeField] private MenuGameBase _gameOver;
       
        public void OnClick_SetPath(int id)
        {
            _view.SetPath(id);
        }

        public void OnClick_Result()
        {
            _view.Result();
            _panelResultUI.SetActive(false);
        }

        public void GameOver(bool isWin)
        {
            ((MenuGameOver) _gameOver).SetTitle(isWin);
            _gameOver.UsePanel();
        }
    }
}