using System;

namespace CMA.Anaconda.Engine
{
    /// <summary>
    /// Represents the workflow engine used to start the workflow execution
    /// </summary>
    public sealed class WorkflowEngine : IDisposable
    {
        #region Constructor
        /// <summary>
        /// Creates a new instance of the class and initializes its components
        /// </summary>
        public WorkflowEngine()
        {
            RegisterEvent();
        }
        #endregion

        #region Events
        /// <summary>
        /// The event fired when workflow has completed
        /// </summary>
        public event Action<bool, Exception> WorkflowCompleted;

        /// <summary>
        /// The event fired when workflow progress has changed
        /// </summary>
        public event Action<uint, string, bool, Exception> WorkflowProgressChanged;
        #endregion

        #region Methods
        /// <summary>
        /// Starts the workflow process
        /// </summary>
        /// <typeparam name="T">The input argument type</typeparam>
        /// <param name="startPoint">The workflow start point</param>
        /// <param name="argument">The start point input argument</param>
        public void Run<T>(WorkflowElement startPoint, T argument)
        {
            startPoint.ExecuteStep(argument);
        }

        /// <summary>
        /// Starts the workflow process
        /// </summary>
        /// <param name="startPoint">The workflow start point</param>
        public void Run(WorkflowElement startPoint)
        {
            startPoint.ExecuteStep(default(object));
        }

        /// <summary>
        /// Registers events
        /// </summary>
        private void RegisterEvent()
        {
            WorkflowElement.ProgressChanged += ProgressChanged;
            WorkflowElement.Completed += Completed;
        }

        /// <summary>
        /// Handles completed event for workflow 
        /// </summary>
        /// <param name="possiblyOccuredException">The exception which has been possibly occured</param>
        private void Completed(Exception possiblyOccuredException)
        {
            if (WorkflowCompleted != null)
                WorkflowCompleted(possiblyOccuredException == null, possiblyOccuredException);
        }

        /// <summary>
        /// Handles progress changed event for workflow
        /// </summary>
        /// <param name="progressInfo">The progress info holding all information for current workflow progress step</param>
        private void ProgressChanged(WorkflowProgressInfo progressInfo)
        {
            if (WorkflowProgressChanged != null)
                WorkflowProgressChanged(progressInfo.CurrentStep, progressInfo.Context, progressInfo.IsSucceeded, progressInfo.PossiblyOccuredException);
        }

        /// <summary>
        /// Disposes the engine
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the members of the engine by deregistering from events 
        /// </summary>
        /// <param name="disposing">Flag indicating if dispose is needed</param>
        private void Dispose(bool disposing)
        {
            if(disposing)
            {
                WorkflowElement.ProgressChanged -= ProgressChanged;
                WorkflowElement.Completed -= Completed;
            }
        }
        #endregion
    }
}
