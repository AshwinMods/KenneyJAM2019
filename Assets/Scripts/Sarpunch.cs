﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sarpunch : MonoBehaviour
{
    private void Awake()
    {
        Input.multiTouchEnabled = false;
    }
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
    public Animation ringChk;
    public Animation ringFix;
    public AnimationClip animClear, animFill, animErr, animDone;
    public Util_Tween ui_mainMenu, ui_lvlSel, ui_InGame;

    int selectedlevel = 0;
    public void OnLevelSelect(int lvl)
    {
        selectedlevel = lvl;
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

    bool checkValid = false;
    public void Check_Revoke()
    {
        checkValid = false;
        ringChk.Stop();
        if (cRoutine != null)
        {
            StopCoroutine(cRoutine);
        }
        ringChk.gameObject.SetActive(false);
    }

    Coroutine cRoutine = null;
    public void Check_Complete()
    {
        checkValid = true;
        ringChk.gameObject.SetActive(true);
        ringChk.Stop();
        ringChk.clip = animFill;
        ringChk.Play();

        if (cRoutine != null)
        {
            StopCoroutine(cRoutine);
        }
        cRoutine = StartCoroutine(Check_Process());
    }

    IEnumerator Check_Process()
    {
        yield return new WaitForSeconds(1);
        if (checkValid && rCunt.accuracy > 0.99)
        {
            ringChk.Stop();
            ringChk.gameObject.SetActive(false);
            ringFix.gameObject.SetActive(true);
            ringFix.clip = animFill;
            ringFix.Play();

            // No time for beauty, beast on duty
            Vector3 a = rCunt.targetObjects[0].eulerAngles;
            while ( Vector3.Distance(Vector3.zero, a) > 0.001f)
            {
                a = Vector3.MoveTowards(a, Vector3.zero, Time.deltaTime * 270);
                rCunt.targetObjects[1].eulerAngles = a;
                rCunt.targetObjects[0].eulerAngles = a;
                yield return null;
            }
            rCunt.targetObjects[1].eulerAngles = Vector3.zero;
            rCunt.targetObjects[0].eulerAngles = Vector3.zero;
            OnLevelComplete();
        }
        else
        {
            ringChk.gameObject.SetActive(true);
            ringChk.Stop();
            ringChk.clip = animErr;
            ringChk.Play();
            yield return new WaitForSeconds(1);
            ringChk.gameObject.SetActive(false);
        }
    }


    public void OnLevelComplete()
    {
        puzzles[selectedlevel].lvlBtn.sprite = puzzles[selectedlevel].img;
        ui_InGame.Close_And_Disable();
        ui_lvlSel.gameObject.SetActive(true);
    }
}
