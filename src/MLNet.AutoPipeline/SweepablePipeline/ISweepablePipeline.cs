﻿// <copyright file="ISweepablePipeline.cs" company="BigMiao">
// Copyright (c) BigMiao. All rights reserved.
// </copyright>

using Microsoft.ML;
using Microsoft.ML.Data;
using MLNet.Sweeper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLNet.AutoPipeline
{
    public interface ISweepablePipeline
    {
        IList<IValueGenerator> ValueGenerators { get; }

        IList<INode> SweepablePipelineNodes { get; }

        ISweeper Sweeper { get; }

        ISweepablePipeline Append(INode builder);

        ISweepablePipeline Append<TTransformer>(TTransformer estimator, TransformerScope scope = TransformerScope.Everything)
            where TTransformer : IEstimator<ITransformer>;

        ISweepablePipeline Append<TNewTran, TOption>(Func<TOption, TNewTran> estimatorBuilder, OptionBuilder<TOption> optionBuilder, TransformerScope scope = TransformerScope.Everything)
            where TNewTran : IEstimator<ITransformer>
            where TOption : class;

        ISweepablePipeline Concat(ISweepablePipeline chain);

        string Summary();

        void UseSweeper(ISweeper sweeper);

        IEnumerable<SweepingInfo> Sweeping(int maximum);
    }
}
