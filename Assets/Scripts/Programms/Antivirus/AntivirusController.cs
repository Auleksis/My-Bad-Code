using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class AntivirusController : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private bool canMove = true;

    private float max_velocity = 1.3f;

    [SerializeField] float lookingRadius = 2f;

    [SerializeField] float delayTimeInSeconds = 10f;

    [SerializeField] STATE checkingFileType = STATE.DEFAULT; //Значит, проверяет всех

    [SerializeField] float minDistanceToApp = 0.6f;

    [SerializeField] float deltaToRecognizeVirus = 0.64f;

    [SerializeField] float catchRadius = 0.64f;

    private bool canCheck = true;

    private AbstractProgramm lastChecked;

    private AbstractProgramm shouldBeChecked;

    private Vector3 requestedAppPosition;

    private bool isRequestedAppVirus = false;

    private bool isChecking = false;

    private float catchScore = 0; 

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (shouldBeChecked != null)
        {

            Vector3 targetPosition = shouldBeChecked.transform.position;

            Vector3 direction = targetPosition - transform.position;

            if (direction.magnitude > minDistanceToApp)
                rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, direction.normalized * max_velocity, Time.fixedDeltaTime * 2);
            else
            {
                rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, Vector2.zero, Time.fixedDeltaTime * 2);
                
                if(!isChecking && !isRequestedAppVirus)
                    StartCheckingRequestedApp();
            }

            if (!isRequestedAppVirus && (targetPosition - requestedAppPosition).magnitude > deltaToRecognizeVirus)
                isRequestedAppVirus = true;
        }
    }

    void Update()
    {
        /*Vector3 velocity_v = new Vector3(1, 0, 0);
        rigidbody.velocity = velocity_v.normalized * max_velocity;*/

        if (isRequestedAppVirus)
        {
            Vector3 targetPosition = shouldBeChecked.transform.position;

            Vector3 direction = targetPosition - transform.position;

            if (direction.magnitude < catchRadius) {

                //Увеличение полоски того, что вирус скоро будет пойман
                //...

                catchScore = Mathf.Lerp(catchScore, 1, Time.deltaTime);
            }
            else
            {
                catchScore = Mathf.Lerp(catchScore, 0, Time.deltaTime);
            }

            if(Mathf.Abs(catchScore - 1) < 0.05)
            {
                //Остановить вирус и запустить анимацию увода его в специальную часть уровня, чтобы затем уничтожить

                lastChecked = shouldBeChecked;
                shouldBeChecked = null;
                isRequestedAppVirus = false;

                catchScore = 0;
            }
        }

        Debug.Log(catchScore);

        CheckApps();
    }

    private void StartCheckingRequestedApp()
    {
        isChecking = true;

        //Запуск анимации проверки

        FinishCheckingRequestedApp();
    }

    public void FinishCheckingRequestedApp()
    {
        isChecking = false;
        StateInfo appState = shouldBeChecked.GetOriginalStateInfo();

        if (appState.GetState() == STATE.VIRUS)
        {
            //Проиграть анимацию красной мегалки, ВНИМАНИЕ!!!

            //Поймать негодяя

            isRequestedAppVirus = true;
        }
        else
        {
            lastChecked = shouldBeChecked;
            shouldBeChecked = null;

            //Проиграть анимацию, что всё хорошо
        }
    }

    public void SetMoveVector(Vector3 velocity_vector)
    {
        if (canMove)
        {
            Vector3 normalized = velocity_vector.normalized;
            if (normalized.sqrMagnitude > 0)
                rigidbody.velocity = normalized * max_velocity;
            else
                rigidbody.velocity = new Vector3(0, 0, 0);
        }
        else
            rigidbody.velocity = new Vector3(0, 0, 0);
    }

    public void CheckApps()
    {
        if (shouldBeChecked != null || !canCheck)
            return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, lookingRadius);

        foreach(Collider2D collider in colliders)
        {
            AbstractProgramm app = collider.GetComponent<AbstractProgramm>();
            if (app != null && app != lastChecked)
            {
                app.RequestToStop();

                shouldBeChecked = app;

                requestedAppPosition = app.transform.position;
            }
        }
    }

    private IEnumerator Delay()
    {
        canCheck = false;
        yield return new WaitForSeconds(delayTimeInSeconds);
        canCheck = true;
    }
}
