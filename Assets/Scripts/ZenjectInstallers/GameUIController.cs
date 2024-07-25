﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniIT.ARKANOID
{
    public class GameUIController : BaseGameUIController
    {
        [SerializeField] private TMP_Text            scoreTMP;

        private int                                  currentScore;

        public override void ShowScore(int value)
        {
            currentScore += value;
            scoreTMP.text = $"Score: {currentScore}";
        }
    }

    public abstract class BaseGameUIController : MonoBehaviour
    {
        public virtual void ShowScore(int value)
        {
            
        }
        public virtual void ShowScore(int ballOwnerId, int value)
        {
            
        }

        public virtual void Exit()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}