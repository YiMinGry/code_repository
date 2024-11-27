using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
public enum Motion
{
    Attack,
    Bounce,
    Clicked,
    Death,
    Eat,
    Fear,
    Fly,
    Hit,
    Idle_A, Idle_B, Idle_C,
    Jump,
    Roll,
    Run,
    Sit,
    Spin,
    Swim,
    Walk,
    ReturnToOriginal
}

public class AnimalBase : MonoBehaviour
{
    public Motion currentState;
    public Animator animator;
    public Rigidbody rig;

    public Vector3 originalPosition; // ���� ��ġ ����
    public float moveRange = 10f; // �̵� ������ ���� �ݰ�

    private void Awake()
    {
        animator = GetComponentsInChildren<Animator>()[0];
        rig = GetComponent<Rigidbody>();
        originalPosition = transform.position; // ���� ��ġ ����
        currentState = Motion.Idle_A;
        StartCoroutine(FSM());
    }

    private IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(HandleState(currentState));
        }
    }

    private IEnumerator HandleState(Motion state)
    {
        switch (state)
        {
            case Motion.Idle_A:
                yield return StartCoroutine(IdleA());
                break;
            case Motion.Idle_B:
                yield return StartCoroutine(IdleB());
                break;
            case Motion.Idle_C:
                yield return StartCoroutine(IdleC());
                break;
            case Motion.Walk:
                yield return StartCoroutine(Walk());
                break;
            case Motion.Run:
                yield return StartCoroutine(Run());
                break;
            case Motion.Attack:
                yield return StartCoroutine(Attack());
                break;
            case Motion.Death:
                yield return StartCoroutine(Death());
                break;
            case Motion.Eat:
                yield return StartCoroutine(Eat());
                break;
            case Motion.Fear:
                yield return StartCoroutine(Fear());
                break;
            case Motion.Fly:
                yield return StartCoroutine(Fly());
                break;
            case Motion.Sit:
                yield return StartCoroutine(Sit());
                break;
            case Motion.Swim:
                yield return StartCoroutine(Swim());
                break;
            case Motion.ReturnToOriginal:
                yield return StartCoroutine(ReturnToOriginalPosition());
                break;
            default:
                yield return StartCoroutine(IdleA());
                break;
        }
    }

    private IEnumerator IdleA()
    {
        yield return new WaitForSeconds(Random.Range(1f,10f));
        ChangeState(Motion.Walk); // ����: Idle �� Walk ���·� ����
    }

    private IEnumerator IdleB()
    {
        yield return new WaitForSeconds(Random.Range(1f, 10f));
        ChangeState(Motion.Idle_C);
    }

    private IEnumerator IdleC()
    {
        yield return new WaitForSeconds(Random.Range(1f, 10f));
        ChangeState(Motion.Walk);
    }

    private IEnumerator Walk()
    {
        float walkDuration = 3f;
        float timer = 0f;

        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        transform.rotation = Quaternion.LookRotation(randomDirection);

        while (timer < walkDuration)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 2f);
            timer += Time.deltaTime;

            if (Vector3.Distance(transform.position, originalPosition) > moveRange)
            {
                ChangeState(Motion.ReturnToOriginal);
                yield break;
            }

            yield return null;
        }

        ChangeState(Motion.Run);
    }

    private IEnumerator Run()
    {
        float runDuration = 5f;
        float timer = 0f;

        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        transform.rotation = Quaternion.LookRotation(randomDirection);

        while (timer < runDuration)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 6f);
            timer += Time.deltaTime;

            if (Vector3.Distance(transform.position, originalPosition) > moveRange)
            {
                ChangeState(Motion.ReturnToOriginal);
                yield break;
            }

            yield return null;
        }

        ChangeState(Motion.Walk);
    }

    private IEnumerator Swim()
    {
        float swimDuration = 5f;
        float timer = 0f;

        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        transform.rotation = Quaternion.LookRotation(randomDirection);

        while (timer < swimDuration)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 4f);
            timer += Time.deltaTime;

            if (Vector3.Distance(transform.position, originalPosition) > moveRange)
            {
                ChangeState(Motion.ReturnToOriginal);
                yield break;
            }

            yield return null;
        }

        ChangeState(Motion.Swim);
    }

    private IEnumerator Fly()
    {
        float swimDuration = 5f;
        float timer = 0f;

        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        transform.rotation = Quaternion.LookRotation(randomDirection);

        while (timer < swimDuration)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5f);
            timer += Time.deltaTime;

            if (Vector3.Distance(transform.position, originalPosition) > moveRange)
            {
                ChangeState(Motion.ReturnToOriginal);
                yield break;
            }

            yield return null;
        }

        ChangeState(Motion.Fly);
    }

    private IEnumerator Attack()
    {
        // ���� �ִϸ��̼��̳� ���� ����
        yield return new WaitForSeconds(1f); // ���� �� ��� ���
        ChangeState(Motion.Idle_A); // Idle ���·� ��ȯ
    }

    private IEnumerator Death()
    {
        // ��� �ִϸ��̼� �� ��� ���� ����
        yield return null;
        // ��� �����̹Ƿ� ���¸� �������� ����
    }

    private IEnumerator Eat()
    {
        // �Դ� �ִϸ��̼��̳� ���� ����
        yield return new WaitForSeconds(2f); // 2�� ���� �Ա�
        ChangeState(Motion.Idle_A);
    }

    private IEnumerator Fear()
    {
        // �η����ϴ� �ִϸ��̼� �� ���� ����
        yield return new WaitForSeconds(2f);
        ChangeState(Motion.Run); // �η��� �� �޸��� ���·� ����
    }

    private IEnumerator Sit()
    {
        // �ɱ� �ִϸ��̼� ����
        yield return new WaitForSeconds(3f);
        ChangeState(Motion.Idle_A);
    }

    private IEnumerator ReturnToOriginalPosition()
    {

        float swimDuration = 5f;
        float timer = 0f;

        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            timer += Time.deltaTime;

            if (timer > swimDuration) 
            {
                timer = 0;

                Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                transform.rotation = Quaternion.LookRotation(randomDirection);
                ChangeState(Motion.Run);
                yield break;
            }

            Vector3 direction = (originalPosition - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.Translate(Vector3.forward * Time.deltaTime * 2f); // ���� �ӵ��� ���ư���

            yield return null;
        }

        ChangeState(Motion.Idle_A);
    }

    // ���� ���� �޼���
    protected virtual void ChangeState(Motion newState)
    {
        currentState = newState;

        if (animator != null && newState != Motion.ReturnToOriginal)
        {
            animator.Play(newState.ToString()); // ���� �̸��� ������ �ִϸ��̼� Ʈ���Ÿ� ����
        }
    }
}
