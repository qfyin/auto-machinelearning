﻿// <copyright file="Parameter.cs" company="BigMiao">
// Copyright (c) BigMiao. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using MLNet.Sweeper;

namespace MLNet.AutoPipeline
{
    /// <summary>
    /// Create a parameter that can be used in <see cref="OptionBuilder{TOption}"/>.
    /// </summary>
    /// <typeparam name="T">type of Parameter.</typeparam>
    public class Parameter<T> : Parameter
    {
        internal Parameter(int min, int max, bool logBase = false, int steps = 100)
        {
            Contract.Assert(max > min);
            Contract.Assert(steps > 0);
            Contract.Assert(!logBase || (logBase && min > 0));
            var option = new Int32ValueGenerator.Option()
            {
                Min = min,
                Max = max,
                Steps = steps,
                LogBase = logBase,
            };

            this.ValueGenerator = new Int32ValueGenerator(option);
        }

        internal Parameter(long min, long max, bool logBase = false, int steps = 100)
        {
            Contract.Assert(max > min);
            Contract.Assert(steps > 0);
            Contract.Assert(!logBase || (logBase && min > 0));
            var option = new LongValueGenerator.Option()
            {
                Min = min,
                Max = max,
                Steps = steps,
                LogBase = logBase,
            };

            this.ValueGenerator = new LongValueGenerator(option);
        }

        internal Parameter(float min, float max, bool logBase = false, int steps = 100)
        {
            Contract.Assert(max > min);
            Contract.Assert(steps > 0);
            Contract.Assert(!logBase || (logBase && min > 0));
            var option = new FloatValueGenerator.Option()
            {
                Min = min,
                Max = max,
                Steps = steps,
                LogBase = logBase,
            };

            this.ValueGenerator = new FloatValueGenerator(option);
        }

        internal Parameter(double min, double max, bool logBase = false, int steps = 100)
        {
            Contract.Assert(max > min);
            Contract.Assert(steps > 0);
            Contract.Assert(!logBase || (logBase && min > 0));
            var option = new DoubleValueGenerator.Option()
            {
                Min = min,
                Max = max,
                Steps = steps,
                LogBase = logBase,
            };

            this.ValueGenerator = new DoubleValueGenerator(option);
        }

        internal Parameter(T[] candidates)
        {
            var option = new DiscreteValueGenerator.Option()
            {
                Values = candidates.Select(x => (object)x).ToArray(),
            };

            this.ValueGenerator = new DiscreteValueGenerator(option);
        }

        internal Parameter(T value)
        {
            this.ValueGenerator = new SingleValueGenerator<T>(null, value);
        }
    }

    /// <summary>
    /// abstract class for <see cref="Parameter{T}"/>.
    /// </summary>
    public abstract class Parameter
    {
        protected internal virtual IValueGenerator ValueGenerator { get; protected set; }
    }
}