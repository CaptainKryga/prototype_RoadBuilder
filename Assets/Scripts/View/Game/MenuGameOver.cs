﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace View.Game
{
    public class MenuGameOver : MenuGameBase
    {
        [SerializeField] private TMPro.TMP_Text _textWin;
        [SerializeField] private TMPro.TMP_Text _textScore;

        public override void UsePanel()
        {
            SetEnable(true);
        }

        public void SetTitle(bool isWin, int score)
        {
            _textWin.text = isWin ? "WIN" : "DEFEAT";
            _textScore.text = "Score: " + score;
        }

        public void OnClick_NextLevel()
        {
            SceneManager.LoadScene(1);
        }
    }
}