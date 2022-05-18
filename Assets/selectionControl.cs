using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.Input;

public class selectionControl : MonoBehaviour
{
    public AudioSource selectAudio;
    public AudioSource unselectAudio;
    public AudioSource copyAudio;
    public AudioSource deleteAudio;
    public AudioSource addBoardAudio;
    public AudioSource hadBoardAudio;
    public AudioSource combineAudio;
    public AudioSource uncombineAudio;

    public List<GameObject> selectedBones = new List<GameObject>();
    public List<GameObject> combined = new List<GameObject>();
    public List<GameObject> bones;
    
    public GameObject selectPane;
    public GameObject operationPane;
    public GameObject debugScreen;
    public GameObject board;
    
    string str1 = "You are in the <operation> mode.";
    string str2 = "You are in the <selection> mode.";

    private GameObject father;
    private Transform boardFatherTransform;

    // Start is called before the first frame update
    async void Start()
    {
        // objectSelected =new int[bones.transform.childCount];
        // string s = "";
        // for(int i =0;i<objectSelected.Length;i++){
        //     s+=objectSelected[i]+" ";
        // }
        // debugScreen.GetComponent<TextMeshPro>().SetText(s);
        operationPane.SetActive(false);
        boardFatherTransform = board.transform.parent;
        foreach (GameObject child in bones)
        {
            for (int i = 0; i < child.transform.childCount; ++i)
            {   
                GameObject obj = child.transform.GetChild(i).gameObject;
                if (obj.GetComponent<Interactable>() != null)
                {
                    obj.GetComponent<Interactable>().enabled = false;
                }
            }
        }
        //debugScreen.GetComponent<TextMeshPro>().SetText("start success");
    }

    // Update is called once per frame
    void Update(){}

    public void startSelect()
    {
        debugScreen.GetComponent<TextMeshPro>().SetText(str2);
        operationPane.SetActive(true);
        selectPane.SetActive(false);
        foreach (GameObject child in bones)
        {
            child.GetComponent<BoxCollider>().enabled                       = false;
            child.GetComponent<RotationAxisConstraint>().enabled            = false;
            child.GetComponent<BoundsControl>().enabled                     = false;
            child.GetComponent<CursorContextObjectManipulator>().enabled    = false;
            child.GetComponent<ObjectManipulator>().enabled                 = false;
            child.GetComponent<MinMaxScaleConstraint>().enabled             = false;
            child.GetComponent<NearInteractionGrabbable>().enabled          = false;
            for (int i = 0; i < child.transform.childCount; ++i)
            {   
                GameObject obj = child.transform.GetChild(i).gameObject;
                if (obj.GetComponent<Interactable>() != null)
                {
                    obj.GetComponent<Interactable>().enabled = true;
                }
            }
        }
    }

    public void endSelect()
    {
        debugScreen.GetComponent<TextMeshPro>().SetText(str1);
        operationPane.SetActive(false);
        selectPane.SetActive(true);
        foreach (GameObject child in bones)
        {
            child.GetComponent<BoxCollider>().enabled                       = true;
            child.GetComponent<RotationAxisConstraint>().enabled            = true;
            child.GetComponent<BoundsControl>().enabled                     = true;
            child.GetComponent<CursorContextObjectManipulator>().enabled    = true;
            child.GetComponent<ObjectManipulator>().enabled                 = true;
            child.GetComponent<MinMaxScaleConstraint>().enabled             = true;
            child.GetComponent<NearInteractionGrabbable>().enabled          = true;
            for (int i = 0; i < child.transform.childCount; ++i)
            {   
                GameObject obj = child.transform.GetChild(i).gameObject;
                if (obj.GetComponent<Interactable>() != null)
                {
                    obj.GetComponent<Interactable>().enabled = false;
                }
            }
        }
        selectedBones.Clear();
    }

    public void select(GameObject obj){
        selectedBones.Add(obj);
    }

    public void deselect(GameObject obj)
    {
        selectedBones.Remove(obj);
    }

    public void copy_paste(){
        // copy paste todo
        
        foreach (GameObject child in selectedBones)
        {
            for (int i = 0; i < child.transform.childCount; ++i)
            {   
                GameObject obj = child.transform.GetChild(i).gameObject;
                if (obj.GetComponent<Interactable>() != null)
                {
                    obj.GetComponent<Interactable>().IsToggled = false;
                }
            }
            GameObject cloneObj = GameObject.Instantiate(child);
            cloneObj.transform.parent = child.transform.parent;
            bones.Add(cloneObj);
            Vector3 newPosition = child.transform.position;
            newPosition.y += 0.2f;
            cloneObj.transform.position = newPosition;
        }
        selectedBones.Clear();
        // this.endSelect();
    }

    public void delete()
    {
        foreach (GameObject child in selectedBones)
        {
            child.SetActive(false);
            bones.Remove(child);
        }
        selectedBones.Clear();
        // this.endSelect();
    }

    public void combine()
    {
        if (selectedBones != null) {
            if (selectedBones[0].Equals(board))
            {
                selectedBones[0] = selectedBones[1];
                selectedBones[1] = board;
            }
            father = selectedBones[0];
            for (int i = 0; i < father.transform.childCount; ++i)
            {   
                GameObject obj = father.transform.GetChild(i).gameObject;
                if (obj.GetComponent<Interactable>() != null)
                {
                    obj.GetComponent<Interactable>().IsToggled = false;
                }
            }
            GameObject cloneFather = GameObject.Instantiate(father);
            cloneFather.transform.parent = father.transform.parent;
            cloneFather.transform.position = father.transform.position;
            father.SetActive(false);
            bones.Remove(father);
            bones.Add(cloneFather);
            combined.Add(cloneFather);
            for (int i = 1; i < selectedBones.Count; ++i)
            {
                selectedBones[i].transform.parent = cloneFather.transform;
                for (int j = 0; j < selectedBones[i].transform.childCount; ++j)
                {   
                    GameObject obj = selectedBones[i].transform.GetChild(j).gameObject;
                    if (obj.GetComponent<Interactable>() != null)
                    {
                        obj.GetComponent<Interactable>().IsToggled = false;
                    }
                }
                combined.Add(selectedBones[i]);
            }
            selectedBones.Clear();
        }
        // this.endSelect();
    }

    public void uncombine()
    {
        if (combined != null) {
            combined[0].SetActive(false);
            bones.Remove(combined[0]);
            father.SetActive(true);
            bones.Add(father);
            for (int i = 1; i < combined.Count; ++i)
            {
                if (combined[i].Equals(board))
                {
                    combined[i].transform.parent = boardFatherTransform;
                } else {
                    combined[i].transform.parent = father.transform.parent;
                }
            }
            combined.Clear();
        }
        // this.endSelect();
    }

    public void addBoard()
    {
        board.SetActive(true);
        if (!bones.Contains(board))
        {
            bones.Add(board);
            addBoardAudio.Play();
        } else {
            hadBoardAudio.Play();
        }
    }

    public void playSelect()
    {
        selectAudio.Play();
    }

    public void playUnselect()
    {
        unselectAudio.Play();
    }

    public void playCopy()
    {
        copyAudio.Play();
    }

    public void playDelete()
    {
        deleteAudio.Play();
    }

    public void playAddBoard()
    {
        addBoardAudio.Play();
    }
    public void playCombine()
    {
        combineAudio.Play();
    }

    public void playUncombine()
    {
        uncombineAudio.Play();
    }
}