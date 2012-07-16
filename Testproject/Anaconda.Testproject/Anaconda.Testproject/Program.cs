using System;

using CMA.Anaconda.Engine.Activities;
using CMA.Anaconda.Engine.Decisions;
using CMA.Anaconda.Engine;

namespace Anaconda.Testproject
{
    class Program
    {
        static void Main(string[] args)
        {
            //4.1)
            SimpleActivity greaterThan100Activity = new SimpleActivity(()=>Console.WriteLine("Value is greater than 100."));
            
            //4.2)
            SimpleActivity lessThan100Activity = new SimpleActivity(()=>Console.WriteLine("Value is less than 100."));

            //3.)
            Decision<int> greaterOrLess100Decision = new Decision<int>(multipliedValue => multipliedValue > 100, greaterThan100Activity, lessThan100Activity);

            //2.)
            InputOutputActivity<int,int> mutiplyBy10Activity = new InputOutputActivity<int, int>(inputValue => inputValue * 10, greaterOrLess100Decision);

            //1.)
            OutputActivity<int> getInputValueActivity = new OutputActivity<int>(()=>
                                                                                {
                                                                                    Console.Write("Please type in value:");
                                                                                    var input = Console.ReadLine();
                                                                                    return Convert.ToInt32(input);
                                                                                }, mutiplyBy10Activity);

            WorkflowEngine engine = new WorkflowEngine();
            engine.WorkflowProgressChanged += (arg1, arg2, arg3, arg4) => Console.WriteLine("Step:{0}\r\nContext:{1}\r\nSuccess:{2}\r\nException message:{3}", arg1, arg2, arg3, arg4 != null ? arg4.Message : string.Empty); 

            engine.WorkflowCompleted += (arg1, arg2) => Console.WriteLine("Completed:\r\nSucceeded:{0}\r\nException message:{1}", arg1, arg2 != null ? arg2.Message : string.Empty); 
           
            engine.Run(getInputValueActivity);

            Console.ReadLine();

            engine.Dispose();
        }
    }
}
