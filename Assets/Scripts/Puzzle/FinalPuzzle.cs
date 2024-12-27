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
        LightPuzzle.m_correctRot = RightGateCorrectRot;
    }

    public UnityEvent FinalOnSolve;
    public UnityEvent FinalGateOnUnsolve;
    public Transform FinalGateCorrectRot;

    public void UpdateLightPuzzleFinal() {
        LightPuzzle.OnSolve = FinalOnSolve;
        LightPuzzle.OnUnsolve = FinalGateOnUnsolve;
        LightPuzzle.m_correctRot = FinalGateCorrectRot;
    }
}
