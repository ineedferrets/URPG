using UnityEngine;
using System.Collections;

public class MonsterBehaviour : MonoBehaviour {

    private float timer = 0;
    private float timeBetweenAttacks;

    void Start ()
    {
        timeBetweenAttacks = Random.Range(0.1f, 1.0f);
    }

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks)
        {
            Attack(2);
            timer = 0;
        }
	}

    void Attack(int amount)
    {
        GameObject.Find("Player").GetComponent<PlayerStats>().Damage(amount);
    }

    public void Death () {
        GameObject.Find("Player").GetComponent<PlayerStats>().playerCombatScore += 10;
    }
}
