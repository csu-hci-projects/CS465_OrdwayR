using UnityEngine;

public class EvironmentManager : MonoBehaviour
{
    public Animator table; // Assign in Inspector

    public void PlayTableLift()
    {
        table.SetTrigger("LiftTables");
    }
}
