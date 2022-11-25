using UnityEngine;

public class OldcopController : MonoBehaviour
{
    public bool seeBench = false;

    public bool canSit = false;

    public bool isSitting = false;

    public Transform bench = null;

    private Animator animator;

    private float sitCountDown = 0;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        sitCountDown += Time.deltaTime;

        if (sitCountDown > 8.0f)
        {
            canSit = true;
        }
        else
        {
            canSit = false;
        }
    }

    public void OnSee(GameObject g)
    {
        if (!bench && canSit)
        {
            seeBench = true;
            bench = g.transform;
        }
    }

    public void CatchThief()
    {
        GetComponentInChildren<EmotionController>().ChangeSprite(1, 5.0f);
    }

    public bool CanISit()
    {
        if (bench)
        {
            if (Vector3.Distance(bench.position, transform.position) < 1.5f)
            {
                return true;
            }
        }        

        return false;
    }

    public void Sit()
    {
        // cambiar animacion
        isSitting = true;
        GetComponentInChildren<EmotionController>().ChangeSprite(0);
        if (animator)
        {
            animator.SetBool("Move", true);
        }
        Invoke("ResetWander", Random.Range(6.0f, 10.0f));
    }

    private void ResetWander()
    {
        canSit = false;
        seeBench = false;
        sitCountDown = 0;
        isSitting = false;
        bench = null;
        if (animator)
        {
            animator.SetBool("Move", false);
        }
        GetComponentInChildren<EmotionController>().ChangeSprite(-1);
    }
}