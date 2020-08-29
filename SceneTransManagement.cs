using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class SceneTransManagement
{
    private static string nextSceneName;
    public static void NextScene(string sceneName,GameObject slidePrefab)
    {
        nextSceneName = sceneName;
        GameObject slideClone = GameObject.Instantiate(slidePrefab);
    }

    public static void SceneTrans(GameObject transObject,Vector3 startPosition,Vector3 transPosition, Vector3 goalPosition)
    {
        float startToCurrent = Vector3.Distance(transObject.transform.position, startPosition);
        float startToTrans = Vector3.Distance(startPosition, transPosition);
        float startToGoal = Vector3.Distance(startPosition, goalPosition);
        if (startToCurrent > startToGoal)
        {
            GameObject.Destroy(transObject);
            return;
        }
        if (startToCurrent > startToTrans)
        {
            if (nextSceneName.Equals(""))
            {
                return;
            }
            string sceneName = nextSceneName;
            nextSceneName = "";
            SceneManager.LoadScene(sceneName);
        }
    }

}
