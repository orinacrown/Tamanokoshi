using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerCircle;
    public GameObject spherePrefab;
    public GameObject destroyObject;
    public GameObject resultCanvas;
    public Text timeText;
    public Text spheresText;
    public GameObject slideObject;
    private static List<GameObject> spheres;
    private float startTime;
    private int lastScore;
    private float playTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        spheres = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            float x = Random.Range(-9f, 9f);
            float y = Random.Range(-4.5f, 4.5f);
            GameObject maleSphere = Instantiate(spherePrefab);
            maleSphere.transform.position = new Vector3(x, y, 0);
            maleSphere.transform.localScale = new Vector3(1, 1, 1);
            maleSphere.GetComponent<SphereManager>().gender = SphereManager.Gender.male;
            //maleSphere.GetComponent<Rigidbody2D>().velocity = (new Vector2(x, y));

            x = Random.Range(-9f, 9f);
            y = Random.Range(-4.5f, 4.5f);
            GameObject femaleSphere = Instantiate(spherePrefab);
            femaleSphere.transform.position = new Vector3(x, y, 0);
            femaleSphere.transform.localScale = new Vector3(1, 1, 1);
            femaleSphere.GetComponent<SphereManager>().gender = SphereManager.Gender.female;
            //femaleSphere.GetComponent<Rigidbody2D>().velocity = (new Vector2(x, y));
        }
        Score.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if(spheres.Count > 100)
        {
            GameObject destroyPrefab = Instantiate(destroyObject);
            destroyPrefab.transform.position = new Vector3(0, 0, 0);
            destroyPrefab.transform.localScale = new Vector3(1, 1, 0);
            return;
        }
        if (spheres.Count < 3)
        {
            for(int i = 0; i < spheres.Count; i++)
            {
                Destroy(spheres[i]);
                spheres.RemoveAt(i);
            }
            lastScore = Score.GetScore(playTime);
            resultCanvas.SetActive(true);
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;
            CreatePlayerCircle(mousePosition);
        }
        playTime = Time.time - startTime;
        timeText.text = $"Time:{playTime.ToString("f0")}";
        spheresText.text = $"Sphere:{spheres.Count}";
    }

    private void CreatePlayerCircle(Vector2 mousePosition)
    {
        GameObject clone = Instantiate(playerCircle);
        clone.transform.localScale = new Vector3(0, 0, 0);
        clone.transform.position = mousePosition;
    }

    public static void SphereAdd(GameObject addSphere)
    {
        spheres.Add(addSphere);
        Score.SphereCreate();
    }

    public static void SphereRemove(GameObject removeSphere)
    {
        spheres.Remove(removeSphere);
    }

    public void Ranking()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(lastScore);
    }

    public void Retry()
    {
        SceneTransManagement.NextScene("GameScene", slideObject);   
    }
}
