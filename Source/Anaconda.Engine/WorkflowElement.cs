using System;

namespace CMA.Anaconda.Engine
{
    /// <summary>
    /// Represents the base element. All parts of a workflow are defined as workflow elements
    /// </summary>
    public abstract class WorkflowElement
    {
        #region Fields
        /// <summary>
        /// Represents the current executing step 
        /// </summary>
        private static uint _steps = 1;

        /// <summary>
        /// Represents the common process delegate
        /// </summary>
        protected Delegate Process;
        #endregion

        #region Events
        /// <summary>
        /// Represents the event fired when the workflow execution progress changes
        /// </summary>
        internal static event Action<WorkflowProgressInfo> ProgressChanged;

        /// <summary>
        /// Represents the event fired when workflow execution is completed
        /// </summary>
        internal static event Action<Exception> Completed;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new instance of the workflow element and initializes its components
        /// </summary>
        /// <param name="process">The associated executable process</param>
        protected WorkflowElement(Delegate process)
        {
            Process = process;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current workflow element successor
        /// </summary>
        public WorkflowElement Successor { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Executes the current workflow step 
        /// </summary>
        /// <typeparam name="TWorkflowInput">The type for the input argument</typeparam>
        /// <param name="argument">The input argument used for current step</param>
        internal void ExecuteStep<TWorkflowInput>(TWorkflowInput argument)
        {
            try
            {
                object stepResult;

                OnProgressChanged(ToString(), true, null);

                ExecuteStepCore(argument, out stepResult);

                _steps++;

                if (Successor != null)
                    Successor.ExecuteStep(stepResult);
                else
                    OnCompletionChanged(null);

            }
            catch(Exception exception)
            {
                OnProgressChanged(ToString(), false, exception);
                OnCompletionChanged(exception);
            }
        }

        /// <summary>
        /// Represents the template method used for step execution
        /// </summary>
        /// <typeparam name="TWorkflowInput">The input argument type</typeparam>
        /// <typeparam name="TWorkflowOutput">The result/output argument type</typeparam>
        /// <param name="argument">The input argument</param>
        /// <param name="stepResult">The result/output argument</param>
        protected abstract void ExecuteStepCore<TWorkflowInput,TWorkflowOutput>(TWorkflowInput argument, out TWorkflowOutput stepResult);

        /// <summary>
        /// Fires the progress changed event
        /// </summary>
        /// <param name="context">The current workflow context</param>
        /// <param name="isSucceeded">Flag indicating if step is succeeded</param>
        /// <param name="exception">The possibly occured exception during process execution</param>
        private static void OnProgressChanged(string context, bool isSucceeded, Exception exception)
        {
            if (ProgressChanged != null)
                ProgressChanged(new WorkflowProgressInfo(_steps, context, isSucceeded, exception));
        }

        /// <summary>
        /// Fires the completed event
        /// </summary>
        /// <param name="exception">The possibly occured exception</param>
        private static void OnCompletionChanged(Exception exception)
        {
            _steps = 1;

            if (Completed != null)
                Completed(exception);
        }

        /// <summary>
        /// Returns the string representation for current step
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetCurrentContext();
        }

        /// <summary>
        /// Represents the template method used for retrieving the string representation for current step
        /// </summary>
        /// <returns>The string representation for current step</returns>
        protected abstract string GetCurrentContext();
        #endregion
    }
}
