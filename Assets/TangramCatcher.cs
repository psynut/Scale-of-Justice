using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TangramCatcher : MonoBehaviour
{
    [SerializeField]
    private Collider shuffleArea;
    [SerializeField]
    private Transform[] placementPoints;
    [SerializeField]
    private Transform[] tangramPieces;
    [SerializeField]
    private Camera m_camera;
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float rotationSpeed = 5f;

    public UnityEvent GameComplete;
    private TestingBoundary[] testingBoundaries;

    private Rigidbody selectedPiece;
    private bool pieceSelected = false;

    private Vector2 mousePos;
    private float rotationAxis;

    private void Start() {
        testingBoundaries = GetComponentsInChildren<TestingBoundary>();
        ShufflePieces();
    }

    private void FixedUpdate() {
        if(pieceSelected) {
            //selectedPiece.GetComponent<Rigidbody>().MovePosition(m_camera.ScreenToWorldPoint(mousePos));
            selectedPiece.velocity = Vector3.Normalize(m_camera.ScreenToWorldPoint(mousePos) - selectedPiece.position)*Time.fixedDeltaTime*movementSpeed;

            Vector3 m_EulerAngleVelocity = new Vector3(0,0,1);
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * rotationSpeed * rotationAxis * Time.fixedDeltaTime);;
            selectedPiece.MoveRotation(selectedPiece.GetComponent<Rigidbody>().rotation * deltaRotation);
            //selectedPiece.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(Vector3.right * Time.fixedDeltaTime * rotationAxis * rotationSpeed));
        }
    }

    private void OnChange() {
        bool testingBoundariesOccupied = false;
        foreach(TestingBoundary testingBoundary in testingBoundaries) {
            if(!testingBoundary.IsEmpty()) {
                testingBoundariesOccupied = true;
                break;
            }
        }
        if(!testingBoundariesOccupied) {
            GameComplete?.Invoke();
            Debug.Log("Tangram complete!");
        }
    }

    private void ShufflePieces() {
        List<int> drawPile = new List<int>{0,1,2,3,4,5,6};
        foreach(Transform placementPoint in placementPoints) {
            int rndPull = drawPile[Random.Range(0,drawPile.Count)];
            tangramPieces[rndPull].position = placementPoint.position;
            tangramPieces[rndPull].rotation = Quaternion.Euler(tangramPieces[rndPull].rotation.eulerAngles.x,90 * drawPile.Count,tangramPieces[rndPull].rotation.eulerAngles.z);
            drawPile.Remove(rndPull);
        }
    }

    public void OnPoint(InputValue value) {
        mousePos = value.Get<Vector2>();
        //Debug.Log(m_camera.ScreenToWorldPoint(mousePos));

    }

    public void OnClick(InputValue value) {
        //Debug.Log("click at" + mousePos );
        //Debug.Log("screen at " + m_camera.ScreenToViewportPoint(mousePos));
        if(!pieceSelected) {
            Ray ray = m_camera.ScreenPointToRay(mousePos);
            Physics.Raycast(ray,out RaycastHit hitInfo,100);
            if(hitInfo.collider.tag == "Tangram") {
                selectedPiece = hitInfo.collider.GetComponent<Rigidbody>();
                selectedPiece.mass = 1f;
                pieceSelected = true;
            }
        } else {
            if(selectedPiece != null) {
                StartCoroutine(SettlePiece(selectedPiece));
            }
            pieceSelected = false;
        }
    }

    public void OnRotate(InputValue value) {
        rotationAxis = value.Get<float>();
        //Debug.Log(rotationAxis);
    }

    private IEnumerator SettlePiece (Rigidbody piece) {
        piece.mass = 100f;
        piece.isKinematic = true;
        yield return new WaitForSeconds(.04f);
        piece.isKinematic = false;
        Vector3 pieceRotation = selectedPiece.transform.localRotation.eulerAngles;
        selectedPiece.transform.localRotation = Quaternion.Euler(270f,pieceRotation.y,pieceRotation.z);
        
    }


    //Could not Get these methods to work properly - switched to fixed points and random assignement of pieces.
    
    /*private void ShufflePieces() {
        foreach(Transform tangramPiece in tangramPieces) {
            StartCoroutine("PlacePiece",tangramPiece);
        }
    }*/

    /*private void ShufflePieces() {
        for(int i = 0; i < tangramPieces.Length; i++) {
            StartCoroutine(PlacePiece(tangramPieces[i], i));
        }
    

    private IEnumerator PlacePiece(Transform tangramPiece, int m_int) {
        float randomX = Random.Range(shuffleArea.bounds.min.x,shuffleArea.bounds.max.x);
        float randomZ = Random.Range(shuffleArea.bounds.min.z,shuffleArea.bounds.max.z);
        tangramPiece.GetComponent<Collider>().isTrigger = true;
        tangramPiece.position = new Vector3(randomX,tangramPiece.position.y,randomZ);
        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(m_int);
        Debug.Log(tangramPiece.GetComponent<TangramPiece>().IsTouchingPiece);
        if(tangramPiece.GetComponent<TangramPiece>().IsTouchingPiece) {
            StartCoroutine(PlacePiece(tangramPiece,m_int));
        } else {
            tangramPiece.GetComponent<Collider>().isTrigger = false;
        }
    }*/

}
