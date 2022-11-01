using UnityEngine.SceneManagement;

namespace View.Game
{
    public abstract class MenuGameBase : MenuBase
    {
        public void OnClick_Restart()
        {
            SceneManager.LoadScene(1);
        }

        public void OnClick_MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public abstract void UsePanel();
    }
}