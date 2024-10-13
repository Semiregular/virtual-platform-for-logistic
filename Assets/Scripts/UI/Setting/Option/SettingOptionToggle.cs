using UnityEngine;


namespace UI.Setting.Option
{
    public class SettingOptionToggle : MonoBehaviour
    {
        public Transform configParent;
        public GameObject mapPara;
        public GameObject mapEdit;
        public GameObject stackKiva;
        public GameObject stackCtu;
        public GameObject liftKiva;
        public GameObject liftCtu;
        public GameObject objPara;
        public GameObject objRelate;
        public GameObject taskPara;
        public GameObject taskBatch;
        public GameObject algoRandom;
        public GameObject algoAStar;
        public GameObject other;

        private int curIndex;

        public static SettingOptionToggle Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Hide(int index)
        {
            GameObject child = configParent.GetChild(index).gameObject;
            child.SetActive(false);
            
        }
        public void MapPara()
        {
            if(curIndex != 0)
            {
                Hide(curIndex);
                mapPara.SetActive(true);
                curIndex = 0; 
            }  
        }

        public void MapEdit()
        {
            if (curIndex != 1)
            {
                Hide(curIndex);
                mapEdit.SetActive(true);
                curIndex = 1;
            }
        }

        public void StackKiva()
        {
            if (curIndex != 2)
            {
                Hide(curIndex);
                stackKiva.SetActive(true);
                curIndex = 2;
            }
        }

        public void StackCtu()
        {
            if (curIndex != 3)
            {
                Hide(curIndex);
                stackCtu.SetActive(true);
                curIndex = 3;
            }
        }

        public void LiftKiva()
        {
            if (curIndex != 4)
            {
                Hide(curIndex);
                liftKiva.SetActive(true);
                curIndex = 4;
            }
        }

        public void LiftCtu()
        {
            if (curIndex != 5)
            {
                Hide(curIndex);
                liftCtu.SetActive(true);
                curIndex = 5;
            }
        }

        public void ObjPara()
        {
            if (curIndex != 6)
            {
                Hide(curIndex);
                objPara.SetActive(true);
                curIndex = 6;
            }
        }

        public void ObjRelate()
        {
            if (curIndex != 7)
            {
                Hide(curIndex);
                objRelate.SetActive(true);
                curIndex = 7;
            }
        }

        public void TaskPara()
        {
            if (curIndex != 8)
            {
                Hide(curIndex);
                taskPara.SetActive(true);
                curIndex = 8;
            }
        }

        public void TaskBatch()
        {
            if (curIndex != 9)
            {
                Hide(curIndex);
                taskBatch.SetActive(true);
                curIndex = 9;
            }
        }

        public void AlgoRandom()
        {
            if (curIndex != 10)
            {
                Hide(curIndex);
                algoRandom.SetActive(true);
                curIndex = 10;
            }
        }

        public void AlgoAStar()
        {
            if (curIndex != 11)
            {
                Hide(curIndex);
                algoAStar.SetActive(true);
                curIndex = 11;
            }
        }

        public void Other()
        {
            if (curIndex != 12)
            {
                Hide(curIndex);
                other.SetActive(true);
                curIndex = 12;
            }
        }
    }

}
