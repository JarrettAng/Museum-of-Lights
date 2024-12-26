using UnityEngine;

[System.Serializable]
public struct ColorSolution {
    ColorSpot r_spot;
    int m_correctColor;
}

public class ColorPuzzle : Puzzle
{
    [SerializeField]
    private ColorSolution[] m_colorSolutions;

    new void Update() {
        base.Update();

        HandleInput();
        CheckStatus();

        void HandleInput() {
           
        }

        void CheckStatus() {
            
        }
    }

    protected override bool CheckSolved() {
        return true;
    }
}
