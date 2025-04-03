// using UnityEngine;

// public class LessonManager : MonoBehaviour
// {
//     public enum ControlSet { Gesture1H, Gesture2H, Controller1H, Controller2H, Gesture2HCombined }
//     public enum LessonStage { Intro, Glyphs, FinalTrial }

//     [Header("Control Settings")]
//     public ControlSet currentControlSet;

//     [Header("Lesson State")]
//     public LessonStage currentLesson;
//     private bool lessonActive = false;

//     [Header("Managers")]
//     public InputRouter inputRouter; // A separate script that tells objects how to respond to current input
//     public UIManager uiManager;
//     public EnvironmentController environmentController;

//     void Start()
//     {
//         StartLesson();
//     }

//     public void StartLesson()
//     {
//         lessonActive = true;
//         currentLesson = LessonStage.Intro;

//         // Set up control scheme
//         inputRouter.SetInputType(currentControlSet);

//         // Kick off lesson 1
//         uiManager.ShowLessonTitle("The Arcane Interface");
//         environmentController.PrepareIntroScene();
//     }

//     public void AdvanceLesson()
//     {
//         switch (currentLesson)
//         {
//             case LessonStage.Intro:
//                 currentLesson = LessonStage.Glyphs;
//                 uiManager.ShowLessonTitle("Weaving the Glyphs");
//                 environmentController.PrepareGlyphScene();
//                 break;

//             case LessonStage.Glyphs:
//                 currentLesson = LessonStage.FinalTrial;
//                 uiManager.ShowLessonTitle("Trial of the Arcane Room");
//                 environmentController.PrepareFinalScene();
//                 break;

//             case LessonStage.FinalTrial:
//                 EndLesson();
//                 break;
//         }
//     }

//     public void EndLesson()
//     {
//         lessonActive = false;
//         uiManager.ShowCompletionMessage("You’ve mastered the weave!");
//         environmentController.OpenPortalToMainMenu();

//         // Send player back to main menu after a delay
//         Invoke("ReturnToMainMenu", 5f);
//     }

//     private void ReturnToMainMenu()
//     {
//         // Load scene or reset game state
//         UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
//     }
// }