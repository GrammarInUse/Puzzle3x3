
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour
{
    public GameObject element01, element02, element03, element04, element05, element06, element07, element08, element09;
    private bool bIntroLatias = false;
    public GameObject intro_Latias;
    public List<GameObject> ProcessTree = new List<GameObject>();
    private bool bIsWon = false;
    // Start is called before the first frame update
    void Start()
    {
        setupGame();
    }

    // Tuple<float, float> getTuple(Transform A){
    //     return new Tuple<float, float>(A.position.x, A.position.z);
    // }

    void setupGame(){
        GameObject[] A = new GameObject[]{element01, element02, element03, element04, element05, element06, element07, element08, element09};
        List<int> result = RandomInt(A.Length);

        for(int i = 0; i < A.Length; i++){
            int a = result[i];
            ProcessTree.Add(A[result[i]]);
            if(i == 8){
                A[i].SetActive(false);
            }
            displayToScreen(ref ProcessTree);
        }
        
    }

    List<int> RandomInt(int n){
        List<int> listOfInt = new List<int>(){0, 1, 2, 3, 4, 5, 6, 7, 8};
        List<int> result = new List<int>();
        for(int i = 0; i < n; i++){
            int temp = Random.Range(0, listOfInt.Count - 1);
            result.Add(listOfInt[temp]);
            listOfInt.RemoveAt(temp);
        }
        return result;
    }

    bool isWon(){
        for(int i = 0; i < ProcessTree.Count - 1; i++){
            string temp1 = ProcessTree[i].name;
            string temp2 = ProcessTree[i + 1].name;
            if(string.Compare(temp1, temp2, true) < 0){
                continue;
            }else{
                return false;
            }
        }
        return true;
    }

    void printProcessTree(){
        for(int i = 0; i < ProcessTree.Count; i++){
            Debug.Log(ProcessTree[i].name);
        }
    }

    void MoveManagement(ref List<GameObject> _list, int _direction){
        int currPos = 0;
        for(int i = 0; i < _list.Count; i++){
            if(_list[i].name == "Cube9"){
                currPos = i;
            }
        }
        switch(_direction){
            case 8:
                if(currPos >= 3 && currPos <= 8){
                    GameObject temp = _list[currPos];
                    _list[currPos] = _list[currPos - 3];
                    _list[currPos - 3] = temp;
                }else{
                    Debug.Log("CAN NOT MOVE!");
                }
                break;
            case 2:
                if(currPos <= 5 && currPos >= 0){
                    GameObject temp = _list[currPos];
                    _list[currPos] = _list[currPos + 3];
                    _list[currPos + 3] = temp;
                }else{
                    Debug.Log("CAN NOT MOVE!");
                }
                break;
            case 4: 
                if(currPos == 1 || currPos == 2 || currPos == 4 || currPos == 5 || currPos == 7 || currPos == 8){
                    GameObject temp = _list[currPos];
                    _list[currPos] = _list[currPos - 1];
                    _list[currPos - 1] = temp;
                }else{
                    Debug.Log("CAN NOT MOVE!");
                }
                break;
            case 6:
                if(currPos == 0 || currPos == 1 || currPos == 3 || currPos == 4 || currPos == 6 || currPos == 7){
                    GameObject temp = _list[currPos];
                    _list[currPos] = _list[currPos + 1];
                    _list[currPos + 1] = temp;
                }else{
                    Debug.Log("CAN NOT MOVE!");
                }
                break;
            default:
                Debug.Log("CAN NOT MOVE!");
                break;
        }
    }

    void moveElementOnTree(){
        if(Input.GetKeyDown("up")){
            MoveManagement(ref ProcessTree, 2);
        }
        if(Input.GetKeyDown("down")){
            MoveManagement(ref ProcessTree, 8);
        }
        if(Input.GetKeyDown("left")){
            MoveManagement(ref ProcessTree, 6);
        }
        if(Input.GetKeyDown("right")){
            MoveManagement(ref ProcessTree, 4);
        }
        if(Input.GetKeyDown("space")){
            bIntroLatias = !bIntroLatias;
            intro_Latias.SetActive(bIntroLatias);        
        }
    }

    void displayToScreen(ref List<GameObject> _list){
        for(int i = 0; i < ProcessTree.Count; i++){
            switch(i){
                case 0:
                    _list[i].transform.position = new Vector3(-85f, 17.5f, 85f);
                    break;
                case 1:
                    _list[i].transform.position = new Vector3(-80f, 17.5f, 85f);
                    break;
                case 2:
                    _list[i].transform.position = new Vector3(-75f, 17.5f, 85f);
                    break;
                case 3:
                    _list[i].transform.position = new Vector3(-85f, 17.5f, 80f);
                    break;
                case 4:
                    _list[i].transform.position = new Vector3(-80f, 17.5f, 80f);
                    break;
                case 5:
                    _list[i].transform.position = new Vector3(-75f, 17.5f, 80f);
                    break;
                case 6:
                    _list[i].transform.position = new Vector3(-85f, 17.5f, 75f);
                    break;
                case 7:
                    _list[i].transform.position = new Vector3(-80f, 17.5f, 75f);
                    break;
                case 8:
                    _list[i].transform.position = new Vector3(-75f, 17.5f, 75f);
                    break;
                default:
                    Debug.Log("Out of range");
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        bIsWon = isWon();

        if(!bIsWon){
            moveElementOnTree();
        }else{
            Debug.Log("YOU WON");
            //Update Point for Player and Switch Camera
            element09.SetActive(true);
        }

        displayToScreen(ref ProcessTree);
    }
}
