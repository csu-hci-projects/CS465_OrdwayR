using System;
using UnityEngine;
using UnityEngine.Rendering;

public class EnvironmentManager : MonoBehaviour
{
    public Animator table;
    public GameObject chairs;

    public Animator teacherBoard;
    public Animator studentBoard;

    public GameObject magicEffect;

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

    public void PlayMoveBoards()
    {
        teacherBoard.SetTrigger("MoveBoard");
        studentBoard.SetTrigger("MoveBoard");
        teacherBoard.speed = speed * 10;
        studentBoard.speed = speed * 10;
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
