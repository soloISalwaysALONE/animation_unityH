using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : IObject
{
    public Transform target; //позиция объекта, к которому будет бежать монстр
    NavMeshAgent agent; //ссылка на объект навигации
    Animator anim; //ссылка на аниматор
    public Animator animOfCharacter; //ссылка на аниматор
    public float attack_distance = 20; //дистанция атаки
    public float wait = 50f;
    public float hp = 50;
   
    
        void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //получение ссылки на объект навигации
        anim = GetComponent<Animator>(); //получение ссылки на аниматор
    }
    bool checkAtt = false;
    private void Update()
    {
        checkAtt = animOfCharacter.GetBool("Att");
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= attack_distance) //если монстр подошёл на дистанцию атаки
        {
            
            HealtsPoints();
        }
    }
    void LateUpdate()
    {
        agent.SetDestination(target.position); //установка цели, к которой будет двигаться монстр
        agent.stoppingDistance = attack_distance; //на каком расстоянии от цели остановится монстр

        checkAtt = animOfCharacter.GetBool("Att");
        //вычисление дистанции между противником и целью
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attack_distance) //если монстр подошёл на дистанцию атаки
        {
            anim.SetBool("Att", true); //запустить анимацию атаки
            HealtsPoints();
        }
        else
        {

            anim.SetBool("Att", false);

        }
        
    }
    void HealtsPoints()
    {
        if (checkAtt == true)
        {
            hp -=  Time.deltaTime * 4;
        }
        else
        {
            hp += Time.deltaTime * 1;
        }
        if (hp<=0)
        {
            anim.SetBool("Death", true);
            Destroy(this.gameObject, wait);
        }
    }
    public override void interact()
    {
        anim.SetBool("Death", true); //анимация смерти монстра
        agent.SetDestination(transform.position); //остановка монстра

        Destroy(this.gameObject, wait);
    }
    public override Actions getAction()
    {
        return Actions.attack; // opening animation
    }

    public override Vector3 getPoint(Vector3 pos)
    {
        return pos;
    }
}
