using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //Serialize Params
    [Header("Values")]
    [SerializeField] float timeBetweenStages = 3f;

    [Header("Game Objects")]
    [SerializeField] ShinyParent shinyActivator;
    [SerializeField] ShinyParent shinyCables;
    [SerializeField] ShinyParent shinyWall;

    [SerializeField] NL wheelLightRed;
    [SerializeField] NL sqrLightRed;
    [SerializeField] NL rndLightRed;

    [SerializeField] NL wheelLightGreen;
    [SerializeField] NL sqrLightGreen;
    [SerializeField] NL rndLightGreen;

    [SerializeField] NL wheelLightBlue;
    [SerializeField] NL sqrLightBlue;
    [SerializeField] NL rndLightBlue;

    [SerializeField] GameObject ladder;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject lever;
    [SerializeField] GameObject oldTruss;
    [SerializeField] GameObject dummyLever;
    [SerializeField] GameObject dummyTruss;
    [SerializeField] GameObject dummyWheels;


    //State
    public int stage;
    bool inbetweenStages;
    bool waitForLights = true;
    bool lightsFallen;
    private void Start()
    {
        stage = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (shinyActivator.activated && !inbetweenStages)
        {
            Debug.Log("Stage Change");
            if (stage == 1)
            {
                inbetweenStages = true;
                stage++;
                StartCoroutine(Stage2());

            }
            else if (stage == 2)
            {
                stage++;
            }
        }

        
        //Stage 3
        if (!shinyCables.activated && stage == 3)
        {
            stage++;
            //Animation for falling light
            StartCoroutine(Stage3());
        }

        if(shinyWall.activated)
        {
            GetComponent<Game>().LevelComplete();
        }


        if(stage == 4 && !waitForLights)
        {
            if (!shinyCables.activated && !lightsFallen)
            {
                StartCoroutine(LightFall());
                lightsFallen = true;
            }
        }

    }

    public IEnumerator Stage2()
    {
        FindObjectOfType<PlayerController>().movementEnabled = false;
        //Add Animations and Particles
        LadderFall();
        yield return new WaitForSeconds(timeBetweenStages);

        wheelLightRed.enabled = false;
        wheelLightRed.gameObject.SetActive(false);
        wheelLightGreen.gameObject.SetActive(true);
        wheelLightGreen.enabled = true;
        wheelLightGreen.transform.position = wheelLightRed.transform.position;
        wheelLightGreen.transform.rotation = wheelLightRed.transform.rotation;

        rndLightGreen.enabled = false;
        rndLightGreen.gameObject.SetActive(false);
        rndLightBlue.gameObject.SetActive(true);
        rndLightBlue.enabled = true;
        rndLightBlue.transform.position = rndLightGreen.transform.position;
        rndLightBlue.transform.rotation = rndLightGreen.transform.rotation;

        sqrLightBlue.enabled = false;
        sqrLightBlue.gameObject.SetActive(false);
        sqrLightRed.gameObject.SetActive(true);
        sqrLightRed.enabled = true;
        sqrLightRed.transform.position = sqrLightBlue.transform.position;
        sqrLightRed.transform.rotation = sqrLightBlue.transform.rotation;

        inbetweenStages = false;


        GetComponent<NLC>().ls.Add(wheelLightGreen);
        GetComponent<NLC>().ls.Add(rndLightBlue);
        GetComponent<NLC>().ls.Add(sqrLightRed);
        wheelLightGreen.lr.material = GetComponent<NLC>().lM;
        rndLightBlue.lr.material = GetComponent<NLC>().lM;
        sqrLightRed.lr.material = GetComponent<NLC>().lM;

        wheelLightGreen.iR = true;
        rndLightBlue.iR = true;
        sqrLightRed.iR = true;

        List<SelectableObj> selectableObjs = new List<SelectableObj>{wheelLightGreen.GetComponent<SelectableObj>(), rndLightBlue.GetComponent<SelectableObj>(),sqrLightRed.GetComponent<SelectableObj>() };
        FindObjectOfType<SelectableObjController>().ResetObjectList(selectableObjs);
        FindObjectOfType<PlayerController>().movementEnabled = true;
    }

    IEnumerator Stage3()
    {
        FindObjectOfType<PlayerController>().movementEnabled = false;

        //Animations
        oldTruss.SetActive(false);

        sqrLightRed.enabled = false;
        sqrLightRed.gameObject.SetActive(false);
        sqrLightGreen.gameObject.SetActive(true);
        sqrLightGreen.enabled = true;


        MoveGreenLightDown();
        LeverFlyAway();
        SmokeRise();
        yield return new WaitForSeconds(timeBetweenStages);

        shinyWall.gameObject.SetActive(true);
        smoke.SetActive(true);

        wheelLightGreen.enabled = false;
        wheelLightGreen.gameObject.SetActive(false);
        wheelLightBlue.gameObject.SetActive(true);
        wheelLightBlue.enabled = true;
        wheelLightBlue.transform.position = wheelLightGreen.transform.position;
        wheelLightBlue.transform.rotation = wheelLightGreen.transform.rotation;

        rndLightBlue.enabled = false;
        rndLightBlue.gameObject.SetActive(false);
        rndLightRed.gameObject.SetActive(true);
        rndLightRed.enabled = true;
        rndLightRed.transform.position = rndLightBlue.transform.position;
        rndLightRed.transform.rotation = rndLightBlue.transform.rotation;


        GetComponent<NLC>().ls.Add(sqrLightGreen);
        GetComponent<NLC>().ls.Add(wheelLightBlue);
        GetComponent<NLC>().ls.Add(rndLightRed);

        wheelLightBlue.lr.material = GetComponent<NLC>().lM;
        rndLightRed.lr.material = GetComponent<NLC>().lM;
        sqrLightGreen.lr.material = GetComponent<NLC>().lM;

        wheelLightBlue.iR = true;
        rndLightRed.iR = true;
        sqrLightGreen.iR = true;
        List<SelectableObj> selectableObjs = new List<SelectableObj> { wheelLightBlue.GetComponent<SelectableObj>(), rndLightRed.GetComponent<SelectableObj>(), sqrLightGreen.GetComponent<SelectableObj>() };
        FindObjectOfType<SelectableObjController>().ResetObjectList(selectableObjs);
        FindObjectOfType<PlayerController>().movementEnabled = true;
        dummyTruss.SetActive(false);
        StartCoroutine(WaitUntilLightsStage3());
    }


    #region Animations
    void MoveGreenLightDown()
    {
        sqrLightGreen.GetComponent<Animator>().SetTrigger("Move");
    }

    IEnumerator LightFall()
    {
        sqrLightGreen.GetComponent<Animator>().SetTrigger("Fall");
        yield return new WaitForSeconds(1f);
        DummyWheels(); 
    }
    void LadderFall()
    {
        Debug.Log("Ladder Fall");
        ladder.GetComponent<Animator>().SetTrigger("Fall");
    }
    void LeverFlyAway()
    {

        lever.SetActive(false);
        Debug.Log("Dummy Lever");
        dummyLever.SetActive(true);
        dummyLever.GetComponent<Animator>().SetBool("flyAway", true);
    }
    void SmokeRise()
    {
        smoke.SetActive(true);
        smoke.GetComponent<Animator>().SetTrigger("smoke");
    }

    IEnumerator WaitUntilLightsStage3()
    {
        yield return new WaitForSeconds(2);
        waitForLights = false;
    }

    public void DummyWheels()
    {
        Debug.Log("Dummy Wheels");
        GetComponent<NLC>().ls.Remove(sqrLightGreen);
        sqrLightGreen.gameObject.SetActive(false);
        dummyWheels.SetActive(true);
        var dummyLight = dummyWheels.GetComponentInChildren<NL>();
        dummyLight.lr.material = GetComponent<NLC>().lM;
        GetComponent<NLC>().ls.Add(dummyLight);
        dummyLight.iR = true;
    }

    #endregion

}
    
