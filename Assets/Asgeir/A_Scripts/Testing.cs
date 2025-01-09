using System;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private void Start()
    {
        MyClass myClass = new MyClass();

        TestInterface(myClass);


        MySecondClass mySecondClass = new MySecondClass();
        TestInterface(myClass);
    }

    private void TestInterface(IMyInterface myInterface)
    {
        myInterface.TestFunction();
    }
}

public interface IMyInterface
{
    

    void TestFunction();
}

public interface IMySecondInterface
{
    void SecondInterfaceFunction(int i);
}

public class MyClass : IMyInterface, IMySecondInterface
{
    public void SecondInterfaceFunction(int i)
    {

    }

    public void TestFunction()
    {
        Debug.Log("MyClass.TestFunction()");

    }
}

public class MySecondClass : IMyInterface
{
    public void TestFunction()
    {
        Debug.Log("MySecondClass");
    }
}