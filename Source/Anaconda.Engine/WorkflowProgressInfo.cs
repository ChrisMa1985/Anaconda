using System;

namespace CMA.Anaconda.Engine
{
    /// <summary>
    /// Represents the workflow progress info instance used for handling progress change specific information
    /// </summary>
    internal sealed class WorkflowProgressInfo
    {
        #region Constructor
        /// <summary>
        /// Creates a new instance of the info class and initializes its components
        /// </summary>
        /// <param name="currentStep">The currently executing workflow step</param>
        /// <param name="context">The currently executing workflow context</param>
        /// <param name="isSucceeded">Flag indicating if current workflow step has succeeded</param>
        /// <param name="possiblyOccuredException">The possibly occured exception during workflow step execution</param>
        internal WorkflowProgressInfo(uint currentStep, string context, bool isSucceeded, Exception possiblyOccuredException)
        {
            CurrentStep = currentStep;
            Context = context;
            IsSucceeded = isSucceeded;
            PossiblyOccuredException = possiblyOccuredException;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the currently executing workflow step
        /// </summary>
        internal uint CurrentStep { private set; get; }

        /// <summary>
        /// Gets the currently executing workflow context
        /// </summary>
        internal string Context { private set; get; }

        /// <summary>
        /// Gets the flag indicating if currently executing workflow step has succeeded
        /// </summary>
        internal bool IsSucceeded { private set; get; }

        /// <summary>
        /// Gets the possibly occured exception during workflow step execution
        /// </summary>
        internal Exception PossiblyOccuredException { private set; get; }
        #endregion
    }
}
