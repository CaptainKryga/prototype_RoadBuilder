using UnityEngine.SceneManagement;

namespace View.Main
{
    public class MenuNewGame : MenuMainBase
    {
        public void OnClick_Random()
        {
            SceneManager.LoadScene(1);
        }
    }
}