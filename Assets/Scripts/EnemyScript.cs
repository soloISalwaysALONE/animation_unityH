using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : IObject
{
    public Transform target; //������� �������, � �������� ����� ������ ������
    NavMeshAgent agent; //������ �� ������ ���������
    Animator anim; //������ �� ��������
    public Animator animOfCharacter; //������ �� ��������
    public float attack_distance = 20; //��������� �����
    public float wait = 50f;
    public float hp = 50;
   
    
        void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //��������� ������ �� ������ ���������
        anim = GetComponent<Animator>(); //��������� ������ �� ��������
    }
    bool checkAtt = false;
    private void Update()
    {
        checkAtt = animOfCharacter.GetBool("Att");
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= attack_distance) //���� ������ ������� �� ��������� �����
        {
            
            HealtsPoints();
        }
    }
    void LateUpdate()
    {
        agent.SetDestination(target.position); //��������� ����, � ������� ����� ��������� ������
        agent.stoppingDistance = attack_distance; //�� ����� ���������� �� ���� ����������� ������

        checkAtt = animOfCharacter.GetBool("Att");
        //���������� ��������� ����� ����������� � �����
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attack_distance) //���� ������ ������� �� ��������� �����
        {
            anim.SetBool("Att", true); //��������� �������� �����
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
        anim.SetBool("Death", true); //�������� ������ �������
        agent.SetDestination(transform.position); //��������� �������

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
