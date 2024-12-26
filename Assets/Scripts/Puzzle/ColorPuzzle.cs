using NUnit.Framework.Constraints;
using UnityEngine;

[System.Serializable]
public struct ColorSolution {
    public ColorSpot r_Spot;
    public int m_CorrectColor;
}

public class ColorPuzzle : Puzzle
{
    [SerializeField]
    private ColorSolution[] m_colorSolutions;

    private bool m_correctColors = false;

    new void Update() {
        base.Update();

        if (m_solvedOnce && m_correctColors) return;

        if (m_LockedIn) {
            m_correctColors = true;
            for (int i = 0; i < m_colorSolutions.Length; i++) {
                if (m_colorSolutions[i].r_Spot.m_currentColor != m_colorSolutions[i].m_CorrectColor) {
                    m_correctColors = false;
                    break;
                }
            }
        }
    }

    protected override bool CheckSolved() {
        return m_correctColors;
    }
}
