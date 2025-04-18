using System;
using UnityEngine;
using UnityEngine.Rendering;

public class EnvironmentManager : MonoBehaviour
{
    public Animator table;
    public GameObject chairs;

    public Animator teacherBoard;
    public Animator studentBoard;

    public Animator controlListBoard;
    public GameObject statisticsBoard;
    public GameObject magicEffect;

    void Start()
    {
        statisticsBoard.SetActive(false);
    }

    float speed = .1f;

    public void PlayTableLift()
    {
        table.SetTrigger("LiftTables");
    }

    public void PlayChairMove()
    {
        foreach (Transform child in chairs.transform)
        {
            Animator animator = child.GetComponent<Animator>();
            if (animator != null)
            {
                animator.speed = 60f;
                animator.SetTrigger("MoveChairs");
                animator.speed = speed;
            }
        }
    }



    public void PlayBoardGlyphToSpellIntro()
    {
        teacherBoard.SetTrigger("SpellLessonIntro");
        studentBoard.SetTrigger("SpellLessonIntro");
        controlListBoard.SetTrigger("SpellLessonIntro");
        teacherBoard.speed = speed * 10;
        studentBoard.speed = speed * 10;
        controlListBoard.speed = speed * 10;
    }
    public void PlayBoardSpellIntroToSpell()
    {
        teacherBoard.SetTrigger("SpellLesson");
        teacherBoard.speed = speed * 10;
    }

    public void PlayBoardSpellToExit()
    {
        teacherBoard.SetTrigger("SpellToExit");
        teacherBoard.speed = speed * 10;
        statisticsBoard.SetActive(true);
    }



    public void PlayRandomMagicEffect()
    {
        if (magicEffect.transform.childCount > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, magicEffect.transform.childCount);
            Transform randomChild = magicEffect.transform.GetChild(randomIndex);
            randomChild.gameObject.SetActive(true);

            StartCoroutine(HideAfterDelay(randomChild.gameObject, Math.Max(2f, randomChild.GetComponent<ParticleSystem>().main.duration)));
        }
    }

    private System.Collections.IEnumerator HideAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
