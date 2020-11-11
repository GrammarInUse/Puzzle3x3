using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ClassManagement
{
    public class GameHandler{
        //Properties
        public List<Transform> _processTree;
        private bool _isWon = false;
        private int _starNum = 3;
        private bool _isLost = false;
        private Transform _player;
        private int _level = 1;
        private double _timeOut = 60;

        public int StarNum{
            get{
                return _starNum;
            }
            set{
                _starNum = value;
            }
        }

        public double TimeOut{
            get{
                return _timeOut;
            }
            set{
                _timeOut = value;
            }
        }

        public int Level{
            get{
                return _level;
            }
            set{
                _level = value;
            }
        }

        public Transform Player{
            get{
                return _player;
            }
            set{
                _player = value;
            }
        }

        public bool IsWon{
            get{
                return _isWon;
            }
            set{
                _isWon = value;
            }
        }

        public bool IsLost{
            get{
                return _isLost;
            }
            set{
                _isLost = value;
            }
        }

        //private bool _isNeedHelp = false;

        //Constructor
        public GameHandler(){
            //
        }

        public GameHandler(List<Transform> _list){
            this._processTree = _list;
            this.Player = this._processTree[this._processTree.Count - 1];
            double newLevel = System.Math.Sqrt(_list.Count);
            Level = System.Convert.ToInt32(newLevel) - 2;
            TimeOut = 60 * Level + 60 * (Level - 1);
        }

        public double GetTimeOut(){
            return TimeOut;
        }

        public bool GetLostChecker(){
            return IsLost;
        }

        public int GetStarNum(){
            return StarNum;
        }

        public void DownTheStar(int _num){
            StarNum = _num;
        }

        //Methods
        public static int CountLessThan(List<int> _list){
            int iCount = 0;
            for(int i = 0; i < _list.Count - 1; i++){
                for(int j = i + 1; j < _list.Count; j++){
                    if(_list[i] == 8){
                        continue;
                    }
                    if(_list[i] > _list[j]){
                        iCount += 1;
                    }
                }         
            }
            return iCount;
        }

        public static List<int> RandomInt (int _quantity){
            List<int> listOfInt = new List<int>();
            List<int> listOfChecker = new List<int>();
            for(int i = 0; i < _quantity; i++){
                listOfInt.Add(i);
            }
            List<int> result = new List<int>();
            for(int i = 0; i < _quantity; i++){
                int temp = Random.Range(0, listOfInt.Count);
                result.Add(listOfInt[temp]);
                listOfChecker.Add(listOfInt[temp]);
                listOfInt.RemoveAt(temp);
            }

            int iCount = CountLessThan(listOfChecker);
            Debug.Log("ICOUNT: " + iCount);
            if(iCount % 2 != 0){
                return RandomInt(_quantity);
            }

            return result;
        }

        public void PrintProcessTree(){
            for(int i = 0; i < this._processTree.Count; i++){
                Debug.Log(this._processTree[i].name);
            }
        }

        public void MoveManagement(){
            if(!this._isWon){
                if(Input.GetKeyDown("up")){
                    MoveElementOnTree(2);
                }
                if(Input.GetKeyDown("down")){
                    MoveElementOnTree(8);
                }
                if(Input.GetKeyDown("left")){
                    MoveElementOnTree(6);
                }
                if(Input.GetKeyDown("right")){
                    MoveElementOnTree(4);
                }
                // if(Input.GetKeyDown("space")){
                //     _isNeedHelp = !_isNeedHelp;
                //     _instruction.SetActive(_isNeedHelp);        
                // }
            }
        }

        public Transform FindPlayer(){
            string temp = "";
            if(Level == 1){
                temp = "Cube9";
            }else if(Level == 2){
                temp = "Cube16";
            }else{
                temp = "Cube25";
            }
            for(int i = 0; i < _processTree.Count; i++){
                if(_processTree[i].gameObject.name == temp){
                    Player = _processTree[i];
                    return _processTree[i];
                }
            }
            return null;
        }

        public bool checkAvailable(int currPos, int _direction){
            for(int i = 0; i < Level + 2; i++){
                for(int j = 0; j < Level + 2; j++){
                    int t = 3 * i + j;
                    if(i > 0 && currPos == t && _direction == 8){  
                        return true;
                    }
                    if(i != Level + 1 && currPos == t && _direction == 2){
                        return true;
                    }
                    if(j > 0 && currPos == t && _direction == 4){
                        return true;
                    }
                    if(j != Level + 1 && currPos == t && _direction == 6){
                        return true;
                    }
                }
            }
            return false;
        }

        public void MoveElementOnTree(int _direction){
            bool isMove = false;
            int currPos = 0;
            for(int i = 0; i < _processTree.Count; i++){
                if(_processTree[i].gameObject.name == Player.gameObject.name){
                    currPos = i;
                    break;
                }
            }
            isMove = checkAvailable(currPos, _direction);
            switch(_direction){
                case 8:
                    if(isMove){
                        Transform temp = _processTree[currPos];
                        _processTree[currPos] = _processTree[currPos - 3];
                        _processTree[currPos - 3] = temp;
                    }else{
                        Debug.Log("CAN NOT MOVE!");
                    }
                    break;
                case 2:
                    if(isMove){
                        Transform temp = _processTree[currPos];
                        _processTree[currPos] = _processTree[currPos + 3];
                        _processTree[currPos + 3] = temp;
                    }else{
                        Debug.Log("CAN NOT MOVE!");
                    }
                    break;
                case 4: 
                    if(isMove){
                        Transform temp = _processTree[currPos];
                        _processTree[currPos] = _processTree[currPos - 1];
                        _processTree[currPos - 1] = temp;
                    }else{
                        Debug.Log("CAN NOT MOVE!");
                    }
                    break;
                case 6:
                    if(isMove){
                        Transform temp = _processTree[currPos];
                        _processTree[currPos] = _processTree[currPos + 1];
                        _processTree[currPos + 1] = temp;
                    }else{
                        Debug.Log("CAN NOT MOVE!");
                    }
                    break;
                default:
                    Debug.Log("CAN NOT MOVE!");
                    break;
            }
        }

        public void DisplayProcessTree(){       
            for(int i = 0; i < _processTree.Count; i++){
                switch(i){
                    case 0:
                        _processTree[i].localPosition = new Vector3(-2.699417f, -47.09757f, -1.849998f);
                        break;
                    case 1:
                        _processTree[i].localPosition = new Vector3(-2.699417f + 5f, -47.09757f, -1.849998f);
                        break;
                    case 2:
                        _processTree[i].localPosition = new Vector3(-2.699417f + 10f, -47.09757f, -1.849998f);
                        break;
                    case 3:
                        _processTree[i].localPosition = new Vector3(-2.699417f, -47.09757f, -6.850677f);
                        break;
                    case 4:
                        _processTree[i].localPosition = new Vector3(-2.699417f + 5f, -47.09757f, -6.850677f);
                        break;
                    case 5:
                        _processTree[i].localPosition = new Vector3(-2.699417f + 10f, -47.09757f, -6.850677f);
                        break;
                    case 6:
                        _processTree[i].localPosition = new Vector3(-2.699417f, -47.09757f, -11.85f);
                        break;
                    case 7:
                        _processTree[i].localPosition = new Vector3(-2.699417f + 5f, -47.09757f, -11.85f);
                        break;
                    case 8:
                        _processTree[i].localPosition = new Vector3(-2.699417f + 10f, -47.09757f, -11.85f);
                        break;
                    default:
                        Debug.Log("Out of range");
                        break;
                }
            }
            this._isWon = CheckWinning();
            if(this._isWon){
                Debug.Log("YOU WON!!");
                this._processTree[this._processTree.Count - 1].gameObject.SetActive(true);
            }
        }

        public bool CheckWinning(){
            for(int i = 0; i < this._processTree.Count - 1; i++){
                string temp1 = this._processTree[i].gameObject.name;
                string temp2 = this._processTree[i + 1].gameObject.name;
                if(string.Compare(temp1, temp2, true) < 0){
                    continue;
                }else{
                    return false;
                }
            }
            IsWon = true;
            return IsWon;
        }

    }
}
