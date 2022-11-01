using UnityEngine;
using UnityEngine.SceneManagement;

namespace View.Game
{
    public class MenuGameOver : MenuGameBase
    {
        [SerializeField] private TMPro.TMP_Text _textWin;
        [SerializeField] private TMPro.TMP_Text _textScore;

        public override void UsePanel()
        {
            _textWin.text = "WIN";//OR DEFEAT
            _textScore.text = "Score: 123";
            SetEnable(true);
        }

        public void OnClick_NextLevel()
        {
            SceneManager.LoadScene(1);
        }
    }
}