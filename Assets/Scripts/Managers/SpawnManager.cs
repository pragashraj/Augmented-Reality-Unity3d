using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private GameObject spawnablePrefab;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject info;
    [SerializeField] private GameObject answerSlot;
    [SerializeField] private GameObject knightPrfab;
    [SerializeField] private GameObject success;
    [SerializeField] private GameObject failure;
    [SerializeField] private GameObject end;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Camera arCam;
    private GameObject spawnedObject;
    private bool objectInstantiated = false;
    private Vector3 spawnPosition;

    private QuestManager questManager;
    private GameManager gameManager;
    private AudioManager audioManager;


    void Start()
    {
        spawnedObject = null;
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
        questManager = FindObjectOfType<QuestManager>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        if (!objectInstantiated)
        {
            TapToPlace();
        }
    }

    private void TapToPlace()
    {
        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (raycastManager.Raycast(Input.GetTouch(0).position, hits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && spawnedObject == null)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Spawnable")
                    {
                        spawnedObject = hit.collider.gameObject;
                    }
                    else
                    {
                        SpawnPrefab(hits[0].pose.position);
                        spawnPosition = hits[0].pose.position;
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject == null)
            {
                spawnedObject.transform.position = hits[0].pose.position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                spawnedObject = null;
            }
        }
    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        spawnedObject = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        objectInstantiated = true;
        info.SetActive(false);
        HandleAnswerSlotActive(true);
    }

    private void HandleNextButtonActive(bool active)
    {
        nextButton.SetActive(active);
    }

    private void HandleAnswerSlotActive(bool active)
    {
        answerSlot.SetActive(active);
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    public void HandleNextOnClick()
    {
        objectInstantiated = false;
        HandleNextButtonActive(false);
        Destroy(spawnedObject);
        questManager.CurrentIndex += 1;
        HandleAnswerSlotActive(false);
    }

    public void HandleAnswerOnClick(Text content)
    {
        GameObject knightInstantiated = Instantiate(knightPrfab, spawnPosition, Quaternion.identity);
        knightInstantiated.transform.Rotate(new Vector3(0, 180, 0));
        HandleAnswerSlotActive(false);
        Destroy(spawnedObject);
        StartCoroutine(Checking(knightInstantiated, content));
    }

    IEnumerator Checking(GameObject knightInstantiated, Text content)
    {
        yield return new WaitForSecondsRealtime(5f);

        Quest quest = questManager.Quests[questManager.CurrentIndex];

        if (content.text == quest.name)
        {
            knightInstantiated.GetComponent<Animator>().SetTrigger("Celebrating");
            gameManager.Score += 1;
            PlayAudio("Success");
            success.SetActive(true);
        }
        else
        {
            knightInstantiated.GetComponent<Animator>().SetTrigger("Failure");
            PlayAudio("Failure");
            failure.SetActive(true);
        }

        yield return new WaitForSecondsRealtime(3f);
        failure.SetActive(false);
        success.SetActive(false);

        yield return new WaitForSecondsRealtime(5f);

        Destroy(knightInstantiated);
        if (questManager.CurrentIndex < 4)
        {
            HandleNextButtonActive(true);
        } else
        {
            end.SetActive(true);
            end.GetComponentInChildren<Text>().text = gameManager.Score.ToString();
            yield return new WaitForSecondsRealtime(3f);
            end.SetActive(false);
        }
    }
}
