using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public GameObject trailObject;
    public Bird TargetBird;

    private List<GameObject> _trails;

    void Start()
    {
        _trails = new List<GameObject>();
    }

    public void SetBird(Bird bird)
    {
        TargetBird = bird;

        for (int i = 0; i < _trails.Count; i++)
        {
            Destroy(_trails[i].gameObject);
        }

        _trails.Clear();
    }

    public IEnumerator SpawnTrail()
    {
        _trails.Add(Instantiate(trailObject, TargetBird.transform.position, Quaternion.identity));

        yield return new WaitForSeconds(0.1f);

        if (TargetBird != null && TargetBird.State != Bird.BirdState.HitSomething)
        {
            StartCoroutine(SpawnTrail());
        }
    }
}
