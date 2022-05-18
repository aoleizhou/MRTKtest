using UnityEngine;
using TMPro;

public class SwitchControl : MonoBehaviour
{
    
    public AudioSource compareAudio;
    public AudioSource elbowAudio;
    public AudioSource wristAudio;
    public AudioSource boneAudio;

    private int status;
    public GameObject bones;
    public GameObject elbow;
    public GameObject wrist;
    public GameObject compare;
    public GameObject[] cases = new GameObject[4];
    public string[] title = new string[4];
    public AudioSource[] audios = new AudioSource[4];
    public GameObject debugScreen;

    // Start is called before the first frame update
    void Start()
    {
        cases[0]=bones;
        cases[1]=elbow;
        cases[2]=wrist;
        cases[3]=compare;
        title[0]="Broken Arm From Shoulder";
        title[1]="Broken Elbow";
        title[2]="Broken Wrist";
        title[3]="Comparation of Broken and Healthy Shoulder";
        audios[0]=boneAudio;
        audios[1]=elbowAudio;
        audios[2]=wristAudio;
        audios[3]=compareAudio;
        bones.SetActive(true);
        elbow.SetActive(false);
        wrist.SetActive(false);
        compare.SetActive(false);
    }

    public void switchLeft()
    {
        Switch(-1);
    }

    public void switchRight()
    {
        Switch(1);
    }

    // -1 left, 1 right
    void Switch(int target)
    {
        cases[status].SetActive(false);
        status = (status+target+4)%4;
        debugScreen.GetComponent<TextMeshPro>().SetText(title[status]);
        cases[status].SetActive(true);
        audios[status].Play();
    }

    // Update is called once per frame
    void Update() {}

    public void playAudio() {}
}
