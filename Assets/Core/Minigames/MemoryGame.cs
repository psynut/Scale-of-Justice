using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;

public class MemoryGame : MonoBehaviour
{
    public UnityEvent OnMatch;
    public UnityEvent OnNoMatch;
    public UnityEvent OnGameComplete;

    [SerializeField]
    private Transform[] cardSpawnPoints;
    [SerializeField]
    private Transform cardsParent;
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private Camera m_Camera;
   
    //Witness Impeachment does not fit on cards
    private List<string> phrases = new List<string>{"Probable Cause", "Illegal Search", "Alibi", "Motion to Supress", "Change of Venue", "Lessor Charge",
                        "Surplusage", "Police Misconduct", "Lack of Intent", "Invoke Privilege", /*"Witness Impeachment",*/  "Defferred Disposition",
                        "Exculpatory Evidence"};

    private Vector2 mousePos;
    private GameObject cardPick1, cardPick2;

    private bool clickPause = false;

    private int matchesLeft;

    private void Awake() {

    }
    // Start is called before the first frame update
    void Start()
    {
        List<Transform> cardSpawnPointList = new List<Transform>(cardSpawnPoints);
        foreach(Transform trans in cardSpawnPointList) {
            Debug.Log(trans.position);
        }
        int count = cardSpawnPointList.Count;
        if(count % 2 != 0) {
            Debug.LogError("Need even set of cardSpawnPoints for " + this + " to properly set up Memory Game");
        }
        matchesLeft = count / 2;
        for(int i=0; i < count/2; i++) {
            string phrase = phrases[Random.Range(0,phrases.Count)];
            int cardNo = Random.Range(0,cardSpawnPointList.Count);
            GameObject card1 = Instantiate(cardPrefab,cardSpawnPointList[cardNo].position,Quaternion.Euler(270f,0,180f),cardsParent);
            TMP_Text m_TMP = card1.GetComponentInChildren<TMP_Text>();
            m_TMP.text = phrase;
            cardSpawnPointList.Remove(cardSpawnPointList[cardNo]);
            cardNo = Random.Range(0,cardSpawnPointList.Count);
            GameObject card2 = Instantiate(cardPrefab,cardSpawnPointList[cardNo].position,Quaternion.Euler(270f, 0, 180f),cardsParent);
            m_TMP = card2.GetComponentInChildren<TMP_Text>();
            m_TMP.text = phrase;
            cardSpawnPointList.Remove(cardSpawnPointList[cardNo]);
            phrases.Remove(phrase);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPoint(InputValue  value) {
            mousePos = value.Get<Vector2>();
    }

    public void OnClick(InputValue value) {
        Ray ray = m_Camera.ScreenPointToRay(mousePos);
        Physics.Raycast(ray,out RaycastHit hitInfo,100);
        if(hitInfo.collider.tag == "Memory" && clickPause == false) {
            //StartCoroutine(ClickPause());
            Debug.Log(cardPick1);
            if(cardPick1 == null) {
                cardPick1 = hitInfo.collider.gameObject;
                cardPick1.GetComponent<Animator>().SetBool("Flip",true);
            } else if(hitInfo.collider.gameObject != cardPick1) {
                cardPick2 = hitInfo.collider.gameObject;
                cardPick2.GetComponent<Animator>().SetBool("Flip",true);
                if(cardPick1.GetComponentInChildren<TMP_Text>().text == cardPick2.GetComponentInChildren<TMP_Text>().text) {
                    OnMatch?.Invoke();
                    matchesLeft--;
                    if(matchesLeft == 0) {
                        OnGameComplete?.Invoke();
                    }
                    Debug.Log("Cards Match!");
                    cardPick1 = null;
                    cardPick2 = null;
                } else {
                    StartCoroutine(SecondCardFlip());
                }

            }
        }
    }

    private IEnumerator SecondCardFlip() {
        clickPause = true;
        yield return new WaitForSeconds(1f);
        cardPick1.GetComponent<Animator>().SetBool("Flip",false);
        cardPick2.GetComponent<Animator>().SetBool("Flip",false);
        OnNoMatch?.Invoke();
        cardPick1 = null;
        cardPick2 = null;
        clickPause = false;
    }

    /*private IEnumerator ClickPause() {
        clickPause = true;
        Debug.Log(clickPause);
        yield return new WaitForSeconds(.5f);
        clickPause = false;
        Debug.Log(clickPause);
    }*/

}
