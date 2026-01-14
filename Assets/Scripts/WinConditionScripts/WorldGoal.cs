using UnityEngine;

public class WorldGoal : MonoBehaviour
{
    // va trebui tinuta apasat tasta E
    [SerializeField] private float holdTime = 3f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private bool playerInside;
    private bool completed;
    private float holdTimer;
    private int index = -1;

    //setam indexul
    public void SetIndex(int i)
    {
        index = i;
    }

    private void Update()
    {
        if (!WinManager.Instance.IsCurrentGoal(this))
            return;

        if (completed) return;
        if (!playerInside) 
        { 
            holdTimer = 0f; 
            WinManager.Instance.SetHoldProgress(0f); 
            return; 
        }

        if (Input.GetKey(interactKey))
        {
            holdTimer += Time.deltaTime;
            WinManager.Instance.SetHoldProgress(holdTimer / holdTime);



            if (holdTimer >= holdTime)
            {
                Complete();
            }
        }
        else
        {
            holdTimer = 0f;
            WinManager.Instance.SetHoldProgress(0f);

        }


    }

    private void Complete()
    {
        if (index < 0)
        {
            Debug.LogError("WorldGoal fara index setat! (SetIndex nu a fost apelat)");
            return;
        }
        if (completed) return;
        completed = true;

        WinManager.Instance.GoalCompleted(index);
        WinManager.Instance.SetHoldProgress(0f);
        WinManager.Instance.ShowHoldBar(false);

        // dezactivezi triggerul sa nu mai intre iar
        Collider c = GetComponent<Collider>();
        if (c != null) c.enabled = false;
        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WinManager.Instance.ShowHoldBar(true);
            playerInside = true;
            WinManager.Instance.SetCurrentGoal(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            holdTimer = 0f;
            WinManager.Instance.ClearCurrentGoal(this);
            WinManager.Instance.ShowHoldBar(false);
            WinManager.Instance.SetHoldProgress(0f);
        }
    }
}
