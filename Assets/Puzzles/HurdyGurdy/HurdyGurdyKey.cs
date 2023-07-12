using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HurdyGurdyKey : MonoBehaviour
{
    [SerializeField] private HurdyGurdyManager melodyManager;
    [SerializeField] public int note;

    private float t;
    private float lerpSpeed = 3.75f;
    private float colorMultiplier = 1.2f;
    private bool isPlayingResolutionMelody;
    private Color primaryColor;
    private Color secondaryColor;
    private MeshRenderer mesh;
    private Animator animator;
    private string[] noteSoundEvents = new string[8]{"NoteOne", "NoteTwo", "NoteThree", "NoteFour", "NoteFive", "NoteSix", "NoteSeven", "NoteEight"};
    private XRSimpleInteractable interactable;

    private void Awake() => interactable = GetComponent<XRSimpleInteractable>();

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(PlayNote);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        mesh = GetComponent<MeshRenderer>();
        primaryColor = mesh.material.color;
        secondaryColor = primaryColor * colorMultiplier;
    }

    private void Update()
    {
        if (!isPlayingResolutionMelody) return;
        t += Time.deltaTime;
        mesh.material.color = Color.Lerp(primaryColor, secondaryColor, Mathf.Abs(Mathf.Sin(t * lerpSpeed)));
    }

    public void PlayNote(SelectEnterEventArgs arg0)
    {
        if (arg0.interactorObject.GetType() == typeof(XRDirectInteractor) || !melodyManager.lever.GetIsMoving() || melodyManager.GetIsPlayingResolutionMelody()) return;
        animator.SetTrigger("PlayNote");
        FMODUnity.RuntimeManager.PlayOneShot($"event:/Diegetic/Sounds/HurdyGurdy/{noteSoundEvents[note - 1]}");
        if (melodyManager.GetIsSolved()) return;
        melodyManager.AddNoteToSequence(note);
    }

    public void SetIsPlayingResolutionMelody(bool val)
    {
        isPlayingResolutionMelody = val;
        if (val == false)
        {
            mesh.material.color = primaryColor;
        }
    }
}
