using System;
using System.Text;
public class Dinglemouse
{
    string? name;
    int? age;
    char? sex;
    String helloMessage = "Hello.";

    public Dinglemouse SetAge(int age)
    {
        if (this.age != null) helloMessage = helloMessage.Replace($"{this.age}.", $"{age}.");
        else helloMessage += $" I am {age}.";

        this.age = age;
        return this;
    }

    public Dinglemouse SetSex(char sex)
    {
        string currentSex = $" I am {(this.sex == 'M' ? "male" : "female")}.";
        string newSex = $" I am {(sex == 'M' ? "male" : "female")}.";
        if (this.sex != null) helloMessage = helloMessage.Replace(currentSex, newSex);
        else helloMessage += newSex;
        this.sex = sex;
        return this;
    }

    public Dinglemouse SetName(string name)
    {
        if (this.name != null) helloMessage = helloMessage.Replace(this.name, name);
        else helloMessage += $" My name is {name}.";
        this.name = name;
        return this;
    }

    public string Hello()
    {
        return helloMessage;
    }
}