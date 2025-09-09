
using UnityEngine;


public class Letters : MonoBehaviour
{
    #region Components
    private Rigidbody2D rb;
    private new Collider2D collider;
    [SerializeField] private ScenarioDatas scenarioData;
    private SceneTransitionManager sceneTransitionManager;
    private Action action;
    private DemoAction demoAction;
    private Box box;
    private DataCollector datas;
    #endregion

    private bool isDraggable;
    private bool isDragging;
    private bool isOnBox;
    private float gravityScale;
    private bool oneTime = false;
    private float timer = 0;

    private void Start()
    {
        datas=FindAnyObjectByType<DataCollector>();
        sceneTransitionManager=FindAnyObjectByType<SceneTransitionManager>();
        action=FindAnyObjectByType<Action>();
        demoAction=FindAnyObjectByType<DemoAction>();
        box=FindAnyObjectByType<Box>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        gravityScale = rb.gravityScale;
        if(!box.onBox)
        isDraggable = true;
        else
        isDraggable = false;

        timer = 0;
    }
    private void OnMouseDown()
    {
        if(action!=null)
        {
            if (isDraggable)
            {
                isDragging = true;
                action.isDraggingForBox = true;
                if (gameObject.name.ToUpper()[0] == scenarioData.Scenarios[GameManager.Instance.currentIndex].ActionSceneTrueLetter.ToUpper()[0])
                {
                    action.isCorrectLetter = true;
                }
                else
                {
                    action.isCorrectLetter = false;
                }
                rb.gravityScale = 0f;
                rb.velocity = new(0, 0);
            }
            else
            {
                rb.gravityScale = gravityScale;
                isDragging = false;
                action.isDraggingForBox = false;
            }
        }
        else
        {
            if (isDraggable)
            {
                isDragging = true;
                demoAction.isDraggingForBox = true;
                if (gameObject.name.ToUpper()[0] == scenarioData.DemoScenarios[GameManager.Instance.currentIndex].DemoActionTrueLetter.ToUpper()[0])
                {
                    demoAction.isCorrectLetter = true;
                }
                else
                {
                    demoAction.isCorrectLetter = false;
                }
                rb.gravityScale = 0f;
                rb.velocity = new(0, 0);
            }
            else
            {
                rb.gravityScale = gravityScale;
                isDragging = false;
                demoAction.isDraggingForBox = false;
            }
        }


    }
    private void Update()
    {
        if (action != null)
        {
            timer += Time.deltaTime;
            if (isDragging)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                transform.Translate(mousePosition);
            }
            if (box.onBox)
            {
                isDraggable = false;
            }
            if (action != null)
            {
                if (!oneTime && action.didEnd)
                {
                    action.isCorrectLetter = false;
                    datas.actionAns[GameManager.Instance.currentIndex] = false;
                    datas.actionTimes[GameManager.Instance.currentIndex] = timer;
                    action.revealText();
                    oneTime = true;
                }
            }
            else
            {
                if (!oneTime && action.didEnd)
                {
                    demoAction.isCorrectLetter = false;
                    datas.actionAns[GameManager.Instance.currentIndex] = false;
                    datas.actionTimes[GameManager.Instance.currentIndex] = timer;
                    demoAction.revealText();
                    oneTime = true;
                }
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (isDragging)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                transform.Translate(mousePosition);
            }
            if (box.onBox)
            {
                isDraggable = false;
            }
                if (!oneTime && demoAction.didEnd)
                {
                    demoAction.isCorrectLetter = false;
                    datas.actionAns[GameManager.Instance.currentIndex] = false;
                    datas.actionTimes[GameManager.Instance.currentIndex] = timer;
                    demoAction.revealText();
                    oneTime = true;
                }
            
            else
            {
                if (!oneTime && demoAction.didEnd)
                {
                    demoAction.isCorrectLetter = false;
                    datas.actionAns[GameManager.Instance.currentIndex] = false;
                    datas.actionTimes[GameManager.Instance.currentIndex] = timer;
                    demoAction.revealText();
                    oneTime = true;
                }
            }
        }

            
    }
    private void OnMouseUp()
    {
        if(action != null)
        {
            isDragging = false;
            action.isDraggingForBox = false;
            if (isOnBox)
            {
                rb.gravityScale = 0f;
                rb.velocity = new(0, 0);
                rb.mass = 0f;
                //ColorBlock cb;
                sceneTransitionManager.actionSceneConditionMet = true;
                isDraggable = false;

                if (!action.oneTime)
                {
                    datas.letterChosen[GameManager.Instance.currentIndex] = gameObject.name[0].ToString();
                    action.revealText();
                    action.oneTime = true;
                    VoiceFunction();
                    if (action.isCorrectLetter)
                        GameManager.Instance.score += 10;
                    else
                        GameManager.Instance.score -= 10;
                }
            }
            else
            {
                isDraggable = true;
            }

        }
        else
        {
            isDragging = false;
            demoAction.isDraggingForBox = false;
            if (isOnBox)
            {
                rb.gravityScale = 0f;
                rb.velocity = new(0, 0);
                rb.mass = 0f;
                //ColorBlock cb;
                sceneTransitionManager.actionSceneConditionMet = true;
                isDraggable = false;

                if (!demoAction.oneTime)
                {
                    datas.letterChosen[GameManager.Instance.currentIndex] = gameObject.name[0].ToString();
                    demoAction.revealText();
                    demoAction.oneTime = true;
                    VoiceFunction();
                    if (demoAction.isCorrectLetter)
                        GameManager.Instance.score += 10;
                    else
                        GameManager.Instance.score -= 10;
                }
            }
            else
            {
                isDraggable = true;
            }
        }

    }
    private void VoiceFunction()
    {
        if (action != null)
        {
            if (action.isCorrectLetter)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.PlacingLetter);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.CorrectClick);
            }
            else
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.PlacingLetter);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.WrongClick);
            }

        }
        else
        {
            if (demoAction.isCorrectLetter)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.PlacingLetter);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.CorrectClick);
            }
            else
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.PlacingLetter);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.WrongClick);
            }
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            isOnBox = true;
            
        }
        else
        {
            isOnBox = false;
        }

    }



}
