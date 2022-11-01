using UnityEngine;

namespace View.Main
{
    public class MenuMain : MenuMainBase
    {
        [SerializeField] private MenuMainBase _newGame;
        [SerializeField] private MenuMainBase _authors;
        
        public void OnClick_NewGame()
        {
            _newGame.UsePanel(PanelBase);
            SetEnable(false);
        }

        public void OnClick_Authors()
        {
            _authors.UsePanel(PanelBase);
            SetEnable(false);
        }

        public void OnClick_Exit()
        {
            Application.Quit();
        }
    }
}