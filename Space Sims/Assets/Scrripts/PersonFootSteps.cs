using UnityEngine;

public class PersonFootSteps : MonoBehaviour
{
    Vector3 LastPositon;

    Person MainPersonScript;

    private void Start()
    {
        MainPersonScript = gameObject.GetComponent<Person>();
        LastPositon = transform.position;
    }

    private void LateUpdate()
    {
        if (MainPersonScript.IsBeingHeld)
        {
            LastPositon = transform.position;
            return;
        }
        else if (Mathf.Abs(Vector3.Distance(transform.position, LastPositon)) >= .5f)
        {
            LastPositon = transform.position;
            SoundManager.Instance.PlaySoundIfVisableAndZoomedIn(SoundManager.Sound.MetalFootSteps, transform.position, 2.5f, 0.02f);
        }
    }
}
