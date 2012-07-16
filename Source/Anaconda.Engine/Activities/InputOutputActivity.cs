using System;

namespace CMA.Anaconda.Engine.Activities
{
    /// <summary>
    /// Represents the input and output activity. This activity or step uses an input and creates an output
    /// </summary>
    /// <typeparam name="TInput">The input value type</typeparam>
    /// <typeparam name="TOutput">The output value type </typeparam>
    public sealed class InputOutputActivity<TInput,TOutput> : WorkflowElement
    {
        #region Constructor
        /// <summary>
        /// Creates a new input and output activity instance
        /// </summary>
        /// <param name="process">The associated process being executed</param>
        public InputOutputActivity(Func<TInput,TOutput> process) : base(process){}
        #endregion

        #region Methods
        /// <summary>
        /// Executes the associated delegate action and returns process result
        /// </summary>
        /// <typeparam name="TWorkflowInput">The type of input argument</typeparam>
        /// <typeparam name="TWorkflowOutput">The type of output/result argument</typeparam>
        /// <param name="argument">The input argument for the process</param>
        /// <param name="stepResult">The result of the process</param>
        protected override void ExecuteStepCore<TWorkflowInput,TWorkflowOutput>(TWorkflowInput argument, out TWorkflowOutput stepResult)
        {
            stepResult = default(TWorkflowOutput);

            if(Process != null)
            {
                stepResult = (TWorkflowOutput)Process.DynamicInvoke(argument);
            }
        }

        /// <summary>
        /// Returns the current context as string
        /// </summary>
        /// <returns>The currently executing context</returns>
        protected override string GetCurrentContext()
        {
            return "InputOutputActivity";
        }
        #endregion
    }
}
