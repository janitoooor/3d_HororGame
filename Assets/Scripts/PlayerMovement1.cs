using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0, vertical);
        m_Movement.Normalize();

        //Mathf.Approximately принимает два параметра с плавающей запятой и возвращает
        //логическое значение — true, если два числа с плавающей запятой
        //примерно равны, и false в противном случае.
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0);

        // || - это оператор or(или),это сравнивает bool с каждой стороны.
        //Eсли одно из них или оба из них истинны, то оно приравнивается к истине,
        //в противном случае оно приравнивается к ложному. 
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool("IsWalking", isWalking);

        if(isWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        //RotateTowards принимает четыре параметра — первые два — это Vector3s,
        //и это векторы, которые вращаются соответственно.Следующие два параметра —
        //это величина изменения между начальным вектором и целевым вектором:
        //сначала изменение угла (в радианах), а затем изменение величины.
        //Этот код изменяет угол на turnSpeed ​​* Time.deltaTime и величину на 0.
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    //Этот метод позволяет применять корневое движение по своему усмотрению,
    //а это означает, что движение и вращение можно применять отдельно.
    void OnAnimatorMove()
    {
        //MovePosition и передаете единственный параметр: его новую позицию
        //deltaPosition аниматора — это изменение положения из-за корневого движения,
        //которое было бы применено к этому кадру. Вы берете величину этого (его длину)
        //и умножаете на вектор движения, который находится в фактическом направлении,
        //в котором мы хотим, чтобы персонаж двигался.  
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
