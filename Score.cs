using UnityEngine;
using System.Collections;

public static class Score
{
    private static int createdSphere;
    private static readonly int maxScore = 999999999;

    public static void Reset()
    {
        createdSphere = 0;
    }

    public static void SphereCreate()
    {
        createdSphere++;
    }

    public static int GetScore(float playTime)
    {
        int playTimeInt = Mathf.RoundToInt(playTime);
        if ((maxScore / playTime) > createdSphere)
        {
            return Mathf.RoundToInt(createdSphere * playTime);
        }
        return maxScore;
    }
}
