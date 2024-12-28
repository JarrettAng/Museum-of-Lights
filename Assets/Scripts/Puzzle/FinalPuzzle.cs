using UnityEngine;
using UnityEngine.Events;

public class FinalPuzzle : MonoBehaviour
{
    // This is a hacked in class for the game jam

    public UnityEvent RightGateOnSolve;
    public UnityEvent RightGateOnUnsolve;
    public Transform RightGateCorrectRot;

    public RotationPuzzle LightPuzzle;
    public void UpdateLightPuzzle() {
        LightPuzzle.OnSolve = RightGateOnSolve;
        LightPuzzle.OnUnsolve = RightGateOnUnsolve;
        LightPuzzle.r_correctRotation = RightGateCorrectRot;
    }

    public UnityEvent FinalOnSolve;
    public UnityEvent FinalGateOnUnsolve;
    public Transform[] FinalGateCorrectRot;

    public void UpdateLightPuzzleFinal() {
        LightPuzzle.m_tolerance = 60f;
        LightPuzzle.OnSolve = FinalOnSolve;
        LightPuzzle.OnUnsolve = FinalGateOnUnsolve;
        LightPuzzle.r_correctRotations = FinalGateCorrectRot;
        LightPuzzle.m_multipleCorrectRotations = true;
    }
}
