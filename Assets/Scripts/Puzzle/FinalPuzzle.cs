using UnityEngine;
using UnityEngine.Events;

public class FinalPuzzle : MonoBehaviour
{
    // This is a hack class

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
    public Transform FinalGateCorrectRot;

    public void UpdateLightPuzzleFinal() {
        LightPuzzle.m_tolerance = 125f;
        LightPuzzle.OnSolve = FinalOnSolve;
        LightPuzzle.OnUnsolve = FinalGateOnUnsolve;
        LightPuzzle.r_correctRotation = FinalGateCorrectRot;
    }
}
