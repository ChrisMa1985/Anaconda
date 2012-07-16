using System;

namespace CMA.Anaconda.Engine.Activities
{
    /// <summary>
    /// Represents the input activity. This activity or step uses an input and creates no output
    /// </summary>
    /// <typeparam name="TInput">The input value type</typeparam>
    public sealed class InputActivity<TInput> : WorkflowElement
    {
        #region Constructor
        /// <summary>
        /// Creates a new input activity instance
        /// </summary>
        /// <param name="process">The associated process being executed</param>
        public InputActivity(Action<TInput> process) : base(process){}
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
                Process.DynamicInvoke(argument);
            }
        }

        /// <summary>
        /// Returns the current context as string
        /// </summary>
        /// <returns>The currently executing context</returns>
        protected override string GetCurrentContext()
        {
            return "InputActivity";
        }
        #endregion
    }
}
