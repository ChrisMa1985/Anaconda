﻿using System;

namespace CMA.Anaconda.Engine.Activities
{
    #region Delegate
    /// <summary>
    /// Represents the process delegate used for output activity. It returns a specific output and takes no input arguments
    /// </summary>
    /// <typeparam name="TOutput">The type of the output argument</typeparam>
    /// <returns>The output value</returns>
    public delegate TOutput OutputActivityHandler<out TOutput>();
    #endregion

    /// <summary>
    /// Represents the output activity. This activity or step uses no input and creates an output
    /// </summary>
    /// <typeparam name="TOutput">The output value type</typeparam>
    public sealed class OutputActivity<TOutput> : WorkflowElement
    {
        #region Constructor
        /// <summary>
        /// Creates a new output activity instance
        /// </summary>
        /// <param name="process">The associated process being executed</param>
        public OutputActivity(OutputActivityHandler<TOutput> process) : base(process){}
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
                stepResult = (TWorkflowOutput)Process.DynamicInvoke();
            }
        }

        /// <summary>
        /// Returns the current context as string
        /// </summary>
        /// <returns>The currently executing context</returns>
        protected override string GetCurrentContext()
        {
            return "OutputActivity";
        }
        #endregion
    }
}
