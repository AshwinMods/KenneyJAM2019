using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sarpunch : MonoBehaviour
{
    [System.Serializable]
    public struct Puzzle
    {
        public string name;
        public GameObject objVisual;
        public GameObject objShadow;
        public Sprite img;
        public Image lvlBtn;
    }

    public Puzzle[] puzzles;

    [Header("Reference")]
    public RotCunt rCunt;
    public Text levelText;

    public void OnLevelSelect(int lvl)
    {
        foreach (var p in puzzles)
        {
            p.objVisual.SetActive(false);
            p.objShadow.SetActive(false);
        }
        rCunt.Randomize_Rotation();
        puzzles[lvl].objVisual.SetActive(true);
        puzzles[lvl].objShadow.SetActive(true);
        levelText.text = puzzles[lvl].name;
    }
}
