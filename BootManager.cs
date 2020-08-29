using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BootManager : MonoBehaviour
{
    public GameObject slideObject;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        SceneTransManagement.NextScene("GameScene", slideObject);
    }
}
