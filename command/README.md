# 命令模式实现撤销与恢复
## 命令模式定义
> 将请求封装成对象，以便使用不同的请求、队列或日志来参数化其他对象。
> 命令对象可以把行动及参数封装起来，于是这些行动可以被：
> * 重复多次
> * 取消
> * 恢复（取消后又再） 

### 整个模式的类图如下：

![命令模式](https://files-cdn.cnblogs.com/files/yaolin1228/%E5%91%BD%E4%BB%A4%E6%A8%A1%E5%BC%8F.bmp)

通过 ICommand 接口，实现了控制类与调用者的解耦。<br>
***
下面通过一个简单的实例来详细说明这种解耦以恢复撤销是如何实现。<br>
假定有一个风扇，当前有四个按钮，分别是 **高速模式** , **低速模式** , **撤销**  ，**恢复**。
### 风扇类类如下（对应类图中的具体类 ConcreteClass)：<br>
有高速运转、低速运转等方法
```c#
public class CeilingFan
{
     public const int HIGH = 2;
     public const int LOW = 1;
     public const int OFF = 0;
     int speed;

     public CeilingFan() { speed = OFF; }
     public void High() { speed = HIGH; }
     public void Low() { speed = LOW; }
     public int getSpeed() { return speed; }
}
```

### 命令接口
```C#
public interface ICommand
{
    void execute();
    void undo();
}

```

### 风扇命令类 (Concrete)
```c#
// 高速运行类
public class CeilingFanHighCommand : ICommand
{
    CeilingFan ceilingFan; // 类中不用 new 方法创建类，降低耦合
    int preSpeed;  // 记录执行按键前的状态,便于回测
    public CeilingFanHighCommand(CeilingFan cf)
    {
        ceilingFan = cf;
    }

    public void execute()
    {
        preSpeed = ceilingFan.getSpeed();
        ceilingFan.High(); 
    }

    public void undo()
    {
        switch(preSpeed)
        {
            case CeilingFan.HIGH:
                ceilingFan.High();
                break;
            case CeilingFan.LOW:
                ceilingFan.Low();
                break;
            default:
                ceilingFan.Off();
                break;
        }
    }
}

// 低速运行类
public class CeilingFanLowCommand : ICommand
{
    CeilingFan ceilingFan; // 类中不用 new 方法创建类，降低耦合
    int preSpeed;  // 记录执行按键前的状态,便于回测
    public CeilingFanHighCommand(CeilingFan cf)
    {
        ceilingFan = cf;
    }

    public void execute()
    {
        preSpeed = ceilingFan.getSpeed();
        ceilingFan.Low(); 
    }

    public void undo()
    {
        switch(preSpeed)
        {
            case CeilingFan.HIGH:
                ceilingFan.High();
                break;
            case CeilingFan.LOW:
                ceilingFan.Low();
                break;
            default:
                ceilingFan.Off();
                break;
        }
    }
}

// 关闭类
public class CeilingFanLowCommand : ICommand
{
    CeilingFan ceilingFan; 
    int preSpeed;
    public CeilingFanHighCommand(CeilingFan cf)
    {
        ceilingFan = cf;
    }

    public void execute()
    {
        preSpeed = ceilingFan.getSpeed();
        ceilingFan.Off(); 
    }

    public void undo()
    {
        switch(preSpeed)
        {
            case CeilingFan.HIGH:
                ceilingFan.High();
                break;
            case CeilingFan.LOW:
                ceilingFan.Low();
                break;
            default:
                ceilingFan.Off();
                break;
        }
    }
}
```
以上风扇的相关命令构建后，需要被一个类来调用控制它，这个控制类不仅仅可以控制风扇，同时可以控制电灯，冰箱等等,所以不能与风扇耦合。
### 控制类 （对应类图中 Control)
```C#
public class Control
{
    List<ICommand> onCommands;
    Stack<ICommand> undoCommands;
    Stack<ICommand> redoCommands;  // 记录前一个命令, 便于 undo

    public Control()
    {
        onCommands = new List<ICommand>();
        undoCommands = new Stack<ICommand>();
        redoCommands = new Stack<ICommand>();
    }

    public void SetCommand(int slot, ICommand onCmd)
    {
        onCommands[slot] = onCmd;
    }

    public void OnButtonWasPressed(int slot)
    {
        if (onCommands[slot] != null)
        {
            onCommands[slot].execute();
            undoCommands.Push(onCommands[slot]);
        }
    }

    public void UndoButtonWasPressed() // 撤销,此处用 stack 后进先出的特性
    {
        if (undoCommands.Count > 0)
        {
            ICommand cmd = undoCommands.Pop();
            redoCommands.Push(cmd);
            cmd.undo();
        }
    }

    public void RedoButtonWasPressed()
    {
        if(redoCommands.Count > 0)
        {
            ICommand cmd = redoCommands.Pop();
            undoCommands.Push(cmd);
            cmd.execute();
        }
    }
}
```
以上一个命令模式大体上完成了。
下面让客户进行调用测试
```C#
// 测试类 （类途中的 RemoteLoader)
class Program
{
    static void Main(string[] args)
    {
        CeilingFan cf = new CeilingFan();
        CeilingFanHighCommand cfh = new CeilingFanHighCommand(cf);
        CeilingFanLowCommand cfl = new CeilingFanLowCommand(cf);

        Control ctr = new Control();
        // 假设 button0, button1 分别为高速低速
        ctr.SetCommand(0, cfh);
        ctr.SetCommand(1, cfl);

        ctr.OnButtonWasPressed(0);
        ctr.OnButtonWasPressed(1);
        ctr.UndoButtonWasPressed();
        ctr.RedoButtonWasPressed();
    }
}
```
### 输出内容如下：<br>
>turn ceilfan to high speed!<br>
>turn ceifan to low speed! <br>
>turn ceilfan to high speed!<br>
>turn ceifan to low speed!<br>


## 撤销与重做功能就此实现。整个过程中，最关键部分是命令对象的封装以及控制类与具体工厂类耦合的解除。
