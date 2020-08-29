using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class SphereManager : MonoBehaviour
{
    private TimeFlagger matureTimeFlag;
    private TimeFlagger deathTimeFlag;
    private int matureTimeMS = 1000;
    private int deathTimeMS = 20000;
    private SpriteRenderer objectSprite;
    private AudioSource audioSource;

    public GameObject childObject;
    public Gender gender;
    public AudioClip birthClip;
    



    public enum Gender
    {
        male,
        female,
        undecided
    };

    private void Awake()
    {
        matureTimeFlag = new TimeFlagger(matureTimeMS);
        deathTimeFlag = new TimeFlagger(deathTimeMS);
        objectSprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        GameManager.SphereAdd(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        if (gender == Gender.undecided)
        {
            GenderDecide();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimeFlag.IsTimeOver())
        {
            GameManager.SphereRemove(gameObject);
            Destroy(gameObject);
        }
        float maturity = (matureTimeFlag.IsTimeOver() ? 0 : (float)matureTimeFlag.PastTimeMS() / matureTimeMS);

        float deathLimit;
        if (deathTimeFlag.PastTimeMS() > (deathTimeMS / 2))
        {
            deathLimit = 1;
        }
        else
        {
            deathLimit = (float)deathTimeFlag.PastTimeMS() / deathTimeMS * 2;
        }
        Color objectColor = new Color(0, 0, 0, 1);
        if (gender == Gender.male)
        {
            objectColor.r = maturity;
            objectColor.g = maturity;
            objectColor.b = deathLimit;
        }
        else
        {
            objectColor.r = deathLimit;
            objectColor.g = maturity;
            objectColor.b = maturity;
        }
        objectSprite.color = objectColor;
    }

    public bool IsMature()
    {
        return matureTimeFlag.IsTimeOver();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (IsMature() == false)
        {
            return;
        }
        if (collision.gameObject.tag.Equals("Sphere"))
        {
            SphereManager collisionManager = collision.gameObject.GetComponent<SphereManager>();
            if (collisionManager.IsMature() &&
                (collisionManager.gender != Gender.undecided) &&
                (gender != Gender.undecided) &&
                (collisionManager.gender !=gender))
            {
                GameObject child = Instantiate(childObject);
                child.transform.localScale = new Vector3(1, 1, 1);
                Vector3 midPosition = (collision.gameObject.transform.position + transform.position) / 2;
                child.transform.position = midPosition;
                child.GetComponent<SphereManager>().GenderDecide();
                matureTimeFlag = new TimeFlagger(matureTimeMS);
                audioSource.PlayOneShot(birthClip);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Destroy"))
        {
            GameManager.SphereRemove(gameObject);
            Destroy(gameObject);

            return;
        }
    }

    public void GenderDecide()
    {
        int random = Random.Range(1, 3);
        if (random == 1)
        {
            gender = Gender.male;
        }
        else
        {
            gender = Gender.female;
        }
    }
}
