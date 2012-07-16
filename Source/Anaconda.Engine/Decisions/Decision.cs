using System;

namespace CMA.Anaconda.Engine.Decisions
{
    /// <summary>
    /// Represents the decision. This decision or step uses an input and returns the specific workflow element depending on given filter
    /// </summary>
    /// <typeparam name="TInput">The input value type</typeparam>
    public sealed class Decision<TInput> : WorkflowElement
    {
        #region Constructor
        /// <summary>
        /// Creates a new decision instance
        /// </summary>
        /// <param name="filter">The associated filter being executed for decision process</param>
        /// /// <param name="secondSuccessorCandidate">The successor workflow element being the next in workflow chain if filter returns false</param>
        public Decision(Predicate<TInput> filter) : base(filter)
        {
        }
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
                var decision = (bool)Process.DynamicInvoke(argument);

                if (!decision)
                    Successor = AlternativeSuccessor;
            }
        }

        /// <summary>
        /// Returns the current context as string
        /// </summary>
        /// <returns>The currently executing context</returns>
        protected override string GetCurrentContext()
        {
            return "Decision";
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the alternative successor
        /// </summary>
        public WorkflowElement AlternativeSuccessor
        {
            set;
            get;
        }
        #endregion
    }
}
