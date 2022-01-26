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



    //State
    public int stage;
    bool inbetweenStages;
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
            //Animation for falling light
            LightFall();
        }

        if(shinyWall.activated && stage == 3)
        {
            GetComponent<Game>().LevelComplete();
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
        sqrLightRed.enabled = false;
        sqrLightRed.gameObject.SetActive(false);
        sqrLightGreen.gameObject.SetActive(true);
        sqrLightGreen.enabled = true;


        MoveGreenLightDown();
        LeverFlyAway();
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


        GetComponent<NLC>().ls.Add(wheelLightGreen);
        GetComponent<NLC>().ls.Add(rndLightBlue);
        GetComponent<NLC>().ls.Add(sqrLightRed);

        wheelLightBlue.lr.material = GetComponent<NLC>().lM;
        rndLightRed.lr.material = GetComponent<NLC>().lM;
        sqrLightGreen.lr.material = GetComponent<NLC>().lM;

        wheelLightBlue.iR = true;
        rndLightRed.iR = true;
        sqrLightGreen.iR = true;
        List<SelectableObj> selectableObjs = new List<SelectableObj> { wheelLightBlue.GetComponent<SelectableObj>(), rndLightRed.GetComponent<SelectableObj>(), sqrLightGreen.GetComponent<SelectableObj>() };
        FindObjectOfType<SelectableObjController>().ResetObjectList(selectableObjs);
        FindObjectOfType<PlayerController>().movementEnabled = true;
    }


    #region Animations
    void MoveGreenLightDown()
    {
        sqrLightGreen.GetComponent<Animator>().SetTrigger("Move");
    }

    void LightFall()
    {
        sqrLightGreen.GetComponent<Animator>().SetTrigger("Fall");
    }
    void LadderFall()
    {
        Debug.Log("Ladder Fall");
        ladder.GetComponent<Animator>().SetTrigger("Fall");
    }
    void LeverFlyAway()
    {
        lever.GetComponent<Animator>().SetTrigger("FlyAway");
    }
    void SmokeRise()
    {

    }

    #endregion

}
    
